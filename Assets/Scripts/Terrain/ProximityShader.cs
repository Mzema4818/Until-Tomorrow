using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProximityShader : MonoBehaviour
{
    List<GameObject> targetObjects = new List<GameObject>();
    Vector3[] proximityPositions;
    float[] proximityMaxDistances;
    MaterialPropertyBlock mpb;
    Renderer myRenderer;

    void Start()
    {
        mpb = new MaterialPropertyBlock();
        myRenderer = GetComponent<Renderer>();
    }

    public void UpdateProximityData()
    {
        if (myRenderer == null)
            return; //Avoid updating if myRenderer is gone

        int count = Mathf.Min(targetObjects.Count, 128);

        if (count == 0)
        {
            mpb.Clear();
            mpb.SetInt("_ProximityPosCount", 0);
            myRenderer.SetPropertyBlock(mpb);
            return;
        }

        if (proximityPositions == null || proximityPositions.Length != count)
            proximityPositions = new Vector3[count];

        if (proximityMaxDistances == null || proximityMaxDistances.Length != count)
            proximityMaxDistances = new float[count];

        for (int i = 0; i < count; i++)
        {
            var obj = targetObjects[i];
            proximityPositions[i] = obj.transform.position;

            //Get individual max distance
            var settings = obj.GetComponent<ProximitySettings>();
            if (settings != null)
            {
                proximityMaxDistances[i] = settings.maxDistance;
            }
            else
            {
                proximityMaxDistances[i] = 0.0f; //fallback if no script is attached
            }
        }

        mpb.Clear();

        // Set proximity positions
        mpb.SetVectorArray("_ProximityPos", proximityPositions.Select(p => new Vector4(p.x, p.y, p.z, 0)).ToArray());

        // Set proximity max distances
        mpb.SetFloatArray("_ProximityMaxDist", proximityMaxDistances);

        mpb.SetInt("_ProximityPosCount", count);

        myRenderer.SetPropertyBlock(mpb);
    }


    // When adding a building:
    public void AddBuilding(GameObject newBuilding)
    {
        if (targetObjects.Count >= 128)
            return; //Cap at 128 objects

        targetObjects.Add(newBuilding);
        UpdateProximityData(); //Refresh proximity data after adding a building
    }

    // When removing a building:
    public void RemoveBuilding(GameObject building)
    {
        targetObjects.Remove(building);
        UpdateProximityData(); //Refresh proximity data after removing a building
    }

}
