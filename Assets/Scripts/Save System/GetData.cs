using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GetData : MonoBehaviour
{
    [Header("Save Game")]
    public TextMeshProUGUI notifiation;

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

    public string[] MineName;
    public Vector3[] MinePosition;
    public Quaternion[] MineRotation;
    public string[] MineItems;
    public int[] MineAmounts;

    public string[] LumbermillName;
    public Vector3[] LumbermillPosition;
    public Quaternion[] LumbermillRotation;
    public string[] LumbermillItems;
    public int[] LumbermillAmounts;

    public string[] FarmName;
    public Vector3[] FarmPosition;
    public Quaternion[] FarmRotation;
    public string[] FarmItems;
    public int[] FarmAmounts;

    public string[] WallName;
    public Vector3[] WallPosition;
    public Quaternion[] WallRotation;

    public string[] DoorName;
    public Vector3[] DoorPosition;
    public Quaternion[] DoorRotation;

    public string[] ChestName;
    public Vector3[] ChestPosition;
    public Quaternion[] ChestRotation;
    public string[] ChestItems;
    public int[] ChestAmounts;

    public string[] MesshallName;
    public Vector3[] MesshallPosition;
    public Quaternion[] MesshallRotation;
    public string[] MesshallItemsRaw;
    public int[] MesshallAmountsRaw;
    public string[] MesshallItemsCooked;
    public int[] MesshallAmountsCooked;
    public string[] MesshallFarm;

    public string[] tavernName;
    public Vector3[] tavernPosition;
    public Quaternion[] tavernRotation;

    public string[] towerName;
    public Vector3[] towerPosition;
    public Quaternion[] towerRotation;

    public string[] knightHutName;
    public Vector3[] knightHutPosition;
    public Quaternion[] knightHutRotation;
    //public Inventory[] inventoryChest;
    //public string[] inventoryItemNamesChest;
    //public int[] inventoryItemAmountsChest;
    //public Dictionary<Item.ItemType[], int[]>[] invetoryItemsArrayChest;

    [Header("Player Data")]
    public GameObject Player;
    public PlayerController playerController;
    public Vector3 playerPosition;
    public Quaternion playerRotation;
    public Vector3 playerRespawnPoint;
    public float health;
    public float hunger;
    public Health playerHealth;
    public Hunger playerHunger;

    [Header("Settings Data")]
    public MouseLook mouseLook;

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
    public InventoryManager inventory;
    public string[] inventoryItemNames;
    public int[] inventoryItemAmounts;

    [Header("Gravestone Data")]
    public GameObject graveStoneParent;
    public Vector3[] graveStonePosition;
    public Quaternion[] graveStoneRotation;
    public GameObject graveStonePrefab;
    public string[] graveStoneItems;
    public int[] graveStoneAmounts;
    //public string graveStoneName;

    //public Inventory[] inventoryGraveStone;
    //public string[] inventoryItemNamesGraveStone;
    //public int[] inventoryItemAmountsGraveStone;
    //public Dictionary<Item.ItemType[], int[]>[] invetoryItemsArrayGraveStone;

    [Header("Resident Data")]
    public GameObject ResidentParent;
    public string[] ResidentName;
    public int[][] ResidentStats;
    public bool[] JoinedTown;
    public bool[] IsResidentWorking;
    public string[] FavoriteItem;
    public string[] ResidentHome;
    public string[] ResidentJob;
    public int[][] ResidentSchedule;
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
    public bool AccessToLevel3Buildings;

    public StoryManager storyManager;
    public bool SpawnPrisonShips;
    public int SpawnPrisonShipDay;

    [Header("Enemies")]
    public Transform enemyParent;

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
        return playerController.sensitivity;
    }

    //Get Player Data
    public Vector3 GetPlayerPosition()
    {
        playerPosition = Player.transform.position;
        return playerPosition;
    }

    public Vector3 GetPlayerRespawnPoint()
    {
        playerRespawnPoint = playerController.respawnPoint;
        return playerRespawnPoint;
    }

    public Quaternion GetPlayerRotation()
    {
        playerRotation = Player.transform.rotation;
        return playerRotation;
    }

    public float getPlayerHealth()
    {
        health = playerHealth.currentHealth;
        return health;
    }

    public float getPlayerFood()
    {
        hunger = playerHunger.currentHunger;
        return hunger;
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

    public bool[] GetResidentIsWorking()
    {
        IsResidentWorking = new bool[ResidentParent.transform.childCount];

        for (int i = 0; i < IsResidentWorking.Length; i++)
        {
            if (ResidentParent.transform.GetChild(i).GetComponent<LumberWorker>() != null)
            {
                IsResidentWorking[i] = true;
            }
        }

        return IsResidentWorking;
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

    public string[] GetResidentHome()
    {
        ResidentHome = new string[ResidentParent.transform.childCount];

        for (int i = 0; i < ResidentHome.Length; i++)
        {
            if (ResidentParent.transform.GetChild(i).GetComponent<ResidentScheudle>().home != null)
            {
                ResidentHome[i] = ResidentParent.transform.GetChild(i).GetComponent<ResidentScheudle>().home.name;
            }
            else
            {
                ResidentHome[i] = "null";
            }
        }

        return ResidentHome;
    }

    public string[] GetResidentJob()
    {
        ResidentJob = new string[ResidentParent.transform.childCount];

        for (int i = 0; i < ResidentJob.Length; i++)
        {
            if (ResidentParent.transform.GetChild(i).GetComponent<ResidentScheudle>().job != null)
            {
                ResidentJob[i] = ResidentParent.transform.GetChild(i).GetComponent<ResidentScheudle>().job.name;
            }
            else
            {
                ResidentJob[i] = "null";
            }
        }

        return ResidentJob;
    }

    //Get resident Scheudle
    public int[][] GetResidentSchedule()
    {
        ResidentSchedule = new int[ResidentParent.transform.childCount][];

        for (int i = 0; i < ResidentSchedule.Length; i++)
        {
            ResidentSchedule[i] = ResidentParent.transform.GetChild(i).GetComponent<ResidentScheudle>().Schedule;
        }

        return ResidentSchedule;
    }

    //Get death Data
    public Vector3[] GetGravestonePosition()
    {
        return GetPositions(ref graveStonePosition, graveStoneParent);
    }

    public Quaternion[] GetGravestoneRotation()
    {
        return GetRotations(ref graveStoneRotation, graveStoneParent);
    }

    public string[] GetGravestoneItems()
    {
        List<string> items = new List<string>();

        for(int i = 0; i < graveStoneParent.transform.childCount; i++)
        {
            Gravestone gravestone = graveStoneParent.transform.GetChild(i).GetComponent<Gravestone>();
            if (gravestone.items.Length == 0)
            {
                items.Add(ItemToString(Item.ItemType.nothing));
                items.Add(ItemToString(Item.ItemType.empty));
                continue;
            }

            for (int j = 0; j < gravestone.items.Length; j++)
            {
                items.Add(ItemToString(gravestone.items[j].itemType));
            }

            items.Add(ItemToString(Item.ItemType.empty));
        }

        graveStoneItems = items.ToArray();
        return items.ToArray();
    }

    public int[] GetGravestoneAmounts()
    {
        List<int> amounts = new List<int>();

        for (int i = 0; i < graveStoneParent.transform.childCount; i++)
        {
            Gravestone gravestone = graveStoneParent.transform.GetChild(i).GetComponent<Gravestone>();
            if (gravestone.amounts.Length == 0)
            {
                amounts.Add(0);
                amounts.Add(-1);
                continue;
            }

            for (int j = 0; j < gravestone.amounts.Length; j++)
            {
                amounts.Add(gravestone.amounts[j]);
            }

            amounts.Add(-1);
        }

        graveStoneAmounts = amounts.ToArray();
        return amounts.ToArray();
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

    //Get mine Data
    public Vector3[] GetMinePosition()
    {
        return GetPositions(ref MinePosition, buildingsParent.transform.GetChild(2).gameObject);
    }

    public Quaternion[] GetMineRotation()
    {
        return GetRotations(ref MineRotation, buildingsParent.transform.GetChild(2).gameObject);
    }

    public string[] GetMineItems()
    {
        List<string> items = new List<string>();

        for (int i = 0; i < buildingsParent.transform.GetChild(2).transform.childCount; i++)
        {
            Mine mine = buildingsParent.transform.GetChild(2).GetChild(i).GetComponent<Mine>();
            Item.ItemType[] array = mine.ReturnItemListType();

            for (int j = 0; j < array.Length; j++)
            {
                items.Add(ItemToString(array[j]));
            }

            items.Add(ItemToString(Item.ItemType.nothing));
        }

        MineItems = items.ToArray();
        return items.ToArray();
    }

    public int[] GetMineAmounts()
    {
        List<int> items = new List<int>();

        for (int i = 0; i < buildingsParent.transform.GetChild(2).transform.childCount; i++)
        {
            Mine mine = buildingsParent.transform.GetChild(2).GetChild(i).GetComponent<Mine>();
            int[] array = mine.ReturnItemListAmount();

            for (int j = 0; j < array.Length; j++)
            {
                items.Add(array[j]);
            }

            items.Add(-1);
        }

        MineAmounts = items.ToArray();
        return items.ToArray();
    }

    public string[] GetMineNames()
    {
        return GetNames(ref MineName, buildingsParent.transform.GetChild(2).gameObject);
    }

    //Get lumbermill Data
    public Vector3[] GetLumbermillPosition()
    {
        return GetPositions(ref LumbermillPosition, buildingsParent.transform.GetChild(3).gameObject);
    }

    public Quaternion[] GetLumbermillRotation()
    {
        return GetRotations(ref LumbermillRotation, buildingsParent.transform.GetChild(3).gameObject);
    }

    public string[] GetLumbermillNames()
    {
        return GetNames(ref LumbermillName, buildingsParent.transform.GetChild(3).gameObject);
    }

    public string[] GetLumbermillItems()
    {
        List<string> items = new List<string>();

        for (int i = 0; i < buildingsParent.transform.GetChild(3).transform.childCount; i++)
        {
            Lumbermill lumbermill = buildingsParent.transform.GetChild(3).GetChild(i).GetComponent<Lumbermill>();
            Item.ItemType[] array = lumbermill.ReturnItemListType();

            for (int j = 0; j < array.Length; j++)
            {
                items.Add(ItemToString(array[j]));
            }

            items.Add(ItemToString(Item.ItemType.nothing));
        }

        LumbermillItems = items.ToArray();
        return items.ToArray();
    }

    public int[] GetLumbermillAmounts()
    {
        List<int> items = new List<int>();

        for (int i = 0; i < buildingsParent.transform.GetChild(3).transform.childCount; i++)
        {
            Lumbermill lumbermill = buildingsParent.transform.GetChild(3).GetChild(i).GetComponent<Lumbermill>();
            int[] array = lumbermill.ReturnItemListAmount();

            for (int j = 0; j < array.Length; j++)
            {
                items.Add(array[j]);
            }

            items.Add(-1);
        }

        LumbermillAmounts = items.ToArray();
        return items.ToArray();
    }

    //Get Farm Data
    public Vector3[] GetFarmPosition()
    {
        return GetPositions(ref FarmPosition, buildingsParent.transform.GetChild(4).gameObject);
    }

    public Quaternion[] GetFarmRotation()
    {
        return GetRotations(ref FarmRotation, buildingsParent.transform.GetChild(4).gameObject);
    }

    public string[] GetFarmNames()
    {
        return GetNames(ref FarmName, buildingsParent.transform.GetChild(4).gameObject);
    }

    public string[] GetFarmItems()
    {
        List<string> items = new List<string>();

        for (int i = 0; i < buildingsParent.transform.GetChild(4).transform.childCount; i++)
        {
            Farm farm = buildingsParent.transform.GetChild(4).GetChild(i).GetComponent<Farm>();
            Item.ItemType[] array = farm.ReturnItemListType();

            for (int j = 0; j < array.Length; j++)
            {
                items.Add(ItemToString(array[j]));
            }

            items.Add(ItemToString(Item.ItemType.nothing));
        }

        FarmItems = items.ToArray();
        return items.ToArray();
    }

    public int[] GetFarmAmounts()
    {
        List<int> items = new List<int>();

        for (int i = 0; i < buildingsParent.transform.GetChild(4).transform.childCount; i++)
        {
            Farm farm = buildingsParent.transform.GetChild(4).GetChild(i).GetComponent<Farm>();
            int[] array = farm.ReturnItemListAmount();

            for (int j = 0; j < array.Length; j++)
            {
                items.Add(array[j]);
            }

            items.Add(-1);
        }

        FarmAmounts = items.ToArray();
        return items.ToArray();
    }

    //Get wall Data
    public Vector3[] GetWallPosition()
    {
        return GetPositions(ref WallPosition, buildingsParent.transform.GetChild(5).gameObject);
    }

    public Quaternion[] GetWallRotation()
    {
        return GetRotations(ref WallRotation, buildingsParent.transform.GetChild(5).gameObject);
    }

    public string[] GetWallNames()
    {
        return GetNames(ref WallName, buildingsParent.transform.GetChild(5).gameObject);
    }

    //Get door Data
    public Vector3[] GetDoorPosition()
    {
        return GetPositions(ref DoorPosition, buildingsParent.transform.GetChild(6).gameObject);
    }

    public Quaternion[] GetDoorRotation()
    {
        return GetRotations(ref DoorRotation, buildingsParent.transform.GetChild(6).gameObject);
    }

    public string[] GetDoorNames()
    {
        return GetNames(ref DoorName, buildingsParent.transform.GetChild(6).gameObject);
    }

    //Get chest Data
    public Vector3[] GetChestPosition()
    {
        return GetPositions(ref ChestPosition, buildingsParent.transform.GetChild(7).gameObject);
    }

    public Quaternion[] GetChestRotation()
    {
        return GetRotations(ref ChestRotation, buildingsParent.transform.GetChild(7).gameObject);
    }

    public string[] GetChestNames()
    {
        return GetNames(ref ChestName, buildingsParent.transform.GetChild(7).gameObject);
    }

    public string[] GetChestItems()
    {
        List<string> items = new List<string>();

        for (int i = 0; i < buildingsParent.transform.GetChild(7).transform.childCount; i++)
        {
            Chest chest = buildingsParent.transform.GetChild(7).GetChild(i).GetComponent<Chest>();
            Item.ItemType[] array = chest.ReturnItemListType();

            for (int j = 0; j < array.Length; j++)
            {
                items.Add(ItemToString(array[j]));
            }

            items.Add(ItemToString(Item.ItemType.nothing));
        }

        ChestItems = items.ToArray();
        return items.ToArray();
    }

    public int[] GetChestAmounts()
    {
        List<int> items = new List<int>();

        for (int i = 0; i < buildingsParent.transform.GetChild(7).transform.childCount; i++)
        {
            Chest chest = buildingsParent.transform.GetChild(7).GetChild(i).GetComponent<Chest>();
            int[] array = chest.ReturnItemListAmount();

            for (int j = 0; j < array.Length; j++)
            {
                items.Add(array[j]);
            }

            items.Add(-1);
        }

        ChestAmounts = items.ToArray();
        return items.ToArray();
    }

    //Get messhall Data
    public Vector3[] GetMesshallPosition()
    {
        return GetPositions(ref MesshallPosition, buildingsParent.transform.GetChild(8).gameObject);
    }

    public Quaternion[] GetMesshallRotation()
    {
        return GetRotations(ref MesshallRotation, buildingsParent.transform.GetChild(8).gameObject);
    }

    public string[] GetMesshallNames()
    {
        return GetNames(ref MesshallName, buildingsParent.transform.GetChild(8).gameObject);
    }

    public string[] GetMesshallItemsRaw()
    {
        List<string> items = new List<string>();

        for (int i = 0; i < buildingsParent.transform.GetChild(8).transform.childCount; i++)
        {
            Messhall messhall = buildingsParent.transform.GetChild(8).GetChild(i).GetComponent<Messhall>();
            Item.ItemType[] array = messhall.ReturnItemListType(messhall.rawFoodSlots);

            for (int j = 0; j < array.Length; j++)
            {
                items.Add(ItemToString(array[j]));
            }

            items.Add(ItemToString(Item.ItemType.nothing));
        }

        MesshallItemsRaw = items.ToArray();
        return items.ToArray();
    }

    public int[] GetMesshallAmountsRaw()
    {
        List<int> items = new List<int>();

        for (int i = 0; i < buildingsParent.transform.GetChild(8).transform.childCount; i++)
        {
            Messhall messhall = buildingsParent.transform.GetChild(8).GetChild(i).GetComponent<Messhall>();
            int[] array = messhall.ReturnItemListAmount(messhall.rawFoodSlots);

            for (int j = 0; j < array.Length; j++)
            {
                items.Add(array[j]);
            }

            items.Add(-1);
        }

        MesshallAmountsRaw = items.ToArray();
        return items.ToArray();
    }

    public string[] GetMesshallItemsCooked()
    {
        List<string> items = new List<string>();

        for (int i = 0; i < buildingsParent.transform.GetChild(8).transform.childCount; i++)
        {
            Messhall messhall = buildingsParent.transform.GetChild(8).GetChild(i).GetComponent<Messhall>();
            Item.ItemType[] array = messhall.ReturnItemListType(messhall.cookedFoodSlots);

            for (int j = 0; j < array.Length; j++)
            {
                items.Add(ItemToString(array[j]));
            }

            items.Add(ItemToString(Item.ItemType.nothing));
        }

        MesshallItemsCooked = items.ToArray();
        return items.ToArray();
    }

    public int[] GetMesshallAmountsCooked()
    {
        List<int> items = new List<int>();

        for (int i = 0; i < buildingsParent.transform.GetChild(8).transform.childCount; i++)
        {
            Messhall messhall = buildingsParent.transform.GetChild(8).GetChild(i).GetComponent<Messhall>();
            int[] array = messhall.ReturnItemListAmount(messhall.cookedFoodSlots);

            for (int j = 0; j < array.Length; j++)
            {
                items.Add(array[j]);
            }

            items.Add(-1);
        }

        MesshallAmountsCooked = items.ToArray();
        return items.ToArray();
    }

    public string[] GetMesshallFarm()
    {
        MesshallFarm = new string[buildingsParent.transform.GetChild(8).transform.childCount];

        for (int i = 0; i < MesshallFarm.Length; i++)
        {
            if (buildingsParent.transform.GetChild(8).transform.GetChild(i).GetComponent<Messhall>().farm != null)
            {
                MesshallFarm[i] = buildingsParent.transform.GetChild(8).transform.GetChild(i).GetComponent<Messhall>().farm.name;
            }
            else
            {
                MesshallFarm[i] = "null";
            }
        }

        return MesshallFarm;
    }

    //Get Tavern Data
    public Vector3[] GetTavernPosition()
    {
        return GetPositions(ref tavernPosition, buildingsParent.transform.GetChild(9).gameObject);
    }

    public Quaternion[] GetTavernRotation()
    {
        return GetRotations(ref tavernRotation, buildingsParent.transform.GetChild(9).gameObject);
    }

    public string[] GetTavernNames()
    {
        return GetNames(ref tavernName, buildingsParent.transform.GetChild(9).gameObject);
    }

    public Vector3[] GetTowerPosition()
    {
        return GetPositions(ref towerPosition, buildingsParent.transform.GetChild(10).gameObject);
    }

    public Quaternion[] GetTowerRotation()
    {
        return GetRotations(ref towerRotation, buildingsParent.transform.GetChild(10).gameObject);
    }

    public string[] GetTowerNames()
    {
        return GetNames(ref towerName, buildingsParent.transform.GetChild(10).gameObject);
    }


    public Vector3[] GetKnightHutPosition()
    {
        return GetPositions(ref knightHutPosition, buildingsParent.transform.GetChild(11).gameObject);
    }

    public Quaternion[] GetKnightHutRotation()
    {
        return GetRotations(ref knightHutRotation, buildingsParent.transform.GetChild(11).gameObject);
    }

    public string[] GetKnightHutNames()
    {
        return GetNames(ref knightHutName, buildingsParent.transform.GetChild(11).gameObject);
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
            //first if statement checks if its an resident, whos working.  then makes the location saved the location before he enters the job (not fully tested)
            if(parent.transform.GetChild(i).GetComponent<Farmer>() != null)
            {
                positions[i] = parent.transform.GetChild(i).GetComponent<Farmer>().locationEntered;
            }else if (parent.transform.GetChild(i).GetComponent<Chef>() != null && parent.transform.GetChild(i).GetComponent<Chef>().Cooking)
            {
                positions[i] = parent.transform.GetChild(i).GetComponent<Chef>().locationEntered;
            }else if (parent.transform.GetChild(i).GetComponent<Archer>() != null)
            {
                positions[i] = parent.transform.GetChild(i).GetComponent<Archer>().locationEntered;
            }
            else
            {
                positions[i] = parent.transform.GetChild(i).position;
            }
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

    public int[][] GetStorage(ref int[][] storage, string StorageType,GameObject parent)
    {
        storage = new int[parent.transform.childCount][];

        for (int i = 0; i < storage.Length; i++)
        {
           //if (StorageType == "Mine") storage[i] = parent.transform.GetChild(i).GetComponent<Mine>().Drops;
           //if (StorageType == "Lumbermill") storage[i] = parent.transform.GetChild(i).GetComponent<Lumbermill>().Drops;
           //if (StorageType == "Farm") storage[i] = parent.transform.GetChild(i).GetComponent<Farm>().Drops;
        }

        return storage;
    }

    //Inventory stuff
    public string[] GetInventoryItemType()
    {
        return inventory.ReturnItemListTypeString();
    }

    public int[] GetInventoryItemAmount()
    {
        return inventory.ReturnItemListAmount();
    }

    /*public Inventory[] GetArrayInventory(ref Inventory[] inventoryArray, GameObject parent, string scriptname)
    {
        inventoryArray = new Inventory[parent.transform.childCount];

        if(scriptname == "death")
        {
            for (int i = 0; i < inventoryArray.Length; i++)
            {
                inventoryArray[i] = parent.transform.GetChild(i).GetComponent<DeathCollider>().inventory;
            }
        }else if (scriptname == "chest")
        {
            for (int i = 0; i < inventoryArray.Length; i++)
            {
                inventoryArray[i] = parent.transform.GetChild(i).GetComponent<Chest>().inventory;
            }
        }

        return inventoryArray;
    }

    /*public Dictionary<Item.ItemType[], int[]>[] GetDictionaryOfInventories(ref Dictionary<Item.ItemType[], int[]>[] invetoryData, ref Inventory[] inventoryArray, GameObject parent, string scriptname)
    {
        invetoryData = new Dictionary<Item.ItemType[], int[]>[parent.transform.childCount];
        inventoryArray = new Inventory[parent.transform.childCount];

        if (scriptname == "death")
        {
            for (int i = 0; i < inventoryArray.Length; i++)
            {
                inventoryArray[i] = parent.transform.GetChild(i).GetComponent<DeathCollider>().inventory;
            }
        }else if (scriptname == "chest")
        {
            for (int i = 0; i < inventoryArray.Length; i++)
            {
                inventoryArray[i] = parent.transform.GetChild(i).GetComponent<Chest>().inventory;
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
    }*/

    /*public Dictionary<Item.ItemType[], int[]>[] GraveStoneData()
    {
        return GetDictionaryOfInventories(ref invetoryItemsArrayGraveStone, ref inventoryGraveStone, graveStoneParent, "death");
    }

    public Dictionary<Item.ItemType[], int[]>[] ChestData()
    {
        return GetDictionaryOfInventories(ref invetoryItemsArrayChest, ref inventoryChest, buildingsParent.transform.GetChild(7).gameObject, "chest");
    }*/

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

    public bool GetAccessToLevel3Buildings()
    {
        AccessToLevel3Buildings = buildMenuUpdater.AccessToLevel3Buildings;
        return AccessToLevel3Buildings;
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
        if(enemyParent.childCount != 0)
        {
            storyManager.Error.gameObject.SetActive(true);
            return;
        }

        notifiation.gameObject.SetActive(true);
        SaveSystem.SaveData(this);
    }

    public void ClearData()
    {
        SaveSystem.WipeData();
    }

    public void LoadData()
    {
        ClearGameObjects();
        GameData data = SaveSystem.LoadData();

        //Terrain Data
        seed = data.seed;
        noiseData.seed = seed;

        //Player Data
        hunger = data.food;
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

        playerHealth.currentHealth = (int)health;
        playerHunger.currentHunger = (int)hunger;
        playerHealth.ModifyHealth(0); //this is to set the bar to right number
        playerHunger.ModifyHunger(0); //this is to set the bar to right number

        playerController.respawnPoint = playerRespawnPoint;
        Player.transform.position = playerPosition;
        Player.transform.rotation = playerRotation;

        //Story Data
        AccessToLevel0Buildings = data.AccessToLevel0Buildings;
        AccessToLevel1Buildings = data.AccessToLevel1Buildings;
        AccessToLevel2Buildings = data.AccessToLevel2Buildings;
        AccessToLevel3Buildings = data.AccessToLevel3Buildings;

        buildMenuUpdater.AccessToLevel0Buildings = AccessToLevel0Buildings;
        buildMenuUpdater.AccessToLevel1Buildings = AccessToLevel1Buildings;
        buildMenuUpdater.AccessToLevel2Buildings = AccessToLevel2Buildings;
        buildMenuUpdater.AccessToLevel3Buildings = AccessToLevel3Buildings;

        SpawnPrisonShips = data.SpawnPrisonShips;
        SpawnPrisonShipDay = data.SpawnPrisonShipDay;

        storyManager.SpawnPrisonShips = SpawnPrisonShips;
        storyManager.SpawnPrisonShipDay = SpawnPrisonShipDay;

        //Settings
        playerController.sensitivity = data.sensitivity;

        //Inventory Data
        inventoryItemAmounts = data.playerInventoryAmount;
        inventoryItemNames = data.playerInventoryItems;
        AddItemToInventory(inventoryItemNames, inventoryItemAmounts);

        //Gravestone Data
        //inventoryItemAmountsGraveStone = data.inventoryItemAmountsGraveStone;
        //inventoryItemNamesGraveStone = data.inventoryItemNamesGraveStone;

        //Day Cycle
        lightingManager.TimeOfDay = data.timeOfDay;
        lightingManager.numberOfDays = data.numberOfDays;

        //Collectable data
        SetActive(data.collectableActive, ref collectableActive);
        for(int i = 0; i < collectableMenu.transform.childCount; i++)
        {
            collectableObjects[i].SetActive(false);
            collectableObjectsButton[i].SetActive(false);

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
        graveStoneItems = data.graveStoneItems;
        graveStoneAmounts = data.graveStoneAmounts;
        SpawnObjects(graveStonePosition, graveStoneRotation, null, graveStoneParent.transform, "death");
        AddItemsToGravestone();

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

        //buildings
        SetPositions(data.CampfirePosition, ref CampfirePosition);
        SetRotations(data.CampfireRotation, ref CampfireRotation);
        SetNames(data.CampfireNames, ref CampfireName);

        SetPositions(data.TentPosition, ref TentPosition);
        SetRotations(data.TentRotation, ref TentRotation);
        SetNames(data.TentNames, ref TentName);

        SetPositions(data.MinePosition, ref MinePosition);
        SetRotations(data.MineRotation, ref MineRotation);
        MineAmounts = data.MineAmounts;
        MineItems = data.MineItems;
        SetNames(data.MineNames, ref MineName);

        SetPositions(data.LumbermillPosition, ref LumbermillPosition);
        SetRotations(data.LumbermillRotation, ref LumbermillRotation);
        LumbermillAmounts = data.LumbermillAmounts;
        LumbermillItems = data.LumbermillItems;
        SetNames(data.LumbermillNames, ref LumbermillName);

        SetPositions(data.FarmPosition, ref FarmPosition);
        SetRotations(data.FarmRotation, ref FarmRotation);
        FarmAmounts = data.FarmAmounts;
        FarmItems = data.FarmItems;
        SetNames(data.FarmNames, ref FarmName);

        SetPositions(data.WallPosition, ref WallPosition);
        SetRotations(data.WallRotation, ref WallRotation);
        SetNames(data.WallNames, ref WallName);

        SetPositions(data.tavernPosition, ref tavernPosition);
        SetRotations(data.tavernRotation, ref tavernRotation);
        SetNames(data.tavernNames, ref tavernName);

        SetPositions(data.towerPosition, ref towerPosition);
        SetRotations(data.towerRotation, ref towerRotation);
        SetNames(data.towerNames, ref towerName);

        SetPositions(data.knightHutPosition, ref knightHutPosition);
        SetRotations(data.knightHutRotation, ref knightHutRotation);
        SetNames(data.knightHutNames, ref knightHutName);

        SetPositions(data.DoorPosition, ref DoorPosition);
        SetRotations(data.DoorRotation, ref DoorRotation);
        SetNames(data.DoorNames, ref DoorName);

        SetPositions(data.ChestPosition, ref ChestPosition);
        SetRotations(data.ChestRotation, ref ChestRotation);
        SetNames(data.ChestNames, ref ChestName);
        ChestAmounts = data.ChestAmounts;
        ChestItems = data.ChestItems;

        //Debug.Log(i + ": " + ChestItems[i] + ": " + ChestAmounts[i]);

        SetPositions(data.MesshallPosition, ref MesshallPosition);
        SetRotations(data.MesshallRotation, ref MesshallRotation);
        SetNames(data.MesshallNames, ref MesshallName);
        MesshallItemsRaw = data.MesshallItemsRaw;
        MesshallAmountsRaw = data.MesshallAmountsRaw;
        MesshallItemsCooked = data.MesshallItemsCooked;
        MesshallAmountsCooked = data.MesshallAmountsCooked;
        MesshallFarm = data.MesshallFarm;
        //inventoryItemAmountsChest = data.inventoryItemAmountsChest;
        //inventoryItemNamesChest = data.inventoryItemNamesChest;
        //TurnStringArrayIntoDictonaries(ref invetoryItemsArrayChest, inventoryItemNamesChest, inventoryItemAmountsChest);

        //ClearGameObjects();
        SpawnObjects(CampfirePosition, CampfireRotation, CampfireName, buildingsParent.transform.GetChild(0), "building");
        SpawnObjects(TentPosition, TentRotation, TentName, buildingsParent.transform.GetChild(1), "building");
        SpawnObjects(MinePosition, MineRotation, MineName, buildingsParent.transform.GetChild(2), "building");
        SpawnObjects(LumbermillPosition, LumbermillRotation, LumbermillName, buildingsParent.transform.GetChild(3), "building");
        SpawnObjects(FarmPosition, FarmRotation, FarmName, buildingsParent.transform.GetChild(4), "building");
        SpawnObjects(WallPosition, WallRotation, WallName, buildingsParent.transform.GetChild(5), "building");;
        SpawnObjects(DoorPosition, DoorRotation, DoorName, buildingsParent.transform.GetChild(6), "building");
        SpawnObjects(ChestPosition, ChestRotation, ChestName, buildingsParent.transform.GetChild(7), "building");
        SpawnObjects(MesshallPosition, MesshallRotation, MesshallName, buildingsParent.transform.GetChild(8), "building");
        SpawnObjects(tavernPosition, tavernRotation, tavernName, buildingsParent.transform.GetChild(9), "building");
        SpawnObjects(towerPosition, towerRotation, towerName, buildingsParent.transform.GetChild(10), "building");
        SpawnObjects(knightHutPosition, knightHutRotation, knightHutName, buildingsParent.transform.GetChild(11), "building");

        //residents
        SetPositions(data.ResidentPosition, ref ResidentPosition);
        SetRotations(data.ResidentRotation, ref ResidentRotation);
        SetNames(data.ResidentName, ref ResidentName);
        ResidentStats = data.ResidentStats;
        ResidentSchedule = data.ResidentSchedule;
        JoinedTown = data.JoinedTown;
        IsResidentWorking = data.IsResidentWorking;
        FavoriteItem = data.FavoriteItem;
        ResidentHome = data.ResidentHome;
        ResidentJob = data.ResidentJob;
        SpawnObjects(ResidentPosition, ResidentRotation, ResidentName, ResidentParent.transform, "resident");

        //Building inventories
        ReturnChestItems();
        ReturnMessHallItems();
        ReturnMineItems();
        ReturnFarmItems();
        ReturnLumbermillItems();
        ReturnMessHallFarm();
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
            /*for (int i = 0; i < positions.Length; i++)
            {
                GameObject newElement = Instantiate(SaplingPrefab, positions[i], rotations[i]);
                Destroy(newElement.GetComponent<ColorChange>());
                newElement.AddComponent<PickUpPopUp>().player = Player;
                newElement.AddComponent<PickUp>();
                //newElement.AddComponent<RegrowSaplings>();
                newElement.name = saplingName;
                newElement.transform.parent = parent;
                CanvasReSizer(newElement, 0.2f);
            }*/

            for (int i = 0; i < positions.Length; i++)
            {
                GameObject newElement = Instantiate(SaplingPrefab, positions[i], rotations[i]);
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

                //TurnDictionaryToInventory(invetoryItemsArrayGraveStone[i], "death", newElement);
                //newElement.name = graveStoneName;
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
                newElement.GetComponent<ResidentScheudle>().Schedule = ResidentSchedule[i];

                newElement.GetComponent<ResidentHealth>().currentHealth = ResidentStats[i][0]; //sets health to health
                newElement.GetComponent<ResidentStats>().StatObject.transform.GetChild(0).GetComponentsInChildren<HealthBar>()[0].SetHeathResident();
                newElement.GetComponent<ResidentHealth>().ModifyHealth(0); //updates healthbar

                newElement.GetComponent<ResidentHunger>().currentHunger = ResidentStats[i][1]; //sets health to health
                newElement.GetComponent<ResidentStats>().StatObject.transform.GetChild(0).GetComponentsInChildren<HungerBar>()[0].SetHungerResident();
                newElement.GetComponent<ResidentHunger>().ModifyHunger(0); //updates healthbar

                //newElement.GetComponent<ResidentScheudle>().AtWork = IsResidentWorking[i];
                //newElement.GetComponent<StatBar>().starbar = newElement.transform.Find("Stats").Find("CanvasStats").gameObject;
                //newElement.GetComponent<StatBar>().stats = ResidentStats[i];
                //newElement.GetComponent<StatBar>().UpdateStats();
                newElement.transform.parent = parent;

                //Giving resident home
                GameObject home = StringToHome(ResidentHome[i]);
                newElement.GetComponent<ResidentScheudle>().home = home;
                if (home != null)
                {
                    home.GetComponent<Tent>().Residents++;
                    home.GetComponent<Tent>().ResidentsActive[home.GetComponent<Tent>().Residents - 1] = newElement;

                    newElement.AddComponent<ResidentConditions>().hasHome = true;
                }
                else
                {
                    newElement.AddComponent<ResidentConditions>().hasHome = false;
                }

                //Giving resident job
                GameObject job = StringToJob(ResidentJob[i]);
                newElement.GetComponent<ResidentScheudle>().job = job;

                if (job != null && job.GetComponent<Job>() != null)
                {
                    job.GetComponent<Job>().Workers++;
                    job.GetComponent<Job>().WorkersActive[job.GetComponent<Job>().Workers - 1] = newElement;
                    //newElement.GetComponent<StatBar>().UpdateJob();

                    if(job.GetComponent<Lumbermill>() != null && IsResidentWorking[i])
                    {
                        newElement.GetComponent<ResidentScheudle>().AtLocation = IsResidentWorking[i];
                        newElement.AddComponent<LumberWorker>().lumbermill = job.GetComponent<Lumbermill>();
                        newElement.GetComponent<ResidentTools>().ChangeEnable(0, true);
                    }
                }
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
                //Tent Stuff
                /*if(newElement.GetComponent<Tent>() != null)
                {
                    Tent tent = newElement.GetComponent<Tent>();
                    if (newElement.name.Contains("Level0")){
                        tent.MaxResidents = 2;
                        tent.ResidentsActive = new GameObject[tent.MaxResidents];
                    }
                }*/

                //Job Stuff
                /*if (newElement.GetComponent<Job>() != null)
                {
                    Job job = newElement.GetComponent<Job>();
                    if (newElement.name.Contains("Level0"))
                    {
                        job.MaxWorkers = 2;
                        job.WorkersActive = new GameObject[job.MaxWorkers];
                    }
                }*/

                //if (newElement.name.Contains("Mine")) newElement.GetComponent<Mine>().Drops = MineResources[i];
                //if (newElement.name.Contains("Lumbermill")) newElement.GetComponent<Lumbermill>().Drops = LumbermillResources[i];
                //if (newElement.name.Contains("Farm")) newElement.GetComponent<Farm>().Drops = FarmResources[i];
            }
        }
    }

    public void ReturnMessHallFarm()
    {
        for (int i = 0; i < buildingsParent.transform.GetChild(8).transform.childCount; i++)
        {
            buildingsParent.transform.GetChild(8).transform.GetChild(i).GetComponent<Messhall>().farm = StringToJob(MesshallFarm[i]);
        }
    }

    public void ReturnChestItems()
    {
        int index = 0;
        int slot = 0;

        for (int i = 0; i < buildingsParent.transform.GetChild(7).transform.childCount; i++)
        {
            Chest chest = buildingsParent.transform.GetChild(7).GetChild(i).GetComponent<Chest>();
            while(ChestAmounts[index] != -1)
            {
                if (ChestAmounts[index] == 0)
                {
                    index++;
                    slot++;
                    continue;
                }
                chest.SetItem(new Item { itemType = StringToItem(ChestItems[index])}, ChestAmounts[index], chest.inventorySlots[slot]);
                index++;
                slot++;
            }

            slot = 0;
            index++;
        }
    }

    public void ReturnMessHallItems()
    {
        int index = 0;
        int slot = 0;

        for (int i = 0; i < buildingsParent.transform.GetChild(8).transform.childCount; i++)
        {
            Messhall messhall = buildingsParent.transform.GetChild(8).GetChild(i).GetComponent<Messhall>();
            while (MesshallAmountsRaw[index] != -1)
            {
                if (MesshallAmountsRaw[index] == 0)
                {
                    index++;
                    slot++;
                    continue;
                }
                messhall.SetItem(new Item { itemType = StringToItem(MesshallItemsRaw[index]) }, MesshallAmountsRaw[index], messhall.rawFoodSlots[slot]);
                index++;
                slot++;
            }

            slot = 0;
            index++;
        }

        index = 0;
        slot = 0;

        for (int i = 0; i < buildingsParent.transform.GetChild(8).transform.childCount; i++)
        {
            Messhall messhall = buildingsParent.transform.GetChild(8).GetChild(i).GetComponent<Messhall>();
            while (MesshallAmountsCooked[index] != -1)
            {
                if (MesshallAmountsCooked[index] == 0)
                {
                    index++;
                    slot++;
                    continue;
                }
                messhall.SetItem(new Item { itemType = StringToItem(MesshallItemsCooked[index]) }, MesshallAmountsCooked[index], messhall.cookedFoodSlots[slot]);
                index++;
                slot++;
            }

            slot = 0;
            index++;
        }
    }

    public void ReturnMineItems()
    {
        int index = 0;
        int slot = 0;

        for (int i = 0; i < buildingsParent.transform.GetChild(2).transform.childCount; i++)
        {
            Mine mine = buildingsParent.transform.GetChild(2).GetChild(i).GetComponent<Mine>();
            while (MineAmounts[index] != -1)
            {
                if (MineAmounts[index] == 0)
                {
                    index++;
                    slot++;
                    continue;
                }
                mine.SetItem(new Item { itemType = StringToItem(MineItems[index]) }, MineAmounts[index], mine.inventorySlots[slot]);
                index++;
                slot++;
            }

            slot = 0;
            index++;
        }
    }

    public void ReturnFarmItems()
    {
        int index = 0;
        int slot = 0;

        for (int i = 0; i < buildingsParent.transform.GetChild(4).transform.childCount; i++)
        {
            Farm farm = buildingsParent.transform.GetChild(4).GetChild(i).GetComponent<Farm>();
            while (FarmAmounts[index] != -1)
            {
                if (FarmAmounts[index] == 0)
                {
                    index++;
                    slot++;
                    continue;
                }
                farm.SetItem(new Item { itemType = StringToItem(FarmItems[index]) }, FarmAmounts[index], farm.inventorySlots[slot]);
                //print("Slot: " + slot + "Amounts: " + FarmAmounts[index]);
                index++;
                slot++;
            }

            slot = 0;
            index++;
        }
    }

    public void ReturnLumbermillItems()
    {
        int index = 0;
        int slot = 0;

        for (int i = 0; i < buildingsParent.transform.GetChild(3).transform.childCount; i++)
        {
            Lumbermill lumbermill = buildingsParent.transform.GetChild(3).GetChild(i).GetComponent<Lumbermill>();
            while (LumbermillAmounts[index] != -1)
            {
                if (LumbermillAmounts[index] == 0)
                {
                    index++;
                    slot++;
                    continue;
                }
                lumbermill.SetItem(new Item { itemType = StringToItem(LumbermillItems[index]) }, LumbermillAmounts[index], lumbermill.inventorySlots[slot]);
                index++;
                slot++;
            }

            slot = 0;
            index++;
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
            //ItemWorld.SpawnItemWorld(positions[i], new Item { itemType = CheckStringsIntoItemNamesObjects(names[i]), amount = 1 }, rotations[i]);
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
        switch (name)
        {
            case string s when s.Contains("Campfire"):
                return buildingPrefabs[0];
            case string s when s.Contains("Tent"):
                return buildingPrefabs[1];
            case string s when s.Contains("Mine"):
                return buildingPrefabs[2];
            case string s when s.Contains("Lumbermill"):
                return buildingPrefabs[3];
            case string s when s.Contains("Farm"):
                return buildingPrefabs[4];
            case string s when s.Contains("Wall"):
                return buildingPrefabs[5];
            case string s when s.Contains("Door"):
                return buildingPrefabs[6];
            case string s when s.Contains("Chest"):
                return buildingPrefabs[7];
            case string s when s.Contains("Messhall"):
                return buildingPrefabs[8];
            case string s when s.Contains("Tavern"):
                return buildingPrefabs[9];
            case string s when s.Contains("Tower"):
                return buildingPrefabs[10];
            case string s when s.Contains("KnightHut"):
                return buildingPrefabs[11];
            default:
                return null;
        }

    }

    private void AddItemToInventory(string[] itemNames, int[] amount)
    {
        for(int i = 0; i < itemNames.Length; i++)
        {
            //wipes inventory if its 0, insures player starts game with fresh inventory when loading game from a loaded game
            InventoryItem itemInSlot = inventory.inventorySlots[i].GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null) DestroyImmediate(itemInSlot.gameObject);

            if (amount[i] == 0) continue;
            inventory.SetItem(new Item { itemType = StringToItem(itemNames[i]) }, amount[i], inventory.inventorySlots[i]);
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
            case Item.ItemType.axe: return "axe";
            case Item.ItemType.sword: return "sword";
            case Item.ItemType.hammer: return "hammer";
            case Item.ItemType.pickaxe: return "pickaxe";
            case Item.ItemType.charredBerry: return "charredBerry";
            case Item.ItemType.nothing: return "nothing";
            case Item.ItemType.empty: return "empty";
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
            case "pickaxe": return Item.ItemType.pickaxe;
            case "axe": return Item.ItemType.axe;
            case "sword": return Item.ItemType.sword;
            case "hammer": return Item.ItemType.hammer;
            case "charredBerry": return Item.ItemType.charredBerry;
            case "nothing": return Item.ItemType.nothing;
            case "empty": return Item.ItemType.empty;
        }
    }

    private void AddItemsToGravestone()
    {
        int index = 0;

        for(int i = 0; i < graveStoneParent.transform.childCount; i++)
        {
            Gravestone gravestone = graveStoneParent.transform.GetChild(i).GetComponent<Gravestone>();

            List<Item> items = new List<Item>();
            List<int> amounts = new List<int>();

            while (graveStoneAmounts[index] != -1)
            {
                items.Add(new Item { itemType = StringToItem(graveStoneItems[index])});
                amounts.Add(graveStoneAmounts[index]);
                index++;
            }

            gravestone.items = items.ToArray();
            gravestone.amounts = amounts.ToArray();

            index++;
        }
    }

    public void ClearGameObjects()
    {
        //DestroyImmediate might not be the best fix, but its working for now
        //Issue before, after i deleted all the objects with Destroy(), they still somehow remained for a little and it caused issues with building inventory

        for (int i = 0; i < TerrainObjects.transform.childCount - 2; i++)
        {
            foreach (Transform child in TerrainObjects.transform.GetChild(i))
            {
                DestroyImmediate(child.gameObject);
            }
        }

        foreach (Transform gameobject in buildingsParent.transform)
        {
            foreach (Transform child in gameobject)
            {
                DestroyImmediate(child.gameObject);
            }
        }
    }

    private GameObject StringToHome(string name)
    {
        GameObject returnData = null;

        for (int i = 0; i < buildingsParent.transform.GetChild(1).transform.childCount; i++) //the getChild(1) is the tents spawn location
        {
            if(name == buildingsParent.transform.GetChild(1).transform.GetChild(i).name)
            {
                returnData = buildingsParent.transform.GetChild(1).transform.GetChild(i).gameObject;
            }
        }

        return returnData;
    }

    private GameObject StringToJob(string name)
    {
        GameObject returnData = null;
        int num = -1;

        if (name.Contains("Mine")) num = 2;
        if (name.Contains("Lumbermill")) num = 3;
        if (name.Contains("Farm")) num = 4;
        if (name.Contains("Messhall")) num = 8;
        if (name.Contains("Tower")) num = 10;
        if (name.Contains("KnightHut")) num = 11;

        if (num == -1) return returnData;

        for (int i = 0; i < buildingsParent.transform.GetChild(num).transform.childCount; i++)
        {
            if (name == buildingsParent.transform.GetChild(num).transform.GetChild(i).name)
            {
                returnData = buildingsParent.transform.GetChild(num).transform.GetChild(i).gameObject;
            }
        }

        return returnData;
    }
}
