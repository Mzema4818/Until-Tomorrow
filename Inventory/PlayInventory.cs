using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayInventory : MonoBehaviour
{
    public bool test = false;
#pragma warning disable IDE0051 // Remove unused private members
    private readonly bool canAdd = true;
#pragma warning restore IDE0051 // Remove unused private members
    [SerializeField] private UI_Inventory uiInventory;
    [SerializeField] private PlayerMovement playerMovement;

    private Inventory inventory;

    // Start is called before the first frame update
    private void Start()
    {
        inventory = new Inventory();
        uiInventory.SetInventory(inventory);
        inventory.AddItem(new Item { itemType = Item.ItemType.rock, amount = 100 });
        inventory.AddItem(new Item { itemType = Item.ItemType.wood, amount = 100 });
        inventory.AddItem(new Item { itemType = Item.ItemType.mushroom, amount = 100 });
        inventory.AddItem(new Item { itemType = Item.ItemType.berry, amount = 100 });
        inventory.AddItem(new Item { itemType = Item.ItemType.bush, amount = 100 });
        inventory.AddItem(new Item { itemType = Item.ItemType.flower, amount = 100 });
        inventory.AddItem(new Item { itemType = Item.ItemType.sapling, amount = 100 });
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //Picking Up Items
        ItemWorld itemWorld = hit.transform.GetComponent<ItemWorld>();
        if(hit.transform.parent != null)
        {
            itemWorld = hit.transform.parent.transform.GetComponent<ItemWorld>();
            if(itemWorld == null)
            {
                itemWorld = hit.transform.GetComponent<ItemWorld>();
            }
        }

        if (playerMovement.first) //this first if statement because controller.move moves multiple times per .move so we slow down detections 
        {
            if (itemWorld != null)
            {
                inventory.AddItem(itemWorld.GetItem());
                itemWorld.DestroySelf();
            }
            playerMovement.first = false;
        }
        else
        {
            return;
        }
    }

    public Inventory ReturnInventory()
    {
        return inventory;
    }

}
