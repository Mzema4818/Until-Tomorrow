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
    public SpawnEnemies spawnEnemies;

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

    // Start is called before the first frame update
    void Start()
    {
        placed = false;
    }

    public void BuildCampfire()
    {
        BuildBuilding(StageOneBuildings[0], new int[] { 10, 10 }, parent.GetChild(0), 0); //First child in "Buildings" is the campfire parent
    }

    public void BuildTent()
    {
        BuildBuilding(StageOneBuildings[1], new int[] { 10, 10 }, parent.GetChild(1), 0); //second child in "Buildings" is the tent parent... and so on 
    }

    public void BuildMine()
    {
        BuildBuilding(StageOneBuildings[2], new int[] { 10, 10 }, parent.GetChild(2), 0);
    }

    public void BuildLumbermill()
    {
        BuildBuilding(StageOneBuildings[3], new int[] { 10, 10 }, parent.GetChild(3), 0);
    }

    public void BuildFarm()
    {
        BuildBuilding(StageOneBuildings[4], new int[] { 10, 10 }, parent.GetChild(4), 0);
    }

    public void BuildWall()
    {
        BuildBuilding(StageOneBuildings[5], new int[] { 10, 10 }, parent.GetChild(5), 0);
    }

    public void BuildDoor()
    {
        BuildBuilding(StageOneBuildings[6], new int[] { 10, 10 }, parent.GetChild(6), 0);
    }

    public void BuildChest()
    {
        BuildBuilding(StageOneBuildings[7], new int[] { 10, 10 }, parent.GetChild(7), 0);
    }

    public void BuildMesshall()
    {
        BuildBuilding(StageOneBuildings[8], new int[] { 10, 10 }, parent.GetChild(8), 0);
    }

    public void BuildTavern()
    {
        BuildBuilding(StageOneBuildings[9], new int[] { 10, 10 }, parent.GetChild(9), 0);
    }

    public void BuildTower()
    {
        BuildBuilding(StageOneBuildings[10], new int[] { 10, 10 }, parent.GetChild(10), 0);
    }

    public void BuildKnightHut()
    {
        BuildBuilding(StageOneBuildings[11], new int[] { 10, 10 }, parent.GetChild(11), 0);
    }

    public void MoveBuilding()
    {
        if (buildingData != null)
        {
            AddBuildingScripts(buildingData, true, CheckParentName(buildingData.transform.name));
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
        else if (name.Contains("KnightHut")) transform = parent.GetChild(10);

        return transform;
    }

    private void RemoveItems(Item.ItemType item, int length)
    {
        for(int i = 0; i < length; i++)
        {
            inventory.RemoveItem(item);
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