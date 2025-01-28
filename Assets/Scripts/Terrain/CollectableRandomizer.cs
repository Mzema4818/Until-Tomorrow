using System.Collections.Generic;
using UnityEngine;

public class CollectableRandomizer : MonoBehaviour
{
    public GameObject meshObject;
    public GameObject player;
    public GetData getData;
    public GameObject HotBar;
    public bool randomize;

    public bool spawnatfeet;
    // Start is called before the first frame update
    void Update()
    {
        if (randomize)
        {
            RandomizeCollectables();
            //getData.SaveData();
            foreach(Transform transform in transform)
            {
                transform.gameObject.SetActive(true);
            }

            if(HotBar != null)
            {
                foreach (Transform transform in HotBar.transform)
                {
                    transform.gameObject.SetActive(false);
                }
            }
            randomize = false;
        }
    }

    // Update is called once per frame
    public void RandomizeCollectables()
    {
        Mesh mesh = meshObject.GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;

        List<Vector3> newVertices = new List<Vector3>();


        for (int i = 0; i < vertices.Length; i += 1)
        {
            if (vertices[i].y >= 1.6)
            {
                newVertices.Add(vertices[i]);
            }
        }

        for(int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).name == "Opening Letter" || spawnatfeet)
            {
                transform.GetChild(i).transform.position = player.transform.position;
            }
            else
            {
                int randValue = Random.Range(0, newVertices.Count);
                transform.GetChild(i).transform.position = newVertices[randValue] * meshObject.transform.localScale.y;
            }
        }
    }
}
