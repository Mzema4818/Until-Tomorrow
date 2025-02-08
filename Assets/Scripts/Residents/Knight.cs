using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Knight : MonoBehaviour
{
    public GameObject location;
    public Vector3 locationEntered;

    public Animator animator;
    public NavMeshAgent agent;
    public ResidentTools residentTools;
    public FieldOfView fieldOfView;
    public Job job;
    public LightingManager time;

    private AnimationEvents AnimationEvents;
    private KnightHut knightHut;
    public ResidentScheudle residentScheudle;

    private Transform targetTransform;
    private int TimeToWakeUp = -1;
    private TurnOnGameObjects turnOnGameObjects;

    [Header("conditions")]
    public bool attackEnemies;
    public GameObject enemyToAttack;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        knightHut = location.GetComponent<KnightHut>();
        residentScheudle = transform.GetComponent<ResidentScheudle>();
        job = residentScheudle.job.GetComponent<Job>();
        residentTools = GetComponent<ResidentTools>();

        FindNext(time.WhatTimeIsIt(), new int[] { 0, 2 });
        //AnimationEvents = residentTools.heldItems[8].GetComponent<AnimationEvents>();

        //locationEntered = transform.position;
        //agent.enabled = false;

        turnOnGameObjects = transform.parent.gameObject.AddComponent<TurnOnGameObjects>();
        turnOnGameObjects.time = time;
        turnOnGameObjects.resident = gameObject;
        turnOnGameObjects.TimeToWakeUp = TimeToWakeUp;
        turnOnGameObjects.building = location;

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (attackEnemies)
        {
            enemyToAttack = location.GetComponent<FieldOfView>().objectSeen.gameObject;
            agent.SetDestination(enemyToAttack.transform.position); //only works with vector3.zero right now, think it has something to do with postion always changes
            //print("current dest: " + agent.destination + " pos dest: " + pos);
        }
        else if (!attackEnemies && Vector3.Distance(transform.position, location.transform.position) < location.GetComponent<IsABuilding>().distance)
        {
            gameObject.SetActive(false);
        }

        /*if (!attackEnemies && Vector3.Distance(transform.position, location.transform.position) < location.GetComponent<IsABuilding>().distance)
        {

        }
        else if(attackEnemies && location.GetComponent<FieldOfView>().canSeePlayer)
        {
            gameObject.SetActive(true);
            agent.SetDestination(location.GetComponent<FieldOfView>().objectSeen.transform.position);
        }*/
    }

    private void FindNext(int time, int[] TimesToNotFind)
    {
        for (int i = time; i < residentScheudle.Schedule.Length; i++)
        {
            if (residentScheudle.Schedule[i] == TimesToNotFind[0] || residentScheudle.Schedule[i] == TimesToNotFind[1])
            {
                TimeToWakeUp = i;
                break;
            }
        }

        if (TimeToWakeUp == -1)
        {
            for (int i = 0; i < time; i++)
            {
                if (residentScheudle.Schedule[i] == TimesToNotFind[0] || residentScheudle.Schedule[i] == TimesToNotFind[1])
                {
                    TimeToWakeUp = i;
                    break;
                }
            }
        }
    }

    private void OnDestroy()
    {
        try
        {
            job.workersWorking--;
            job.statMultiplier -= transform.GetComponent<ResidentStats>().Stats[3];
        }
        catch { };

        residentScheudle.AtLocation = false;
        if (turnOnGameObjects != null) Destroy(turnOnGameObjects);
    }
}
