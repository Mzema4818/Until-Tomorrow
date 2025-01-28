using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    public Transform itemSlotTemplate;
    public bool HalfStackActive;
    public GameObject HalfStack;

    private GameObject[] tools;

    private OpenMenus openMenus;

    public bool TurnOffOnStart;

    private void Awake()
    {
        if(TurnOffOnStart) gameObject.SetActive(false);
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryItems();
    }

    public Inventory ReturnInventory()
    {
        return inventory;
    }

    public void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        print("refreshed inventory");
        foreach(Transform child in transform)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }

        int i = 0;

        foreach (Item item in inventory.GetArray())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, transform).GetComponent<RectTransform>();
            itemSlotRectTransform.name = itemSlotRectTransform.name + "_" + i;
            itemSlotRectTransform.gameObject.SetActive(true);
            i++;

            Image image = itemSlotRectTransform.Find("image").GetComponent<Image>();
            if (item == null)
            {
                Destroy(image.gameObject);
                continue;
            }

            image.sprite = item.getSprite();
            TextMeshProUGUI uiText = image.transform.Find("amountText").GetComponent<TextMeshProUGUI>();
            //if (item.amount > 1)
            //{
                //uiText.SetText(item.amount.ToString());
            //}
            //else
           //{
                //uiText.SetText("");
            //}

            ItemHeld itemHeld = itemSlotRectTransform.Find("image").GetComponent<ItemHeld>();
            itemHeld.inventory = inventory;
            itemHeld.item = item;
        }
    }
}
