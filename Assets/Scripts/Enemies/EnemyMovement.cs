using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    private FieldOfView fieldOfView;
    private Transform townHall;
    public Transform townhallParent;
    public Transform attackingObject;

    //Attacking
    public float timeBetweenAttacks;
    public int attackRange;
    private bool alreadyAttacked;

    //Stats
    public int damage;

    public void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        fieldOfView = GetComponent<FieldOfView>();

        townHall = townhallParent.GetChild(0);
        if (townHall == null)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        animator.SetFloat("Velocity Z", Mathf.Clamp(agent.velocity.magnitude, 0, 0.5f));

        if (fieldOfView.canSeePlayer)
        {
            //enemy attacks object until either its dead, or the objects dead
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
            GoToTownHall();
        }
    }

    private void GoToTownHall()
    {
        transform.LookAt(townHall);
        agent.SetDestination(townHall.position);
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

            //Attacking objects that have health on their parents
            Health parentHealth = attackingObject.parent.GetComponent<Health>();
            if (parentHealth != null)
            {
                parentHealth.ModifyHealth(-damage);
                if (parentHealth.currentHealth < 0) attackingObject = null;
                return;
            }

            //Attacking buildings
            BuildingHealth buildingHealth = attackingObject.parent.GetComponent<BuildingHealth>();
            if (buildingHealth != null)
            {
                buildingHealth.ModifyHealth(-damage);
                if (buildingHealth.currentHealth < 0) attackingObject = null;
                return;
            }

            ResidentHealth residentHealth = attackingObject.GetComponent<ResidentHealth>();
            if (residentHealth != null)
            {
                residentHealth.ModifyHealth(-damage);
                if (residentHealth.currentHealth < 0) attackingObject = null;
                return;
            }

        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
