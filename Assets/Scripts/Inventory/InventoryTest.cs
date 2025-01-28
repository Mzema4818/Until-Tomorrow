using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTest : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public bool displayInventory = false;

   public void SpawnRock()
    {
        //new Item { itemType = Item.ItemType.wood, amount = 2 };
        for (int i = 0; i < 20; i++)
        {
            bool result = inventoryManager.AddItem(new Item { itemType = Item.ItemType.wood });
        }
    }

    public void SpawnWood()
    {
        //new Item { itemType = Item.ItemType.wood, amount = 2 };
        for (int i = 0; i < 20; i++)
        {
            bool result = inventoryManager.AddItem(new Item { itemType = Item.ItemType.rock });
        }
    }

    public void SpawnFlower()
    {
        //new Item { itemType = Item.ItemType.wood, amount = 2 };
        for (int i = 0; i < 1; i++)
        {
            bool result = inventoryManager.AddItem(new Item { itemType = Item.ItemType.hammer });
        }
    }

    private void Update()
    {
        if (displayInventory)
        {
            inventoryManager.DisplayInventory();
            displayInventory = false;
        }
    }
}
