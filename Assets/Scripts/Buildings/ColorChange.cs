using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    public Color[] rend;
    public Color[] touchingColors;
    public bool touching;

    private float x, y, z;
    //private float rotationLimit = 20f;

    // Start is called before the first frame update
    void Start()
    {
        rend = new Color[transform.childCount];

        transform.GetComponent<BoxCollider>().enabled = true;

        for (int i = 0; i < rend.Length; i++)
        {
            MeshCollider meshCollider = transform.GetChild(i).transform.GetComponent<MeshCollider>();
            Renderer renderer = transform.GetChild(i).GetComponent<Renderer>();

            if (meshCollider != null && renderer != null)
            {
                rend[i] = transform.GetChild(i).GetComponent<Renderer>().material.color;
                transform.GetChild(i).gameObject.GetComponent<MeshCollider>().convex = true;
                transform.GetChild(i).GetComponent<MeshCollider>().enabled = false;
            }
        }

        gameObject.AddComponent<Rigidbody>();
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationY;
        //StartCoroutine(ExampleCoroutine());

        for (int i = 0; i < transform.childCount; i++)
        {
            Renderer renderer = transform.GetChild(i).GetComponent<Renderer>();
            if (renderer != null)
            {
                transform.GetChild(i).GetComponent<Renderer>().material.color = touchingColors[1];
                touching = false;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.name != "Terrain")
        {
            ChangeColor(touchingColors[0]);
            touching = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        x = transform.localEulerAngles.x;
        y = transform.localEulerAngles.y;
        z = transform.localEulerAngles.z;

        if (other.transform.name != "Terrain")
        {
            ChangeColor(touchingColors[1]);
            touching = false;
        }
    }

    private void OnDestroy()
    {
        Destroy(transform.GetComponent<Rigidbody>());
        for (int i = 0; i < rend.Length; i++)
        {
            MeshCollider meshCollider = transform.GetChild(i).transform.GetComponent<MeshCollider>();
            Renderer renderer = transform.GetChild(i).GetComponent<Renderer>();

            if (meshCollider != null && renderer != null)
            {
                transform.GetChild(i).GetComponent<Renderer>().material.color = rend[i];
                transform.GetChild(i).gameObject.GetComponent<MeshCollider>().convex = true;
                transform.GetChild(i).GetComponent<MeshCollider>().enabled = true;
            }
        }
        transform.GetComponent<BoxCollider>().enabled = false;
    }

    IEnumerator ExampleCoroutine()
    {
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = transform.position;
        yield return new WaitForSeconds(1);
        Destroy(cube);
    }

    private void ChangeColor(Color color)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Renderer renderer = transform.GetChild(i).GetComponent<Renderer>();
            if (renderer != null)
            {
                transform.GetChild(i).GetComponent<Renderer>().material.color = color;
            }
        }
    }
}
