using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Builder : MonoBehaviour
{
    public TextMeshProUGUI Announcement;
    public BuildMenuUpdater buildMenuUpdater;
    [Header("Building Prefabs")]
    public GameObject[] StageOneBuildings;
    public Transform parent;

    [Header("Player Stuff")]
    public GameObject player;
    public OpenMenus openMenus;

    [Header("Information")]
    public GameObject buildingData;
    public bool isBuilding;
    public bool placed;
    public GameObject notEnough;
    public GameObject notEnoughRepair;
    public GameObject fullHeath;
    public SpawnEnemies spawnEnemies;
    public InventoryManager inventoryManager;
    public Book book;

    [Header("Hammer")]
    public GameObject hammer;

    [Header("Materials")]
    public InventoryManager inventory;
    private Dictionary<Item.ItemType[], int[]> inventoryItems;
    public TextMeshProUGUI woodText;
    public TextMeshProUGUI stoneText;

    [Header("Sounds")]
    public AudioSource audioSource;
    public AudioClip placingItem;

    [Header("Building Costs")]
    public BuildingHealth buildingHealth;
    public int[] campfireCost;
    public int[] tentCost;
    public int[] mineCost;
    public int[] lumberMillCost;
    public int[] farmCost;
    public int[] wallCost;
    public int[] doorCost;
    public int[] chestCost;
    public int[] messhallCost;
    public int[] tavernCost;
    public int[] towerCost;
    public int[] knightHutCost;

    void Start()
    {
        placed = false;
    }

    public void BuildCampfire()
    {
        BuildBuilding(StageOneBuildings[0], campfireCost, parent.GetChild(0), 0); //First child in "Buildings" is the campfire parent
    }

    public void BuildTent()
    {
        BuildBuilding(StageOneBuildings[1], tentCost, parent.GetChild(1), 0); //second child in "Buildings" is the tent parent... and so on 
    }

    public void BuildMine()
    {
        BuildBuilding(StageOneBuildings[2], mineCost, parent.GetChild(2), 0);
    }

    public void BuildLumbermill()
    {
        BuildBuilding(StageOneBuildings[3], lumberMillCost, parent.GetChild(3), 0);
    }

    public void BuildFarm()
    {
        BuildBuilding(StageOneBuildings[4], farmCost, parent.GetChild(4), 0);
    }

    public void BuildWall()
    {
        BuildBuilding(StageOneBuildings[5], wallCost, parent.GetChild(5), 0);
    }

    public void BuildDoor()
    {
        BuildBuilding(StageOneBuildings[6], doorCost, parent.GetChild(6), 0);
    }

    public void BuildChest()
    {
        BuildBuilding(StageOneBuildings[7], chestCost, parent.GetChild(7), 0);
    }

    public void BuildMesshall()
    {
        BuildBuilding(StageOneBuildings[8], messhallCost, parent.GetChild(8), 0);
    }

    public void BuildTavern()
    {
        BuildBuilding(StageOneBuildings[9], tavernCost, parent.GetChild(9), 0);
    }

    public void BuildTower()
    {
        BuildBuilding(StageOneBuildings[10], tavernCost, parent.GetChild(10), 0);
    }

    public void BuildKnightHut()
    {
        BuildBuilding(StageOneBuildings[11], knightHutCost, parent.GetChild(11), 0);
    }

    public void MoveBuilding()
    {
        if (buildingData != null)
        {
            AddBuildingScripts(buildingData, true, CheckParentName(buildingData.transform.name));
        }
    }

    public void RepairBuilding()
    {
        int[] amounts = CheckRepairCost(buildingHealth.name);

        if(buildingHealth.currentHealth != buildingHealth.maxHealth)
        {
            if (inventory.GetAmountByName(Item.ItemType.wood) >= amounts[0] / 3 &&
           inventory.GetAmountByName(Item.ItemType.rock) >= amounts[1] / 3)
            {
                if (inventoryManager != null)
                {
                    RemoveItem(Item.ItemType.wood, amounts[0] / 3);
                    RemoveItem(Item.ItemType.rock, amounts[1] / 3);
                    buildingHealth.ModifyHealth(30);
                }
            }
            else
            {
                notEnoughRepair.SetActive(true);
            }
        }
        else
        {
            fullHeath.SetActive(true);
        }
    }

    public void DestroyBuilding()
    {
        Destroy(buildingData);
    }

    private void BuildBuilding(GameObject buildingPrefab, int[] amounts, Transform parent, float offset)
    {
        //Array index to type
        //0 - wood
        //1 - rock

        if (inventory.GetAmountByName(Item.ItemType.wood) >= amounts[0] &&
           inventory.GetAmountByName(Item.ItemType.rock) >= amounts[1])
        {
            book.GoBackToPageOne();
            GameObject building = Instantiate(buildingPrefab, player.transform.position, player.transform.rotation);
            buildingData = building;
            AddBuildingScripts(building, parent, offset, amounts);
            //audioSource.PlayOneShot(placingItem);
        }
        else
        {
            notEnough.SetActive(true);
        }
    }

    private void AddBuildingScripts(GameObject building, Transform parent, float offset, int[] cost)
    {
        building.AddComponent<GroundPlacementController>();
        building.GetComponent<GroundPlacementController>().offset = offset;
        building.GetComponent<GroundPlacementController>().Announcement = Announcement;
        building.GetComponent<GroundPlacementController>().buildMenuUpdater = buildMenuUpdater;
        building.GetComponent<GroundPlacementController>().hammer = hammer;
        building.GetComponent<GroundPlacementController>().distance = 50;
        building.GetComponent<GroundPlacementController>().inventoryManager = inventory;
        building.GetComponent<GroundPlacementController>().Cost = cost;
        building.GetComponent<GroundPlacementController>().currentPlaceableObject = building;
        building.GetComponent<GroundPlacementController>().parent = parent;
        building.GetComponent<GroundPlacementController>().builder = this;
        building.GetComponent<GroundPlacementController>().spawnEnemies = spawnEnemies;
        building.GetComponent<GroundPlacementController>().audioSource = audioSource;
        building.GetComponent<GroundPlacementController>().placingItem = placingItem;
        building.GetComponent<NavMeshObstacle>().enabled = false;
        building.AddComponent<ColorChange>();
        isBuilding = true;
        openMenus.TurnOffMenus();
    }

    private void AddBuildingScripts(GameObject building, bool moving, Transform parent)
    {
        building.AddComponent<GroundPlacementController>();
        building.GetComponent<GroundPlacementController>().Announcement = Announcement;
        building.GetComponent<GroundPlacementController>().buildMenuUpdater = buildMenuUpdater;
        building.GetComponent<GroundPlacementController>().hammer = hammer;
        building.GetComponent<GroundPlacementController>().distance = 50;
        building.GetComponent<GroundPlacementController>().currentPlaceableObject = building;
        building.GetComponent<GroundPlacementController>().parent = parent;
        building.GetComponent<GroundPlacementController>().builder = this;
        building.GetComponent<GroundPlacementController>().moving = moving;
        building.GetComponent<NavMeshObstacle>().enabled = false;
        building.AddComponent<ColorChange>();
        building.GetComponent<GroundPlacementController>().audioSource = audioSource;
        building.GetComponent<GroundPlacementController>().placingItem = placingItem;
        isBuilding = true;
        openMenus.TurnOffMenus();
    }

    private Transform CheckParentName(string name)
    {
        Transform transform = null;

        if (name.Contains("Campfire")) transform = parent.GetChild(0);
        else if (name.Contains("Tent")) transform = parent.GetChild(1);
        else if (name.Contains("Mine")) transform = parent.GetChild(2);
        else if (name.Contains("Lumbermill")) transform = parent.GetChild(3);
        else if (name.Contains("Farm")) transform = parent.GetChild(4);
        else if (name.Contains("Wall")) transform = parent.GetChild(5);
        else if (name.Contains("Door")) transform = parent.GetChild(6);
        else if (name.Contains("Chest")) transform = parent.GetChild(7);
        else if (name.Contains("Messhall")) transform = parent.GetChild(8);
        else if (name.Contains("Tavern")) transform = parent.GetChild(9);
        else if (name.Contains("Tower")) transform = parent.GetChild(10);
        else if (name.Contains("KnightHut")) transform = parent.GetChild(11);

        return transform;
    }

    private int[] CheckRepairCost(string buildingName)
    {
        string lowerName = buildingName.ToLower();
        return lowerName switch
        {
            string name when name.Contains("campfire") => campfireCost,
            string name when name.Contains("tent") => tentCost,
            string name when name.Contains("mine") => mineCost,
            string name when name.Contains("lumbermill") => lumberMillCost,
            string name when name.Contains("farm") => farmCost,
            string name when name.Contains("wall") => wallCost,
            string name when name.Contains("door") => doorCost,
            string name when name.Contains("chest") => chestCost,
            string name when name.Contains("messhall") => messhallCost,
            string name when name.Contains("tavern") => tavernCost,
            string name when name.Contains("tower") => towerCost,
            string name when name.Contains("knighthut") => knightHutCost,
            _ => null,
        };
    }

    public string RepairCostText(string name)
    {
        int[] amounts = CheckRepairCost(name);
        string build = "";

        for(int i = 0; i < amounts.Length; i++)
        {
            if (amounts[i] == 0) continue;
            build += OrderOfMaterials(i) + ": " + amounts[i] / 3 + "\n";
        }

        return build;
    }

    private string OrderOfMaterials(int num)
    {
        switch (num)
        {
            case 0: return "Wood";
            case 1: return "Rock";
            default: return "";
        }
    }

    private void RemoveItem(Item.ItemType item, int cost)
    {
        for (int i = 0; i < cost; i++)
        {
            inventoryManager.RemoveItem(item);
        }
    }

    /*private void RemoveItems(Item.ItemType item, int amount)
    {
        int[] array = inventory.GetAmountArrayByName(item);

        for (int i = array.Length - 1; i >= 0; i--)
        {
            print(amount);
            if (array[i] == 0) continue;

            if (amount == 0) break;

            if (amount - array[i] > 0)
            {
                amount -= array[i];
                //inventory.RemoveItem(new Item { itemType = item, amount = array[i], index = i });
            }
            else
            {
                for (int j = 0; j < amount; j++)
                {
                    //inventory.RemoveItem(new Item { itemType = item, amount = 1, index = i });
                }
                break;
            }
        }
    }*/
}