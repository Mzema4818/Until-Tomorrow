using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Crosshair crosshair;
    public int ChestSize;

    public GameObject prefab;
    public GameObject inventoryItemPrefab;
    public GameObject inventory;
    public InventorySlot[] inventorySlots;

    public bool test;

    private void Awake()
    {
        inventorySlots = new InventorySlot[ChestSize];
        for (int i = 0; i < ChestSize; i++)
        {
            GameObject slot = Instantiate(prefab, inventory.transform.GetChild(0));
            slot.name = FixName(slot.name) + "(" + i + ")";
            inventorySlots[i] = slot.GetComponent<InventorySlot>();
        }
    }

    private void Update()
    {
        if (test)
        {
            Item.ItemType[] tester = ReturnItemListType();
            int[] test2 = ReturnItemListAmount();

            for (int i = 0; i < tester.Length; i++)
            {
                print(i + ": " + tester[i] + ": " + test2[i]);
            }

            test = false;
        }
    }

    private void OnDestroy()
    {
        crosshair.BasicCloseMenu(ref crosshair.chestOpen);
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            foreach (Transform child in inventorySlots[i].transform)
            {
                InventoryItem item = child.GetComponent<InventoryItem>();
                int dropCount = item.count;

                for (int j = 0; j < dropCount; j++)
                {
                    item.DropItem();
                }
            }
        }

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
