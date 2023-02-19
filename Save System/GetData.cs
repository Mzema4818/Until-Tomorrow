using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GetData : MonoBehaviour
{
    [Header("Terrain Data")]
    public NoiseData noiseData;
    public int seed;
    public LightingManager lightingManager;
    public GameObject TerrainObjects;

    [Header("Object Data")]
    public GameObject dropableParent;
    public string[] dropableNames;
    public Vector3[] dropablePosition;
    public Quaternion[] dropableRotation;

    public GameObject TreesParent;
    public string[] treeNames;
    public Color[] treeColors;
    public Vector3[] treeScales;
    public Vector3[] treePosition;
    public Quaternion[] treeRotation;
    public GameObject[] treePrefabs;
    public Shader shader;

    public GameObject RocksParent;
    public string[] rockNames;
    public Vector3[] rockPosition;
    public Color[] rockColors;
    public Vector3[] rockScales;
    public Quaternion[] rockRotation;
    public GameObject[] rockPrefabs;

    public GameObject StumpParent;
    public string[] stumpNames;
    public Vector3[] stumpPosition;
    public Vector3[] stumpScales;
    public Quaternion[] stumpRotation;
    public GameObject[] stumpPrefabs;

    public GameObject FallenParent;
    public string[] fallenNames;
    public Vector3[] fallenPosition;
    public Vector3[] fallenScales;
    public Quaternion[] fallenRotation;
    public GameObject[] fallenPrefabs;

    public GameObject SmallRockParent;
    public string[] smallRockNames;
    public Vector3[] smallRockPosition;
    public Vector3[] smallRockScales;
    public Quaternion[] smallRockRotation;
    public GameObject[] smallRockPrefabs;

    public GameObject FlowerParent;
    public string[] flowerNames;
    public Vector3[] flowerPosition;
    public Vector3[] flowerScales;
    public Quaternion[] flowerRotation;
    public GameObject[] flowerPrefabs;

    public GameObject MushroomParent;
    public string[] mushroomNames;
    public Vector3[] mushroomPosition;
    public Vector3[] mushroomScales;
    public Quaternion[] mushroomRotation;
    public GameObject[] mushroomPrefabs;

    public GameObject BerryParent;
    public string[] BerryNames;
    public Vector3[] BerryPosition;
    public Vector3[] BerryScales;
    public Quaternion[] BerryRotation;
    public GameObject[] BerryPrefabs;

    public GameObject SaplingParent;
    public Vector3[] SaplingPosition;
    public Quaternion[] SaplingRotation;
    public string saplingName;
    public GameObject SaplingPrefab;

    public GameObject DeerParent;
    public string[] deerNames;
    public Vector3[] deerPosition;
    public Vector3[] deerScales;
    public Quaternion[] deerRotation;
    public GameObject[] deerPrefabs;

    public GameObject FoxParent;
    public string[] foxNames;
    public Vector3[] foxPosition;
    public Vector3[] foxScales;
    public Quaternion[] foxRotation;
    public GameObject[] foxPrefabs;

    public GameObject BearParent;
    public string[] bearNames;
    public Vector3[] bearPosition;
    public Vector3[] bearScales;
    public Quaternion[] bearRotation;
    public GameObject[] bearPrefabs;

    [Header("Building Data")]
    public GameObject buildingsParent;
    public GameObject[] buildingPrefabs;

    public string[] CampfireName;
    public Vector3[] CampfirePosition;
    public Quaternion[] CampfireRotation;

    public string[] TentName;
    public Vector3[] TentPosition;
    public Quaternion[] TentRotation;

    [Header("Player Data")]
    public GameObject Player;
    public PlayerMovement playerMovement;
    public Vector3 playerPosition;
    public Quaternion playerRotation;
    public Vector3 playerRespawnPoint;
    public float health;
    public float food;
    public Slider playerHealth;
    public Slider playerFood;

    [Header("Settings Data")]
    public MouseLook mouseLook;

    [Header("Inventory Data")]
    public TextMeshProUGUI woodText;
    public TextMeshProUGUI stoneText;
    public TextMeshProUGUI MushroomText;
    public TextMeshProUGUI FlowerText;
    public string wood;
    public string stone;
    public string mushroom;
    public string flower;

    [Header("Collectable Data")]
    public GameObject collectableMenu;
    public GameObject collectableObjectsButtonMenu;
    public GameObject collectableObjectsMenu;
    public bool[] collectableActive;
    public Vector3[] collectablePosition;
    public Quaternion[] collectableRotation;
    public GameObject[] collectableObjectsButton;
    public GameObject[] collectableObjects;

    [Header("HotBarItem Data")]
    public GameObject HotBarItemParent;
    public Vector3[] HotBarItemPosition;
    public Quaternion[] HotBarItemRotation;
    public bool[] HotBarItemActive;
    public GameObject[] HotBarMenuItems;

    [Header("Inventory Data")]
    public Inventory inventory;
    public string[] inventoryItemNames;
    public int[] inventoryItemAmounts;

    [Header("Death Data")]
    public GameObject graveStoneParent;
    public Vector3[] graveStonePosition;
    public Quaternion[] graveStoneRotation;
    public GameObject graveStonePrefab;
    public string graveStoneName;

    public Inventory[] inventoryGraveStone;
    public string[] inventoryItemNamesGraveStone;
    public int[] inventoryItemAmountsGraveStone;
    public Dictionary<Item.ItemType[], int[]>[] invetoryItemsArrayGraveStone;

    [Header("Resident Data")]
    public GameObject ResidentParent;
    public string[] ResidentName;
    public int[][] ResidentStats;
    public bool[] JoinedTown;
    public string[] FavoriteItem;
    public Vector3[] ResidentPosition;
    public Quaternion[] ResidentRotation;
    public GameObject ResidentPrefab;

    [Header("Ship Data")]
    public GameObject PrisonShipParent;
    public string[] PrisonShipName;
    public Vector3[] PrisonShipPosition;
    public Quaternion[] PrisonShipRotation;
    public GameObject PrisonShipPrefab;

    [Header("Story Data")]
    public BuildMenuUpdater buildMenuUpdater;
    public bool AccessToLevel0Buildings;
    public bool AccessToLevel1Buildings;
    public bool AccessToLevel2Buildings;

    public StoryManager storyManager;
    public bool SpawnPrisonShips;
    public int SpawnPrisonShipDay;

    private void Start()
    {
        collectableObjectsButton = new GameObject[collectableObjectsButtonMenu.transform.childCount];
        collectableObjects = new GameObject[collectableObjectsButtonMenu.transform.childCount];
        for (int i = 0; i < collectableObjectsButtonMenu.transform.childCount; i++)
        {
            collectableObjectsButton[i] = collectableObjectsButtonMenu.transform.GetChild(i).gameObject;
            collectableObjects[i] = collectableObjectsMenu.transform.GetChild(i).gameObject;
        }
    }

    //GetSeed
    public int getSeed()
    {
        seed = noiseData.seed;
        return seed;
    }

    //Get Inventory
    public string GetInventoryWood()
    {
        wood = woodText.text;
        return wood;
    }

    public string GetInventoryStone()
    {
        stone = stoneText.text;
        return stone;
    }

    public string GetInventoryMushroom()
    {
        mushroom = MushroomText.text;
        return mushroom;
    }

    public string GetInventoryFlower()
    {
        flower = FlowerText.text;
        return flower;
    }

    //Get Day
    public float GetTimeOfDay()
    {
        return lightingManager.TimeOfDay;
    }

    public int GetNumberOfDays()
    {
        return lightingManager.numberOfDays;
    }

    //Get Settings
    public float GetSensitivity()
    {
        return mouseLook.mouseSensitivity;
    }

    //Get Player Data
    public Vector3 GetPlayerPosition()
    {
        playerPosition = Player.transform.position;
        return playerPosition;
    }

    public Vector3 GetPlayerRespawnPoint()
    {
        playerRespawnPoint = playerMovement.respawnPoint;
        return playerRespawnPoint;
    }

    public Quaternion GetPlayerRotation()
    {
        playerRotation = Player.transform.rotation;
        return playerRotation;
    }

    public float getPlayerHealth()
    {
        health = playerHealth.value;
        return health;
    }

    public float getPlayerFood()
    {
        food = playerFood.value;
        return food;
    }

    //Get Dropable Data
    public Vector3[] GetDropablePosition()
    {
        return GetPositions(ref dropablePosition, dropableParent);
    }

    public Quaternion[] GetDropableRotation()
    {
        return GetRotations(ref dropableRotation, dropableParent);
    }

    public string[] GetDropableNames()
    {
        return GetNames(ref dropableNames, dropableParent);
    }

    //Get Tree Data
    public Vector3[] GetTreePosition()
    {
        return GetPositions(ref treePosition, TreesParent);
    }

    public Quaternion[] GetTreeRotation()
    {
        return GetRotations(ref treeRotation, TreesParent);
    }

    public string[] GetTreeNames()
    {
        return GetNames(ref treeNames, TreesParent);
    }

    public Color[] GetTreeColor()
    {
        return GetColor(ref treeColors, TreesParent);
    }

    public Vector3[] GetTreeScale()
    {
        return GetScale(ref treeScales, TreesParent);
    }

    //Get Rock Data
    public Vector3[] GetRockPosition()
    {
        return GetPositions(ref rockPosition, RocksParent);
    }

    public Quaternion[] GetRockRotation()
    {
        return GetRotations(ref rockRotation, RocksParent);
    }

    public string[] GetRockNames()
    {
        return GetNames(ref rockNames, RocksParent);
    }

    public Color[] GetRockColor()
    {
        return GetColorRock(ref rockColors, RocksParent);
    }

    public Vector3[] GetRockScale()
    {
        return GetScale(ref rockScales, RocksParent);
    }

    //Get Stump Data
    public Vector3[] GetStumpPosition()
    {
        return GetPositions(ref stumpPosition, StumpParent);
    }

    public Quaternion[] GetStumpRotation()
    {
        return GetRotations(ref stumpRotation, StumpParent);
    }

    public string[] GetStumpNames()
    {
        return GetNames(ref stumpNames, StumpParent);
    }

    public Vector3[] GetStumpScale()
    {
        return GetScale(ref stumpScales, StumpParent);
    }

    //Get Fallen Data
    public Vector3[] GetFallenPosition()
    {
        return GetPositions(ref fallenPosition, FallenParent);
    }

    public Quaternion[] GetFallenRotation()
    {
        return GetRotations(ref fallenRotation, FallenParent);
    }

    public string[] GetFallenNames()
    {
        return GetNames(ref fallenNames, FallenParent);
    }

    public Vector3[] GetFallenScale()
    {
        return GetScale(ref fallenScales, FallenParent);
    }


    //Get Small Rock Data
    public Vector3[] GetSmallRockPosition()
    {
        return GetPositions(ref smallRockPosition, SmallRockParent);
    }

    public Quaternion[] GetSmallRockRotation()
    {
        return GetRotations(ref smallRockRotation, SmallRockParent);
    }

    public string[] GetSmallRockNames()
    {
        return GetNames(ref smallRockNames, SmallRockParent);
    }

    public Vector3[] GetSmallRockScale()
    {
        return GetScale(ref smallRockScales, SmallRockParent);
    }

    //Get flower Data
    public Vector3[] GetFlowerPosition()
    {
        return GetPositions(ref flowerPosition, FlowerParent);
    }

    public Quaternion[] GetFlowerRotation()
    {
        return GetRotations(ref flowerRotation, FlowerParent);
    }

    public string[] GetFlowerNames()
    {
        return GetNames(ref flowerNames, FlowerParent);
    }

    public Vector3[] GetFlowerScale()
    {
        return GetScale(ref flowerScales, FlowerParent);
    }

    //Get Mushroom Data
    public Vector3[] GetMushroomPosition()
    {
        return GetPositions(ref mushroomPosition, MushroomParent);
    }

    public Quaternion[] GetMushroomRotation()
    {
        return GetRotations(ref mushroomRotation, MushroomParent);
    }

    public string[] GetMushroomNames()
    {
        return GetNames(ref mushroomNames, MushroomParent);
    }

    public Vector3[] GetMushroomScale()
    {
        return GetScale(ref mushroomScales, MushroomParent);
    }

    //Get Berry Data
    public Vector3[] GetBerryPosition()
    {
        return GetPositions(ref BerryPosition, BerryParent);
    }

    public Quaternion[] GetBerryRotation()
    {
        return GetRotations(ref BerryRotation, BerryParent);
    }

    public string[] GetBerryNames()
    {
        return GetNames(ref BerryNames, BerryParent);
    }

    public Vector3[] GetBerryScale()
    {
        return GetScale(ref BerryScales, BerryParent);
    }

    //Get sapling Data
    public Vector3[] GetSaplingPosition()
    {
        return GetPositions(ref SaplingPosition, SaplingParent);
    }

    public Quaternion[] GetSaplingRotation()
    {
        return GetRotations(ref SaplingRotation, SaplingParent);
    }

    //Get Deer Data
    public Vector3[] GetDeerPosition()
    {
        return GetPositions(ref deerPosition, DeerParent);
    }

    public Quaternion[] GetDeerRotation()
    {
        return GetRotations(ref deerRotation, DeerParent);
    }

    public string[] GetDeerNames()
    {
        return GetNames(ref deerNames, DeerParent);
    }

    public Vector3[] GetDeerScale()
    {
        return GetScale(ref deerScales, DeerParent);
    }

    //Get Fox Data
    public Vector3[] GetFoxPosition()
    {
        return GetPositions(ref foxPosition, FoxParent);
    }

    public Quaternion[] GetFoxRotation()
    {
        return GetRotations(ref foxRotation, FoxParent);
    }

    public string[] GetFoxNames()
    {
        return GetNames(ref foxNames, FoxParent);
    }

    public Vector3[] GetFoxScale()
    {
        return GetScale(ref foxScales, FoxParent);
    }

    //Get Bear Data
    public Vector3[] GetBearPosition()
    {
        return GetPositions(ref bearPosition, BearParent);
    }

    public Quaternion[] GetBearRotation()
    {
        return GetRotations(ref bearRotation, BearParent);
    }

    public string[] GetBearNames()
    {
        return GetNames(ref bearNames, BearParent);
    }

    public Vector3[] GetBearScale()
    {
        return GetScale(ref bearScales, BearParent);
    }

    //Get Prison Ship Data
    public Vector3[] GetPrisonShipPosition()
    {
        return GetPositions(ref PrisonShipPosition, PrisonShipParent);
    }

    public Quaternion[] GetPrisonShipRotation()
    {
        return GetRotations(ref PrisonShipRotation, PrisonShipParent);
    }

    public string[] GetPrisonShipNames()
    {
        return GetNames(ref PrisonShipName, PrisonShipParent);
    }

    //Get resident Data
    public Vector3[] GetResidentPosition()
    {
        return GetPositions(ref ResidentPosition, ResidentParent);
    }

    public Quaternion[] GetResidentRotation()
    {
        return GetRotations(ref ResidentRotation, ResidentParent);
    }

    public string[] GetResidentNames()
    {
        return GetNames(ref ResidentName, ResidentParent);
    }

    public int[][] GetResidentStats()
    {
        return GetStats(ref ResidentStats, ResidentParent);
    }

    public bool[] GetResidentJoinedTown()
    {
        JoinedTown = new bool[ResidentParent.transform.childCount];

        for (int i = 0; i < JoinedTown.Length; i++)
        {
            JoinedTown[i] = ResidentParent.transform.GetChild(i).GetComponent<ResidentStats>().joinedTown;
        }

        return JoinedTown;
    }

    public string[] GetResidentFavoriteItem()
    {
        FavoriteItem = new string[ResidentParent.transform.childCount];

        for (int i = 0; i < FavoriteItem.Length; i++)
        {
            FavoriteItem[i] = ItemToString(ResidentParent.transform.GetChild(i).GetComponent<ResidentStats>().FavoriteItem);
        }

        return FavoriteItem;
    }

    //Get death Data
    public Vector3[] GetDeathPosition()
    {
        return GetPositions(ref graveStonePosition, graveStoneParent);
    }

    public Quaternion[] GetDeathRotation()
    {
        return GetRotations(ref graveStoneRotation, graveStoneParent);
    }

    //Get campfire Data
    public Vector3[] GetCampfirePosition()
    {
        return GetPositions(ref CampfirePosition, buildingsParent.transform.GetChild(0).gameObject);
    }

    public Quaternion[] GetCampfireRotation()
    {
        return GetRotations(ref CampfireRotation, buildingsParent.transform.GetChild(0).gameObject);
    }

    public string[] GetCampfireNames()
    {
        return GetNames(ref CampfireName, buildingsParent.transform.GetChild(0).gameObject);
    }

    //Get tent Data
    public Vector3[] GetTentPosition()
    {
        return GetPositions(ref TentPosition, buildingsParent.transform.GetChild(1).gameObject);
    }

    public Quaternion[] GetTentRotation()
    {
        return GetRotations(ref TentRotation, buildingsParent.transform.GetChild(1).gameObject);
    }

    public string[] GetTentNames()
    {
        return GetNames(ref TentName, buildingsParent.transform.GetChild(1).gameObject);
    }

    //Get hotbaritem Data
    public Vector3[] GetHotBarItemPosition()
    {
        return GetPositions(ref HotBarItemPosition, HotBarItemParent);
    }

    public Quaternion[] GetHotBarItemRotation()
    {
        return GetRotations(ref HotBarItemRotation, HotBarItemParent);
    }

    public bool[] GetHotBarItemActive()
    {
        return GetActive(ref HotBarItemActive, HotBarItemParent);
    }

    //Get Collectables
    public bool[] GetCollectableActive()
    {
        return GetActive(ref collectableActive, collectableMenu);
    }

    public Vector3[] GetCollectablePosition()
    {
        return GetPositions(ref collectablePosition, collectableMenu);
    }

    public Quaternion[] GetCollectableRotation()
    {
        return GetRotations(ref collectableRotation, collectableMenu);
    }

    //GetFunctions
    public Vector3[] GetPositions(ref Vector3[] positions, GameObject parent)
    {
        positions = new Vector3[parent.transform.childCount];
        
        for(int i = 0; i < positions.Length; i++)
        {
            positions[i] = parent.transform.GetChild(i).position;
        }

        return positions;
    }

    public Quaternion[] GetRotations(ref Quaternion[] rotations, GameObject parent)
    {
        rotations = new Quaternion[parent.transform.childCount];

        for (int i = 0; i < rotations.Length; i++)
        {
            rotations[i] = parent.transform.GetChild(i).rotation;
        }

        return rotations;
    }

    public string[] GetNames(ref string[] names, GameObject parent)
    {
        names = new string[parent.transform.childCount];

        for (int i = 0; i < names.Length; i++)
        {
            names[i] = parent.transform.GetChild(i).name;
        }

        return names;
    }

    public bool[] GetActive(ref bool[] active, GameObject parent)
    {
        active = new bool[parent.transform.childCount];

        for (int i = 0; i < active.Length; i++)
        {
            active[i] = parent.transform.GetChild(i).gameObject.activeSelf;
        }

        return active;
    }

    public Color[] GetColor(ref Color[] color, GameObject parent)
    {
        color = new Color[parent.transform.childCount];

        for(int i = 0; i < color.Length; i++)
        {
            color[i] = parent.transform.GetChild(i).transform.Find("Tree_Leaves_LOD0").transform.GetComponent<Renderer>().material.GetColor("Color_8DC4F919");
        }

        return color;
    }

    public Color[] GetColorRock(ref Color[] color, GameObject parent)
    {
        color = new Color[parent.transform.childCount];

        for (int i = 0; i < color.Length; i++)
        {
            color[i] = parent.transform.GetChild(i).transform.Find("Rock_LOD0").transform.GetComponent<Renderer>().material.color;
        }

        return color;
    }

    public Vector3[] GetScale(ref Vector3[] scales, GameObject parent)
    {
        scales = new Vector3[parent.transform.childCount];

        for (int i = 0; i < scales.Length; i++)
        {
            scales[i] = parent.transform.GetChild(i).localScale;
        }

        return scales;
    }

    public int[][] GetStats(ref int[][] stats, GameObject parent)
    {
        stats = new int[parent.transform.childCount][];

        for (int i = 0; i < stats.Length; i++)
        {
            stats[i] = parent.transform.GetChild(i).GetComponent<ResidentStats>().Stats;
        }

        return stats;
    }

    //Inventory stuff
    public Item.ItemType[] GetInventoryItemType()
    {
        return inventory.GetItemArrayList();
    }

    public int[] GetInventoryItemAmount()
    {
        return inventory.GetItemArrayListAmount();
    }

    public Inventory[] GetArrayInventory(ref Inventory[] inventoryArray, GameObject parent, string scriptname)
    {
        inventoryArray = new Inventory[parent.transform.childCount];

        if(scriptname == "death")
        {
            for (int i = 0; i < inventoryArray.Length; i++)
            {
                inventoryArray[i] = parent.transform.GetChild(i).GetComponent<DeathCollider>().inventory;
            }
        }

        return inventoryArray;
    }

    public Dictionary<Item.ItemType[], int[]>[] GetDictionaryOfInventories(ref Dictionary<Item.ItemType[], int[]>[] invetoryData, ref Inventory[] inventoryArray, GameObject parent, string scriptname)
    {
        invetoryData = new Dictionary<Item.ItemType[], int[]>[parent.transform.childCount];
        inventoryArray = new Inventory[parent.transform.childCount];

        if (scriptname == "death")
        {
            for (int i = 0; i < inventoryArray.Length; i++)
            {
                inventoryArray[i] = parent.transform.GetChild(i).GetComponent<DeathCollider>().inventory;
            }
        }

        //invetoryData
        for (int i = 0; i < invetoryData.Length; i++)
        {
            Item.ItemType[] items = inventoryArray[i].GetItemArrayList();

            int[] amounts = inventoryArray[i].GetItemArrayListAmount();
            Dictionary<Item.ItemType[], int[]> allItems = new Dictionary<Item.ItemType[], int[]>();

            allItems.Add(items, amounts);

            invetoryData[i] = allItems;
        }

        return invetoryData;
    }

    public Dictionary<Item.ItemType[], int[]>[] GraveStoneData()
    {
        return GetDictionaryOfInventories(ref invetoryItemsArrayGraveStone, ref inventoryGraveStone, graveStoneParent, "death");
    }

    //Story stuff
    public bool GetShouldSpawnPrisonShip()
    {
        SpawnPrisonShips = storyManager.SpawnPrisonShips;
        return SpawnPrisonShips;
    }

    public int GetPrisonShipSpawnDay()
    {
        SpawnPrisonShipDay = storyManager.SpawnPrisonShipDay;
        return SpawnPrisonShipDay;
    }

    public bool GetAccessToLevel0Buildings()
    {
        AccessToLevel0Buildings = buildMenuUpdater.AccessToLevel0Buildings;
        return AccessToLevel0Buildings;
    }

    public bool GetAccessToLevel1Buildings()
    {
        AccessToLevel1Buildings = buildMenuUpdater.AccessToLevel1Buildings;
        return AccessToLevel1Buildings;
    }

    public bool GetAccessToLevel2Buildings()
    {
        AccessToLevel2Buildings = buildMenuUpdater.AccessToLevel2Buildings;
        return AccessToLevel2Buildings;
    }


    /*public Dictionary<Item.ItemType[], int[]> GetInventoryItemTypeArray(ref Dictionary<Item.ItemType[], int[]> invetoryItems, GameObject parent, Inventory[] inventorys, string scriptname)
    {
        GetArrayInventory(ref inventorys, parent, scriptname);

        invetoryItems = new Dictionary<Item.ItemType[], int[]>();
        
        for(int i = 0; i < inventorys.Length; i++)
        {
            invetoryItems.Add(inventorys[i].GetItemArrayList(), inventorys[i].GetItemArrayListAmount());
        }

        return invetoryItems;
    }

    public Dictionary<Item.ItemType[], int[]> GetInventoryItemTypeArray2()
    {
        return GetInventoryItemTypeArray(ref invetoryItemsArray, DeathParent, inventoryDeath, "death");
    }*/

    //Other End
    public void SaveData()
    {
        SaveSystem.SaveData(this);
    }

    public void LoadData()
    {
        ClearGameObjects();
        GameData data = SaveSystem.LoadData();

        //Terrain Data
        seed = data.seed;
        noiseData.seed = seed;

        //Player Data
        food = data.food;
        health = data.health;

        playerPosition.x = data.playerPosition[0];
        playerPosition.y = data.playerPosition[1];
        playerPosition.z = data.playerPosition[2];

        playerRespawnPoint.x = data.playerRespawnPoint[0];
        playerRespawnPoint.y = data.playerRespawnPoint[1];
        playerRespawnPoint.z = data.playerRespawnPoint[2];

        playerRotation.x = data.playerRotation[0];
        playerRotation.y = data.playerRotation[1];
        playerRotation.z = data.playerRotation[2];
        playerRotation.w = data.playerRotation[3];

        playerHealth.value = health;
        playerFood.value = food;
        playerMovement.respawnPoint = playerRespawnPoint;
        Player.transform.position = playerPosition;
        Player.transform.rotation = playerRotation;

        //Story Data
        AccessToLevel0Buildings = data.AccessToLevel0Buildings;
        AccessToLevel1Buildings = data.AccessToLevel1Buildings;
        AccessToLevel2Buildings = data.AccessToLevel2Buildings;

        buildMenuUpdater.AccessToLevel0Buildings = AccessToLevel0Buildings;
        buildMenuUpdater.AccessToLevel1Buildings = AccessToLevel1Buildings;
        buildMenuUpdater.AccessToLevel2Buildings = AccessToLevel2Buildings;

        SpawnPrisonShips = data.SpawnPrisonShips;
        SpawnPrisonShipDay = data.SpawnPrisonShipDay;

        storyManager.SpawnPrisonShips = SpawnPrisonShips;
        storyManager.SpawnPrisonShipDay = SpawnPrisonShipDay;

        //Settings
        mouseLook.mouseSensitivity = data.sensitivity;

        //Inventory Data
        inventoryItemAmounts = data.playerInventoryAmount;
        inventoryItemNames = data.playerInventoryItems;
        AddItemToInventory(inventoryItemNames, inventoryItemAmounts);

        //Gravestone Data
        inventoryItemAmountsGraveStone = data.inventoryItemAmountsGraveStone;
        inventoryItemNamesGraveStone = data.inventoryItemNamesGraveStone;

        wood = data.wood;
        stone = data.stone;
        mushroom = data.mushroom;
        flower = data.flower;

        woodText.text = wood;
        stoneText.text = stone;
        MushroomText.text = mushroom;
        FlowerText.text = flower;

        //Day Cycle
        lightingManager.TimeOfDay = data.timeOfDay;
        lightingManager.numberOfDays = data.numberOfDays;

        //Collectable data
        SetActive(data.collectableActive, ref collectableActive);
        for(int i = 0; i < collectableMenu.transform.childCount; i++)
        {
            if (collectableMenu.transform.GetChild(i).gameObject.activeSelf)
            {
                SetPositions(data.collectablePosition, ref collectablePosition);
                SetRotations(data.collectableRotation, ref collectableRotation);
                SetCollectablePosition(collectablePosition, collectableMenu.transform);
                SetCollectableRotation(collectableRotation, collectableMenu.transform);
            }
            else
            {
                collectableObjects[i].SetActive(true);
                collectableObjectsButton[i].SetActive(true);
            }
        }

        //hotbaritem
        SetActive2(data.HotBarItemActive, ref HotBarItemActive);
        SetPositions(data.HotBarItemPosition, ref HotBarItemPosition);
        SetRotations(data.HotBarItemRotation, ref HotBarItemRotation);
        SetHotBarItem(HotBarItemPosition, HotBarItemRotation, HotBarItemActive);

        //Object Data
        SetPositions(data.dropablePosition, ref dropablePosition);
        SetRotations(data.dropableRotation, ref dropableRotation);
        SetNames(data.dropableNames, ref dropableNames);
        SpawnDropables(dropablePosition, dropableRotation, dropableNames);

        SetPositions(data.treePosition, ref treePosition);
        SetRotations(data.treeRotation, ref treeRotation);
        SetPositions(data.treeScales, ref treeScales);
        SetNames(data.treeNames, ref treeNames);
        SetColors(data.treeColors, ref treeColors);
        SpawnObjects(treePosition, treeRotation, treeNames, TreesParent.transform ,"tree");

        SetPositions(data.rockPosition, ref rockPosition);
        SetRotations(data.rockRotation, ref rockRotation);
        SetPositions(data.rockScales, ref rockScales);
        SetNames(data.rockNames, ref rockNames);
        SetColors(data.rockColors, ref rockColors);
        SpawnObjects(rockPosition, rockRotation, rockNames, RocksParent.transform, "rock");

        SetPositions(data.stumpPosition, ref stumpPosition);
        SetRotations(data.stumpRotation, ref stumpRotation);
        SetPositions(data.stumpScales, ref stumpScales);
        SetNames(data.stumpNames, ref stumpNames);
        SpawnObjects(stumpPosition, stumpRotation, stumpNames, StumpParent.transform, "stump");

        SetPositions(data.fallenPosition, ref fallenPosition);
        SetRotations(data.fallenRotation, ref fallenRotation);
        SetPositions(data.fallenScales, ref fallenScales);
        SetNames(data.fallenNames, ref fallenNames);
        SpawnObjects(fallenPosition, fallenRotation, fallenNames, FallenParent.transform, "fallen");

        SetPositions(data.smallRockPosition, ref smallRockPosition);
        SetRotations(data.smallRockRotation, ref smallRockRotation);
        SetPositions(data.smallRockScales, ref smallRockScales);
        SetNames(data.smallRockNames, ref smallRockNames);
        SpawnObjects(smallRockPosition, smallRockRotation, smallRockNames, SmallRockParent.transform, "smallrock");

        SetPositions(data.flowerPosition, ref flowerPosition);
        SetRotations(data.flowerRotation, ref flowerRotation);
        SetPositions(data.flowerScales, ref flowerScales);
        SetNames(data.flowerNames, ref flowerNames);
        SpawnObjects(flowerPosition, flowerRotation, flowerNames, FlowerParent.transform, "flower");

        SetPositions(data.mushroomPosition, ref mushroomPosition);
        SetRotations(data.mushroomRotation, ref mushroomRotation);
        SetPositions(data.mushroomScales, ref mushroomScales);
        SetNames(data.mushroomNames, ref mushroomNames);
        SpawnObjects(mushroomPosition, mushroomRotation, mushroomNames, MushroomParent.transform, "mushroom");

        SetPositions(data.BerryPosition, ref BerryPosition);
        SetRotations(data.BerryRotation, ref BerryRotation);
        SetPositions(data.BerryScales, ref BerryScales);
        SetNames(data.BerryNames, ref BerryNames);
        SpawnObjects(BerryPosition, BerryRotation, BerryNames, BerryParent.transform, "berry");

        SetPositions(data.SaplingPosition, ref SaplingPosition);
        SetRotations(data.SaplingRotation, ref SaplingRotation);
        SpawnObjects(SaplingPosition, SaplingRotation, null, SaplingParent.transform, "sapling");

        //gravestones
        SetPositions(data.graveStonePosition, ref graveStonePosition);
        SetRotations(data.graveStoneRotation, ref graveStoneRotation);
        TurnStringArrayIntoDictonaries(ref invetoryItemsArrayGraveStone, inventoryItemNamesGraveStone, inventoryItemAmountsGraveStone);
        SpawnObjects(graveStonePosition, graveStoneRotation, null, graveStoneParent.transform, "death");

        //animals
        SetPositions(data.deerPosition, ref deerPosition);
        SetRotations(data.deerRotation, ref deerRotation);
        SetPositions(data.deerScales, ref deerScales);
        SetNames(data.deerNames, ref deerNames);
        SpawnObjects(deerPosition, deerRotation, deerNames, DeerParent.transform, "deer");

        SetPositions(data.foxPosition, ref foxPosition);
        SetRotations(data.foxRotation, ref foxRotation);
        SetPositions(data.foxScales, ref foxScales);
        SetNames(data.foxNames, ref foxNames);
        SpawnObjects(foxPosition, foxRotation, foxNames, FoxParent.transform, "fox");

        SetPositions(data.bearPosition, ref bearPosition);
        SetRotations(data.bearRotation, ref bearRotation);
        SetPositions(data.bearScales, ref bearScales);
        SetNames(data.bearNames, ref bearNames);
        SpawnObjects(bearPosition, bearRotation, bearNames, BearParent.transform, "bear");

        //prisonship
        SetPositions(data.PrisonShipPosition, ref PrisonShipPosition);
        SetRotations(data.PrisonShipRotation, ref PrisonShipRotation);
        SetNames(data.PrisonShipNames, ref PrisonShipName);
        SpawnObjects(PrisonShipPosition, PrisonShipRotation, PrisonShipName, PrisonShipParent.transform, "ship");

        //residents
        SetPositions(data.ResidentPosition, ref ResidentPosition);
        SetRotations(data.ResidentRotation, ref ResidentRotation);
        SetNames(data.ResidentName, ref ResidentName);
        ResidentStats = data.ResidentStats;
        JoinedTown = data.JoinedTown;
        FavoriteItem = data.FavoriteItem;
        SpawnObjects(ResidentPosition, ResidentRotation, ResidentName, ResidentParent.transform, "resident");

        //buildings
        SetPositions(data.CampfirePosition, ref CampfirePosition);
        SetRotations(data.CampfireRotation, ref CampfireRotation);
        SetNames(data.CampfireNames, ref CampfireName);

        SetPositions(data.TentPosition, ref TentPosition);
        SetRotations(data.TentRotation, ref TentRotation);
        SetNames(data.TentNames, ref TentName);

        SpawnObjects(CampfirePosition, CampfireRotation, CampfireName, buildingsParent.transform.GetChild(0), "building");
        SpawnObjects(TentPosition, TentRotation, TentName, buildingsParent.transform.GetChild(1), "building");
    }

    public void SetPositions(float[] getPositions, ref Vector3[] SetPositions)
    {
        SetPositions = new Vector3[getPositions.Length / 3];

        for(int i = 0; i < SetPositions.Length * 3; i += 3)
        {
            SetPositions[i / 3] = new Vector3(getPositions[i], getPositions[i + 1], getPositions[i + 2]);
        }
    }

    public void SetRotations(float[] getRotations, ref Quaternion[] SetRotations)
    {
        SetRotations = new Quaternion[getRotations.Length / 4];

        for (int i = 0; i < SetRotations.Length * 4; i += 4)
        {
            SetRotations[i / 4] = new Quaternion(getRotations[i], getRotations[i + 1], getRotations[i + 2], getRotations[i + 3]);
        }
    }

    public void SetColors(float[] getColors, ref Color[] SetColors)
    {
        SetColors = new Color[getColors.Length / 4];

        for (int i = 0; i < SetColors.Length * 4; i += 4)
        {
            SetColors[i / 4] = new Color(getColors[i], getColors[i + 1], getColors[i + 2], getColors[i + 3]);
        }
    }

    public void SetNames(string[] getNames, ref string[] SetNames)
    {
        SetNames = new string[getNames.Length];

        for(int i = 0; i < SetNames.Length; i++)
        {
            SetNames[i] = getNames[i];
        }
    }

    public void SetActive(bool[] getActive, ref bool[] SetActive)
    {
        SetActive = new bool[getActive.Length];

        for (int i = 0; i < SetActive.Length; i++)
        {
            SetActive[i] = getActive[i];
            collectableMenu.transform.GetChild(i).gameObject.SetActive(SetActive[i]);
        }
    }

    public void SetActive2(bool[] getActive, ref bool[] SetActive)
    {
        SetActive = new bool[getActive.Length];

        for (int i = 0; i < SetActive.Length; i++)
        {
            SetActive[i] = getActive[i];
        }
    }

    public void SpawnObjects(Vector3[] positions, Quaternion[] rotations, string[] names, Transform parent ,string name)
    {
        if(name == "tree")
        {
            for (int i = 0; i < positions.Length; i++)
            {
                GameObject newElement = Instantiate(CheckTreeName(names[i]), positions[i], rotations[i]);
                newElement.transform.localScale = treeScales[i];
                newElement.name = names[i];
                newElement.transform.parent = parent;
                CanvasReSizer(newElement, 0.2f);
                TreeShader(newElement, i);
            }
        }

        if (name == "rock")
        {
            for (int i = 0; i < positions.Length; i++)
            {
                GameObject newElement = Instantiate(CheckRockName(names[i]), positions[i], rotations[i]);
                newElement.transform.localScale = rockScales[i];
                newElement.name = names[i];
                newElement.transform.parent = parent;
                CanvasReSizer(newElement, 0.5f);
                RockShader(newElement, i);
            }
        }

        if (name == "stump")
        {
            for (int i = 0; i < positions.Length; i++)
            {
                GameObject newElement = Instantiate(CheckStumpName(names[i]), positions[i], rotations[i]);
                newElement.transform.localScale = stumpScales[i];
                newElement.name = names[i];
                newElement.transform.parent = parent;
                CanvasReSizer(newElement, 0.2f);
            }
        }

        if (name == "fallen")
        {
            for (int i = 0; i < positions.Length; i++)
            {
                GameObject newElement = Instantiate(CheckFallenName(names[i]), positions[i], rotations[i]);
                newElement.transform.localScale = fallenScales[i];
                newElement.name = names[i];
                newElement.transform.parent = parent;
                CanvasReSizer(newElement, 0.2f);
            }
        }

        if (name == "smallrock")
        {
            for (int i = 0; i < positions.Length; i++)
            {
                GameObject newElement = Instantiate(CheckSmallRockName(names[i]), positions[i], rotations[i]);
                newElement.transform.localScale = smallRockScales[i];
                newElement.name = names[i];
                newElement.transform.parent = parent;
                CanvasReSizer(newElement, 0.2f);
            }
        }

        if (name == "flower")
        {
            for (int i = 0; i < positions.Length; i++)
            {
                GameObject newElement = Instantiate(CheckFlowerName(names[i]), positions[i], rotations[i]);
                newElement.transform.localScale = flowerScales[i];
                newElement.name = names[i];
                newElement.transform.parent = parent;
                CanvasReSizer(newElement, 0.2f);
            }
        }

        if (name == "mushroom")
        {
            for (int i = 0; i < positions.Length; i++)
            {
                GameObject newElement = Instantiate(CheckMushroomName(names[i]), positions[i], rotations[i]);
                newElement.transform.localScale = mushroomScales[i];
                newElement.name = names[i];
                newElement.transform.parent = parent;
                CanvasReSizer(newElement, 0.1f);
            }
        }

        if (name == "berry")
        {
            for (int i = 0; i < positions.Length; i++)
            {
                GameObject newElement = Instantiate(CheckBerryName(names[i]), positions[i], rotations[i]);
                newElement.transform.localScale = BerryScales[i];
                newElement.name = names[i];
                CanvasReSizer(newElement, 0.2f);
                if (newElement.name.Contains("regrow")){
                    for(int i2 = 0; i2 < newElement.transform.childCount; i2++)
                    {
                        if(newElement.transform.GetChild(i2).name == "Berries")
                        {
                            newElement.transform.GetChild(i2).gameObject.SetActive(false);
                        }
                    }
                }
                newElement.transform.parent = parent;
            }
        }

        if (name == "sapling")
        {
            for (int i = 0; i < positions.Length; i++)
            {
                GameObject newElement = Instantiate(SaplingPrefab, positions[i], rotations[i]);
                Destroy(newElement.GetComponent<ColorChange>());
                newElement.AddComponent<PickUpPopUp>();
                newElement.AddComponent<PickUp>();
                newElement.AddComponent<RegrowSaplings>();
                newElement.name = saplingName;
                newElement.transform.parent = parent;
                CanvasReSizer(newElement, 0.2f);
            }
        }

        if (name == "death")
        {
            for (int i = 0; i < positions.Length; i++)
            {
                GameObject newElement = Instantiate(graveStonePrefab, positions[i], rotations[i]);
                TurnDictionaryToInventory(invetoryItemsArrayGraveStone[i], "death", newElement);
                newElement.name = graveStoneName;
                newElement.transform.parent = parent;
            }
        }

        if (name == "deer")
        {
            for (int i = 0; i < positions.Length; i++)
            {
                GameObject newElement = Instantiate(CheckDeerName(names[i]), positions[i], rotations[i]);
                newElement.transform.localScale = deerScales[i];
                newElement.name = names[i];
                newElement.transform.parent = parent;
                CanvasReSizer(newElement, 0.2f);
            }
        }

        if (name == "fox")
        {
            for (int i = 0; i < positions.Length; i++)
            {
                GameObject newElement = Instantiate(CheckFoxName(names[i]), positions[i], rotations[i]);
                newElement.transform.localScale = foxScales[i];
                newElement.name = names[i];
                newElement.transform.parent = parent;
                CanvasReSizer(newElement, 0.2f);
            }
        }

        if (name == "bear")
        {
            for (int i = 0; i < positions.Length; i++)
            {
                GameObject newElement = Instantiate(CheckBearName(names[i]), positions[i], rotations[i]);
                newElement.transform.localScale = bearScales[i];
                newElement.name = names[i];
                newElement.transform.parent = parent;
                CanvasReSizer(newElement, 0.2f);
            }
        }

        if (name == "ship")
        {
            for (int i = 0; i < positions.Length; i++)
            {
                GameObject newElement = Instantiate(PrisonShipPrefab, positions[i], rotations[i]);
                newElement.name = names[i];
                newElement.transform.parent = parent;
            }
        }

        if (name == "resident")
        {
            for (int i = 0; i < positions.Length; i++)
            {
                GameObject newElement = Instantiate(ResidentPrefab, positions[i], rotations[i]);
                newElement.name = names[i];
                newElement.GetComponent<ResidentStats>().joinedTown = JoinedTown[i];
                newElement.GetComponent<ResidentStats>().FavoriteItem = StringToItem(FavoriteItem[i]);
                newElement.GetComponent<ResidentStats>().Stats = ResidentStats[i];
                newElement.GetComponent<StatBar>().starbar = newElement.transform.Find("Stats").Find("CanvasStats").gameObject;
                newElement.GetComponent<StatBar>().stats = ResidentStats[i];
                newElement.GetComponent<StatBar>().UpdateStats();
                newElement.transform.parent = parent;
            }
        }

        if (name == "building")
        {
            for (int i = 0; i < positions.Length; i++)
            {
                GameObject newElement = Instantiate(CheckBuildingName(names[i]), positions[i], rotations[i]);
                newElement.name = names[i];
                newElement.transform.parent = parent;

                Destroy(newElement.GetComponent<Rigidbody>());
                for (int i2 = 0; i2 < newElement.transform.childCount; i2++)
                {
                    newElement.transform.GetChild(i).gameObject.GetComponent<MeshCollider>().convex = true;
                    newElement.transform.GetChild(i).GetComponent<MeshCollider>().enabled = true;
                }
                newElement.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }

    public void CanvasReSizer(GameObject newElement, float canvasSize)
    {
        try
        {
            newElement.transform.Find("Canvas").transform.localScale = new Vector3(canvasSize, canvasSize, canvasSize);
        }
        catch
        {

        }
    }

    public void SpawnDropables(Vector3[] positions, Quaternion[] rotations, string[] names)
    {
        for(int i = 0; i < positions.Length; i++)
        {
            ItemWorld.SpawnItemWorld(positions[i], new Item { itemType = CheckStringsIntoItemNamesObjects(names[i]), amount = 1 }, rotations[i]);
        }
    }

    public void SetCollectablePosition(Vector3[] positions, Transform parent)
    {
        for(int i = 0; i < parent.childCount; i++)
        {
            parent.GetChild(i).transform.position = positions[i];
        }
    }

    public void SetCollectableRotation(Quaternion[] rotations, Transform parent)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            parent.GetChild(i).transform.rotation = rotations[i];
        }
    }

    public void SetHotBarItem(Vector3[] positions, Quaternion[] rotations, bool[] active)
    {
        for (int i = 0; i < HotBarItemParent.transform.childCount; i++)
        {
            HotBarItemParent.transform.GetChild(i).transform.position = positions[i];
            HotBarItemParent.transform.GetChild(i).transform.rotation = rotations[i];
            HotBarItemParent.transform.GetChild(i).gameObject.SetActive(active[i]);

            if (!active[i])
            {
                HotBarMenuItems[i].SetActive(true);
            }
        }
    }

    public void TreeShader(GameObject newElement, int num)
    {
        Color color = treeColors[num];
        for (int i = 0; i < newElement.transform.childCount; i++)
        {
            if (newElement.transform.GetChild(i).name.Contains("Leaves"))
            {
                Renderer rend = newElement.transform.GetChild(i).GetComponent<Renderer>();
                rend.material = new Material(shader);
                rend.material.SetColor("Color_8DC4F919", color);
                rend.material.SetFloat("Vector1_4FA07C5C", 20);
                rend.material.SetVector("Vector2_D588F14A", new Vector2(0.2f, 0.5f));
            }
        }
    }

    public void RockShader(GameObject newElement, int num)
    {
        Color color = rockColors[num];
        for (int i = 0; i < newElement.transform.childCount; i++)
        {
            if (newElement.transform.GetChild(i).name.Contains("Rock"))
            {
                Renderer rend = newElement.transform.GetChild(i).GetComponent<Renderer>();
                rend.material.color = color;
            }
        }
    }

    public GameObject CheckTreeName(string name)
    {
        if(name == "tree1")
        {
            return treePrefabs[0];
        }
        else if(name == "tree2")
        {
            return treePrefabs[1];
        }
        else 
        {
            return null;
        }
    }

    public GameObject CheckRockName(string name)
    {
        if (name == "rock6")
        {
            return rockPrefabs[0];
        }
        else if (name == "rock5")
        {
            return rockPrefabs[1];
        }else
        {
            return null;
        }
    }

    public GameObject CheckStumpName(string name)
    {
        if (name == "treeStump")
        {
            return stumpPrefabs[0];
        }
        else
        {
            return null;
        }
    }

    public GameObject CheckFallenName(string name)
    {
        if (name == "FallenTree")
        {
            return fallenPrefabs[0];
        }
        else
        {
            return null;
        }
    }

    public GameObject CheckSmallRockName(string name)
    {
        if (name == "rockSingle1")
        {
            return smallRockPrefabs[0];
        }
        else
        {
            return null;
        }
    }

    public GameObject CheckFlowerName(string name)
    {
        if (name == "flower")
        {
            return flowerPrefabs[0];
        }
        else
        {
            return null;
        }
    }

    public GameObject CheckMushroomName(string name)
    {
        if (name == "mushroom")
        {
            return mushroomPrefabs[0];
        }
        else
        {
            return null;
        }
    }

    public GameObject CheckBerryName(string name)
    {
        if (name == "BerryBush" || name  == "BerryBush(regrow)")
        {
            return BerryPrefabs[0];
        }
        else
        {
            return null;
        }
    }

    public GameObject CheckDeerName(string name)
    {
        if (name == "DeerAnimate")
        {
            return deerPrefabs[0];
        }
        else
        {
            return null;
        }
    }

    public GameObject CheckFoxName(string name)
    {
        if (name == "FoxAnimate")
        {
            return foxPrefabs[0];
        }
        else
        {
            return null;
        }
    }

    public GameObject CheckBearName(string name)
    {
        if (name == "BearAnimate")
        {
            return bearPrefabs[0];
        }
        else
        {
            return null;
        }
    }

    public GameObject CheckBuildingName(string name)
    {
        if (name.Contains("Campfire"))
        {
            return buildingPrefabs[0];
        }
        else if (name.Contains("Tent"))
        {
            return buildingPrefabs[1];
        }
        else 
        { 
            return null;
        }
    }

    private void AddItemToInventory(string[] itemNames, int[] amount)
    {
        for(int i = 0; i < itemNames.Length; i++)
        {
            inventory.AddItem(new Item { itemType = CheckStringsIntoItemNames(itemNames[i]), amount = amount[i] });
        }
    }

    private void TurnStringArrayIntoDictonaries(ref Dictionary<Item.ItemType[], int[]>[] OriginalGraveStoneInventory, string[] inventoryItems, int[] inventoryAmounts)
    {
        int size = 0;
        int arrayIndex = 0;
        int arrayIndex2 = 0;

        foreach(int number in inventoryAmounts)
        {
            if(number == 0)
            {
                size++;
            }
        }

        OriginalGraveStoneInventory = new Dictionary<Item.ItemType[], int[]>[size];

        for (int i = 0; i < OriginalGraveStoneInventory.Length; i++)
        {
            Dictionary<Item.ItemType[], int[]> allItems = new Dictionary<Item.ItemType[], int[]>();

            int arraySize = 0;
            for (int j = arrayIndex; j < inventoryItems.Length; j++)
            {
                arraySize++;
                if (inventoryAmounts[j] == 0)
                {
                    arraySize--;
                    break;
                }
            }

            Item.ItemType[] items = new Item.ItemType[arraySize];
            int[] amounts = new int[arraySize];

            for (int j = 0; j < arraySize; j++)
            {
                arrayIndex++;
                items[j] = CheckStringsIntoItemNames(inventoryItems[arrayIndex2]);
                amounts[j] = inventoryAmounts[arrayIndex2];
                arrayIndex2++;
            }

            arrayIndex2++;
            arrayIndex++;

            allItems.Add(items, amounts);
            OriginalGraveStoneInventory[i] = allItems;
            allItems = null;
        }
    }

    private void TurnDictionaryToInventory(Dictionary<Item.ItemType[], int[]> inventories, string scriptName, GameObject gameObject)
    {
        Inventory inventory = null;

        if (scriptName == "death")
        {
            inventory = gameObject.GetComponent<DeathCollider>().inventory;
        }

        foreach (var kvp in inventories)
        {
            for (int j = 0; j < kvp.Value.Length; j++)
            {
                inventory.AddItem(new Item { itemType = kvp.Key[j], amount = kvp.Value[j] });
            }
        }
    }

    private Item.ItemType CheckStringsIntoItemNames(string item)
    {
        switch (item)
        {
            default:
            case "rock": return Item.ItemType.rock;
            case "wood": return Item.ItemType.wood;
            case "mushroom": return Item.ItemType.mushroom;
            case "flower": return Item.ItemType.flower;
            case "berry": return Item.ItemType.berry;
            case "bush": return Item.ItemType.bush;
            case "sapling": return Item.ItemType.sapling;
        }
    }

    private Item.ItemType CheckStringsIntoItemNamesObjects(string item)
    {
        switch (item)
        {
            default:
            case "rockDropable": return Item.ItemType.rock;
            case "woodDropable": return Item.ItemType.wood;
            case "mushroomDropable": return Item.ItemType.mushroom;
            case "flowerDropable": return Item.ItemType.flower;
            case "BerrySoloDropable": return Item.ItemType.berry;
            case "BerryBushSoloDropable": return Item.ItemType.bush;
            case "SaplingDropable": return Item.ItemType.sapling;
        }
    }

    private string ItemToString(Item.ItemType item)
    {
        switch (item)
        {
            default:
            case Item.ItemType.berry: return "berry";
            case Item.ItemType.bush: return "bush";
            case Item.ItemType.flower: return "flower";
            case Item.ItemType.mushroom: return "mushroom";
            case Item.ItemType.rock: return "rock";
            case Item.ItemType.sapling: return "sapling";
            case Item.ItemType.wood: return "wood";
        }
    }

    private Item.ItemType StringToItem(string item)
    {
        switch (item)
        {
            default:
            case "berry": return Item.ItemType.berry;
            case "bush": return Item.ItemType.bush;
            case "flower": return Item.ItemType.flower;
            case "mushroom": return Item.ItemType.mushroom;
            case "rock": return Item.ItemType.rock;
            case "sapling": return Item.ItemType.sapling;
            case "wood": return Item.ItemType.wood;
        }
    }

    private void ClearGameObjects()
    {
        for (int i = 0; i < TerrainObjects.transform.childCount - 2; i++)
        {
            foreach (Transform child in TerrainObjects.transform.GetChild(i))
            {
                Destroy(child.gameObject);
            }
        }
    }
}
