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
    public JobCamera jobCamera;

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

    public PlayerInteractions playerInteractions;

    [Header("Job Stuff")]
    public BuildMenuUpdater buildMenuUpdater;
    public TextMeshProUGUI Announcement;
    [Header("Sounds")]
    public AudioSource pickup;

    [Header("Building Health")]
    public BuildingHealth buildingHealth;
    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < canvasItems.Length; i++)
        {
            for (int i2 = 0; i2 < canvasItems[i].transform.childCount; i2++)
            {
                canvasObjects.Add(canvasItems[i].transform.GetChild(i2).gameObject);
            }
        }
        canvasObjectsActive = new bool[canvasObjects.Count];
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, distance * 2, ~IgnoreMe))
        {
            TouchingObjects();
        }
    }

    public void TouchingObjects()
    {
        if(hit.distance <= distance && CheckIfAllTrue() && !mainMenu.activeSelf)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if(hit.collider.transform.parent != null)
                {
                    MarksObject.SetActive(false);
                    Transform parent = hit.collider.transform.parent;

                    //opening buildings
                    if (parent.GetComponent<IsABuilding>() != null && !playerInteractions.residentFollowing && !playerInteractions.assign)
                    {
                        buildingHealth = hit.collider.transform.parent.GetComponent<BuildingHealth>();
                        healthBar.buildingHealth = buildingHealth;
                        healthBar.SetHealth();
                        buildingHealth.ModifyHealth(0);

                        BasicBuildingSelect();

                        if (hammer.activeSelf)
                        {
                            builder.buildingData = parent.gameObject;
                            menuNames[0].SetActive(true);
                            buildEditor.SetActive(true);

                            //you cant destroy the campfire so if you click on the campfire, disable the destroy option
                            if (hit.collider.transform.parent.transform.name.Contains("Campfire")) buildEditor.transform.GetChild(0).gameObject.SetActive(false);
                            else buildEditor.transform.GetChild(0).gameObject.SetActive(true);
                            openMenus.changePlayerState(false);
                        }
                        else
                        {
                            //Open Chest
                            if (parent.GetComponent<Chest>() != null)
                            {
                                openMenus.changePlayerState(false);
                                inventoryObject.SetActive(true);
                                toolBar.SetActive(true);
                                ChestInventory.SetActive(true);
                                ChestInventory.GetComponent<WhatInventory>().inventoryOpen1 = parent.GetComponent<Chest>().inventory;
                                parent.GetComponent<Chest>().inventory.SetActive(true);
                            }

                            //Open Tent
                            if (parent.GetComponent<Tent>() != null)
                            {
                                ResidentNamesBar.gameObject.SetActive(true);

                                ResidentNames.text = "";
                                Tent tent = parent.GetComponent<Tent>();
                                for (int i = 0; i < tent.Residents; i++)
                                {
                                    ResidentNames.text += tent.ResidentsActive[i].name + "\n";
                                }
                            }

                            //OpenJob
                            if (parent.GetComponent<Job>() != null)
                            {
                                //BasicBuildingSelect();

                                //Destroy all children
                                foreach (Transform child in JobNamesBar.transform)
                                {
                                    Destroy(child.gameObject);
                                }

                                JobNamesBar.gameObject.SetActive(true);

                                JobNames.text = "";
                                Job job = parent.GetComponent<Job>();
                                for (int i = 0; i < job.Workers; i++)
                                {
                                    GameObject worker = Instantiate(JobNames.gameObject, JobNames.transform.position, Quaternion.identity);
                                    worker.SetActive(true);
                                    worker.transform.SetParent(JobNamesBar.transform);
                                    worker.GetComponent<TextMeshProUGUI>().text = job.WorkersActive[i].name;
                                    worker.transform.GetChild(0).GetComponent<JobOptions>().resident = job.WorkersActive[i];
                                    worker.transform.GetChild(0).GetComponent<JobOptions>().num = i;
                                }

                                storage.gameObject.SetActive(true);
                                storage.GetComponent<AutoUpdate>().storage = hit.collider.transform.parent.gameObject;
                            }

                            //Open Door
                            if (parent.GetComponent<IsADoor>() != null)
                            {
                                Animator animator = parent.GetComponent<Animator>();
                                IsADoor door = parent.GetComponent<IsADoor>();

                                door.open = !door.open;
                                animator.SetTrigger(door.open ? "Open Door" : "Close Door");
                            }

                            //Open mine
                            if (parent.GetComponent<Mine>() != null)
                            {
                                toolBar.SetActive(true);
                                Mine mine = parent.GetComponent<Mine>();
                                ChestInventory.GetComponent<WhatInventory>().inventoryOpen1 = mine.inventory;
                                mine.inventory.SetActive(true);
                            }

                            //Open lumbermill
                            if (parent.GetComponent<Lumbermill>() != null)
                            {
                                toolBar.SetActive(true);
                                Lumbermill lumbermill = parent.GetComponent<Lumbermill>();
                                ChestInventory.GetComponent<WhatInventory>().inventoryOpen1 = lumbermill.inventory;
                                lumbermill.inventory.SetActive(true);
                            }

                            //Open farm
                            if (parent.GetComponent<Farm>() != null && !playerInteractions.assign)
                            {
                                toolBar.SetActive(true);
                                Farm farm = parent.GetComponent<Farm>();
                                ChestInventory.GetComponent<WhatInventory>().inventoryOpen1 = farm.inventory;
                                farm.inventory.SetActive(true);
                            }

                            //Open messhall
                            if (parent.GetComponent<Messhall>() != null)
                            {
                                MarksObject.SetActive(true);
                                toolBar.SetActive(true);
                                Messhall messhall = parent.GetComponent<Messhall>();
                                if (messhall.farm != null) MarksObject.transform.GetChild(1).GetComponent<RawImage>().texture = MarksObject.GetComponent<Marks>().checkMark;
                                else MarksObject.transform.GetChild(1).GetComponent<RawImage>().texture = MarksObject.GetComponent<Marks>().xMark;
                                ChestInventory.GetComponent<WhatInventory>().inventoryOpen1 = messhall.rawFood;
                                ChestInventory.GetComponent<WhatInventory>().inventoryOpen2 = messhall.cookedFood;
                                messhall.rawFood.SetActive(true);
                                messhall.cookedFood.SetActive(true);
                            }

                            //Open Archertower
                            if (parent.GetComponent<ArcherTower>() != null && !playerInteractions.assign)
                            {
                                toolBar.SetActive(true);
                                ArcherTower archerTower = parent.GetComponent<ArcherTower>();
                            }

                            if (parent.GetComponent<KnightHut>() != null && !playerInteractions.assign)
                            {
                                toolBar.SetActive(true);
                                KnightHut knightHut = parent.GetComponent<KnightHut>();
                            }

                        }
                    }

                    if (parent.GetComponent<Farm>() && playerInteractions.assign)
                    {
                        Messhall messhall = playerInteractions.resident.GetComponent<ResidentScheudle>().job.GetComponent<Messhall>();
                        messhall.farm = parent.gameObject;

                        playerInteractions.residentText.text = "Okay perfect I will collect food from here now";
                        playerInteractions.resident.GetComponent<BoxCollider>().enabled = true;
                        playerInteractions.resident.GetComponent<ResidentScheudle>().isBeingTalkedTo = true;
                        playerInteractions.resident.GetComponent<ResidentScheudle>().followPlayer = false;
                        playerInteractions.resident.GetComponent<NavMeshAgent>().stoppingDistance = 0;

                        playerInteractions.resident = null;
                        playerInteractions.residentText = null;
                        playerInteractions.assign = false;
                        playerInteractions.resident = null;
                    }

                    if (parent.GetComponent<Job>() && playerInteractions.residentFollowing)
                    {
                        Job job = parent.GetComponent<Job>();

                        //Check if mine was selected
                        if (job != null && (job.Workers < job.MaxWorkers))
                        {
                            SelectButton(playerInteractions.resident, parent.gameObject);
                            job.Workers++;
                            job.WorkersActive[job.Workers - 1] = playerInteractions.resident;

                            playerInteractions.residentText.text = "Thanks! I will work here from now on.";
                            playerInteractions.resident.GetComponent<StatBar>().UpdateJob();
                        }
                        else
                        {
                            playerInteractions.residentText.text = "this job is full, I can't work here";
                        }

                        playerInteractions.resident.GetComponent<BoxCollider>().enabled = true;
                        playerInteractions.resident.GetComponent<ResidentScheudle>().isBeingTalkedTo = true;
                        playerInteractions.resident.GetComponent<ResidentScheudle>().followPlayer = false;
                        playerInteractions.resident.GetComponent<NavMeshAgent>().stoppingDistance = 0;

                        playerInteractions.residentFollowing = false;
                        playerInteractions.resident = null;
                        playerInteractions.residentText = null;
                    }
                }
                
                if(hit.collider.transform != null)
                {
                    Transform gameObject = hit.collider.transform;

                    if (gameObject.GetComponent<IsACollectable>() != null)
                    {
                        hit.collider.transform.gameObject.SetActive(false);
                        exclamation.SetActive(true);

                        if (gameObject.name == "Opening Letter")
                        {
                            collectables[0].SetActive(true);
                        }
                    }

                    if (gameObject.GetComponent<IsATool>() != null)
                    {
                        hit.collider.transform.gameObject.SetActive(false);

                        gameObject.GetComponent<IsATool>().AddItem();

                        storyManager.CheckSpawnShips = true;
                        pickup.Play();
                    }

                    if (gameObject.GetComponent<ResidentStats>() != null)
                    {
                        //Turn off all menus of the resident, if you are were just talking to one
                        if (playerInteractions.residentTalkingTo != null)
                        {
                            playerInteractions.residentTalkingTo.GetComponent<ResidentStats>().textBox.SetActive(false);
                            playerInteractions.residentTalkingTo.GetComponent<ResidentStats>().schedule.SetActive(false);
                            //playerInteractions.residentTalkingTo.GetComponent<StatBar>().starbar.transform.parent.gameObject.SetActive(false);
                        }
                        hit.collider.GetComponent<ResidentScheudle>().isBeingTalkedTo = true;
                        playerInteractions.residentTalkingTo = hit.collider.gameObject;

                        //stops having 2 residents following you at once, after you tell one to follow you, if one is folllowing you, stop that first
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
                        if (textBox.activeSelf)
                        {
                            //textBox.SetActive(false);
                            //textBox.SetActive(true);
                        }
                        else textBox.SetActive(true);
                        //ResidentTextChoices.GetComponent<TextBoxChoices>().ResidentTalkingTo = hit.collider.gameObject;
                        //jobCamera.ResidentTalkingTo = hit.collider.gameObject;
                        //ResidentTextBox.GetComponent<TextBox>().ResidentTalkingTo = hit.collider.gameObject;
                        //ResidentTextBox.SetActive(true);
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                if(hit.collider.transform.parent != null || hit.collider.transform != null)
                {
                    Transform GameObject = hit.collider.transform;
                    Transform parent = hit.collider.transform.parent;
                    PickUp pickUp = null;

                    if (parent.GetComponent<PickUp>() != null) pickUp = parent.GetComponent<PickUp>();
                    else if (GameObject.GetComponent<PickUp>() != null) pickUp = GameObject.GetComponent<PickUp>();


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

                        pickup.Play();
                    }

                    if (parent.GetComponent<Gravestone>() != null)
                    {
                        parent.GetComponent<Gravestone>().ReturnInventory();
                        Destroy(hit.collider.transform.parent.gameObject);
                    }
                }
            }
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

    private void BasicBuildingSelect()
    {
        menuNames[0].SetActive(true);
        menuStats.SetActive(true);
        openMenus.changePlayerState(false);

        ResidentNamesBar.gameObject.SetActive(false);
        JobNamesBar.gameObject.SetActive(false);
        storage.gameObject.SetActive(false);
    }
}
