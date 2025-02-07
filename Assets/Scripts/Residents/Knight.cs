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
    public FieldOfView fieldOfView;
    public Job job;

    private AnimationEvents AnimationEvents;
    private KnightHut knightHut;
    private ResidentScheudle residentScheudle;

    private Transform targetTransform;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        knightHut = location.GetComponent<KnightHut>();
        residentScheudle = transform.GetComponent<ResidentScheudle>();
        job = residentScheudle.job.GetComponent<Job>();
        residentTools = GetComponent<ResidentTools>();
        //AnimationEvents = residentTools.heldItems[8].GetComponent<AnimationEvents>();

        locationEntered = transform.position;
        agent.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
