using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayInventory : MonoBehaviour
{
    public InventoryManager inventoryManager;
    private void OnTriggerEnter(Collider other)
    {
        ItemWorld itemWorld = other.transform.GetComponent<ItemWorld>();
        if (other.transform.parent != null)
        {
            itemWorld = other.transform.parent.transform.GetComponent<ItemWorld>();
            if (itemWorld == null)
            {
                itemWorld = other.transform.GetComponent<ItemWorld>();
            }
        }

        if (itemWorld != null)
        {
            inventoryManager.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
            return;
        }
    }
}
