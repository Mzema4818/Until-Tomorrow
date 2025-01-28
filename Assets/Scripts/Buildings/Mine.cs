using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public Job job;

    [Header("inventory stuff")]
    public int size;
    public GameObject prefab;
    public GameObject inventoryItemPrefab;

    [Header("Slots")]
    public GameObject inventory;
    public InventorySlot[] inventorySlots;

    private void Awake()
    {
        job = gameObject.GetComponent<Job>();

        inventorySlots = new InventorySlot[size];
        for (int i = 0; i < size; i++)
        {
            GameObject slot = Instantiate(prefab, inventory.transform.GetChild(0));
            slot.GetComponent<InventorySlot>().dontDrop = true;
            slot.name = FixName(slot.name) + "(" + i + ")";
            inventorySlots[i] = slot.GetComponent<InventorySlot>();
        }
    }

    private void Start()
    {
        InvokeRepeating("GatheringResources", 0.0f, (float)(5 - (job.workersWorking * 1))); //subtracts 1 seconds off for each new worker
    }


    private void GatheringResources()
    {
        if (job.workersWorking == 0) return;
        for(int i = 0; i < Mathf.FloorToInt((1 * job.workersWorking) * Mathf.Pow(job.statMultiplier, 1f / 3f)); i++)
        {
            AddItem(new Item { itemType = Item.ItemType.rock });
        }
    }

    public bool AddItem(Item item)
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

    public Item.ItemType[] ReturnItemListType()
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

    public int[] ReturnItemListAmount()
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
