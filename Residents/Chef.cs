using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chef : MonoBehaviour
{
    public GameObject location;
    public Vector3 locationEntered;

    public Animator animator;
    public NavMeshAgent agent;
    public ResidentTools residentTools;
    public int HeldAmount;

    private Messhall messhall;
    private Item.ItemType item;
    private int amount;

    public bool Cooking;
    public bool WalkToFarm;
    public bool WalkToMesshall;

    // Start is called before the first frame update
    void Start()
    {
        //later on do a stat check to see holding amount
        HeldAmount = 2;

        Cooking = true;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        residentTools = GetComponent<ResidentTools>();
        messhall = location.GetComponent<Messhall>();

        locationEntered = transform.position;
        residentTools.TurnOffAll();
        residentTools.ChangeEnable(5, true);
        transform.position = getY(messhall.walkable.transform.position);
        //transform.rotation = getYRot(messhall.walkable.transform.position); //when i find a good spot add this in to test
        //transform.rotation = messhall.transform.rotation;

        transform.LookAt(location.transform.position);
        transform.eulerAngles = new Vector3(messhall.transform.rotation.x, transform.eulerAngles.y, messhall.transform.rotation.z);
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            if (location.GetComponent<IsABuilding>().beingMoved)
            {
                if (!Cooking) agent.ResetPath();
                Destroy(this);
            }
        }
        catch
        {
            if (!Cooking)
            {
                print("reset path");
                agent.ResetPath();
            }
            Destroy(this);
        }

        if (messhall.farm != null && messhall.farm.GetComponent<Farm>().CheckIfEmpty() && !messhall.CheckIfRawFoodIsEmpty() && !messhall.farm.GetComponent<IsABuilding>().beingMoved && Cooking)
        {
            LeaveJob();
            transform.position = locationEntered;
            Cooking = false;
            WalkToFarm = true;
        }

        if (WalkToFarm )
        {
            if (messhall.farm != null && !messhall.farm.GetComponent<IsABuilding>().beingMoved && messhall != null && !location.GetComponent<IsABuilding>().beingMoved)
            {
                agent.SetDestination(messhall.farm.gameObject.transform.position);

                if (agent.remainingDistance != 0 && agent.remainingDistance < messhall.farm.GetComponent<IsABuilding>().distance)
                {
                    Farm farm = messhall.farm.GetComponent<Farm>();

                    WalkToFarm = false;
                    WalkToMesshall = true;

                    for (int i = 0; i < HeldAmount; i++) farm.RemoveItem(farm.grownItem);

                    item = farm.grownItem;
                    amount = HeldAmount;
                }
            }
            else
            {
                WalkToFarm = false;
                WalkToMesshall = true;
            }
        }

        if (WalkToMesshall && messhall != null && !location.GetComponent<IsABuilding>().beingMoved)
        {
            agent.SetDestination(messhall.gameObject.transform.position);

            if (agent.remainingDistance != 0 && agent.remainingDistance < messhall.GetComponent<IsABuilding>().distance)
            {
                WalkToMesshall = false;
                JoinJob();

                for (int i = 0; i < HeldAmount; i++) messhall.AddItem(new Item { itemType = item }, messhall.rawFoodSlots);

                item = Item.ItemType.nothing;
                amount = 0;

                Cooking = true;
            }
        }

        /*try
        {
            if (location.GetComponent<IsABuilding>().beingMoved)
            {
                Destroy(this);
            }

            if (messhall.farm.GetComponent<IsABuilding>().beingMoved && WalkToFarm)
            {
                WalkToFarm = false;
                WalkToMesshall = true;
            }
        }
        catch
        {
            Destroy(this);
        }

        //print(messhall.farm.GetComponent<Farm>().CheckIfEmpty());

        if (messhall.farm != null && messhall.farm.GetComponent<Farm>().CheckIfEmpty() && !messhall.CheckIfRawFoodIsEmpty() && !messhall.farm.GetComponent<IsABuilding>().beingMoved && Cooking)
        {
            LeaveJob();
            Cooking = false;
            WalkToFarm = true;
        }

        if (WalkToFarm)
        {
            try
            {
                agent.SetDestination(messhall.farm.gameObject.transform.position);

                if (agent.remainingDistance != 0 && agent.remainingDistance < messhall.farm.GetComponent<IsABuilding>().distance)
                {
                    Farm farm = messhall.farm.GetComponent<Farm>();

                    WalkToFarm = false;
                    WalkToMesshall = true;

                    for (int i = 0; i < HeldAmount; i++) farm.RemoveItem(farm.grownItem);

                    item = farm.grownItem;
                    amount = HeldAmount;
                }
            }
            catch
            {
                WalkToFarm = false;
                WalkToMesshall = true;
            }
        }

        if (WalkToMesshall)
        {
            agent.SetDestination(messhall.gameObject.transform.position);

            if (agent.remainingDistance != 0 && agent.remainingDistance < messhall.GetComponent<IsABuilding>().distance)
            {
                WalkToMesshall = false;
                JoinJob();

                for (int i = 0; i < HeldAmount; i++) messhall.AddItem(new Item { itemType = item }, messhall.rawFoodSlots);

                item = Item.ItemType.nothing;
                amount = 0;

                Cooking = true;
            }
        }*/
    }

    private void OnDestroy()
    {
        /*if (Cooking)
        {
            LeaveJob();
            transform.GetComponent<ResidentScheudle>().AtLocation = false;
        }
        else
        {
            agent.ResetPath();
            transform.GetComponent<ResidentScheudle>().AtLocation = false;
        }*/

        try
        {
            location.GetComponent<Farm>().workersActive--;
            location.GetComponent<Farm>().statMultiplier -= transform.GetComponent<ResidentStats>().Stats[3];
        }
        catch { };

        if(Cooking) transform.position = locationEntered;
        transform.GetComponent<ResidentScheudle>().AtLocation = false;
        LeaveJob();

    }

    private void LeaveJob()
    {
        agent.enabled = true;
        animator.SetBool("Cooking", false);
        residentTools.TurnOffAll();
    }

    private void JoinJob()
    {
        agent.ResetPath();
        agent.enabled = false;
        animator.SetBool("Cooking", true);
        locationEntered = transform.position;
        residentTools.TurnOffAll();
        residentTools.ChangeEnable(5, true);
        transform.position = getY(messhall.walkable.transform.position);

        //transform.LookAt(location.transform.position);
        //transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        //transform.eulerAngles = new Vector3(transform.eulerAngles.x - messhall.transform.eulerAngles.x, transform.eulerAngles.x, transform.eulerAngles.z);

        messhall.workersActive += 1;
        messhall.statMultiplier += transform.GetComponent<ResidentStats>().Stats[3];
    }

    private Vector3 getY(Vector3 pos)
    {

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(pos, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
        {
            //Debug.DrawRay(newposition, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            return hit.point - new Vector3(0, 0, 0); //the subtraction of 1 makes the resident slightly lower
        }

        return Vector3.zero;
    }

    private Quaternion getYRot(Vector3 pos)
    {

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(pos, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
        {
            //Debug.DrawRay(newposition, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            return Quaternion.FromToRotation(Vector3.up, hit.normal); //the subtraction of 1 makes the resident slightly lower
        }
        return Quaternion.identity;
    }
}
