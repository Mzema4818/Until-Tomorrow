using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnGameObjects : MonoBehaviour
{
    public GameObject building;
    public LightingManager time; 
    public GameObject resident;
    public int TimeToWakeUp;

    void Update()
    {
        try
        {
            if (time.WhatTimeIsIt() == TimeToWakeUp)
            {
                //print(1);
                EnableResident();
            }
            else if (building.GetComponent<IsABuilding>().beingMoved)
            {
                //print(2);
                EnableResident();
            }
        }
        catch
        {
            //print(3);
            EnableResident();
        }
    }

    private void EnableResident()
    {
        resident.GetComponent<ResidentScheudle>().AtLocation = false;
        resident.SetActive(true);

        try
        {
            Destroy(resident.GetComponent<Sleeping>());
            Destroy(resident.GetComponent<Miner>());
        }
        catch { };

        //it removes stats[3] because this job replies on strength stat
        //if(building.GetComponent<Mine>() != null)
        //{
        //building.GetComponent<Mine>().workersActive--;
        // building.GetComponent<Mine>().statMultiplier -= resident.GetComponent<ResidentStats>().Stats[3];
        //resident.GetComponent<Miner>().dontDestory = true;
        // }

        //Destroy(resident.GetComponent<Sleeping>());
        //Destroy(resident.GetComponent<Miner>());
        Destroy(this);
    }
}
