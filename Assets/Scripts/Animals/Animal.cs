using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Animal : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    private FieldOfView fieldOfView;

    //Attacking
    public Transform attackingObject;
    public float timeBetweenAttacks;
    public int attackRange;
    private bool alreadyAttacked;

    //stats
    public bool shouldGoWander;
    public int walkRadius;
    public float walkSpeed;
    public float runSpeed;
    public int damage;

    public void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        fieldOfView = GetComponent<FieldOfView>();

        shouldGoWander = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Speed stuff
        if (fieldOfView.canSeePlayer) animator.SetFloat("velocity Z", Mathf.Clamp(agent.velocity.magnitude, 0, 1f));
        else animator.SetFloat("velocity Z", Mathf.Clamp(agent.velocity.magnitude, 0, 0.5f));

        if (fieldOfView.canSeePlayer) agent.speed = runSpeed;
        else agent.speed = walkSpeed;

        //Scheudle
        if (!fieldOfView.canSeePlayer)
        {
            if(shouldGoWander) StartCoroutine(WaitForWander());
        }
        else
        {
            if (damage != 0)
            {
                if (attackingObject == null) { attackingObject = null; try { attackingObject = fieldOfView.objectSeen.transform; } catch { }; }
                else
                {
                    bool enemyInAttackRange = Vector3.Distance(transform.position, attackingObject.transform.position) <= attackRange;
                    if (enemyInAttackRange) Attack();
                    else GoToEnemy();
                }
            }
            else
            {
                RunAway();
            }
        }
    }


    private void GoToEnemy()
    {
        transform.LookAt(attackingObject);
        agent.SetDestination(attackingObject.position);
    }

    private void Attack()
    {
        transform.LookAt(attackingObject);
        agent.SetDestination(transform.position);

        if (!alreadyAttacked)
        {
            animator.SetTrigger("Attack");
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
            alreadyAttacked = true;

            //Attacking objects that have health on themselves
            Health itselfHealth = attackingObject.GetComponent<Health>();
            if (itselfHealth != null)
            {
                itselfHealth.ModifyHealth(-damage);
                if (itselfHealth.currentHealth < 0) attackingObject = null;
                return;
            }

        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void RunAway()
    {
        agent.SetDestination(RunAway(fieldOfView.objectSeen.gameObject));
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

    IEnumerator WaitForWander()
    {
        shouldGoWander = false;
        yield return new WaitForSeconds(Random.Range(2, 5));
        shouldGoWander = true;
        try
        {
            if (agent.isActiveAndEnabled) agent.SetDestination(RandomNavMeshLocation());
        }
        catch { }; //might need to get rid of try catch for testing
    }

    private Vector3 RandomNavMeshLocation()
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
}
