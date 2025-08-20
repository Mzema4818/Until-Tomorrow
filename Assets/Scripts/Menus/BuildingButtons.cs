using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingButtons : MonoBehaviour
{
    public Builder builder;
    public GameObject hammer;
    public GameObject checkpoint;
    public Transform enemiesParent;
    public GameObject error;

    [Header("Type Of Button")]
    public bool Repair;
    public bool Move;
    public bool destroy;
    public bool fire;
    public bool remove;
    public bool yes;
    public bool no;
    public bool close;

    public void ButtonClick()
    {
        if (Repair && hammer.activeSelf)
        {
            builder.RepairBuilding();
        }
        else if (Move && hammer.activeSelf)
        {
            if (enemiesParent.childCount == 0) builder.MoveBuilding();
            else error.SetActive(true);
        }
        else if (destroy && hammer.activeSelf)
        {
            checkpoint.SetActive(true);
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
        else if (yes && hammer.activeSelf)
        {
            BuildingHealth buildingHealth = builder.buildingData.GetComponent<BuildingHealth>();
            buildingHealth.ModifyHealth(-buildingHealth.maxHealth);
            builder.buildingData = null;
        }
        else if (no)
        {
            checkpoint.SetActive(false);
        }
        else if (close)
        {
            IsABuilding isABuilding = transform.parent.parent.parent.parent.GetComponent<IsABuilding>();
            if (isABuilding != null) isABuilding.actions.SetActive(false);
            builder.buildingData = null;
        }
    }
}
