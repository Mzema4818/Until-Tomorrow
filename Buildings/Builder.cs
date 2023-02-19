using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    [Header("Hammer")]
    public GameObject hammer;

    [Header("Materials")]
    public Inventory inventory;
    private Dictionary<Item.ItemType[], int[]> inventoryItems;
    public MaterialManager materialManager;
    public TextMeshProUGUI woodText;
    public TextMeshProUGUI stoneText;

    private int wood;
    private int stone;

    // Start is called before the first frame update
    void Start()
    {
        placed = false;
    }

    // Update is called once per frame
    void Update()
    {
        wood = int.Parse(woodText.text);
        stone = int.Parse(stoneText.text);
    }

    public void BuildCampfire()
    {
        BuildBuilding(StageOneBuildings[0], 10, 10, parent.GetChild(0)); //First child in "Buildings" is the campfire parent
    }

    public void BuildTent()
    {
        BuildBuilding(StageOneBuildings[1], 4, 10, parent.GetChild(1)); //second child in "Buildings" is the tent parent... and so on 
    }

    public void MoveBuilding()
    {
        if(buildingData != null)
        {
            AddBuildingScripts(buildingData, true, CheckParentName(buildingData.transform.name));
        }
    }

    public void DestroyBuilding()
    {
        Destroy(buildingData);
    }

    private void BuildBuilding(GameObject buildingPrefab, int woodValue, int stoneValue, Transform parent)
    {
        if(inventory.GetAmountByName(Item.ItemType.wood) >= woodValue && inventory.GetAmountByName(Item.ItemType.rock) >= stoneValue)
        {
            inventory.RemoveItem(new Item { itemType = Item.ItemType.wood, amount = woodValue });
            inventory.RemoveItem(new Item { itemType = Item.ItemType.rock, amount = stoneValue });
            GameObject building = Instantiate(buildingPrefab, player.transform.position, player.transform.rotation);
            AddBuildingScripts(building, parent);
        }
        else
        {
            notEnough.SetActive(true);
        }
    }

    private void AddBuildingScripts(GameObject building, Transform parent)
    {
        building.AddComponent<GroundPlacementController>();
        building.GetComponent<GroundPlacementController>().Announcement = Announcement;
        building.GetComponent<GroundPlacementController>().buildMenuUpdater = buildMenuUpdater;
        building.GetComponent<GroundPlacementController>().hammer = hammer;
        building.GetComponent<GroundPlacementController>().distance = 50;
        building.GetComponent<GroundPlacementController>().currentPlaceableObject = building;
        building.GetComponent<GroundPlacementController>().parent = parent;
        building.GetComponent<GroundPlacementController>().builder = this;
        building.GetComponent<GroundPlacementController>().materialManager = materialManager;
        building.AddComponent<ColorChange>();
        isBuilding = true;
        openMenus.turnOffAllMenus();
        openMenus.CloseBuildMenus();
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
        building.GetComponent<GroundPlacementController>().materialManager = materialManager;
        building.AddComponent<ColorChange>();
        isBuilding = true;
        openMenus.turnOffAllMenus();
        openMenus.CloseBuildMenus();
    }

    private Transform CheckParentName(string name)
    {
        Transform transform = null;

        if (name.Contains("Campfire")){
            transform = parent.GetChild(0);
        }else if (name.Contains("Tent"))
        {
            transform = parent.GetChild(1);
        }

        return transform;
    }
}
