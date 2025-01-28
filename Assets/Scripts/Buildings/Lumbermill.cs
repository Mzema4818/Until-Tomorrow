using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lumbermill : MonoBehaviour
{
    [Header("lumbermill stuff")]
    public int radius;
    public Transform trees;
    public GameObject SaplingPrefab;
    public ForestGenerator forestGenerator;
    public GameObject treeParent;
    public GameObject saplingParent;

    [Header("inventory stuff")]
    public int size;
    public GameObject prefab;
    public GameObject inventoryItemPrefab;

    [Header("Slots")]
    public GameObject inventory;
    public InventorySlot[] inventorySlots;

    private void Awake()
    {
        inventorySlots = new InventorySlot[size];
        for (int i = 0; i < size; i++)
        {
            GameObject slot = Instantiate(prefab, inventory.transform.GetChild(0));
            slot.GetComponent<InventorySlot>().dontDrop = true;
            slot.name = FixName(slot.name) + "(" + i + ")";
            inventorySlots[i] = slot.GetComponent<InventorySlot>();
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

    public GameObject GetClosestTree()
    {
        foreach (Transform e in trees)
        {
            Transform treeLocation = e.GetComponent<TreeLocation>().location.transform;
            if (Vector3.Distance(treeLocation.position, transform.position) < radius)
            {
                return treeLocation.gameObject;
            }
        }

        return null;
    }

    public void GatheringResources(ResidentStats stats)
    {
        //if (workersActive == 0) return;
        //the "1" means 1 wood as base
        //Drops[0] += Mathf.FloorToInt(Mathf.Pow(stats.Stats[3], 1f / 3f)); //cube root stat multipler
        //if(Random.value < 0.15f) Drops[1] += Mathf.FloorToInt(Mathf.Pow(stats.Stats[3], 1f / 3f)); //15 drop chance

        for (int i = 0; i < Mathf.FloorToInt(Mathf.Pow(stats.Stats[3], 1f / 3f)); i++)
        {
            AddItem(new Item { itemType = Item.ItemType.wood });
        }

        if (Random.value < 0.15f)
        {
            for (int i = 0; i < Mathf.FloorToInt(Mathf.Pow(stats.Stats[3], 1f / 3f)); i++)
            {
                AddItem(new Item { itemType = Item.ItemType.sapling });
            }
        }

    }
}
