using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Archer : MonoBehaviour
{
    public GameObject location;
    public Vector3 locationEntered;

    public Animator animator;
    public NavMeshAgent agent;
    public ResidentTools residentTools;
    public FieldOfView fieldOfView;
    public Job job;

    private AnimationEvents AnimationEvents;
    private ArcherTower archerTower;
    private ResidentScheudle residentScheudle;

    private Transform targetTransform;

    // Start is called before the first frame update
    void Start()
    {
        fieldOfView = GetComponent<FieldOfView>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        archerTower = location.GetComponent<ArcherTower>();
        residentScheudle = transform.GetComponent<ResidentScheudle>();
        job = residentScheudle.job.GetComponent<Job>();
        residentTools = GetComponent<ResidentTools>();
        residentTools.heldItems[8].SetActive(true);

        fieldOfView.enabled = true;
        locationEntered = transform.position;
        agent.enabled = false;
        transform.position = archerTower.placeToStand.transform.position;

        residentTools.TurnOffAll();
        residentTools.ChangeEnable(7, true);
        
    }

    // Update is called once per frame
    void Update()
    {
        /*try
        {
            if (location.GetComponent<IsABuilding>().beingMoved)
            {
                Destroy(this);
            }
        }
        catch
        {
            Destroy(this);
        }*/

        if (fieldOfView.canSeePlayer)
        {
            if (targetTransform == null || targetTransform != fieldOfView.objectSeen.transform)
            {
                if (fieldOfView.objectSeen == null) return;
                targetTransform = fieldOfView.objectSeen.transform;
            }

            Vector3 targetPosition = new Vector3(targetTransform.position.x, transform.position.y, targetTransform.position.z);
            transform.LookAt(targetPosition);

            if (!animator.GetBool("Shooting"))
            {
                animator.SetBool("Shooting", true);
            }

            AnimationEvents.location = targetTransform.gameObject;
        }
        else
        {
            if (animator.GetBool("Shooting"))
            {
                animator.SetBool("Shooting", false);
            }
        }
    }

    private void OnDestroy()
    {
        //it removes stats[3] because this job replies on strength stat

        try
        {
            job.workersWorking--;
            job.statMultiplier -= transform.GetComponent<ResidentStats>().Stats[3];
        }
        catch { };

        fieldOfView.enabled = false;
        residentScheudle.AtLocation = false;
        transform.position = locationEntered;
        agent.enabled = true;
        animator.SetBool("Shooting", false);
        residentTools.TurnOffAll();
    }
}
