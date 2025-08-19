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
    [Range(0, 100)] public float runSpeed;
    private float realSpeed;
    [Range(0, 500)] public float walkRadius;

    [Header("Schedule Side Conditions")]
    public bool isBeingTalkedTo;
    public bool followPlayer;
    public bool followPlayerHome;
    public bool shouldGoWander;
    public bool recentlyAttacked;
    public bool dead;
    private Coroutine wanderRoutine;
    public Transform runAwayFrom;

    [Header("Schedule Conditions")]
    public bool shouldWork;
    public bool shouldSleep;
    public bool shouldWander;

    [Header("Job Stuff")]
    public bool AtLocation;

    public GameObject closestMessHall;
    public GameObject closestTavern;
    public ResidentTools residentTools;

    public int[] Schedule = new int[24];

    private void Awake()
    {
        realSpeed = speed;

        residentTools = GetComponent<ResidentTools>();
        animator = GetComponent<Animator>();

        UpdateActivity(time.WhatTimeIsIt());

        //if (shouldWander || (shouldWork && job == null) || (shouldSleep && home == null)) shouldGoWander = true;
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        residentStats = GetComponent<ResidentStats>();

        agent.speed = realSpeed;
        speed = agent.speed;
    }

    //Update is called once per frame
    void Update()
    {
        if (dead) return;

        //Handle Speed;
        if (shouldRun) animator.SetFloat("velocity Z", Mathf.Clamp(agent.velocity.magnitude, 0, 1f));
        else animator.SetFloat("velocity Z", Mathf.Clamp(agent.velocity.magnitude, 0, 0.5f));

        //Handle movement
        if (HandleImmediateReactions()) return;
        HandleDynamicBehavior();
    }

    private bool HandleImmediateReactions()
    {
        if (runAwayFrom != null)
        {
            RunAway(player.transform);
            return true;
        }

        if (followPlayer || followPlayerHome)
        {
            agent.SetDestination(player.transform.position);
            return true;
        }

        if (isBeingTalkedTo)
        {
            if (!AtLocation) LookAtPlayer();
            pause();
            return true;
        }

        return false;
    }

    private void HandleDynamicBehavior()
    {
        //Wander or eat
        if (shouldWander ||
            (shouldWork && (job == null || job.GetComponent<IsABuilding>().beingMoved)) ||
            (shouldSleep && (home == null || home.GetComponent<IsABuilding>().beingMoved)))
        {
            HandleWanderOrEat();
            return;
        }

        //Sleep
        if (shouldSleep && home != null && !home.GetComponent<IsABuilding>().beingMoved)
        {
            GoHome();
            return;
        }

        //Work
        if (shouldWork && job != null && !job.GetComponent<IsABuilding>().beingMoved)
        {
            GoWork();
        }
    }

    private void HandleWanderOrEat()
    {
        if (closestMessHall == null) closestMessHall = FindClosestBuilding(messHalls.transform);
        if (closestTavern == null) closestTavern = FindClosestBuilding(taverns.transform);

        if (closestTavern != null)
            tavern = closestTavern.GetComponent<Tavern>();

        //Eat conditions
        if (residentStats.joinedTown &&
            foodBar.GetComponent<Image>().fillAmount < .85f &&
            closestMessHall != null && closestTavern != null &&
            CheckIfHasFood(closestMessHall) &&
            !closestTavern.GetComponent<IsABuilding>().beingMoved &&
            !closestMessHall.GetComponent<IsABuilding>().beingMoved &&
            tavern != null && tavern.sitting < tavern.maxSeats)
        {
            if (GetComponent<Hungry>() == null)
            {
                var hungry = gameObject.AddComponent<Hungry>();
                hungry.messhall = closestMessHall;
                hungry.tavern = closestTavern;
                hungry.foodBar = foodBar;
            }

            //Stop wandering if we decide to eat
            if (wanderRoutine != null)
            {
                StopCoroutine(wanderRoutine);
                wanderRoutine = null;
            }
        }
        else
        {
            if (wanderRoutine == null)
            {
                wanderRoutine = StartCoroutine(WaitForWander());
            }
        }
    }

    private void GoHome()
    {
        if (!AtLocation)
        {
            //sometimes tent is too far above terrain to get recongized???
            NavMeshHit hit;
            bool isOnNavMesh = NavMesh.SamplePosition(home.transform.position, out hit, 2.0f, NavMesh.AllAreas);

            if(!isOnNavMesh) agent.SetDestination(home.transform.position - new Vector3(0, 2, 0));
            else agent.SetDestination(home.transform.position);

            //agent.SetDestination(home.transform.position - new Vector3(0,2,0));
            StopCoroutine(WaitForWander());
            wanderRoutine = null;
            GoToLocation(home, shouldSleep);
        }
    }

    private void GoWork()
    {
        if (!AtLocation)
        {
            agent.SetDestination(job.transform.position);
            StopCoroutine(WaitForWander());
            wanderRoutine = null;
            GoToLocation(job, shouldWork);
        }
    }

    IEnumerator WaitForWander()
    {
        //shouldGoWander = false;
        yield return new WaitForSeconds(Random.Range(2, 5));
        //shouldGoWander = true;
        try { 
            if(agent.isActiveAndEnabled) agent.SetDestination(RandomNavMeshLocation());
            wanderRoutine = null;
        } catch { }; //might need to get rid of try catch for testing
    }

    public void RunAway(Transform target)
    {
        Vector3 newPos = Vector3.zero;
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance < 20)
        {
            Vector3 dirToPlayer = transform.position - target.position;
            newPos = transform.position + dirToPlayer;
        }

        agent.SetDestination(newPos);
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
        StopCoroutine(WaitForWander());
        wanderRoutine = null;
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


    public void UpdateActivity(int hour)
    {
        //0 = sleep
        //1 = work
        //2 = wander

        switch (Schedule[hour])
        {
            case 0:
                shouldSleep = true;
                shouldWork = false;
                shouldWander = false;
                RemoveJobs();
                break;

            case 1:
                shouldWork = true;
                shouldSleep = false;
                shouldWander = false;
                break;

            case 2:
                shouldWander = true;
                shouldWork = false;
                shouldSleep = false;
                RemoveJobs();
                break;
        }
    }

    public void UpdateActivityOnce(int num)
    {
        //0 = sleep
        //1 = work
        //2 = wander

        switch (num)
        {
            case 0:
                shouldSleep = true;
                shouldWork = false;
                shouldWander = false;
                RemoveJobs();
                break;

            case 1:
                shouldWork = true;
                shouldSleep = false;
                shouldWander = false;
                break;

            case 2:
                shouldWander = true;
                shouldWork = false;
                shouldSleep = false;
                RemoveJobs();
                break;
        }
    }

    public void RemoveJobs()
    {
        Destroy(transform.GetComponent<Farmer>());
        Destroy(transform.GetComponent<LumberWorker>());
        Destroy(transform.GetComponent<Miner>());
        Destroy(transform.GetComponent<Chef>());
        Destroy(transform.GetComponent<Archer>());
        Destroy(transform.GetComponent<Knight>());
    }

    private void GoToLocation(GameObject location, bool EnterTime)
    {
        Job job = location.GetComponent<Job>();
        Tent tent = location.GetComponent<Tent>();

        if (EnterTime)
        {
            if (Vector3.Distance(transform.position, location.transform.position) < location.GetComponent<IsABuilding>().distance)
            {
                if (tent != null)
                {
                    gameObject.AddComponent<Sleeping>().time = time;
                    gameObject.GetComponent<Sleeping>().building = location;
                }

                if (job != null)
                {
                    job.JoinJob(job.GetWorkerIndex(gameObject));
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

    private int FindNext(int time, int[] TimesToNotFind)
    {
        int TimeToWakeUp = -1;

        for (int i = time; i <Schedule.Length; i++)
        {
            if (Schedule[i] == TimesToNotFind[0] || Schedule[i] == TimesToNotFind[1])
            {
                TimeToWakeUp = i;
                break;
            }
        }

        if (TimeToWakeUp == -1)
        {
            for (int i = 0; i < time; i++)
            {
                if (Schedule[i] == TimesToNotFind[0] || Schedule[i] == TimesToNotFind[1])
                {
                    TimeToWakeUp = i;
                    break;
                }
            }
        }

        return TimeToWakeUp;
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
