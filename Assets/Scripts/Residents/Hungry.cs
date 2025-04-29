using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static Item;

public class Hungry : MonoBehaviour
{
    public GameObject tavern;
    public GameObject messhall;
    public Tavern tavernScript;
    public GameObject foodBar;
    public ResidentScheudle residentScheudle;

    public bool goingToFood;
    public bool goingToSit;
    public bool goingToEat;

    [Header("NavMeshStuff")]
    public NavMeshAgent agent;

    [Header("Animations")]
    public Animator animator;

    public ResidentTools residentTools;

    public Vector3 locationEntered;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        locationEntered = new Vector3(-100,-100,-100);

        residentScheudle = GetComponent<ResidentScheudle>();
    }

    void Start()
    {
        goingToFood = true;
        goingToSit = false;
        goingToEat = false;
        tavernScript = tavern.GetComponent<Tavern>();
    }

    // Update is called once per frame
    void Update()
    {
        if(messhall == null || tavern == null || tavern.GetComponent<IsABuilding>().beingMoved || messhall.GetComponent<IsABuilding>().beingMoved || tavernScript.sitting > tavernScript.maxSeats ||
            residentScheudle.shouldSleep || residentScheudle.shouldWork || residentScheudle.followPlayer || residentScheudle.isBeingTalkedTo)
        {
            Destroy(gameObject.GetComponent<Hungry>());
        }

        if (goingToFood && messhall != null)
        {
            agent.SetDestination(messhall.transform.position);
            if (Vector3.Distance(transform.position, messhall.transform.position) < messhall.GetComponent<IsABuilding>().distance)
            {
                Messhall messhallScript = messhall.GetComponent<Messhall>();
                //goingToEat = true;
                //goingToGetFood = false;
                agent.ResetPath();
                animator.SetBool("Holding", true);
                Item.ItemType itemShouldEat = messhallScript.GetItemInSlot(messhallScript.cookedFoodSlots);
                //print(itemShouldEat);
                messhallScript.RemoveItem(itemShouldEat, messhallScript.cookedFoodSlots);
                residentTools.ChangeEnable(ItemToInt(itemShouldEat), true);
                goingToFood = false;
                goingToSit = true;
            }
        }
        else if (goingToSit && tavern != null)
        {
            agent.SetDestination(tavern.transform.position);
            if (Vector3.Distance(transform.position, tavern.transform.position) < tavern.GetComponent<IsABuilding>().distance)
            {
                animator.SetBool("Holding", false);
                //goingToEat = false;
                agent.ResetPath();
                agent.enabled = false;
                animator.SetBool("Eating", true);
                //gameObject.AddComponent<Eating>().location = closestTavern;
                //StartCoroutine(Eat());
                goingToSit = false;
                goingToEat = true;
            }
        }
        else if (goingToEat && tavern != null)
        {
            locationEntered = transform.position;
            transform.position = tavernScript.seats[tavernScript.sitting].transform.position;
            transform.LookAt(tavern.transform);
            tavernScript.sitting++;
            StartCoroutine(Eat());
            goingToEat = false;
        }
    }
    public bool CheckIfHasFood(GameObject messhall)
    {
        Messhall building = messhall.GetComponent<Messhall>();

        foreach (InventorySlot slot in building.cookedFoodSlots)
        {
            if (slot.gameObject.transform.childCount != 0) return true;
        }

        return false;
    }

    IEnumerator Eat()
    {
        yield return new WaitForSeconds(5);
        animator.SetBool("Eating", false);
        foodBar.GetComponent<Image>().fillAmount += (float).3; //Hard coded how much food per food.  Gotta change it depending on food held. //also sometimes food doesnt go up?
        transform.GetComponent<ResidentStats>().Stats[1] += 30;

        Destroy(gameObject.GetComponent<Hungry>());
    }

    private int ItemToInt(Item.ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.charredBerry: return 6;
        }
    }

    private void OnDestroy()
    {
        StopCoroutine(Eat());
        residentTools.TurnOffAll();
        agent.enabled = true;

        if(locationEntered != new Vector3(-100, -100, -100))
        {
            transform.position = locationEntered;
        }

        animator.SetBool("Eating", false);
        animator.SetBool("Holding", false);
    }
}
