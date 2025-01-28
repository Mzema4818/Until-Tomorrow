using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject townhall;

    [Header("NavMeshStuff")]
    public NavMeshAgent agent;
    Animator animator;
    [Range(0, 100)] public float speed;
    public float realSpeed;


    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        agent.SetDestination(townhall.transform.position);
    }

    void Update()
    {
        animator.SetFloat("Velocity Z", Mathf.Clamp(agent.velocity.magnitude, 0, 0.5f));
    }

    public void SetSpeed()
    {
        agent.speed = realSpeed;
        speed = agent.speed;
    }
}
