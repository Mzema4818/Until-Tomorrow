using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestScript : MonoBehaviour
{
    public NavMeshSurface navMeshSurface;
    public bool test;
    // Start is called before the first frame update
    void Start()
    {
        navMeshSurface = transform.GetComponent<NavMeshSurface>();
    }

    // Update is called once per frame
    void Update()
    {
        if (test)
        {
            navMeshSurface.BuildNavMesh();
            print("hi");
            test = false;
        }
    }
}
