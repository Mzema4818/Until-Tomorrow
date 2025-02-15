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
    public GameObject heartParticles;
    public Transform tempParent;

    public Vector3 originalPosition;

    public bool enableResidents;

    // Start is called before the first frame update
    private void Awake()
    {
        if (name.Contains("Level0")) MaxResidents = MaxNumberOfWorkers;
        ResidentsActive = new GameObject[MaxResidents];

        originalPosition = transform.position;
    }

    private void Start()
    {
        InvokeRepeating("HealResidents", 0.0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        //if (!time.sleepTime && enableResidents) WakeUpResidents();
        //WakeUpSleepingResidents();

        if (transform.position != originalPosition) { WakeUpResidents(); originalPosition = transform.position; }
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

    private void HealResidents()
    {
        if (ResidentsActive[0] == null) return;

        foreach (GameObject resident in ResidentsActive)
        {
            if(resident == null) continue;
            //ResidentScheudle residentScheudle = resident.GetComponent<ResidentScheudle>();
            ResidentHealth residentHealth = resident.GetComponent<ResidentHealth>();
            Sleeping sleeping = resident.GetComponent<Sleeping>();

            //if the resident is sleeping and has a health bar, heal health
            if (sleeping != null && residentHealth != null && residentHealth.currentHealth != residentHealth.maxHealth)
            {
                residentHealth.ModifyHealth(10);
                GameObject heart = Instantiate(heartParticles, transform.position, heartParticles.transform.rotation);
                heart.transform.parent = tempParent;
                Destroy(heart, 5);
            }
        }
    }

    private void WakeUpResidents()
    {
        foreach (GameObject resident in ResidentsActive)
        {
            if (resident != null) resident.SetActive(true);
        }
        enableResidents = false;
    }
}
