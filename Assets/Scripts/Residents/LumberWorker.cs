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
        //print(agent.remainingDistance);

        if (!IsLumbermillValid()) return;

        if (ShouldFindTree)
        {
            HandleFindTree();
        }
        else if (ShouldGoToTree)
        {
            HandleGoToTree();
        }
        else if (ShouldChopTree)
        {
            HandleChopTree();
        }
        else if (ShouldDropOffTree)
        {
            HandleDropOffTree();
        }
    }

    bool IsLumbermillValid()
    {
        try
        {
            if (lumbermill.gameObject.GetComponent<IsABuilding>().beingMoved)
            {
                Destroy(this);
                return false;
            }
        }
        catch
        {
            Destroy(this);
            return false;
        }
        return true;
    }

    void HandleFindTree()
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

    void HandleGoToTree()
    {
        try
        {
            agent.SetDestination(Tree.transform.position);

            if (agent.remainingDistance > 0 && agent.remainingDistance < 5)
            {
                ShouldGoToTree = false;
                ShouldChopTree = true;
                agent.ResetPath();
            }
        }
        catch
        {
            ResetTreeSearch();
        }
    }

    void HandleChopTree()
    {
        try
        {
            agent.isStopped = true;
            LookAtTree();
            animator.SetBool("Chopping", true);
        }
        catch
        {
            agent.isStopped = false;
            animator.SetBool("Chopping", false);
            ResetTreeSearch();
        }
    }

    void HandleDropOffTree()
    {
        agent.SetDestination(lumbermill.gameObject.transform.position);

        if (agent.remainingDistance > 0 && agent.remainingDistance < lumbermill.GetComponent<IsABuilding>().distance)
        {
            ShouldDropOffTree = false;
            lumbermill.GatheringResources(GetComponent<ResidentStats>());
            ShouldFindTree = true;
            var tools = GetComponent<ResidentTools>();
            tools.ChangeEnable(2, false);
            tools.ChangeEnable(0, true);
            animator.SetBool("Holding", false);
        }
    }

    void ResetTreeSearch()
    {
        Tree = null;
        ShouldGoToTree = false;
        ShouldChopTree = false;
        ShouldFindTree = true;
    }

    private void OnDestroy()
    {
        //it removes stats[3] because this job replies on strength stat
        //lumbermill.workersActive--;
        //lumbermill.statMultiplier -= transform.GetComponent<ResidentStats>().Stats[3];

        agent.isStopped = false;
        transform.GetComponent<ResidentScheudle>().AtLocation = false;
        if (agent.isOnNavMesh) agent.ResetPath();
        animator.SetBool("Chopping", false);
        animator.SetBool("Holding", false);
        residentTools.TurnOffAll();
    }

    private void GetPosition(Vector3 pos)
    {
        int layerMask = 1 << 8;

        // Raycast straight down from above the position to avoid intersection issues
        Vector3 rayStart = pos + Vector3.up * 10f;

        if (Physics.Raycast(rayStart, Vector3.down, out RaycastHit hit, 20f, layerMask))
        {
            TreeLocation = hit.point;
            TreeRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
        }
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
