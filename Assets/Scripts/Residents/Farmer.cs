using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Farmer : MonoBehaviour
{
    public GameObject location;
    public Vector3 locationEntered;

    public Animator animator;
    public NavMeshAgent agent;
    public ResidentTools residentTools;

    private Job job;
    private ResidentScheudle residentScheudle;

    // Start is called before the first frame update
    void Start()
    {
        residentScheudle = transform.GetComponent<ResidentScheudle>();
        job = residentScheudle.job.GetComponent<Job>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        residentTools = GetComponent<ResidentTools>();

        locationEntered = transform.position;
        residentTools.TurnOffAll();
        residentTools.ChangeEnable(3, true);
        transform.position = getY(location.transform.position, location.GetComponent<Farm>().radius);

        transform.LookAt(location.transform.position);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

        animator.SetBool("Farming", true);
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

        residentScheudle.AtLocation = false;
        agent.enabled = true;
        transform.position = locationEntered;
        animator.SetBool("Farming", false);
        residentTools.TurnOffAll();
    }

    private Vector3 getY(Vector3 pos, int radius)
    {
        float x = pos.x + Random.Range(-radius, radius);
        float z = pos.z + Random.Range(-radius, radius);
        Vector3 newposition = new Vector3(x, pos.y + 10, z);

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(newposition, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
        {
            //Debug.DrawRay(newposition, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            return hit.point - new Vector3(0, 1, 0); //the subtraction of 1 makes the resident slightly lower
        }

        return Vector3.zero;
    }
}
