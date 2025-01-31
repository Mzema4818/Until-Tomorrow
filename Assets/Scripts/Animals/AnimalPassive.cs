using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimalPassive : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    public Transform player;
    public LayerMask groundMask, playerMask;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //states
    public float sightRange;
    public bool playerInSightRange;

    //Stats
    public float walkSpeed;
    public float runSpeed;

    public void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //Set animations
        if (playerInSightRange) animator.SetFloat("velocity Z", Mathf.Clamp(agent.velocity.magnitude, 0, 1f));
        else animator.SetFloat("velocity Z", Mathf.Clamp(agent.velocity.magnitude, 0, 0.5f));

        //Set speeds;
        if (playerInSightRange) agent.speed = runSpeed;
        else agent.speed = walkSpeed;

        //Check for sight attack range;
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerMask);

        if (!playerInSightRange) Patroling();
        else RunAway();
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();
        else agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //WalkPoint reached
        if (distanceToWalkPoint.magnitude < 2f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundMask)) walkPointSet = true;
    }

    private void RunAway()
    {
        agent.SetDestination(RunAway(player.gameObject));
    }

    public Vector3 RunAway(GameObject enemy)
    {
        Vector3 newPos = Vector3.zero;
        float distance = Vector3.Distance(enemy.transform.position, transform.position);

        if (distance < 20)
        {
            Vector3 dirToPlayer = transform.position - enemy.transform.position;
            newPos = transform.position + dirToPlayer;
        }

        return newPos;
    }
}
