using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static Item;

public class ResidentScheudle : MonoBehaviour
{
    [Header("GameObjects")]
    public LightingManager time;
    public GameObject home;
    public GameObject job;
    public GameObject player;
    public GameObject foodBar;
    public ResidentStats residentStats;
    public GameObject messHalls;
    public GameObject taverns;
    private Tavern tavern = null;

    [Header("Animations")]
    public Animator animator;

    [Header("NavMeshStuff")]
    public NavMeshAgent agent;

    [Header("Bools")]
    public bool shouldRun;

    [Header("Resident Movement")]
    [Range(0, 100)] public float speed;
    private float realSpeed;
    [Range(0, 500)] public float walkRadius;

    [Header("Schedule Side Conditions")]
    public bool isBeingTalkedTo;
    public bool followPlayer;
    public bool followPlayerHome;
    public bool shouldGoWander;

    [Header("Schedule Conditions")]
    public bool shouldWork;
    public bool shouldSleep;
    public bool shouldWander;

    [Header("Job Stuff")]
    public bool AtLocation;

    public GameObject closestMessHall;
    public GameObject closestTavern;
    public ResidentTools residentTools;

    //0 = sleep
    //1 = work
    //2 = wander
    public int[] Schedule = new int[24];

    private void Awake()
    {
        realSpeed = speed;

        residentTools = GetComponent<ResidentTools>();
        animator = GetComponent<Animator>();
        WhatShouldResidentDo();
        if (shouldWander) shouldGoWander = true;
        if (shouldWork && job == null) shouldGoWander = true;
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        residentStats = GetComponent<ResidentStats>();

        agent.speed = realSpeed;
        speed = agent.speed;
    }

    // Update is called once per frame
    void Update()
    {
        //Handle Speed;
        if (shouldRun) animator.SetFloat("velocity Z", Mathf.Clamp(agent.velocity.magnitude, 0, 1f));
        else animator.SetFloat("velocity Z", Mathf.Clamp(agent.velocity.magnitude, 0, 0.5f));

        WhatShouldResidentDo();

        //Following the player takes all priority 
        if (followPlayer || followPlayerHome)
        {
            agent.SetDestination(player.transform.position);
        }
        else
        {
            //Talking to the player is next 
            if (isBeingTalkedTo)
            {
                if(!AtLocation) LookAtPlayer();
                pause();
            }
            else
            {
                //If NPC is not being spoken to, or following player, then it follows its basic scheudle

                //If its wander time OR work time, OR sleep time, but the NPC doesnt have a job/home, OR the job/home is being moved, then they wander around aimlessly
                if (shouldWander || (shouldWork && job == null) || (shouldSleep && home == null) || (home != null && home.GetComponent<IsABuilding>().beingMoved) || (job != null && job.GetComponent<IsABuilding>().beingMoved))
                {
                    if(closestMessHall == null) closestMessHall = FindClosestBuilding(messHalls.transform);
                    if(closestTavern == null) closestTavern = FindClosestBuilding(taverns.transform);

                    if (closestTavern != null)
                    {
                        tavern = closestTavern.GetComponent<Tavern>();
                    }

                    //Check if the NPC is hungry, if there exsists a messhall and tavern, and the tavern seats arent full, AND messhall and tavern aren't being moved
                    if (residentStats.joinedTown && foodBar.GetComponent<Image>().fillAmount < .85 && closestMessHall != null && closestTavern != null && CheckIfHasFood(closestMessHall) 
                        && !closestTavern.GetComponent<IsABuilding>().beingMoved && !closestMessHall.GetComponent<IsABuilding>().beingMoved && tavern != null && tavern.sitting < tavern.maxSeats)
                    {
                        if(gameObject.GetComponent<Hungry>() == null)
                        {
                            gameObject.AddComponent<Hungry>().messhall = closestMessHall;
                            gameObject.GetComponent<Hungry>().tavern = closestTavern;
                            gameObject.GetComponent<Hungry>().residentTools = residentTools;
                            gameObject.GetComponent<Hungry>().foodBar = foodBar;
                        }
                    }
                    else
                    {
                        //dont forget to fix it so, if resident takes last food but hes holding it, it doesnt remove script
                        if (shouldGoWander)
                        {
                            StartCoroutine(WaitForWander());
                        }
                    }
                }
                //If its sleep time, a home exists, and it isnt being moved, then go to home
                else if (shouldSleep && home != null && !home.GetComponent<IsABuilding>().beingMoved)
                {
                    if (home != null && !AtLocation) agent.SetDestination(home.transform.position); StopCoroutine(WaitForWander());
                    if (home != null && !AtLocation) GoToWork(home, shouldSleep);
                }
                //If its work time, a work exists, and it isnt being moved, then go to work
                else if (shouldWork && job != null && !job.GetComponent<IsABuilding>().beingMoved)
                {
                    if (job != null && !AtLocation) agent.SetDestination(job.transform.position); StopCoroutine(WaitForWander());
                    if (job != null && !AtLocation) GoToWork(job, shouldWork);
                }
            }
        }
    }

    IEnumerator WaitForWander()
    {
        shouldGoWander = false;
        yield return new WaitForSeconds(Random.Range(2, 5));
        shouldGoWander = true;
        try { 
            if(agent.isActiveAndEnabled) agent.SetDestination(RandomNavMeshLocation());
        } catch { }; //might need to get rid of try catch for testing
    }

    public Vector3 RandomNavMeshLocation()
    {
        Vector3 finalPosition = Vector3.zero;
        Vector3 randomPosition = Random.insideUnitSphere * walkRadius;
        randomPosition += transform.position;
        if (NavMesh.SamplePosition(randomPosition, out NavMeshHit hit, walkRadius, 1))
        {
            finalPosition = hit.position;
        }

        return finalPosition;
    }

    void pause()
    {
        //lastAgentVelocity = agent.velocity;
        //lastAgentPath = agent.path;
        agent.velocity = Vector3.zero;
        if(agent.enabled) agent.ResetPath();
    }

    private void LookAtPlayer()
    {
        var lookPos = player.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
    }

    private void WhatShouldResidentDo()
    {
        for(int i = 0; i < time.Hours.Length; i++)
        {
            if (time.Hours[i])
            {
                Convert(Schedule[i]);
            }
        }
    }

    public void Convert(int i)
    {
        if(i == 0)
        {
            shouldWork = false;
            shouldSleep = true;
            shouldWander = false;
        }
        else if (i == 1)
        {
            shouldWork = true;
            shouldSleep = false;
            shouldWander = false;
        }
        else if (i == 2)
        {
            shouldWork = false;
            shouldSleep = false;
            shouldWander = true;
            RemoveJobs();
        }
    }

    public void RemoveJobs()
    {
        //UpdateWorkers(null, job.GetComponent<Farm>(), transform.GetComponent<Lumbermill>(), -1);

        Destroy(transform.GetComponent<Farmer>());
        Destroy(transform.GetComponent<LumberWorker>());
        Destroy(transform.GetComponent<Miner>());
        Destroy(transform.GetComponent<Chef>());
        Destroy(transform.GetComponent<Archer>());
        Destroy(transform.GetComponent<Knight>());
    }

    private void GoToWork(GameObject location, bool EnterTime)
    {
        Job job = location.GetComponent<Job>();
        if (EnterTime)
        {
            Tent tent = location.GetComponent<Tent>();

            if (Vector3.Distance(transform.position, location.transform.position) < location.GetComponent<IsABuilding>().distance)
            {
                if (tent != null)
                {
                    gameObject.AddComponent<Sleeping>().time = time;
                    gameObject.GetComponent<Sleeping>().building = location;
                }

                if (job != null)
                {
                    job.JoinJob(job.GetWorkerIndex(gameObject.name));
                    GetComponent<Hats>().GetHat(job.name);
                }

                AtLocation = true;
                agent.ResetPath();
            }
        }
    }

    GameObject FindClosestBuilding(Transform building)
    {
        GameObject tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform t in building)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                tMin = t.gameObject;
                minDist = dist;
            }
        }
        return tMin;
    }

    public bool CheckIfHasFood(GameObject messhall)
    {
        Messhall building = messhall.GetComponent<Messhall>();

        foreach(InventorySlot slot in building.cookedFoodSlots)
        {
            if (slot.gameObject.transform.childCount != 0) return true;
        }

        return false;
    }

    private GameObject[] ReorganizeArray()
    {
        //when deleting a resident from an array theres an empty space there which messes things up, this just gets rid of that space
        Job jobComponet = job.GetComponent<Job>();
        GameObject[] returnArray = new GameObject[jobComponet.MaxWorkers];

        int index = 0;
        for (int i = 0; i < returnArray.Length; i++)
        {
            if (jobComponet.WorkersActive[i] != null)
            {
                returnArray[index] = jobComponet.WorkersActive[i];
                index++;
            }
        }

        return returnArray;
    }

    private void OnDestroy()
    {
        if(job != null)
        {
            Job jobComponet = job.GetComponent<Job>();
            for(int i = 0; i < jobComponet.WorkersActive.Length; i++)
            {
                if (gameObject == jobComponet.WorkersActive[i])
                {
                    jobComponet.WorkersActive[i] = null;
                    jobComponet.Workers--;
                }
            }

            jobComponet.WorkersActive = ReorganizeArray();
        }
    }
}
