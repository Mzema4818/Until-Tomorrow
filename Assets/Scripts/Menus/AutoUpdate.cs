using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AutoUpdate : MonoBehaviour
{
    private TextMeshProUGUI StorageText;
    public PlayInventory playInventory;
    public InventoryManager inventoryManager;
    public GameObject storage;

    // Start is called before the first frame update
    private void Awake()
    {
        StorageText = transform.GetComponent<TextMeshProUGUI>();
        //inventory = playInventory.ReturnInventory();
    }

    // Update is called once per frame
    void Update()
    {
       /* if (storage.GetComponent<Mine>() != null)
        {
            StorageText.text = "Stone: " + storage.GetComponent<Mine>().Drops[0] + "\n";
        }else if(storage.GetComponent<Lumbermill>() != null)
        {
            StorageText.text = "Wood: " + storage.GetComponent<Lumbermill>().Drops[0] + "\n" +
                               "Saplings " + storage.GetComponent<Lumbermill>().Drops[1];
        }else if(storage.GetComponent<Farm>() != null)
        {
            StorageText.text = "Berry: " + storage.GetComponent<Farm>().Drops[0] + "\n";
        }
        else
        {
            gameObject.SetActive(false);*/
        //}
    }

    public void Collect()
    {
        /*if (storage.GetComponent<Mine>() != null)
        {
            Mine mine = storage.GetComponent<Mine>();
            for (int i = 0; i < mine.Drops[0]; i++) inventoryManager.AddItem(new Item { itemType = Item.ItemType.rock }); //Adding item
            for (int i = 0; i < mine.Drops.Length; i++) mine.Drops[i] = 0; //Making that item have 0
        }else if(storage.GetComponent<Lumbermill>() != null)
        {
            Lumbermill lumbermill = storage.GetComponent<Lumbermill>();
            for (int i = 0; i < lumbermill.Drops[0]; i++) inventoryManager.AddItem(new Item { itemType = Item.ItemType.wood }); //Adding item
            for (int i = 0; i < lumbermill.Drops[1]; i++) inventoryManager.AddItem(new Item { itemType = Item.ItemType.sapling }); //Adding item
            for (int i = 0; i < lumbermill.Drops.Length; i++) lumbermill.Drops[i] = 0; //Making that item have 0
        }else if(storage.GetComponent<Farm>() != null)
        {
            Farm farm = storage.GetComponent<Farm>();
            for (int i = 0; i < farm.Drops[0]; i++) inventoryManager.AddItem(new Item { itemType = Item.ItemType.berry }); //Adding item
            for (int i = 0; i < farm.Drops.Length; i++) farm.Drops[i] = 0; //Making that item have 0
        }*/
    }

    private void OnDisable()
    {
        storage = null;
    }
}
