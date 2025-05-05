using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Messhall : MonoBehaviour
{
    [Header("Messhall stuff")]
    public GameObject walkable;
    public GameObject farm;

    [Header("inventory stuff")]
    public int size;
    public GameObject prefab;
    public GameObject inventoryItemPrefab;

    [Header("Raw Food")]
    public GameObject rawFood;
    public InventorySlot[] rawFoodSlots;

    [Header("Cooked Food")]
    public GameObject cookedFood;
    public InventorySlot[] cookedFoodSlots;

    public bool addFood;

    public Job job;

    private void Awake()
    {
        job = gameObject.GetComponent<Job>();
        rawFoodSlots = new InventorySlot[size];
        cookedFoodSlots = new InventorySlot[size];

        for (int i = 0; i < size; i++)
        {
            GameObject slot1 = Instantiate(prefab, rawFood.transform.GetChild(0));
            GameObject slot2 = Instantiate(prefab, cookedFood.transform.GetChild(0));

            slot1.GetComponent<InventorySlot>().OnlyFood = true;
            slot2.GetComponent<InventorySlot>().OnlyFood = true;
            slot2.GetComponent<InventorySlot>().dontDrop = true;

            slot1.name = FixName(slot1.name) + "(" + i + ")";
            slot2.name = FixName(slot2.name) + "(" + i + ")";

            rawFoodSlots[i] = slot1.GetComponent<InventorySlot>();
            cookedFoodSlots[i] = slot2.GetComponent<InventorySlot>();
        }

        //this is for a test
        //for(int i = 0; i < 2; i++)
        // {
        //    AddItem(new Item { itemType = Item.ItemType.berry }, cookedFoodSlots);
        // }
    }

    void Start()
    {
        farm.GetComponent<Farm>().messhall = this;

        InvokeRepeating("Cook", 0.0f, 5.0f); 
    }

    private void Update()
    {
        if (addFood)
        {
            AddItem(new Item { itemType = Item.ItemType.charredBerry}, cookedFoodSlots);
            addFood = false;
        }
    }

    private void OnDestroy()
    {
        farm.GetComponent<Farm>().messhall = null;
    }

    private void Cook()
    {
        if (job.workersWorking == 0) return;

        if (CheckIfEmpty(rawFoodSlots) && CheckIfFull(cookedFoodSlots) && transform.GetComponent<Job>().WorkersActive[0].GetComponent<Chef>() != null && transform.GetComponent<Job>().WorkersActive[0].GetComponent<Chef>().Cooking) //checks if theres any raw food and if the cooked food slots are full
        {
            Item.ItemType item = GetItemInSlot(rawFoodSlots);

            if (RemoveItem(item, rawFoodSlots)) AddItem(new Item { itemType = StringToFood(item.ToString()) }, cookedFoodSlots); //removes the raw food, adds the cooked food using the name of item to cooked item
        }
    }

    public bool CheckIfRawFoodIsEmpty()
    {
        return CheckIfEmpty(rawFoodSlots);
    }

    private Item.ItemType StringToFood(string name)
    {
        switch (name)
        {
            default:
            case "berry": return Item.ItemType.charredBerry;
        }
    }

    public bool CheckIfEmpty(InventorySlot[] inventorySlots)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            try
            {
                if (inventorySlots[i].transform.childCount == 0) continue;
                return true;
            }
            catch { };
        }

        return false;
    }

    public bool CheckIfFull(InventorySlot[] inventorySlots)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].transform.childCount == 0) return true;

            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot.count != itemInSlot.item.MaxAmount()) return true;
        }

        return false;
    }

    public Item.ItemType GetItemInSlot(InventorySlot[] inventorySlots)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].transform.childCount == 0) continue;

            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            return itemInSlot.item.itemType;
        }

        return Item.ItemType.empty;
    }

    public bool RemoveItem(Item.ItemType item, InventorySlot[] inventorySlots)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot != null && itemInSlot.item.itemType == item)
            {
                if (itemInSlot.count == 0) continue;
                itemInSlot.count--;
                itemInSlot.RefreshCount();
                return true;
            }
        }

        return false;
    }

    public bool AddItem(Item item, InventorySlot[] inventorySlots)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot != null &&
                itemInSlot.item.itemType == item.itemType &&
                itemInSlot.item.IsStackable() &&
                itemInSlot.count < itemInSlot.item.MaxAmount())
            {
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }

        return false;
    }

    private void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGameObject = Instantiate(inventoryItemPrefab, slot.transform);
        newItemGameObject.name = FixName(newItemGameObject.name);
        InventoryItem inventoryItem = newItemGameObject.GetComponent<InventoryItem>();
        inventoryItem.InitializeItem(item);
    }

    public void SetItem(Item item, int amount, InventorySlot slot)
    {
        GameObject newItemGameObject = Instantiate(inventoryItemPrefab, slot.transform);
        newItemGameObject.name = FixName(newItemGameObject.name);
        InventoryItem inventoryItem = newItemGameObject.GetComponent<InventoryItem>();
        inventoryItem.InitializeItem(item);

        inventoryItem.count = amount;
        inventoryItem.RefreshCount();
    }

    public Item.ItemType[] ReturnItemListType(InventorySlot[] inventorySlots)
    {
        Item.ItemType[] itemList = new Item.ItemType[inventorySlots.Length];

        for (int i = 0; i < itemList.Length; i++)
        {
            if (inventorySlots[i].transform.childCount == 0) continue;

            InventoryItem inventoryItem = inventorySlots[i].transform.GetChild(0).GetComponent<InventoryItem>();
            itemList[i] = inventoryItem.item.itemType;
        }

        return itemList;
    }

    public int[] ReturnItemListAmount(InventorySlot[] inventorySlots)
    {
        int[] itemList = new int[inventorySlots.Length];

        for (int i = 0; i < itemList.Length; i++)
        {
            if (inventorySlots[i].transform.childCount == 0) continue;

            InventoryItem inventoryItem = inventorySlots[i].transform.GetChild(0).GetComponent<InventoryItem>();
            itemList[i] = inventoryItem.count;
        }

        return itemList;
    }

    private string FixName(string name)
    {
        int strSet = name.IndexOf("(");

        return name.Substring(0, strSet);
    }
}
