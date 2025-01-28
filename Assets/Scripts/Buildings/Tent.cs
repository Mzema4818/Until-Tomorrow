using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tent : MonoBehaviour
{
    public int MaxNumberOfWorkers;
    public int MaxResidents = 0;
    public int Residents;
    public GameObject[] ResidentsActive;
    public LightingManager time;

    public Vector3 originalPosition;

    public bool enableResidents;

    // Start is called before the first frame update
    private void Awake()
    {
        if (name.Contains("Level0")) MaxResidents = MaxNumberOfWorkers;
        ResidentsActive = new GameObject[MaxResidents];

        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //if (!time.sleepTime && enableResidents) WakeUpResidents();
        //WakeUpSleepingResidents();

        if (transform.position != originalPosition) WakeUpResidents(); originalPosition = transform.position;

    }

    private void OnDestroy()
    {
        try
        {
            WakeUpResidents();
            for (int i = 0; i < ResidentsActive.Length; i++)
            {
                ResidentsActive[i].GetComponent<ResidentScheudle>().home = null;
                //ResidentsActive[i].GetComponent<ResidentWander>().ResetSchedule();
                ResidentsActive[i].GetComponent<ResidentConditions>().hasHome = false;
            }
        }
        catch { };
    }

    /*public void WakeUpSleepingResidents()
    {
        for(int i = 0; i < ResidentsActive.Length; i++)
        {
            if(ResidentsActive[i] != null)
            {
                ResidentScheudle residentScheudle = ResidentsActive[i].GetComponent<ResidentScheudle>();
                if (residentScheudle.shouldWakeUp && time.WhatTimeIsIt() == residentScheudle.TimeToWakeUp)
                {
                    residentScheudle.TimeToWakeUp = -1;
                    residentScheudle.shouldWakeUp = false;
                    ResidentsActive[i].SetActive(true);
                }

            }
        }
    }*/

    private void WakeUpResidents()
    {
        foreach (GameObject resident in ResidentsActive)
        {
            if (resident != null) resident.SetActive(true);
        }
        enableResidents = false;
    }
}
