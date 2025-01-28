using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.FilePathAttribute;

public class Job : MonoBehaviour
{
    [Header("Essential Job Options")]
    public int MaxNumberOfWorkers; //Number of workers job level0 job can have
    public int increment; //Number of max workers job goes up for each level up
    public int MaxWorkers = 0; //Number of maxworkers for that building
    public int Workers; //Workers you have
    public GameObject[] WorkersActive; //Array of all workers

    [Header("Should Change")]
    public int workersWorking = 0;
    public float statMultiplier; //stat multiplier for the building

    [Header("Other")]
    public LightingManager time;
    public bool beingMoved;

    //Job types:
    //0 = farmer
    //1 = miner
    //2 = lumber
    //3 = chef
    //4 = archer
    public bool[] JobType = new bool[5]; //5 jobs 


    private void Awake()
    {
        if (name.Contains("Level0")) MaxWorkers = MaxNumberOfWorkers;
        else if (name.Contains("Level1")) MaxWorkers = MaxNumberOfWorkers + increment;

        WorkersActive = new GameObject[MaxWorkers];
    }

    private void Update()
    {
        try
        {
            if (workersWorking != 0 && GetComponent<IsABuilding>().beingMoved)
            {
                for(int i = 0; i < Workers; i++)
                {
                    RemoveJob(i);
                }
            }
        }
        catch
        {
            for (int i = 0; i < Workers; i++)
            {
                RemoveJob(i);
            }
        }
    }

    public void JoinJob(int num)
    {
        ResidentStats stats = WorkersActive[num].GetComponent<ResidentStats>();
        GameObject jobResidentHas = WorkersActive[num].GetComponent<ResidentScheudle>().job;

        GameObject resident = WorkersActive[num];

        if (JobType[0]) //farmer
        {
            resident.AddComponent<Farmer>().location = jobResidentHas.gameObject;
            statMultiplier += stats.Stats[3];
        }
        else if (JobType[1]) //miner
        {
            resident.AddComponent<Miner>().time = time;
            resident.GetComponent<Miner>().building = jobResidentHas.gameObject;
            statMultiplier += stats.Stats[3];
        }
        else if (JobType[2]) //lumber
        {
            resident.AddComponent<LumberWorker>().location = jobResidentHas.gameObject;
        }
        else if (JobType[3]) //chef
        {
            resident.AddComponent<Chef>().location = jobResidentHas.gameObject; //was gameObject.addcompent
            statMultiplier += stats.Stats[3];
        }
        else if (JobType[4]) //archer
        {
            resident.AddComponent<Archer>().location = jobResidentHas.gameObject;
            statMultiplier += stats.Stats[3];
        }

        workersWorking++;
    }

    public void RemoveJob(int num)
    {
        ResidentStats stats = WorkersActive[num].GetComponent<ResidentStats>();
        GameObject jobResidentHas = WorkersActive[num].GetComponent<ResidentScheudle>().job;

        GameObject resident = WorkersActive[num];

        if (JobType[0]) //farmer
        {
            //resident.GetComponent<Farmer>().location = jobResidentHas.gameObject;
            //statMultiplier += stats.Stats[3];
            Destroy(resident.GetComponent<Farmer>());
        }
        else if (JobType[1]) //miner
        {
            Destroy(resident.GetComponent<Miner>());
        }
        else if (JobType[2]) //lumber
        {
            Destroy(resident.GetComponent<LumberWorker>());
        }
        else if (JobType[3]) //chef
        {
            Destroy(resident.GetComponent<Chef>());
        }
        else if (JobType[4]) //archer
        {
            Destroy(resident.GetComponent<Archer>());
        }

        //resident.GetComponent<Hats>().RemoveHats();
        print(resident);
    }

    public int GetWorkerIndex(string name)
    {
        for(int i = 0; i < WorkersActive.Length; i++)
        {
            if (name.Contains(WorkersActive[i].name)) return i;
        }

        return -1;
    }

    private void OnDestroy()
    {
        for (int i = 0; i < WorkersActive.Length; i++)
        {
            if (WorkersActive[i] != null)
            {
                WorkersActive[i].GetComponent<ResidentScheudle>().job = null;
                WorkersActive[i].GetComponent<StatBar>().UpdateJob();
                WorkersActive[i].GetComponent<Hats>().RemoveHats();
                WorkersActive[i].GetComponent<ResidentTools>().TurnOffAll();
                WorkersActive[i].GetComponent<ResidentTools>().TurnOffAllAnimations();
                //try { WorkersActive[i].GetComponent<NavMeshAgent>().ResetPath(); } catch { };
                //WorkersActive[i].GetComponent<StatBar>().UpdateJob();
            }
        }
    }
}
