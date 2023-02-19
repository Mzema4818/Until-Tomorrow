using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ResidentWander : MonoBehaviour
{
    public NavMeshAgent agent;
    Animator animator;

    [Range(0, 100)] public float speed;
    public float realSpeed;
    [Range(0, 500)] public float walkRadius;
    private bool shouldWander;
    public bool shouldRun;
    public bool getOffBoat;
    public bool BeingTalkedTo;
    public ExportPrisoners exportPrisoners;
    public GameObject player;

    public Vector3 lastAgentVelocity;
    public NavMeshPath lastAgentPath;

    private void Awake()
    {
        realSpeed = speed;
    }

    private void Start()
    {
        shouldWander = true;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        if(agent != null && getOffBoat)
        {
            agent.speed = speed;
            agent.SetDestination(RandomNavMeshLocation());
        }
    }

    public void Update()
    {
        if(agent != null && agent.remainingDistance <= agent.stoppingDistance && shouldWander && !getOffBoat)
        {
            StartCoroutine(WaitForWander());
        }

        if (agent != null && agent.remainingDistance <= agent.stoppingDistance && getOffBoat)
        {
            agent.SetDestination(RandomNavMeshLocation());
        }

        //animator.SetFloat("velocity X", Mathf.Clamp(agent.velocity.magnitude,0, 0.5f));
        if (shouldRun)
        {
            animator.SetFloat("velocity Z", Mathf.Clamp(agent.velocity.magnitude, 0, 1f));
        }
        else
        {
            animator.SetFloat("velocity Z", Mathf.Clamp(agent.velocity.magnitude, 0, 0.5f));
        }

        if (BeingTalkedTo)
        {
            pause();
            LookAtPlayer();
        }
    }

    public Vector3 RandomNavMeshLocation()
    {
        Vector3 finalPosition = Vector3.zero;
        Vector3 randomPosition = Random.insideUnitSphere * walkRadius;
        randomPosition += transform.position;
        if(NavMesh.SamplePosition(randomPosition, out NavMeshHit hit, walkRadius, 1))
        {
            finalPosition = hit.position;
        }

        //Debug.Log(finalPosition);
        return finalPosition;
    }

    IEnumerator WaitForWander()
    {
        shouldWander = false;
        yield return new WaitForSeconds(Random.Range(1, 7));
        shouldWander = true;
        agent.SetDestination(RandomNavMeshLocation());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.name == "Terrain" && exportPrisoners != null && getOffBoat)
        {
            //print(collision.transform.name);
            speed = realSpeed;
            agent.speed = realSpeed;
            exportPrisoners.prisonersExported++;
            getOffBoat = false;
        }
    }

    void pause()
    {
        lastAgentVelocity = agent.velocity;
        lastAgentPath = agent.path;
        agent.velocity = Vector3.zero;
        agent.ResetPath();
    }

    public void resume()
    {
        agent.velocity = lastAgentVelocity;
        agent.SetPath(lastAgentPath);
    }

    private void LookAtPlayer()
    {
        var lookPos = player.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
    }
}
