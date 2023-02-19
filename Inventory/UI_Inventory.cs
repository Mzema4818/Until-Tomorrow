using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private Transform itemSlotContainer;
    private Transform itemSlotTemplate;

    public GameObject player;
    public GameObject equipablesItems;
    private GameObject[] equipables;

    public GameObject toolsItems;
    private GameObject[] tools;

    private OpenMenus openMenus;
    public int spacing = 4;

    private void Awake()
    {
        itemSlotContainer = transform.Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
        openMenus = player.GetComponent<OpenMenus>();
        transform.parent.gameObject.SetActive(false);

        equipables = new GameObject[equipablesItems.transform.childCount];
        for(int i = 0; i < equipables.Length; i++)
        {
            equipables[i] = equipablesItems.transform.GetChild(i).gameObject;
        }

        tools = new GameObject[toolsItems.transform.childCount];
        for (int i = 0; i < tools.Length; i++)
        {
            tools[i] = toolsItems.transform.GetChild(i).gameObject;
        }
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventoryItems();
    }

    public void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }

    private void RefreshInventoryItems()
    {
        foreach(Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }

        int x = 0;
        int y = 0;
        float itemSlotCellSize = 30f;

        foreach(Item item in inventory.GetItemList())
        {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

            //drop item
            itemSlotRectTransform.GetComponent<Button_UI>().ClickFunc = () => //left click
            {
                Item duplicateItem = new Item { itemType = item.itemType, amount = 1 };
                inventory.RemoveItem(new Item { itemType = item.itemType, amount = 1 });
                ItemWorld.DropItem(player.transform, duplicateItem);
            };

            //use item
            itemSlotRectTransform.GetComponent<Button_UI>().MouseRightClickFunc = () => //right click
            {
                if (item.IsEquipable())
                {
                    DisableTools();
                    transform.parent.gameObject.SetActive(false);
                    openMenus.changePlayerState(true);

                    equipables[item.EquipableAction()].SetActive(true);
                    
                    //Check if its a food item
                    if(equipables[item.EquipableAction()].GetComponent<Food>() != null)
                    {
                        if (equipables[item.EquipableAction()].GetComponent<Food>().inventory == null)
                        {
                            equipables[item.EquipableAction()].GetComponent<Food>().inventory = inventory;
                        }
                        equipables[0].GetComponent<Food>().item = item;
                    }

                    //Check if its a seed item
                    if (equipables[item.EquipableAction()].GetComponent<PlacingSeeds>() != null)
                    {
                        if (equipables[item.EquipableAction()].GetComponent<PlacingSeeds>().inventory == null)
                        {
                            equipables[item.EquipableAction()].GetComponent<PlacingSeeds>().inventory = inventory;
                        }
                        equipables[1].GetComponent<PlacingSeeds>().item = item;
                    }

                    //Check if its a sapling
                    if (equipables[item.EquipableAction()].GetComponent<PlacingSaplings>() != null)
                    {
                        if (equipables[item.EquipableAction()].GetComponent<PlacingSaplings>().inventory == null)
                        {
                            equipables[item.EquipableAction()].GetComponent<PlacingSaplings>().inventory = inventory;
                        }
                        equipables[2].GetComponent<PlacingSaplings>().item = item;
                    }

                }
            };

            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, y * itemSlotCellSize);
            Image image = itemSlotRectTransform.Find("image").GetComponent<Image>();
            image.sprite = item.getSprite();
            TextMeshProUGUI uiText = itemSlotRectTransform.Find("amountText").GetComponent<TextMeshProUGUI>();
            if(item.amount > 1)
            {
                uiText.SetText(item.amount.ToString());
            }
            else
            {
                uiText.SetText("");
            }

            x += (1 * spacing);
            if(x > 6 * spacing)
            {
                x = 0;
                y -= (1 * spacing);
            }
        }
    }

    private void DisableTools()
    {
        foreach(GameObject tool in tools)
        {
            tool.SetActive(false);
        }
    }
}
