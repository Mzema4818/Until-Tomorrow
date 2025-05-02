using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingButtons : MonoBehaviour
{
    public Builder builder;
    public GameObject hammer;

    [Header("Type Of Button")]
    public bool Repair;
    public bool Move;
    public bool destroy;
    public bool fire;
    public bool remove;

    public void ButtonClick()
    {
        if (Repair && hammer.activeSelf)
        {
            builder.RepairBuilding();
        }
        else if (Move && hammer.activeSelf)
        {
            builder.MoveBuilding();
        }
        else if (destroy && hammer.activeSelf)
        {
            Destroy(builder.buildingData);
        }
        else if (fire)
        {
           transform.root.GetComponent<JobOptions>().Fire(builder.buildingData.GetComponent<Job>(), transform.parent.GetSiblingIndex());
           Destroy(transform.parent.gameObject);
        }
        else if (remove)
        {
            transform.parent.parent.parent.parent.parent.parent.GetComponent<Tent>().RemoveResident(transform.parent.GetSiblingIndex());
            Destroy(transform.parent.gameObject);
        }
    }
}
