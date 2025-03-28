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
    public GameObject enemy;
    public bool enemyInAttackRange;
    public int attackRange;

    //Attacking
    public float timeBetweenAttacks;
    public bool alreadyAttacked;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        attackRange = 3;
        timeBetweenAttacks = 2;
        damage = 10;

        //pos = new Vector3(216.800003f, 17.4599991f, -172.899994f);
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        knightHut = location.GetComponent<KnightHut>();
        residentScheudle = transform.GetComponent<ResidentScheudle>();
        job = residentScheudle.job.GetComponent<Job>();
        residentTools = GetComponent<ResidentTools>();

        FindNext(time.WhatTimeIsIt(), new int[] { 0, 2 });

        residentTools.TurnOffAll();
        residentTools.ChangeEnable(8, true);

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
            FieldOfView fov = location.GetComponent<FieldOfView>();
            if (fov.objectSeen != null) // Check if an enemy is detected
            {
                enemy = fov.objectSeen.gameObject;

                if (enemy != null) // Ensure enemy is still valid
                {
                    // Issue with the location might not be on the navmesh, need more testing
                    agent.SetDestination(enemy.transform.position);

                    enemyInAttackRange = Vector3.Distance(transform.position, enemy.transform.position) <= attackRange;
                    if (enemyInAttackRange) Attack();
                }
            }
        }
        else
        {
            agent.SetDestination(location.transform.position);
            if (Vector3.Distance(transform.position, location.transform.position) < location.GetComponent<IsABuilding>().distance)
            {
                gameObject.SetActive(false);
            }
        }

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
        residentTools.TurnOffAll();

        if (turnOnGameObjects != null) Destroy(turnOnGameObjects);
    }

    private void Attack()
    {
        agent.SetDestination(transform.position);

        if (!alreadyAttacked)
        {
            animator.SetTrigger("Attack");
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
            alreadyAttacked = true;

            Health health = enemy.GetComponent<Health>();
            if (health != null)
            {
                health.ModifyHealth(-damage);
                if (health.currentHealth < 0) enemy = null;
                return;
            }
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
