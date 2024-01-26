using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static Item;
using static UnityEditor.FilePathAttribute;

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
    public bool goingToGetFood;
    public bool goingToEat;
    //public bool shouldWakeUp;
    //public bool shouldGetOffWork;
    public bool shouldGoWander;
    //public int TimeToWakeUp;
    public Item.ItemType itemShouldEat;


    [Header("Schedule Conditions")]
    public bool shouldWork;
    public bool shouldSleep;
    public bool shouldWander;
    public bool shouldEat;

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

        if (followPlayer)
        {
            agent.SetDestination(player.transform.position);
        }
        else
        {
            if (isBeingTalkedTo)
            {
                if(!AtLocation) LookAtPlayer();
                pause();
            }
            else
            {
                if (shouldWander && shouldEat)
                {
                    if (closestMessHall != null && !closestMessHall.GetComponent<IsABuilding>().beingMoved && closestTavern != null && !closestTavern.GetComponent<IsABuilding>().beingMoved)
                    {
                        if (goingToGetFood)
                        {
                            StopCoroutine(WaitForWander());
                            agent.SetDestination(closestMessHall.transform.position);
                            GetFood(closestMessHall);
                        }

                        if (goingToEat)
                        {
                            StopCoroutine(WaitForWander());
                            agent.SetDestination(closestTavern.transform.position);
                            EatFood(closestTavern);
                        }
                    }
                    else
                    {
                        shouldEat = false;
                        goingToGetFood = false;
                        goingToEat = false;
                        //StartCoroutine(WaitForWander());
                        StopCoroutine(Eat());
                        animator.SetBool("Holding", false);
                        animator.SetBool("Eating", false);
                        residentTools.TurnOffAll();
                        Destroy(gameObject.GetComponent<Eating>());
                    }
                }
                else if ((shouldWander && !shouldEat) || (shouldWork && job == null) || (shouldSleep && home == null) || (home != null && home.GetComponent<IsABuilding>().beingMoved) || (job != null && job.GetComponent<IsABuilding>().beingMoved))
                {
                    try
                    {
                        if (agent != null && agent.isOnNavMesh && agent.remainingDistance <= agent.stoppingDistance && shouldGoWander)
                        {
                            closestMessHall = FindClosestBuilding(messHalls.transform);
                            closestTavern = FindClosestBuilding(taverns.transform);
                            Tavern tavern = closestTavern.GetComponent<Tavern>();

                            if (residentStats.joinedTown && foodBar.GetComponent<Image>().fillAmount < .85 && closestMessHall != null && CheckIfHasFood(closestMessHall) && closestTavern != null && tavern.sitting < tavern.maxSeats)
                            {
                                //shouldGoWander = false;
                                shouldEat = true;
                                goingToGetFood = true;
                            }
                            else
                            {
                                StartCoroutine(WaitForWander());
                            }
                        }
                    }
                    catch { };
                }
                else if (shouldSleep && home != null && !home.GetComponent<IsABuilding>().beingMoved)
                {
                    if (home != null && !AtLocation) agent.SetDestination(home.transform.position); StopCoroutine(WaitForWander());
                    if (home != null && !AtLocation) GoToWork(home, shouldSleep);
                }
                else if (shouldWork && job != null && !job.GetComponent<IsABuilding>().beingMoved)
                {
                    if (job != null && !AtLocation) agent.SetDestination(job.transform.position); StopCoroutine(WaitForWander());
                    if (job != null && !AtLocation) GoToWork(job, shouldWork);
                    /*if (job != null && !AtLocation) agent.SetDestination(job.transform.position); StopCoroutine(WaitForWander());
                    if (job != null && !AtLocation) GoToWork(job, shouldWork);*/
                }
            }
        }
    }

    IEnumerator WaitForWander()
    {
        shouldGoWander = false;
        yield return new WaitForSeconds(Random.Range(1, 7));
        shouldGoWander = true;
        try { agent.SetDestination(RandomNavMeshLocation()); } catch { }; //might need to get rid of try catch for testing
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

            shouldEat = false;
            goingToGetFood = false;
            goingToEat = false;
            //StartCoroutine(WaitForWander());
            shouldGoWander = true;
            StopCoroutine(Eat());
            animator.SetBool("Holding", false);
            animator.SetBool("Eating", false);
            Destroy(gameObject.GetComponent<Eating>());
            if (residentTools.heldItems[6].activeSelf) residentTools.ChangeEnable(6, false);
            RemoveJobs();
        }
        else if (i == 1)
        {
            shouldWork = true;
            shouldSleep = false;
            shouldWander = false;

            shouldEat = false;
            goingToGetFood = false;
            goingToEat = false;
            //StartCoroutine(WaitForWander());
            shouldGoWander = true;
            StopCoroutine(Eat());
            animator.SetBool("Holding", false);
            animator.SetBool("Eating", false);
            if (residentTools.heldItems[6].activeSelf) residentTools.ChangeEnable(6, false);
            Destroy(gameObject.GetComponent<Eating>());
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
    }

    private void GoToWork(GameObject location, bool EnterTime)
    {
        Job job = location.GetComponent<Job>();

        //&& job != null && !job.GetComponent<IsABuilding>().beingMoved
        if (EnterTime)
        {
            Farm farm = location.GetComponent<Farm>();
            Mine mine = location.GetComponent<Mine>();
            Tent tent = location.GetComponent<Tent>();
            Lumbermill lumbermill = location.GetComponent<Lumbermill>();
            Messhall messhall = location.GetComponent<Messhall>();

            if (Vector3.Distance(transform.position, location.transform.position) < location.GetComponent<IsABuilding>().distance)
            {
                if (tent != null)
                {
                    AtLocation = true;
                    gameObject.AddComponent<Sleeping>().time = time;
                    gameObject.GetComponent<Sleeping>().building = location;
                    agent.ResetPath();
                }

                if (job != null)
                {
                    UpdateWorkers(mine, farm, lumbermill, messhall, 1);
                    UpdateStats(mine, farm, lumbermill, messhall);

                    if (mine != null)
                    {
                        AtLocation = true;
                        gameObject.AddComponent<Miner>().time = time;
                        gameObject.GetComponent<Miner>().building = this.job;
                        agent.ResetPath();
                    }
                    else if (farm != null)
                    {
                        AtLocation = true;
                        gameObject.AddComponent<Farmer>().location = location;
                        agent.ResetPath();
                        agent.enabled = false;
                        animator.SetBool("Farming", true);
                    }
                    else if (lumbermill != null)
                    {
                        AtLocation = true;
                        gameObject.AddComponent<LumberWorker>().lumbermill = lumbermill;
                    }
                    else if (messhall != null)
                    {
                        AtLocation = true;
                        gameObject.AddComponent<Chef>().location = location;
                        agent.ResetPath();
                        agent.enabled = false;
                        animator.SetBool("Cooking", true);
                    }
                }
            }
        }
    }

    private void GetFood(GameObject location)
    {
        if (Vector3.Distance(transform.position, location.transform.position) < location.GetComponent<IsABuilding>().distance)
        {
            Messhall messhall = closestMessHall.GetComponent<Messhall>();
            goingToEat = true;
            goingToGetFood = false;
            agent.ResetPath();
            animator.SetBool("Holding", true);
            itemShouldEat = messhall.GetItemInSlot(messhall.cookedFoodSlots);
            print(itemShouldEat);
            messhall.RemoveItem(itemShouldEat, messhall.cookedFoodSlots);
            residentTools.ChangeEnable(ItemToInt(itemShouldEat), true);
        }
    }

    private int ItemToInt(Item.ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.charredBerry: return 6;
        }
    }

    private void EatFood(GameObject location)
    {
        if (Vector3.Distance(transform.position, location.transform.position) < location.GetComponent<IsABuilding>().distance)
        {
            animator.SetBool("Holding", false);
            goingToEat = false;
            agent.ResetPath();
            agent.enabled = false;
            animator.SetBool("Eating", true);
            gameObject.AddComponent<Eating>().location = location;
            StartCoroutine(Eat());
        }
    }

    IEnumerator Eat()
    {
        yield return new WaitForSeconds(5);
        animator.SetBool("Eating", false);
        foodBar.GetComponent<Image>().fillAmount += (float).3; //Hard coded how much food per food.  Gotta change it depending on food held. //also sometimes food doesnt go up?
        transform.GetComponent<ResidentStats>().Stats[1] += 30;
        shouldEat = false;
        goingToGetFood = false;
        goingToEat = false;
        //shouldGoWander = true;
        Destroy(gameObject.GetComponent<Eating>());
        residentTools.TurnOffAll();
    }

    public void UpdateWorkers(Mine mine, Farm farm, Lumbermill lumbermill, Messhall messhall, int num)
    {
        if (mine != null) mine.workersActive += num;
        else if (farm != null) farm.workersActive += num;
        else if (messhall != null) messhall.workersActive += num;
        //else if (lumbermill != null) lumbermill.workersActive += num;
    }

    public void UpdateStats(Mine mine, Farm farm, Lumbermill lumbermill, Messhall messhall)
    {
        ResidentStats stats = transform.GetComponent<ResidentStats>();

        //all of them are stats[3] rn because all of these jobs depend on strength stat
        //if i change what stats the job depends on change it also when removing the script
        if (mine != null) mine.statMultiplier += stats.Stats[3];
        else if (farm != null) farm.statMultiplier += stats.Stats[3];
        else if (messhall != null) messhall.statMultiplier += stats.Stats[3];
        //else if (lumbermill != null) lumbermill.statMultiplier += stats.Stats[3];
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

    public int GetFoodSlot(GameObject messhall)
    {
        Messhall building = messhall.GetComponent<Messhall>();
        int i = 0;
        foreach (InventorySlot slot in building.cookedFoodSlots)
        {
            if (slot.gameObject.transform.childCount != 0) return i;
            i++;
        }

        return -1;
    }
}
