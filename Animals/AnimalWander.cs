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
    private GameObject Player;

    public float runDistance;
    public bool shouldRun;
    public bool shouldFollow;
    [Range(0, 100)] public float speed;
    [Range(0, 100)] public float runSpeed;
    [Range(1, 500)] public float walkRadius;

    private Vector3 location;
    private bool setLocation;
    private bool distanceRun;
    private bool distanceFollow;
    public bool isHurt;
    // Start is called before the first frame update
    void Start()
    {
        setLocation = true;
        animator = GetComponent<Animator>();
        Player = GameObject.Find("Main Character");

        agent = GetComponent<NavMeshAgent>();
        if(agent != null)
        {
            agent.speed = speed;
            location = RandomNavMeshLocation();
            agent.SetDestination(location);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(Player.transform.position, transform.position);

        if (agent != null && agent.remainingDistance <= agent.stoppingDistance && setLocation)
        {
            //StartCoroutine(StartMoving());
            agent.SetDestination(RandomNavMeshLocation());
            StartCoroutine(StartMoving());
            setLocation = false;
        }

        //Running away
        if(distance < runDistance && shouldRun && (isHurt || !Player.GetComponent<PlayerMovement>().crouching))
        {
            setLocation = false;
            distanceRun = true;
            agent.speed = runSpeed;
            agent.SetDestination(RunAway());
        }

        if (distanceRun && agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.speed = speed;
            setLocation = true;
            isHurt = false;
            distanceRun = false;
        }

        //Running to player
        if (distance < runDistance && shouldFollow && Player.GetComponent<PlayerHealth>().isAlive)
        {
            setLocation = false;
            distanceFollow = true;
            agent.speed = runSpeed;
            agent.SetDestination(Player.transform.position);
        }

        if (distanceFollow && distance > runDistance)
        {
            agent.speed = speed;
            setLocation = true;
            distanceFollow = false;
        }

        //Attacking player
        if (distanceFollow && distance < 5 && Player.GetComponent<PlayerHealth>().isAlive)
        {
            //DealDamage();
            animator.SetTrigger("Attack");
        }

        //Death Of player
        if (!Player.GetComponent<PlayerHealth>().isAlive)
        {
            agent.speed = speed;
            setLocation = true;
        }

        //Setting spped
        if (agent.speed == speed)
        {
            animator.SetFloat("Move", Mathf.Clamp(agent.velocity.magnitude, 0.0f, 0.5f));
        }
        else
        {
            animator.SetFloat("Move", Mathf.Clamp(agent.velocity.magnitude, 0.0f, 1.0f));
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

        return finalPosition;
    }

    public Vector3 RunAway()
    {
        Vector3 dirToPlayer = transform.position - Player.transform.position;
        Vector3 newPos = transform.position + dirToPlayer;

        return newPos;
    }

    public void DealDamage()
    {
        Player.GetComponent<PlayerHealth>().changeHealth(-25);
    }

    IEnumerator StartMoving()
    {
        float seconds = Random.Range(5, 15);
        yield return new WaitForSeconds(seconds);
        setLocation = true;
    }
}
