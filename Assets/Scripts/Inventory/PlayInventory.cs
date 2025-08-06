using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayInventory : MonoBehaviour
{
    public InventoryManager inventoryManager;
    private AudioSource audioSource;
    public AudioClip pickupItem;

    private void Start()
    {
        audioSource = transform.GetComponent<AudioSource>();
    }

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

        if (itemWorld != null && itemWorld.TryPickUp())
        {
            inventoryManager.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
            audioSource.PlayOneShot(pickupItem);
            return;
        }
    }

}
