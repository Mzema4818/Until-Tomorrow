using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravestone : MonoBehaviour
{
    public InventoryManager inventoryManager;

    public int size;

    public Item[] items;
    public int[] amounts;

    public void ReturnInventory()
    {
        for(int i = 0; i < items.Length; i++)
        {
            while(amounts[i] != 0)
            {
                inventoryManager.AddItem(items[i]);
                amounts[i]--;
            }
        }
    }
}
