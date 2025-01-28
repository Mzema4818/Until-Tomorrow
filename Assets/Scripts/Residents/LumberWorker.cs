using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LumberWorker : MonoBehaviour
{
    [Header("Scripts")]
    public NavMeshAgent agent;
    public GameObject location;
    public ResidentTools residentTools;

    [Header("Animations")]
    public Animator animator;

    [Header("Tree Stuff")]
    public GameObject Tree;
    public Vector3 TreeLocation;
    public Quaternion TreeRotation;
    public Lumbermill lumbermill;

    [Header("Schedule Conditions")]
    public bool ShouldFindTree;
    public bool ShouldGoToTree;
    public bool ShouldChopTree;
    public bool ShouldDropOffTree;

    void Start()
    {
        if(lumbermill == null) lumbermill = location.GetComponent<Lumbermill>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        residentTools = GetComponent<ResidentTools>();

        residentTools.ChangeEnable(0, true);
        ShouldFindTree = true;
    }

    void Update()
    {
        try
        {
            if (lumbermill.gameObject.GetComponent<IsABuilding>().beingMoved)
            {
                Destroy(this);
            }
        }
        catch
        {
            Destroy(this);
        }

        if (ShouldFindTree)
        {
            if (Tree == null)
            {
                Tree = lumbermill.GetClosestTree();
                agent.SetDestination(lumbermill.gameObject.transform.position);
            }
            else
            {
                GetPosition(Tree.transform.position);
                agent.ResetPath();
                ShouldFindTree = false;
                ShouldGoToTree = true;
            }
        }
        else if (ShouldGoToTree)
        {
            try
            {
                agent.SetDestination(Tree.transform.position);

                if (agent.remainingDistance != 0 && agent.remainingDistance < 5)
                {
                    ShouldGoToTree = false;
                    ShouldChopTree = true;
                    agent.ResetPath();
                }
            }
            catch
            {
                ShouldGoToTree = false;
                ShouldFindTree = true;
                Tree = null;
            }
        }
        else if (ShouldChopTree)
        {
            try
            {
                LookAtTree();
                animator.SetBool("Chopping", true);
            }
            catch
            {
                animator.SetBool("Chopping", false);
                ShouldChopTree = false;
                ShouldFindTree = true;
                Tree = null;
            }
        }
        else if (ShouldDropOffTree)
        {
            agent.SetDestination(lumbermill.gameObject.transform.position);

            if (agent.remainingDistance != 0 && agent.remainingDistance < lumbermill.GetComponent<IsABuilding>().distance)
            {
                ShouldDropOffTree = false;
                lumbermill.GatheringResources(transform.GetComponent<ResidentStats>()); //go to lumbermill script to change, rn its getting strength stat to see how many resources it gains;
                ShouldFindTree = true;
                transform.GetComponent<ResidentTools>().ChangeEnable(2, false);
                transform.GetComponent<ResidentTools>().ChangeEnable(0, true);
                animator.SetBool("Holding", false);
            }
        }
    }

    private void OnDestroy()
    {
        //it removes stats[3] because this job replies on strength stat
        //lumbermill.workersActive--;
        //lumbermill.statMultiplier -= transform.GetComponent<ResidentStats>().Stats[3];

        transform.GetComponent<ResidentScheudle>().AtLocation = false;
        if (agent.isOnNavMesh) agent.ResetPath();
        animator.SetBool("Chopping", false);
        animator.SetBool("Holding", false);
        residentTools.TurnOffAll();
    }

    private void GetPosition(Vector3 pos)
    {
        /*Vector3 newposition = new Vector3(pos.x, pos.y + 10, pos.z);

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(newposition, transform.TransformDirection(Vector3.down), out hit, 10, 8))
        {
            //Debug.DrawRay(newposition, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            Debug.DrawRay(newposition, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
            //return hit.point - new Vector3(0, 0, 0); //the subtraction of 1 makes the resident slightly lower
        }
        else
        {
            Debug.DrawRay(newposition, transform.TransformDirection(Vector3.down) * 10, Color.white);
            Debug.Log("Did not Hit");
        }

        return Vector3.zero;*/


        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        //layerMask = ~layerMask;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(pos, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask))
        {
            //Debug.DrawRay(pos, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            TreeLocation = hit.point;
            TreeRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            //Debug.Log("Did Hit");
        }
       // else
       // {
           // Debug.DrawRay(pos, transform.TransformDirection(Vector3.down) * 1000, Color.white);
           // Debug.Log("Did not Hit");
        //}
    }

    private void LookAtTree()
    {
        var lookPos = Tree.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
    }

    private void CheckPath(Vector3 Target)
    {
        //create empty path
        NavMeshPath navMeshPath = new NavMeshPath();
        //create path and check if it can be done
        // and check if navMeshAgent can reach its target
        if (agent.CalculatePath(Target, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete)
        {
            //move to target
            //agent.SetPath(navMeshPath);
        }
        else
        {
            //Fail condition here
            Tree = null;
        }
    }
}
