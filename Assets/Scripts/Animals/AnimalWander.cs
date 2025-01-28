using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
public class AnimalWander : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    private FieldOfView FieldOfView;
    public GameObject Player;

    [Header("Movement")]
    [Range(0, 100)] public float speed;
    [Range(0, 100)] public float runSpeed;
    [Range(0, 500)] public float walkRadius;
    private float enemyDistanceRun;

    [Header("Conditions")]
    public bool shouldGoWander;
    public bool setDestination;
    public bool seeEnemy;
    public bool attacker;

    [Header("Bools")]
    public bool shouldAttack;

    public bool test;

    private void Awake()
    {
        shouldGoWander = true;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        FieldOfView = GetComponent<FieldOfView>();

        agent.speed = speed;
        enemyDistanceRun = walkRadius;
    }

    private void Update()
    {
        if (FieldOfView.canSeePlayer)
        {
            agent.speed = runSpeed;
            seeEnemy = true;
        }
        else
        {
            agent.speed = speed;
            seeEnemy = false;
        }

        //Handle Speed;
        if (seeEnemy) animator.SetFloat("velocity Z", Mathf.Clamp(agent.velocity.magnitude, 0, 1f));
        else animator.SetFloat("velocity Z", Mathf.Clamp(agent.velocity.magnitude, 0, 0.5f));

        if (seeEnemy)
        {
            if (attacker && FieldOfView.objectSeen == Player)
            {
                LookAtPlayer(FieldOfView.objectSeen);
                agent.SetDestination(FieldOfView.objectSeen.transform.position);

                if(agent.remainingDistance < 4)
                {
                    AttackAnimation();
                }
            }
            else
            {
                agent.SetDestination(RunAway(FieldOfView.objectSeen));
            }
        }
        else if (shouldGoWander)
        {
            StartCoroutine(WaitForWander());
        }
        else if (!shouldGoWander && setDestination)
        {
            if(agent.remainingDistance < 2)
            {
                agent.ResetPath();
                agent.velocity = Vector3.zero;
                shouldGoWander = true;
            }
        }
    }

    IEnumerator WaitForWander()
    {
        shouldGoWander = false;
        setDestination = false;

        yield return new WaitForSeconds(Random.Range(2, 5));
        setDestination = true;

        try
        {
            if (agent.isActiveAndEnabled) agent.SetDestination(RandomNavMeshLocation());
        }
        catch { }; //might need to get rid of try catch for testing
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

    public Vector3 RunAway(GameObject enemy)
    {
        Vector3 newPos = Vector3.zero;
        float distance = Vector3.Distance(enemy.transform.position, transform.position);

        if(distance < enemyDistanceRun)
        {
            Vector3 dirToPlayer = transform.position - enemy.transform.position;
            newPos = transform.position + dirToPlayer;
        }

        return newPos;
    }

    private void LookAtPlayer(GameObject enemy)
    {
        var lookPos = enemy.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10);
    }

    public void AttackAnimation()
    {
        animator.SetTrigger("Attack");
    }

    public void AttackDamage()
    {
        if (agent.remainingDistance < 4)
        {
            Player.GetComponent<Health>().ModifyHealth(-25);
        }
    }
}
