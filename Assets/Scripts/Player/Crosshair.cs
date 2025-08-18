using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    public RaycastHit hit;
    public float distance;

    public GameObject CharacterStuff;
    public Builder builder;
    public StoryManager storyManager;
    public GameObject buildEditor;
    public GameObject menuStats;
    public GameObject builderHealth;
    public GameObject[] menuNames;
    public GameObject[] menuObjects;
    public OpenMenus openMenus;
    public GameObject hammer;
    public GameObject axe;
    public GameObject pickaxe;
    public GameObject exclamation;
    public GameObject[] collectables;
    public GameObject[] HotBarItems;
    public GameObject[] HotBar;
    public GameObject ResidentTextBox;
    public GameObject ResidentTextChoices;
    public GameObject ChestInventory;
    public GameObject MarksObject;
    public GameObject mainMenu;
    public bool canHit;
    public LayerMask IgnoreMe;
    public LayerMask RayCastBlocker;
    public JobCamera jobCamera;
    public Book book;

    public TextMeshProUGUI ResidentNames;
    public TextMeshProUGUI ResidentNamesBar;
    public TextMeshProUGUI JobNames;
    public TextMeshProUGUI storage;
    public GameObject JobNamesBar;

    public TextMeshProUGUI stone;
    public TextMeshProUGUI wood;
    public TextMeshProUGUI mushroom;
    public TextMeshProUGUI flower;

    public GameObject[] canvasItems;
    private List<GameObject> canvasObjects = new List<GameObject>();
    private bool[] canvasObjectsActive;

    public InventoryManager inventoryManager;
    public GameObject inventoryObject;
    public GameObject toolBar;
    public bool chestOpen;
    public bool messHallOpen;
    public bool farmOpen;
    public bool mineOpen;
    public bool lumberMillOpen;

    public PlayerInteractions playerInteractions;

    [Header("Job Stuff")]
    public BuildMenuUpdater buildMenuUpdater;
    public TextMeshProUGUI Announcement;

    [Header("Sounds")]
    public AudioSource audioSource;
    public AudioClip openingBuilding;
    public AudioClip buildingInventory;
    //public AudioSource pickup;
    //public AudioSource openingBuilding;
    //public AudioSource openingBuildInventory;

    [Header("Building Health")]
    public BuildingHealth buildingHealth;
    public HealthBar healthBar;
    public TextMeshProUGUI repairCosts;

    //Start is called before the first frame update
    void Start()
    {
        audioSource = transform.parent.parent.GetComponent<AudioSource>();

        for (int i = 0; i < canvasItems.Length; i++)
        {
            for (int i2 = 0; i2 < canvasItems[i].transform.childCount; i2++)
            {
                canvasObjects.Add(canvasItems[i].transform.GetChild(i2).gameObject);
            }
        }
        canvasObjectsActive = new bool[canvasObjects.Count];
    }

    //Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, distance * 2, ~IgnoreMe))
        {
            if (hit.collider.gameObject.layer == 20) return; //the ignoreraycast layer

            TouchingObjects();
            OpeningMenus();
        }
    }

    public void TouchingObjects()
    {
        if(hit.distance <= distance && CheckIfAllTrue() && !mainMenu.activeSelf)
        {
            //If user clicks mouse 0 
            if (Input.GetMouseButtonDown(0))
            {
                Transform parent = hit.collider.transform.parent;
                Transform gameObject = hit.collider.transform;

                if (parent != null && playerInteractions.residentTalkingTo == null)
                {
                    //opening buildings
                    if (parent.GetComponent<IsABuilding>() != null && !playerInteractions.residentFollowing && !playerInteractions.assign)
                    {
                        IsABuilding isABuilding = parent.GetComponent<IsABuilding>();

                        //If there's already a building open, close its actions
                        if (builder.buildingData != null && builder.buildingData != parent.gameObject)
                        {
                            IsABuilding previousBuilding = builder.buildingData.GetComponent<IsABuilding>();
                            if (previousBuilding != null) previousBuilding.actions.SetActive(false);
                        }

                        //If we're clicking the same building again, toggle it off
                        if (builder.buildingData == parent.gameObject && isABuilding.actions.activeSelf)
                        {
                            isABuilding.actions.SetActive(false);
                            builder.buildingData = null;
                            audioSource.PlayOneShot(openingBuilding);
                            return;
                        }

                        //Set the new building and activate
                        builder.buildingData = parent.gameObject;
                        buildingHealth = hit.collider.transform.parent.GetComponent<BuildingHealth>();
                        builder.buildingHealth = buildingHealth;

                        //isABuilding.SetPosition();
                        isABuilding.actions.SetActive(true);
                        audioSource.PlayOneShot(openingBuilding);
                    }
                }

                if (parent != null && playerInteractions.residentTalkingTo != null)
                {
                    //Assigning farm
                    if (parent.GetComponent<Farm>() && playerInteractions.assign)
                    {
                        ResidentScheudle residentScheudle = playerInteractions.resident.GetComponent<ResidentScheudle>();
                        Messhall messhall = residentScheudle.job.GetComponent<Messhall>();
                        messhall.farm = parent.gameObject;
                        parent.GetComponent<Farm>().messhall = messhall;

                        playerInteractions.residentText.text = "Okay perfect I will collect food from here now";
                        playerInteractions.resident.GetComponent<BoxCollider>().enabled = true;
                        residentScheudle.isBeingTalkedTo = true;
                        residentScheudle.followPlayer = false;
                        playerInteractions.resident.GetComponent<NavMeshAgent>().stoppingDistance = 0;

                        playerInteractions.resident = null;
                        playerInteractions.residentText = null;
                        playerInteractions.assign = false;
                        playerInteractions.resident = null;

                        messhall.GetComponent<IsABuilding>().actions.GetComponent<BuildingMenuUpdater>().farmCheck.text = "Farm: yes";
                    }

                    //While resident is following player
                    if (playerInteractions.residentFollowing)
                    {
                        //Assigning a job or home to resident
                        if (playerInteractions.resident.GetComponent<ResidentScheudle>().followPlayer)
                        {
                            Job job = parent.GetComponent<Job>();

                            //Check if job was selected
                            if (job != null && (job.Workers < job.MaxWorkers))
                            {
                                SelectButton(playerInteractions.resident, parent.gameObject);
                                job.Workers++;
                                job.WorkersActive[job.Workers - 1] = playerInteractions.resident;

                                playerInteractions.residentText.text = "Thanks! I will work here from now on.";
                                playerInteractions.resident.GetComponent<StatBar>().UpdateJob();
                                parent.GetComponent<IsABuilding>().actions.GetComponent<BuildingMenuUpdater>().ChangeResidents();
                            }
                            else
                            {
                                playerInteractions.residentText.text = "I can't do anything with this, can I go?";
                            }

                            playerInteractions.resident.GetComponent<BoxCollider>().enabled = true;
                            playerInteractions.resident.GetComponent<ResidentScheudle>().isBeingTalkedTo = true;
                            playerInteractions.resident.GetComponent<ResidentScheudle>().followPlayer = false;
                            playerInteractions.resident.GetComponent<NavMeshAgent>().stoppingDistance = 0;

                            playerInteractions.residentFollowing = false;
                            playerInteractions.resident = null;
                            playerInteractions.residentText = null;
                        }
                        else if (playerInteractions.resident.GetComponent<ResidentScheudle>().followPlayerHome)
                        {
                            print("selected home");
                            Tent tent = parent.GetComponent<Tent>();

                            //Check if job was selected
                            if (tent != null && (tent.Residents < tent.MaxResidents))
                            {
                                tent.Residents++;
                                tent.ResidentsActive[tent.Residents - 1] = playerInteractions.resident;
                                playerInteractions.resident.GetComponent<ResidentScheudle>().home = parent.gameObject;

                                playerInteractions.residentText.text = "Thanks! I will sleep here from now on.";
                                playerInteractions.resident.GetComponent<StatBar>().UpdateHome();
                                parent.GetComponent<IsABuilding>().actions.GetComponent<BuildingMenuUpdater>().ChangeResidentsTent();
                            }
                            else
                            {
                                playerInteractions.residentText.text = "I can't do anything with this, can I go?";
                            }

                            playerInteractions.resident.GetComponent<BoxCollider>().enabled = true;
                            playerInteractions.resident.GetComponent<ResidentScheudle>().isBeingTalkedTo = true;
                            playerInteractions.resident.GetComponent<ResidentScheudle>().followPlayerHome = false;
                            playerInteractions.resident.GetComponent<NavMeshAgent>().stoppingDistance = 0;

                            playerInteractions.residentFollowing = false;
                            playerInteractions.resident = null;
                            playerInteractions.residentText = null;
                        }
                    }
                }

                if (gameObject != null)
                {
                    //user touches a collectable
                    if (gameObject.GetComponent<IsACollectable>() != null)
                    {
                        hit.collider.transform.gameObject.SetActive(false);
                        exclamation.SetActive(true);

                        if (gameObject.name == "Opening Letter")
                        {
                            collectables[0].SetActive(true);
                        }
                    }

                    //User touches a tool
                    if (gameObject.GetComponent<IsATool>() != null)
                    {
                        hit.collider.transform.gameObject.SetActive(false);

                        gameObject.GetComponent<IsATool>().AddItem();

                        storyManager.toolsCollected++;
                        //Once all tools are collected
                        if (storyManager.toolsCollected == 4)
                        {
                            storyManager.CanSpawnShips = true;
                            storyManager.SpawnShipDay = storyManager.lightingManager.numberOfDays + 1;
                        }

                        //storyManager.CheckSpawnShips = true;
                        //pickup.Play();
                    }

                    //User talks to a resident
                    if (gameObject.GetComponent<ResidentStats>() != null && !gameObject.GetComponent<ResidentScheudle>().recentlyAttacked)
                    {
                        //Turn off all menus of the resident, if you are were just talking to one
                        if (playerInteractions.residentTalkingTo != null)
                        {
                            ResidentStats resident = playerInteractions.residentTalkingTo.GetComponent<ResidentStats>();
                            resident.textBox.SetActive(false);
                            resident.schedule.SetActive(false);
                            resident.StatObject.SetActive(false);
                            //playerInteractions.residentTalkingTo.GetComponent<StatBar>().starbar.transform.parent.gameObject.SetActive(false);
                        }

                        playerInteractions.residentTalkingTo = hit.collider.gameObject;
                        hit.collider.GetComponent<ResidentScheudle>().isBeingTalkedTo = true;

                        //stops having 2 residents following you at once, after you tell one to follow you, if one is following you, stop that first
                        if (playerInteractions.resident != null)
                        {
                            playerInteractions.resident.GetComponent<ResidentScheudle>().followPlayer = false;
                            playerInteractions.resident.GetComponent<ResidentScheudle>().isBeingTalkedTo = false;
                            playerInteractions.resident.GetComponent<NavMeshAgent>().stoppingDistance = 0;
                            playerInteractions.resident.GetComponent<BoxCollider>().enabled = true;

                            playerInteractions.resident = null;
                            playerInteractions.residentFollowing = false;
                            playerInteractions.residentText = null;
                        }

                        GameObject textBox = hit.collider.GetComponent<ResidentStats>().textBox;
                        if (!textBox.activeSelf) textBox.SetActive(true);
                        
                    }
                }

            }

            //If user clicks F
            if (Input.GetKeyDown(KeyCode.F))
            {
                if(hit.collider.transform.parent != null || hit.collider.transform != null)
                {
                    Transform GameObject = hit.collider.transform;
                    Transform parent = hit.collider.transform.parent;
                    PickUp pickUp = null;

                    if (parent.GetComponent<PickUp>() != null) pickUp = parent.GetComponent<PickUp>();
                    else if (GameObject.GetComponent<PickUp>() != null) pickUp = GameObject.GetComponent<PickUp>();

                    //Pickupables 
                    if (pickUp != null)
                    {
                        if (pickUp.name.Contains("mushroom"))
                        {
                            inventoryManager.AddItem(new Item { itemType = Item.ItemType.mushroom });
                            Destroy(hit.collider.transform.parent.gameObject);
                        }
                        else if (pickUp.name.Contains("flower"))
                        {
                            inventoryManager.AddItem(new Item { itemType = Item.ItemType.flower });
                            Destroy(hit.collider.transform.parent.gameObject);
                        }
                        else if (pickUp.name.Contains("rock"))
                        {
                            inventoryManager.AddItem(new Item { itemType = Item.ItemType.rock });
                            Destroy(hit.collider.transform.gameObject);
                        }
                        else if (pickUp.name.Contains("BerryBush"))
                        {
                            GameObject berries = pickUp.transform.Find("Berries").gameObject;

                            if (berries.activeSelf)
                            {
                                inventoryManager.AddItem(new Item { itemType = Item.ItemType.berry });
                                berries.SetActive(false);
                            }
                            else
                            {
                                inventoryManager.AddItem(new Item { itemType = Item.ItemType.bush });
                                Destroy(hit.collider.transform.parent.gameObject);
                            }
                        }
                        else if (pickUp.name.Contains("sapling"))
                        {
                            inventoryManager.AddItem(new Item { itemType = Item.ItemType.sapling });
                            Destroy(hit.collider.transform.parent.gameObject);
                        }

                        //pickup.Play();
                    }

                    //Open gravestone
                    if (parent.GetComponent<Gravestone>() != null)
                    {
                        parent.GetComponent<Gravestone>().ReturnInventory();
                        Destroy(hit.collider.transform.parent.gameObject);
                    }
                }
            }
        }
    }

    public void OpeningMenus()
    {
        if (hit.distance <= distance && (CheckIfAllTrue() || openMenus.ChestInventory.activeSelf) && !mainMenu.activeSelf && playerInteractions.residentTalkingTo == null)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (hit.collider.transform.parent != null || hit.collider.transform != null)
                {
                    Transform parent = hit.collider.transform.parent;

                    //Open chest
                    if(parent.GetComponent<Chest>() != null && !chestOpen)
                    {
                        BasicOpenMenu(ref chestOpen);

                        ChestInventory.GetComponent<WhatInventory>().inventoryOpen1 = parent.GetComponent<Chest>().inventory;
                        parent.GetComponent<Chest>().inventory.SetActive(true);
                    }
                    else if (chestOpen)
                    {
                        BasicCloseMenu(ref chestOpen);
                    }

                    //Open messhall
                    if (parent.GetComponent<Messhall>() != null && !messHallOpen)
                    {
                        BasicOpenMenu(ref messHallOpen);

                        Messhall messhall = parent.GetComponent<Messhall>();
                        ChestInventory.GetComponent<WhatInventory>().inventoryOpen1 = messhall.rawFood;
                        ChestInventory.GetComponent<WhatInventory>().inventoryOpen2 = messhall.cookedFood;
                        messhall.rawFood.SetActive(true);
                        messhall.cookedFood.SetActive(true);
                    }
                    else if (messHallOpen)
                    {
                        BasicCloseMenu(ref messHallOpen);
                    }

                    //Open farm
                    if (parent.GetComponent<Farm>() != null && !playerInteractions.assign && !farmOpen)
                    {
                        BasicOpenMenu(ref farmOpen);

                        Farm farm = parent.GetComponent<Farm>();
                        ChestInventory.GetComponent<WhatInventory>().inventoryOpen1 = farm.inventory;
                        farm.inventory.SetActive(true);
                    }
                    else if (farmOpen)
                    {
                        BasicCloseMenu(ref farmOpen);
                    }

                    //Open mine
                    if (parent.GetComponent<Mine>() != null && !mineOpen)
                    {
                        BasicOpenMenu(ref mineOpen);
                        Mine mine = parent.GetComponent<Mine>();
                        ChestInventory.GetComponent<WhatInventory>().inventoryOpen1 = mine.inventory;
                        mine.inventory.SetActive(true);
                    }
                    else if (mineOpen)
                    {
                        BasicCloseMenu(ref mineOpen);
                    }

                    //Open lumbermill
                    if (parent.GetComponent<Lumbermill>() != null && !lumberMillOpen)
                    {
                        BasicOpenMenu(ref lumberMillOpen);
                        Lumbermill lumbermill = parent.GetComponent<Lumbermill>();
                        ChestInventory.GetComponent<WhatInventory>().inventoryOpen1 = lumbermill.inventory;
                        lumbermill.inventory.SetActive(true);
                    }
                    else if (lumberMillOpen)
                    {
                        BasicCloseMenu(ref lumberMillOpen);
                    }

                    //Open Door
                    if (parent.GetComponent<IsADoor>() != null)
                    {
                        Animator animator = parent.GetComponent<Animator>();
                        IsADoor door = parent.GetComponent<IsADoor>();

                        door.open = !door.open;
                        animator.SetTrigger(door.open ? "Open Door" : "Close Door");
                    }
                }
            }
        }
    }

    private void BasicOpenMenu(ref bool change)
    {
        change = true;
        openMenus.changePlayerState(false);
        inventoryObject.SetActive(true);
        toolBar.SetActive(true);
        ChestInventory.SetActive(true);
        audioSource.PlayOneShot(buildingInventory);
        //openingBuildInventory.Play();
    }

    public void BasicCloseMenu(ref bool change)
    {
        change = false;

        if (openMenus != null)
        {
            openMenus.CloseChestInventory();

            if (openMenus.Inventory != null)
                openMenus.Inventory.SetActive(false);

            openMenus.changePlayerState(true);

            if (openMenus.ChestInventory != null)
                openMenus.ChestInventory.SetActive(false);

            audioSource.PlayOneShot(buildingInventory);
        }
    }


    private bool CheckIfAllTrue()
    {
        for (int i = 0; i < canvasObjects.Count; i++)
        {
            canvasObjectsActive[i] = canvasObjects[i].activeSelf;
        }

        bool returnBool = true;

        for (int i = 0; i < canvasObjectsActive.Length; i++)
        {
            if (canvasObjectsActive[i] == true)
            {
                returnBool = false;
                break;
            }
        }
        return returnBool;
    }

    private void SelectButton(GameObject ResidentTalkingTo, GameObject job)
    {
        //mainCamera.gameObject.SetActive(true);
        //transform.parent.gameObject.SetActive(false);

        ResidentTalkingTo.GetComponent<ResidentScheudle>().job = job;
        ResidentTalkingTo.GetComponent<Hats>().GetHat(job.name);
        //ResidentTalkingTo.GetComponent<StatBar>().UpdateJob();
        //TextBoxChoices.SetActive(true);

        if (!buildMenuUpdater.AccessToLevel3Buildings)
        {
            buildMenuUpdater.AccessToLevel2Buildings = false;
            buildMenuUpdater.AccessToLevel3Buildings = true;
            Announcement.gameObject.SetActive(true);
            Announcement.text = "New Buildings Unlocked";
        }
    }

    private void BasicBuildingSelect(string name)
    {
        buildingHealth = hit.collider.transform.parent.GetComponent<BuildingHealth>();
        builder.buildingHealth = buildingHealth;
        repairCosts.text = builder.RepairCostText(name);
        healthBar.buildingHealth = buildingHealth;
        healthBar.SetHealth();
        buildingHealth.ModifyHealth(0);

        menuNames[0].SetActive(true);
        menuStats.SetActive(true);
        openMenus.changePlayerState(false);
        menuObjects[1].GetComponent<TextMeshProUGUI>().text = name; //name of building
        menuObjects[2].GetComponent<RawImage>().texture = NameToImage(name); //image of building
        ChangeCosts(GetCosts(name));

        ResidentNamesBar.gameObject.SetActive(false);
        JobNamesBar.gameObject.SetActive(false);
        storage.gameObject.SetActive(false);
        MarksObject.SetActive(false);
    }

    private void ChangeCosts(int[] cost)
    {
        for (int i = 0; i < cost.Length; i++)
        {
            GameObject child = menuObjects[0].transform.GetChild(i).gameObject; //cost checker
            if (cost[i] == 0) child.SetActive(false);
            else child.SetActive(true);

            GameObject text = child.transform.GetChild(0).GetChild(0).gameObject;
            text.GetComponent<TextMeshProUGUI>().text = (cost[i] / 3).ToString();
        }
    }

    private int[] GetCosts(string name)
    {
        switch (name)
        {
            case "Campfire":
                return builder.campfireCost;
            case "Tent":
                return builder.tentCost;
            case "Mine":
                return builder.mineCost;
            case "Lumbermill":
                return builder.lumberMillCost;
            case "Farm":
                return builder.farmCost;
            case "Wall":
                return builder.wallCost;
            case "Door":
                return builder.doorCost;
            case "Chest":
                return builder.chestCost;
            case "Messhall":
                return builder.messhallCost;
            case "Tavern":
                return builder.tavernCost;
            case "Tower":
                return builder.towerCost;
            case "KnightHut":
                return builder.knightHutCost;
        }

        return null;
    }

    private Texture NameToImage(string name)
    {
        switch (name)
        {
            case "Campfire":
                return book.images[0];
            case "Tent":
                return book.images[1];
            case "Mine":
                return book.images[2];
            case "Lumbermill":
                return book.images[3];
            case "Farm":
                return book.images[4];
            case "Wall":
                return book.images[5];
            case "Door":
                return book.images[6];
            case "Chest":
                return book.images[7];
            case "Messhall":
                return book.images[8];
            case "Tavern":
                return book.images[9];
            case "Tower":
                return book.images[10];
            case "KnightHut":
                return book.images[11];
        }

        return null;

    }
}
