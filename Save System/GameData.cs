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

    public float[] TentPosition;
    public float[] TentRotation;
    public string[] TentNames;

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
    public string[] FavoriteItem;

    //gravestone
    public float[] graveStonePosition;
    public float[] graveStoneRotation;

    public string[] inventoryItemNamesGraveStone;
    public int[] inventoryItemAmountsGraveStone;

    //player inventory
    public int[] playerInventoryAmount;
    public string[] playerInventoryItems;

    public string wood;
    public string stone;
    public string mushroom;
    public string flower;

    public float timeOfDay;
    public int numberOfDays;

    public float sensitivity;

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

    public bool SpawnPrisonShips;
    public int SpawnPrisonShipDay;

    public GameData(GetData data)
    {
        data.GetInventoryItemAmount();
        //Terrain Data
        data.getSeed();
        seed = data.seed;

        //Inventory Data
        playerInventoryAmount = data.GetInventoryItemAmount();
        TurnItemTypeIntoStrings(data.GetInventoryItemType(), ref playerInventoryItems);

        data.GetInventoryWood();
        data.GetInventoryStone();
        data.GetInventoryMushroom();
        data.GetInventoryFlower();
        wood = data.wood;
        stone = data.stone;
        mushroom = data.mushroom;
        flower = data.flower;

        //Story Data
        AccessToLevel0Buildings = data.GetAccessToLevel0Buildings();
        AccessToLevel1Buildings = data.GetAccessToLevel1Buildings();
        AccessToLevel2Buildings = data.GetAccessToLevel2Buildings();

        SpawnPrisonShips = data.GetShouldSpawnPrisonShip();
        SpawnPrisonShipDay = data.GetPrisonShipSpawnDay();

        //Player Data
        data.getPlayerHealth();
        health = data.health;
        data.getPlayerFood();
        food = data.food;

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
        turnIntoPositions(data.GetDeathPosition(), ref graveStonePosition);

        turnIntoRotations(data.GetDeathRotation(), ref graveStoneRotation);


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

        //tent data
        turnIntoPositions(data.GetTentPosition(), ref TentPosition);

        turnIntoRotations(data.GetTentRotation(), ref TentRotation);

        turnIntoNames(data.GetTentNames(), ref TentNames);

        //prison boat data
        turnIntoPositions(data.GetPrisonShipPosition(), ref PrisonShipPosition);

        turnIntoRotations(data.GetPrisonShipRotation(), ref PrisonShipRotation);

        turnIntoNames(data.GetPrisonShipNames(), ref PrisonShipNames);

        //resident data
        turnIntoPositions(data.GetResidentPosition(), ref ResidentPosition);

        turnIntoRotations(data.GetResidentRotation(), ref ResidentRotation);

        ResidentStats = data.GetResidentStats();

        JoinedTown = data.GetResidentJoinedTown();

        FavoriteItem = data.GetResidentFavoriteItem();

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

        //gravestones
        TurnItemTypeIntoStringsArray(data.GraveStoneData(), ref inventoryItemNamesGraveStone, ref inventoryItemAmountsGraveStone);
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
