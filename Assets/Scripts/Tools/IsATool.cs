using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsATool : MonoBehaviour
{
    public InventoryManager inventory;
    public Item.ItemType item;

    //NEED THIS EMPTY SCRIPT FOR CHECKING CROSSHAIR PLACEMENT DONT DELETE
    public void AddItem()
    {
        inventory.AddItem(new Item { itemType = item });
    }
}
