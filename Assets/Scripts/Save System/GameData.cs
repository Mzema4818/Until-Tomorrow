using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int seed;
    public float[] playerPosition;
    public float[] playerRespawnPoint;
    public float[] playerRotation;
    public float health;
    public float food;

    //dropable
    public float[] dropablePosition;
    public float[] dropableRotation;
    public string[] dropableNames;

    //tree
    public float[] treePosition;
    public float[] treeRotation;
    public float[] treeScales;
    public string[] treeNames;
    public float[] treeColors;

    //rock
    public float[] rockPosition;
    public float[] rockRotation;
    public string[] rockNames;
    public float[] rockColors;
    public float[] rockScales;

    //stump
    public float[] stumpPosition;
    public float[] stumpRotation;
    public string[] stumpNames;
    public float[] stumpScales;

    //fallen
    public float[] fallenPosition;
    public float[] fallenRotation;
    public string[] fallenNames;
    public float[] fallenScales;

    //small rock
    public float[] smallRockPosition;
    public float[] smallRockRotation;
    public string[] smallRockNames;
    public float[] smallRockScales;

    //flower
    public float[] flowerPosition;
    public float[] flowerRotation;
    public string[] flowerNames;
    public float[] flowerScales;

    //mushroom
    public float[] mushroomPosition;
    public float[] mushroomRotation;
    public string[] mushroomNames;
    public float[] mushroomScales;

    //berry
    public float[] BerryPosition;
    public float[] BerryRotation;
    public string[] BerryNames;
    public float[] BerryScales;

    //sapling
    public float[] SaplingPosition;
    public float[] SaplingRotation;

    //deer
    public float[] deerPosition;
    public float[] deerRotation;
    public string[] deerNames;
    public float[] deerScales;

    //fox
    public float[] foxPosition;
    public float[] foxRotation;
    public string[] foxNames;
    public float[] foxScales;

    //bear
    public float[] bearPosition;
    public float[] bearRotation;
    public string[] bearNames;
    public float[] bearScales;

    //building
    public float[] CampfirePosition;
    public float[] CampfireRotation;
    public string[] CampfireNames;
    public int[] CampfireHealth;

    public float[] TentPosition;
    public float[] TentRotation;
    public string[] TentNames;
    public int[] TentHealth;

    public float[] MinePosition;
    public float[] MineRotation;
    public string[] MineNames;
    public string[] MineItems;
    public int[] MineAmounts;
    public int[] MineHealth;

    public float[] LumbermillPosition;
    public float[] LumbermillRotation;
    public string[] LumbermillNames;
    public string[] LumbermillItems;
    public int[] LumbermillAmounts;
    public int[] LumbermillHealth;

    public float[] FarmPosition;
    public float[] FarmRotation;
    public string[] FarmNames;
    public string[] FarmItems;
    public int[] FarmAmounts;
    public int[] FarmHealth;

    public float[] WallPosition;
    public float[] WallRotation;
    public string[] WallNames;
    public int[] WallHealth;

    public float[] DoorPosition;
    public float[] DoorRotation;
    public string[] DoorNames;
    public int[] DoorHealth;

    public float[] ChestPosition;
    public float[] ChestRotation;
    public string[] ChestNames;
    public string[] ChestItems;
    public int[] ChestAmounts;
    public int[] ChestHealth;

    public float[] MesshallPosition;
    public float[] MesshallRotation;
    public string[] MesshallNames;
    public string[] MesshallItemsRaw;
    public int[] MesshallAmountsRaw;
    public string[] MesshallItemsCooked;
    public int[] MesshallAmountsCooked;
    public string[] MesshallFarm;
    public int[] MesshallHealth;

    public float[] tavernPosition;
    public float[] tavernRotation;
    public string[] tavernNames;
    public int[] tavernHealth;

    public float[] towerPosition;
    public float[] towerRotation;
    public string[] towerNames;
    public int[] towerHealth;

    public float[] knightHutPosition;
    public float[] knightHutRotation;
    public string[] knightHutNames;
    public int[] knightHutHealth;
    //public string[] inventoryItemNamesChest;
    //public int[] inventoryItemAmountsChest;

    //prisonship
    public float[] PrisonShipPosition;
    public float[] PrisonShipRotation;
    public string[] PrisonShipNames;

    //resident
    public float[] ResidentPosition;
    public float[] ResidentRotation;
    public int[][] ResidentStats;
    public string[] ResidentName;
    public bool[] JoinedTown;
    public bool[] IsResidentWorking;
    public string[] FavoriteItem;
    public string[] ResidentHome;
    public string[] ResidentJob;
    public int[][] ResidentSchedule;

    //gravestone
    public float[] graveStonePosition;
    public float[] graveStoneRotation;
    public string[] graveStoneItems;
    public int[] graveStoneAmounts;

    //public string[] inventoryItemNamesGraveStone;
    //public int[] inventoryItemAmountsGraveStone;

    //player inventory
    public int[] playerInventoryAmount;
    public string[] playerInventoryItems;


    public float timeOfDay;
    public int numberOfDays;

    //Settings
    public float sensitivity;
    public float volume;

    //hotbaritem
    public bool[] HotBarItemActive;
    public float[] HotBarItemPosition;
    public float[] HotBarItemRotation;

    //collectable
    public bool[] collectableActive;
    public float[] collectablePosition;
    public float[] collectableRotation;

    //Story Manager
    public bool AccessToLevel0Buildings;
    public bool AccessToLevel1Buildings;
    public bool AccessToLevel2Buildings;
    public bool AccessToLevel3Buildings;

    public bool CanSpawnShips;
    public int SpawnShipDay;
    public int SpawnEnemyDay;

    public GameData(GetData data)
    {
        //data.GetInventoryItemAmount();
        //Terrain Data
        data.getSeed();
        seed = data.seed;

        //Inventory Data
        playerInventoryAmount = data.GetInventoryItemAmount();
        playerInventoryItems = data.GetInventoryItemType();

        //Story Data
        AccessToLevel0Buildings = data.GetAccessToLevel0Buildings();
        AccessToLevel1Buildings = data.GetAccessToLevel1Buildings();
        AccessToLevel2Buildings = data.GetAccessToLevel2Buildings();
        AccessToLevel3Buildings = data.GetAccessToLevel3Buildings();

        CanSpawnShips = data.GetCanSpawnShips();
        SpawnShipDay = data.GetSpawnShipDay();
        SpawnEnemyDay = data.GetSpawnEnemyDay();

        //Player Data
        data.getPlayerHealth();
        health = data.health;
        data.getPlayerFood();
        food = data.hunger;

        data.GetPlayerPosition();
        playerPosition = new float[3];
        playerPosition[0] = data.GetPlayerPosition().x;
        playerPosition[1] = data.GetPlayerPosition().y;
        playerPosition[2] = data.GetPlayerPosition().z;

        data.GetPlayerRespawnPoint();
        playerRespawnPoint = new float[3];
        playerRespawnPoint[0] = data.GetPlayerRespawnPoint().x;
        playerRespawnPoint[1] = data.GetPlayerRespawnPoint().y;
        playerRespawnPoint[2] = data.GetPlayerRespawnPoint().z;

        data.GetPlayerRotation();
        playerRotation = new float[4];
        playerRotation[0] = data.GetPlayerRotation().x;
        playerRotation[1] = data.GetPlayerRotation().y;
        playerRotation[2] = data.GetPlayerRotation().z;
        playerRotation[3] = data.GetPlayerRotation().w;

        //dropable data
        turnIntoPositions(data.GetDropablePosition(), ref dropablePosition);

        turnIntoRotations(data.GetDropableRotation(), ref dropableRotation);

        turnIntoNames(data.GetDropableNames(), ref dropableNames);

        //tree data
        turnIntoPositions(data.GetTreePosition(), ref treePosition);

        turnIntoRotations(data.GetTreeRotation(), ref treeRotation);

        turnIntoNames(data.GetTreeNames(), ref treeNames);

        turnIntoColors(data.GetTreeColor(), ref treeColors);

        turnIntoPositions(data.GetTreeScale(), ref treeScales);

        //rock data
        turnIntoPositions(data.GetRockPosition(), ref rockPosition);

        turnIntoRotations(data.GetRockRotation(), ref rockRotation);

        turnIntoNames(data.GetRockNames(), ref rockNames);

        turnIntoColors(data.GetRockColor(), ref rockColors);

        turnIntoPositions(data.GetRockScale(), ref rockScales);

        //stump data
        turnIntoPositions(data.GetStumpPosition(), ref stumpPosition);

        turnIntoRotations(data.GetStumpRotation(), ref stumpRotation);

        turnIntoNames(data.GetStumpNames(), ref stumpNames);

        turnIntoPositions(data.GetStumpScale(), ref stumpScales);

        //fallen data
        turnIntoPositions(data.GetFallenPosition(), ref fallenPosition);

        turnIntoRotations(data.GetFallenRotation(), ref fallenRotation);

        turnIntoNames(data.GetFallenNames(), ref fallenNames);

        turnIntoPositions(data.GetFallenScale(), ref fallenScales);

        //small rock data
        turnIntoPositions(data.GetSmallRockPosition(), ref smallRockPosition);

        turnIntoRotations(data.GetSmallRockRotation(), ref smallRockRotation);

        turnIntoNames(data.GetSmallRockNames(), ref smallRockNames);

        turnIntoPositions(data.GetSmallRockScale(), ref smallRockScales);

        //flower data
        turnIntoPositions(data.GetFlowerPosition(), ref flowerPosition);

        turnIntoRotations(data.GetFlowerRotation(), ref flowerRotation);

        turnIntoNames(data.GetFlowerNames(), ref flowerNames);

        turnIntoPositions(data.GetFlowerScale(), ref flowerScales);

        //mushroom data
        turnIntoPositions(data.GetMushroomPosition(), ref mushroomPosition);

        turnIntoRotations(data.GetMushroomRotation(), ref mushroomRotation);

        turnIntoNames(data.GetMushroomNames(), ref mushroomNames);

        turnIntoPositions(data.GetMushroomScale(), ref mushroomScales);

        //berry data
        turnIntoPositions(data.GetBerryPosition(), ref BerryPosition);

        turnIntoRotations(data.GetBerryRotation(), ref BerryRotation);

        turnIntoNames(data.GetBerryNames(), ref BerryNames);

        turnIntoPositions(data.GetBerryScale(), ref BerryScales);

        //sapling data
        turnIntoPositions(data.GetSaplingPosition(), ref SaplingPosition);

        turnIntoRotations(data.GetSaplingRotation(), ref SaplingRotation);

        //death data
        turnIntoPositions(data.GetGravestonePosition(), ref graveStonePosition);

        turnIntoRotations(data.GetGravestoneRotation(), ref graveStoneRotation);

        graveStoneItems = data.GetGravestoneItems();

        graveStoneAmounts = data.GetGravestoneAmounts();

        //deer data
        turnIntoPositions(data.GetDeerPosition(), ref deerPosition);

        turnIntoRotations(data.GetDeerRotation(), ref deerRotation);

        turnIntoNames(data.GetDeerNames(), ref deerNames);

        turnIntoPositions(data.GetDeerScale(), ref deerScales);

        //fox data
        turnIntoPositions(data.GetFoxPosition(), ref foxPosition);

        turnIntoRotations(data.GetFoxRotation(), ref foxRotation);

        turnIntoNames(data.GetFoxNames(), ref foxNames);

        turnIntoPositions(data.GetFoxScale(), ref foxScales);

        //bear data
        turnIntoPositions(data.GetBearPosition(), ref bearPosition);

        turnIntoRotations(data.GetBearRotation(), ref bearRotation);

        turnIntoNames(data.GetBearNames(), ref bearNames);

        turnIntoPositions(data.GetBearScale(), ref bearScales);

        //campfire data
        turnIntoPositions(data.GetCampfirePosition(), ref CampfirePosition);

        turnIntoRotations(data.GetCampfireRotation(), ref CampfireRotation);

        turnIntoNames(data.GetCampfireNames(), ref CampfireNames);

        turnIntoHealth(data.GetCampFireHealth(), ref CampfireHealth);

        //tent data
        turnIntoPositions(data.GetTentPosition(), ref TentPosition);

        turnIntoRotations(data.GetTentRotation(), ref TentRotation);

        turnIntoNames(data.GetTentNames(), ref TentNames);

        turnIntoHealth(data.GetTentHealth(), ref TentHealth);

        //mine data
        turnIntoPositions(data.GetMinePosition(), ref MinePosition);

        turnIntoRotations(data.GetMineRotation(), ref MineRotation);

        turnIntoNames(data.GetMineNames(), ref MineNames);

        turnIntoHealth(data.GetMineHealth(), ref MineHealth);

        MineItems = data.GetMineItems();

        MineAmounts = data.GetMineAmounts();

        //lumbermill data
        turnIntoPositions(data.GetLumbermillPosition(), ref LumbermillPosition);

        turnIntoRotations(data.GetLumbermillRotation(), ref LumbermillRotation);

        LumbermillItems = data.GetLumbermillItems();

        LumbermillAmounts = data.GetLumbermillAmounts();

        turnIntoNames(data.GetLumbermillNames(), ref LumbermillNames);

        turnIntoHealth(data.GetLumbermillHealth(), ref LumbermillHealth);

        //Farm data
        turnIntoPositions(data.GetFarmPosition(), ref FarmPosition);

        turnIntoRotations(data.GetFarmRotation(), ref FarmRotation);

        FarmItems = data.GetFarmItems();

        FarmAmounts = data.GetFarmAmounts();

        turnIntoNames(data.GetFarmNames(), ref FarmNames);

        turnIntoHealth(data.GetFarmHealth(), ref FarmHealth);

        //wall data
        turnIntoPositions(data.GetWallPosition(), ref WallPosition);

        turnIntoRotations(data.GetWallRotation(), ref WallRotation);

        turnIntoNames(data.GetWallNames(), ref WallNames);

        turnIntoHealth(data.GetWallHealth(), ref WallHealth);

        //tavern data
        turnIntoPositions(data.GetTavernPosition(), ref tavernPosition);

        turnIntoRotations(data.GetTavernRotation(), ref tavernRotation);

        turnIntoNames(data.GetTavernNames(), ref tavernNames);

        turnIntoHealth(data.GetTavernHealth(), ref tavernHealth);

        //tower data
        turnIntoPositions(data.GetTowerPosition(), ref towerPosition);

        turnIntoRotations(data.GetTowerRotation(), ref towerRotation);

        turnIntoNames(data.GetTowerNames(), ref towerNames);

        turnIntoHealth(data.GetTowerHealth(), ref towerHealth);

        //knight hut data
        turnIntoPositions(data.GetKnightHutPosition(), ref knightHutPosition);

        turnIntoRotations(data.GetKnightHutRotation(), ref knightHutRotation);

        turnIntoNames(data.GetKnightHutNames(), ref knightHutNames);

        turnIntoHealth(data.GetKnightHutHealth(), ref knightHutHealth);

        //door data
        turnIntoPositions(data.GetDoorPosition(), ref DoorPosition);

        turnIntoRotations(data.GetDoorRotation(), ref DoorRotation);

        turnIntoNames(data.GetDoorNames(), ref DoorNames);

        turnIntoHealth(data.GetDoorHealth(), ref DoorHealth);

        //chest data
        turnIntoPositions(data.GetChestPosition(), ref ChestPosition);

        turnIntoRotations(data.GetChestRotation(), ref ChestRotation);

        turnIntoNames(data.GetChestNames(), ref ChestNames);

        ChestItems = data.GetChestItems();

        ChestAmounts = data.GetChestAmounts();

        turnIntoHealth(data.GetChestHealth(), ref ChestHealth);

        //messhall data
        turnIntoPositions(data.GetMesshallPosition(), ref MesshallPosition);

        turnIntoRotations(data.GetMesshallRotation(), ref MesshallRotation);

        turnIntoNames(data.GetMesshallNames(), ref MesshallNames);

        MesshallItemsRaw = data.GetMesshallItemsRaw();

        MesshallAmountsRaw = data.GetMesshallAmountsRaw();

        MesshallItemsCooked = data.GetMesshallItemsCooked();

        MesshallAmountsCooked = data.GetMesshallAmountsCooked();

        MesshallFarm = data.GetMesshallFarm();

        turnIntoHealth(data.GetMesshallHealth(), ref MesshallHealth);

        //TurnItemTypeIntoStringsArray(data.ChestData(), ref inventoryItemNamesChest, ref inventoryItemAmountsChest);

        //prison boat data
        turnIntoPositions(data.GetPrisonShipPosition(), ref PrisonShipPosition);

        turnIntoRotations(data.GetPrisonShipRotation(), ref PrisonShipRotation);

        turnIntoNames(data.GetPrisonShipNames(), ref PrisonShipNames);

        //resident data
        turnIntoPositions(data.GetResidentPosition(), ref ResidentPosition);

        turnIntoRotations(data.GetResidentRotation(), ref ResidentRotation);

        ResidentStats = data.GetResidentStats();

        ResidentSchedule = data.GetResidentSchedule();

        JoinedTown = data.GetResidentJoinedTown();

        IsResidentWorking = data.GetResidentIsWorking();

        FavoriteItem = data.GetResidentFavoriteItem();

        ResidentHome = data.GetResidentHome();

        ResidentJob = data.GetResidentJob();

        turnIntoNames(data.GetResidentNames(), ref ResidentName);

        //collectables
        collectableActive = new bool[data.GetCollectableActive().Length];
        collectableActive = data.GetCollectableActive();

        turnIntoPositions(data.GetCollectablePosition(), ref collectablePosition);

        turnIntoRotations(data.GetCollectableRotation(), ref collectableRotation);

        //hotbaritems
        HotBarItemActive = new bool[data.GetHotBarItemActive().Length];
        HotBarItemActive = data.GetHotBarItemActive();

        turnIntoPositions(data.GetHotBarItemPosition(), ref HotBarItemPosition);

        turnIntoRotations(data.GetHotBarItemRotation(), ref HotBarItemRotation);

        //settings
        numberOfDays = data.GetNumberOfDays();
        timeOfDay = data.GetTimeOfDay();
        sensitivity = data.GetSensitivity();
        volume = data.GetVolume();

        //gravestones
        //TurnItemTypeIntoStringsArray(data.GraveStoneData(), ref inventoryItemNamesGraveStone, ref inventoryItemAmountsGraveStone);

    }

    private void turnIntoPositions(Vector3[] function, ref float[] dataCreated)
    {
        int num = 0;
        dataCreated = new float[function.Length * 3];

        for (int i = 0; i < function.Length * 3; i++)
        {
            dataCreated[i] = function[num].x;
            dataCreated[i + 1] = function[num].y;
            dataCreated[i + 2] = function[num].z;
            num++;
            i += 2;
        }
    }
    
    private void turnIntoRotations(Quaternion[] function, ref float[] dataCreated)
    {
        int num = 0;
        dataCreated = new float[function.Length * 4];

        for (int i = 0; i < function.Length * 4; i++)
        {
            dataCreated[i] = function[num].x;
            dataCreated[i + 1] = function[num].y;
            dataCreated[i + 2] = function[num].z;
            dataCreated[i + 3] = function[num].w;
            num++;
            i += 3;
        }
    }

    private void turnIntoNames(string[] function, ref string[] dataCreated)
    {
        dataCreated = new string[function.Length];

        for(int i = 0; i < dataCreated.Length; i++)
        {
            dataCreated[i] = function[i];
        }
    }

    private void turnIntoHealth(int[] function, ref int[] dataCreated)
    {
        dataCreated = new int[function.Length];

        for (int i = 0; i < dataCreated.Length; i++)
        {
            dataCreated[i] = function[i];
        }
    }

    private void turnIntoColors(Color[] function, ref float[] dataCreated)
    {
        int num = 0;
        dataCreated = new float[function.Length * 4];

        for (int i = 0; i < function.Length * 4; i++)
        {
            dataCreated[i] = function[num].r;
            dataCreated[i + 1] = function[num].g;
            dataCreated[i + 2] = function[num].b;
            dataCreated[i + 3] = function[num].a;
            num++;
            i += 3;
        }
    }

    private void TurnItemTypeIntoStrings(Item.ItemType[] function, ref string[] dataCreated)
    {
        dataCreated = new string[function.Length];
        for(int i = 0; i < function.Length; i++)
        {
            dataCreated[i] = CheckItemNamesIntoString(function[i]);
        }
    }

    private void TurnItemTypeIntoStringsArray(Dictionary<Item.ItemType[], int[]>[] function, ref string[] inventoryItems, ref int[] inventoryAmount)
    {
        int num = 0;

        for(int i = 0; i < function.Length; i++)
        {
            foreach (var kvp in function[i])
            {
                for (int j = 0; j < kvp.Value.Length; j++)
                {
                    //Debug.Log(kvp.Value[j] + "     " + kvp.Key[j]);
                    num++;
                }
                num++;
            }
        }

        inventoryItems = new string[num];
        inventoryAmount = new int[num];
        int number = 0;

        for (int i = 0; i < function.Length; i++)
        {
            foreach (var kvp in function[i])
            {
                for (int j = 0; j < kvp.Value.Length; j++)
                {
                    inventoryItems[number] = CheckItemNamesIntoString(kvp.Key[j]);
                    inventoryAmount[number] = kvp.Value[j];
                    number++;
                }
                number++;
            }
        }
    }

    private void ItemToString(Item.ItemType[] function, ref string[] dataCreated)
    {
        dataCreated = new string[function.Length];

        for (int i = 0; i < dataCreated.Length; i++)
        {
            dataCreated[i] = CheckItemNamesIntoString(function[i]);
        }
    }

    private string CheckItemNamesIntoString(Item.ItemType item)
    { 
        switch (item)
        {
            default:
            case Item.ItemType.rock: return "rock";
            case Item.ItemType.wood: return "wood";
            case Item.ItemType.mushroom: return "mushroom";
            case Item.ItemType.flower: return "flower";
            case Item.ItemType.berry: return "berry";
            case Item.ItemType.bush: return "bush";
            case Item.ItemType.sapling: return "sapling";
        }
    }
}
