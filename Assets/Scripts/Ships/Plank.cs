using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Plank : MonoBehaviour
{
    private ExportPrisoners exportPrisoners;
    private void Start()
    {
        exportPrisoners = transform.parent.GetComponent<ExportPrisoners>();
    }

    private void OnTriggerExit(Collider other)
    {
        print("hi");
    }
}
