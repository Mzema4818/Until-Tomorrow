using System.Collections.Generic;
using UnityEngine;

public class CollectableRandomizer : MonoBehaviour
{
    public GameObject meshObject;
    public GameObject player;
    public GetData getData;
    public GameObject HotBar;
    public bool spawnatfeet;

    public bool IsRandomized { get; private set; } = false; // Track completion

    public void Randomize()
    {
        IsRandomized = false; // Reset before starting

        Mesh mesh = meshObject.GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        List<Vector3> newVertices = new List<Vector3>();

        for (int i = 0; i < vertices.Length; i++)
        {
            if (vertices[i].y >= 1.6)
            {
                newVertices.Add(vertices[i]);
            }
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name == "Opening Letter" || spawnatfeet)
            {
                transform.GetChild(i).transform.position = player.transform.position;
            }
            else
            {
                int randValue = Random.Range(0, newVertices.Count);
                transform.GetChild(i).transform.position = newVertices[randValue] * meshObject.transform.localScale.y + new Vector3(0, 0.5f, 0);
            }
        }

        foreach (Transform t in transform)
        {
            t.gameObject.SetActive(true);
        }

        if (HotBar != null)
        {
            foreach (Transform t in HotBar.transform)
            {
                t.gameObject.SetActive(false);
            }
        }

        IsRandomized = true; // Mark as completed
        //getData.SaveData(); //not happy with this fix but, the data is being saved before the collectables
    }
}
