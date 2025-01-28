using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ResidentWander : MonoBehaviour
{
    [Header("GameObjects")]
    public LightingManager time;
    public GameObject home;
    public GameObject job;
    public GameObject player;

    [Header("NavMeshStuff")]
    public NavMeshAgent agent;
    Animator animator;
    public NavMeshPath lastAgentPath;
    public Vector3 lastAgentVelocity;

    [Header("Resident Movement")]
    [Range(0, 100)] public float speed;
    public float realSpeed;
    [Range(0, 500)] public float walkRadius;

    [Header("Conditions")]
    public bool shouldWander;

    public bool shouldGoHome;
    public bool shouldWork;
    public bool shouldDoNothing;

    public bool shouldRun;
    public bool BeingTalkedTo;

    private void Awake()
    {
        realSpeed = speed;
    }

    private void Start()
    {
        shouldWander = true;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = realSpeed;
        speed = agent.speed;
    }

    public void Update()
    {
        if (agent != null && agent.remainingDistance <= agent.stoppingDistance && shouldWander)
        {
            StartCoroutine(WaitForWander());
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
        else
        {
            //Go to places
            if (shouldWork)
            {
                agent.SetDestination(job.transform.position);
                shouldWork = false;
            }

            if (shouldGoHome)
            {
                agent.SetDestination(home.transform.position);
                shouldGoHome = false;
            }

            //telling resident where to go
            if (time.workTime && job != null && !shouldWork)
            {
                StopAllCoroutines();
                shouldWork = true;
                shouldDoNothing = false;
                shouldGoHome = false;
                shouldWander = false;
            }

            if (time.sleepTime && home != null && !shouldGoHome)
            {
                StopAllCoroutines();
                shouldWork = false;
                shouldDoNothing = false;
                shouldGoHome = true;
                shouldWander = false;
            }

            if (time.wanderTime && !shouldDoNothing)
            {
                StopAllCoroutines();
                shouldWork = false;
                shouldDoNothing = true;
                shouldGoHome = false;
                shouldWander = true;
                agent.SetDestination(RandomNavMeshLocation());
            }
        }

        //disapear at home
        //if (home != null) Disappear(home, time.sleepTime);
        //if (job != null) Disappear(job, time.workTime);
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

    public void SetDestination()
    {
        agent.SetDestination(RandomNavMeshLocation());
    }

    public void ResetSchedule()
    {
        shouldWork = false;
        shouldDoNothing = false;
        shouldGoHome = false;
        shouldWander = true;
        try { agent.SetDestination(RandomNavMeshLocation()); } catch { };
    }


    /*private void Disappear(GameObject location, bool time)
    {
        if (time && !BeingTalkedTo)
        {
            if (Vector3.Distance(transform.position, location.transform.position) < location.GetComponent<IsABuilding>().distance)
            {
                if (location.GetComponent<Job>() != null)
                {
                    location.GetComponent<Job>().WorkersWorking++;
                    //location.GetComponent<Job>().SendWorkers();
                    location.GetComponent<Job>().StatMultiplier += transform.GetComponent<ResidentStats>().Stats[3];
                    location.GetComponent<Job>().SendStats();
                }

                if (location.GetComponent<Tent>() != null && !location.GetComponent<Tent>().enableResidents) location.GetComponent<Tent>().enableResidents = true;
                if (location.GetComponent<Job>() != null && !location.GetComponent<Job>().enableWorkers) location.GetComponent<Job>().enableWorkers = true;
                gameObject.SetActive(false);
            }
        }
    }*/
}
