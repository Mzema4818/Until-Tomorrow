using UnityEngine;

public class PlacingSeeds : MonoBehaviour
{
    [Header("Inventory")]
    public InventoryManager inventoryManager;
    public int itemIndex;

    [Header("Actual Objects")]
    public GameObject prefab;
    public GameObject currentObject;

    [Header("Parents")]
    public Transform TempObjects;
    public Transform parent;
    public Transform TreeParent;

    [Header("Information")]
    public ForestGenerator forestGenerator;
    public Color[] colors;
    public bool tree;
    public bool berry;

    private void OnEnable()
    {
        SpawnObject();
    }

    private void OnDisable()
    {
        Destroy(currentObject);
    }

    public void SpawnObject()
    {
        currentObject = Instantiate(prefab, TempObjects);
        currentObject.name = FixName(currentObject.name);
        currentObject.AddComponent<GroundPlacementControllerNonBuilder>().currentPlaceableObject = currentObject;
        currentObject.GetComponent<GroundPlacementControllerNonBuilder>().parent = parent;
        currentObject.GetComponent<GroundPlacementControllerNonBuilder>().placingSeeds = this;
        currentObject.GetComponent<GroundPlacementControllerNonBuilder>().inventoryManager = inventoryManager;
        currentObject.GetComponent<GroundPlacementControllerNonBuilder>().forestGenerator = forestGenerator;
        currentObject.GetComponent<GroundPlacementControllerNonBuilder>().itemIndex = itemIndex;
        currentObject.GetComponent<GroundPlacementControllerNonBuilder>().TreeParent = TreeParent;
        currentObject.GetComponent<GroundPlacementControllerNonBuilder>().tree = tree;
        currentObject.GetComponent<GroundPlacementControllerNonBuilder>().berry = berry;
        currentObject.GetComponent<PickUpPopUp>().enabled = false;

        currentObject.AddComponent<ColorChange>().touchingColors = colors;
    }

    private string FixName(string name)
    {
        int strSet = name.IndexOf("(");

        return name.Substring(0, strSet) + "(regrow)";
    }
}