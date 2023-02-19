using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public event EventHandler OnItemListChanged;
    private List<Item> itemList;

    public Inventory()
    {
        itemList = new List<Item>();
    }

    public void AddItem(Item item)
    {
        if (item.IsStackable())
        {
            bool itemAlreadyInInventory = false;
            foreach(Item inventoryItem in itemList)
            {
                if(inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount += item.amount;
                    itemAlreadyInInventory = true;
                }
            }
            if (!itemAlreadyInInventory)
            {
                itemList.Add(item);
            }
        }
        else
        {
            itemList.Add(item);
        }

        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveItem(Item item)
    {
        if (item.IsStackable())
        {
            Item itemInInventory = null;
            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount -= item.amount;
                    itemInInventory = inventoryItem;
                }
            }
            if (itemInInventory != null && itemInInventory.amount <= 0)
            {
                itemList.Remove(itemInInventory);
            }
        }
        else
        {
            itemList.Remove(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public bool CheckIfEmpty(Item item)
    {
        Item itemInInventory = null;
        foreach (Item inventoryItem in itemList)
        {
            if (inventoryItem.itemType == item.itemType)
            {
                itemInInventory = inventoryItem;
            }
        }

        try
        {
            if (itemInInventory.itemType == item.itemType)
            {
                return false;
            }
        }
        catch { }

        return true;
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }

    public Item.ItemType[] GetItemArrayList()
    {
        Item[] items = GetItemArray();
        Item.ItemType[] itemTypes = new Item.ItemType[itemList.Count];

        for(int i = 0; i < items.Length; i++)
        {
            itemTypes[i] = items[i].itemType;
        }

        return itemTypes;
    }

    public int[] GetItemArrayListAmount()
    {
        Item[] items = GetItemArray();
        int[] itemAmount = new int[itemList.Count];

        for (int i = 0; i < items.Length; i++)
        {
            itemAmount[i] = items[i].amount;
        }

        return itemAmount;
    }

    private Item[] GetItemArray()
    {
        return itemList.ToArray();
    }

    public void TransferItems(Inventory OriginalInventory, Inventory TransferInventory)
    {
        Item.ItemType[] itemTypes = GetItemArrayList();
        int[] itemAmount = GetItemArrayListAmount();

        int stop = GetItemArray().Length;

        for (int i = 0; i < stop; i++)
        {
            //Debug.Log("Inventory Item: " + itemTypes[i] + "    Inventory Amount: " + itemAmount[i]);
            TransferInventory.AddItem(new Item { itemType = itemTypes[i], amount = itemAmount[i] });
            OriginalInventory.RemoveItem(new Item { itemType = itemTypes[i], amount = itemAmount[i] });
        }
    }

    public int GetAmountByName(Item.ItemType itemType)
    {
        for(int i = 0; i < itemList.Count; i++)
        {
            if(itemList[i].itemType == itemType)
            {
                return itemList[i].amount;
            }
        }

        return 0;
    }
}
