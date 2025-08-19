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

    public Item.ItemType heldFood;

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
        residentTools = GetComponent<ResidentTools>();
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
        // Check if hunger should be cancelled
        if (ShouldCancelHunger())
        {
            Destroy(GetComponent<Hungry>());
            return;
        }

        // Handle hunger states
        if (goingToFood)
        {
            HandleGoingToFood();
        }
        else if (goingToSit)
        {
            HandleGoingToSit();
        }
        else if (goingToEat)
        {
            HandleGoingToEat();
        }
    }

    private bool ShouldCancelHunger()
    {
        //might want to add something where if resident already grabbed the food from the messhall, he can still go to the tavern and eat 

        /*return messhall == null ||
               tavern == null ||
               tavern.GetComponent<IsABuilding>().beingMoved ||
               messhall.GetComponent<IsABuilding>().beingMoved ||
               tavernScript.sitting > tavernScript.maxSeats ||
               residentScheudle.followPlayer ||
               residentScheudle.isBeingTalkedTo ||
               (residentScheudle.shouldSleep && residentScheudle.home != null) ||
               (residentScheudle.shouldWork && residentScheudle.job != null);*/

        bool tavernProblem = tavern == null || (tavern != null && tavern.GetComponent<IsABuilding>().beingMoved) ||
                                 (tavernScript != null && tavernScript.sitting > tavernScript.maxSeats);

        bool messhallProblem = heldFood == ItemType.empty &&
                               (messhall == null || (messhall != null && messhall.GetComponent<IsABuilding>().beingMoved));

        bool residentProblem = residentScheudle.followPlayer ||
                               (residentScheudle.shouldSleep && residentScheudle.home != null) ||
                               (residentScheudle.shouldWork && residentScheudle.job != null);

        return tavernProblem || messhallProblem || residentProblem;
    }

    private void HandleGoingToFood()
    {
        agent.SetDestination(messhall.transform.position);

        if (Vector3.Distance(transform.position, messhall.transform.position) <
            messhall.GetComponent<IsABuilding>().distance)
        {
            Messhall messhallScript = messhall.GetComponent<Messhall>();
            agent.ResetPath();
            animator.SetBool("Holding", true);

            // Grab food
            Item.ItemType food = messhallScript.GetItemInSlot(messhallScript.cookedFoodSlots);
            heldFood = food;

            messhallScript.RemoveItem(heldFood, messhallScript.cookedFoodSlots);
            residentTools.ChangeEnable(ItemToInt(heldFood), true);

            goingToFood = false;
            goingToSit = true;
        }
    }

    private void HandleGoingToSit()
    {
        agent.SetDestination(tavern.transform.position);

        if (Vector3.Distance(transform.position, tavern.transform.position) <
            tavern.GetComponent<IsABuilding>().distance)
        {
            animator.SetBool("Holding", false);
            agent.ResetPath();
            agent.enabled = false;
            animator.SetBool("Eating", true);

            goingToSit = false;
            goingToEat = true;
        }
    }

    private void HandleGoingToEat()
    {
        locationEntered = transform.position;
        transform.position = tavernScript.seats[tavernScript.sitting].transform.position;
        transform.LookAt(tavern.transform);

        tavernScript.sitting++;
        StartCoroutine(Eat());

        goingToEat = false;
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

        transform.GetComponent<ResidentHunger>().ModifyHunger(FoodToItem(heldFood));
        transform.GetComponent<ResidentStats>().Stats[1] += FoodToItem(heldFood);

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

    private int FoodToItem(Item.ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.charredBerry: return 30;
        }
    }

    private void OnDestroy()
    {
        StopCoroutine(Eat());
        residentTools.TurnOffAll();

        if(locationEntered != new Vector3(-100, -100, -100))
        {
            transform.position = locationEntered;
        }

        agent.enabled = true;
        agent.ResetPath();

        animator.SetBool("Eating", false);
        animator.SetBool("Holding", false);
    }
}
