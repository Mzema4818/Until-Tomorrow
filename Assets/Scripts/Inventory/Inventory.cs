using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public event EventHandler OnItemListChanged;
    public Item[] itemList;
    public int inventorySize = 35;

    public Inventory(int size)
    {
        itemList = new Item[size];
    }

    public void AddItem(Item item)
    {
        if (item.IsStackable())
        {
            bool itemAlreadyInInventory = false;
            foreach(Item inventoryItem in itemList)
            {
                if (inventoryItem == null) continue;
                //if(inventoryItem.itemType == item.itemType && inventoryItem.amount + 1 <= item.MaxAmount())
                //{
                    //inventoryItem.amount += item.amount;
                    itemAlreadyInInventory = true;
                    break;
                //}
            }
            if (!itemAlreadyInInventory)
            {
                for(int i = 0; i < itemList.Length; i++)
                {
                    if (itemList[i] != null) continue;
                    else
                    {
                        itemList[i] = item;
                        break;
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < itemList.Length; i++)
            {
                if (itemList[i] != null) continue;
                else
                {
                    itemList[i] = item;
                    break;
                }
            }
        }

        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveItem(Item item)
    {
        int i = 0;

        if (item.IsStackable())
        {
            Item itemInInventory = null;
            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem == null)
                {
                    i++; 
                    continue;
                }
                if (inventoryItem.itemType == item.itemType && item.index == i)
                {
                   // inventoryItem.amount -= item.amount;
                    itemInInventory = inventoryItem;
                    break;
                }
                i++;
            }
            //if (itemInInventory != null && itemInInventory.amount <= 0)
            //{
                //itemList.Remove(itemInInventory);
                //itemList[i] = null;
            //}
        }
        else
        {
            itemList[i] = null;
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void SwitchItems(int index1, int index2)
    {
        Item temp = itemList[index1];
        itemList[index1] = itemList[index2];
        itemList[index2] = temp;
    }

    //broken
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
        List<Item> returnItem = new List<Item>();
        return returnItem;
    }

    public string[] GetItemArrayList()
    {
        string[] itemTypes = new string[itemList.Length];

        for(int i = 0; i < itemList.Length; i++)
        {
            if (itemList[i] == null) continue;
            itemTypes[i] = itemList[i].itemType.ToString();
        }

        return itemTypes;
    }

    public int[] GetItemArrayListAmount()
    {
        int[] itemAmount = new int[itemList.Length];

        for (int i = 0; i < itemList.Length; i++)
        {
            if (itemList[i] == null) continue;
            //itemAmount[i] = itemList[i].amount;
        }

        return itemAmount;
    }

    private Item[] GetItemArray()
    {
        return itemList;
    }

    /*public void TransferItems(Inventory OriginalInventory, Inventory TransferInventory)
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
    }*/

    public int[] GetAmountArrayByName(Item.ItemType itemType)
    {
        int[] num = new int[itemList.Length];

        for(int i = 0; i < itemList.Length; i++)
        {
            if (itemList[i] == null) continue;
            if (itemList[i].itemType == itemType)
            {
                //num[i] = itemList[i].amount;
            }
        }

        return num;
    }

    public int GetAmountByName(Item.ItemType itemType)
    {
        int num = 0;

        for (int i = 0; i < itemList.Length; i++)
        {
            if (itemList[i] == null) continue;
            if (itemList[i].itemType == itemType)
            {
                //num += itemList[i].amount;
            }
        }

        return num;
    }

    public int GetLastLocationOfItem(Item.ItemType itemType)
    {
        int[] itemArray = GetAmountArrayByName(itemType);

        for (int i = 0; i < itemArray.Length; i++)
        {
            if (itemArray[i] == 0) continue;
            return i;
        }

        return -1;
    }

    public Item[] GetArray()
    {
        return itemList;
    }

    public void ChangeAmount(int index1, int index2, int amount)
    {
        //itemList[index1].amount = amount;
        itemList[index2] = null;
    }

    public void ChangeAmount(int index1, int index2, int amount1, int amount2)
    {
        //itemList[index1].amount = amount1;
        //itemList[index2].amount = amount2;
    }

    public void ChangeAmount(int index, int amount)
    {
        //itemList[index].amount = amount;
    }

    public void SetItem(int index, Item item)
    {
        itemList[index] = item;
    }

    public void RefreshInventory()
    {
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }
}
