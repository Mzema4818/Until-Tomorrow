using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static Item;

public class Chef : MonoBehaviour
{
    public GameObject location;
    public Vector3 locationEntered;

    public Animator animator;
    public NavMeshAgent agent;
    public ResidentTools residentTools;
    public int maxHeldAmount;

    private Messhall messhall;
    public Item.ItemType item;
    public int amount;

    public bool Cooking;
    public bool WalkToFarm;
    public bool WalkToMesshall;

    public Job job;

    // Start is called before the first frame update
    void Start()
    {
        job = location.GetComponent<Job>();
        //later on do a stat check to see holding amount
        maxHeldAmount = 2;

        Cooking = true;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        residentTools = GetComponent<ResidentTools>();
        messhall = location.GetComponent<Messhall>();

        JoinJob();
    }

    private void Update()
    {
        //If farm exists and grown food isnt empty, and messhall has room for raw food, and both buildings arent being moved
        if(messhall.farm != null && !messhall.farm.GetComponent<IsABuilding>().beingMoved && messhall.farm.GetComponent<Farm>().CheckIfEmpty() && !messhall.CheckIfRawFoodIsEmpty())
        {
            if (Cooking)
            {
                LeaveJob();
                transform.position = locationEntered;

                WalkToFarm = true;
                Cooking = false;
            }

            if (WalkToFarm)
            {
                agent.SetDestination(messhall.farm.gameObject.transform.position);
                if (agent.remainingDistance < messhall.farm.GetComponent<IsABuilding>().distance)
                {
                    int num = 0;
                    Farm farm = messhall.farm.GetComponent<Farm>();
                    for (int i = 0; i < maxHeldAmount; i++)
                    {
                        if (!farm.RemoveItem(farm.grownItem)) break;
                        num++;
                    }

                    item = farm.grownItem;
                    amount = num;

                    residentTools.ChangeEnable(WhatItemIsHeld(item), true);

                    WalkToMesshall = true;
                    WalkToFarm = false;
                }
            }

            if (WalkToMesshall)
            {
                agent.SetDestination(messhall.gameObject.transform.position);

                if(agent.remainingDistance < messhall.GetComponent<IsABuilding>().distance)
                {
                    for (int i = 0; i < amount; i++)
                    {
                        messhall.AddItem(new Item { itemType = item }, messhall.rawFoodSlots);
                    }

                    item = ItemType.nothing;
                    amount = 0;

                    JoinJob();
                    WalkToMesshall = false;
                    Cooking = true;
                }
            }
        }
        else
        {
            if (!Cooking)
            {
                agent.SetDestination(messhall.gameObject.transform.position);

                if (agent.remainingDistance < messhall.GetComponent<IsABuilding>().distance)
                {
                    if (amount != 0 && item != Item.ItemType.nothing)
                    {
                        for (int i = 0; i < amount; i++)
                        {
                            messhall.AddItem(new Item { itemType = item }, messhall.rawFoodSlots);
                        }

                        item = ItemType.nothing;
                        amount = 0;
                    }

                    JoinJob();
                    WalkToFarm = false;
                    WalkToMesshall = false;
                    Cooking = true;
                }
            }
        }
    }

    private void JoinJob()
    {
        locationEntered = transform.position;

        residentTools.TurnOffAll();
        residentTools.ChangeEnable(5, true);
        agent.ResetPath();
        agent.velocity = Vector3.zero;
        agent.enabled = false;
        animator.SetBool("Cooking", true);

        transform.position = messhall.walkable.transform.position;
        transform.LookAt(location.transform.position);
    }

    private void LeaveJob()
    {
        agent.enabled = true;
        animator.SetBool("Cooking", false);
        residentTools.TurnOffAll();
    }

    private void OnDestroy()
    {
        try
        {
            job.workersWorking--;
            job.statMultiplier -= transform.GetComponent<ResidentStats>().Stats[3];
        }
        catch { };

        if (Cooking) transform.position = locationEntered;
        transform.GetComponent<ResidentScheudle>().AtLocation = false;
        LeaveJob();
    }

    private int WhatItemIsHeld(Item.ItemType itemType)
    {
        //the 6 in this case is what array number the berry is in
        switch (itemType)
        {
            default:
            case ItemType.berry: return 6;
        }
    }

}
