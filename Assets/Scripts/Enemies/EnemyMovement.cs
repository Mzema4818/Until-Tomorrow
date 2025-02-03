using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    public Transform player;
    public Transform townhallParent;
    public Transform attackingObject;
    public LayerMask groundMask, playerMask, residentMask, buildingMask;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    private bool alreadyAttacked;

    //states
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange, runToTownHall;

    //Stats
    public int damage;

    public void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        runToTownHall = true;
        Transform townhall = townhallParent.GetChild(0);
        if (townhall == null)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.LookAt(townhall);
            agent.SetDestination(townhall.position);
        }
    }

    private void Update()
    {
        // Set Animations
        animator.SetFloat("Velocity Z", Mathf.Clamp(agent.velocity.magnitude, 0, 0.5f));

        // Check for sight and attack range
        LayerMask combinedMask = playerMask | residentMask | buildingMask;
        Collider[] objectsInRange = Physics.OverlapSphere(transform.position, sightRange, combinedMask);

        // Determine the closest object to attack
        if (objectsInRange.Length > 0)
        {
            attackingObject = FindClosestObject(objectsInRange);
            playerInSightRange = true;
        }
        else
        {
            attackingObject = null;
            playerInSightRange = false;
        }

        playerInAttackRange = attackingObject != null && Vector3.Distance(transform.position, attackingObject.position) <= attackRange;

        // Decide behavior
        if (runToTownHall) AttackTownHall();
        else if (attackingObject == null) Patroling();
        else if (!playerInAttackRange) ChasePlayer();
        else AttackPlayer();
    }

    private Transform FindClosestObject(Collider[] objects)
    {
        Transform closest = null;
        float minDistance = Mathf.Infinity;

        foreach (Collider obj in objects)
        {
            PlayerController playerController = obj.GetComponent<PlayerController>();
            if (playerController != null && !playerController.shouldMove)
                continue; // Skip the player if their script is disabled (i.e., they're "dead")

            float distance = Vector3.Distance(transform.position, obj.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = obj.transform;
            }
        }
        return closest;
    }



    private void AttackTownHall()
    {
        if (playerInSightRange)
        {
            runToTownHall = false;
        }
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

    private void ChasePlayer()
    {
        agent.SetDestination(attackingObject.position);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(attackingObject);

        if (!alreadyAttacked)
        {
            animator.SetTrigger("Attack");
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
            alreadyAttacked = true;

            Health itselfHealth = attackingObject.GetComponent<Health>();
            if (itselfHealth != null)
            {
                itselfHealth.ModifyHealth(-damage);
                if (itselfHealth.currentHealth < 0) attackingObject = null;
                return;
            }

            Health parentHealth = attackingObject.parent.GetComponent<Health>();
            if (parentHealth != null)
            {
                parentHealth.ModifyHealth(-damage);
                if (parentHealth.currentHealth < 0) attackingObject = null;
            }

        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
