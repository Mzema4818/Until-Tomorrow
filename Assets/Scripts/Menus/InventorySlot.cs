using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public Image image;
    public Color selectedColor;
    public Color notSelectedColor;
    public InventoryManager inventoryManager;

    [Header("Halfstack stuff")]
    public Transform player;
    public Transform dropableParent;

    public bool OnlyFood;
    public bool dontDrop;

    private void Start()
    {
        image = transform.GetComponent<Image>();
        //Deselect(); //this was on last time i used this, it might have another use i forgot about
    }

    public void Select()
    {
        transform.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.2f, 1.2f);
        transform.GetComponent<Image>().color = selectedColor;
    }

    public void Deselect()
    {
        transform.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        transform.GetComponent<Image>().color = notSelectedColor;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!dontDrop)
        {
            InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();

            //Below was causing issues with dropping items into chests cause it would do the same thing,
            //its not reallllly needed anymore because the change in size is smaller and it changes color so its hard to notice

            /*//dropping an item into an active slot/non active slot keeps its size it should be and item held gets changed
            if (inventoryManager.selectedSlot == transform.GetSiblingIndex())
            {
                inventoryManager.changeSlotOnDropSame(inventoryItem.item.itemType);
                inventoryItem.GetComponent<RectTransform>().localScale = new Vector3(1.5f, 1.5f, 1.5f);
            }
            else 
            {
                inventoryManager.ChangeSlotOnDrop(inventoryItem.item.itemType);
                inventoryItem.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
            }*/

            if (OnlyFood && inventoryItem.item.IsFood())
            {
                if (inventoryItem.ClickedLeft)
                {
                    if (transform.childCount == 0)
                    {
                        inventoryItem.ParentAfterDrag = transform;
                    }
                    else
                    {
                        InventoryItem thisInventory = transform.GetChild(0).GetComponent<InventoryItem>();

                        if (thisInventory.item.itemType == inventoryItem.item.itemType &&
                            thisInventory.count + inventoryItem.count <= inventoryItem.item.MaxAmount())
                        {
                            thisInventory.count += inventoryItem.count;
                            thisInventory.RefreshCount();
                            Destroy(inventoryItem.gameObject);
                            inventoryManager.DraggingItem = false;
                            inventoryManager.ItemDragged = null;
                        }
                    }
                }

                if (inventoryItem.ClickedRight)
                {
                    if (transform.childCount == 0)
                    {
                        GameObject halfStack = inventoryItem.halfStack;

                        halfStack.transform.SetParent(transform);
                        halfStack.GetComponent<Image>().raycastTarget = true;
                        GiveItem(halfStack, inventoryItem);

                        inventoryItem.halfStack = null;
                    }
                    else
                    {
                        GameObject halfStack = inventoryItem.halfStack;
                        GiveItem(halfStack, inventoryItem);

                        InventoryItem thisInventory = transform.GetChild(0).GetComponent<InventoryItem>();
                        InventoryItem halfStackInventoryItem = halfStack.GetComponent<InventoryItem>();

                        if (thisInventory.item.itemType == halfStackInventoryItem.item.itemType &&
                            thisInventory.count + halfStackInventoryItem.count <= halfStackInventoryItem.item.MaxAmount())
                        {
                            thisInventory.count += halfStackInventoryItem.count;
                            thisInventory.RefreshCount();
                            Destroy(halfStackInventoryItem.gameObject);
                            inventoryItem.halfStack = null;
                            inventoryManager.DraggingItem = false;
                            inventoryManager.ItemDragged = null;
                        }
                    }
                }
            }
            else if (!OnlyFood)
            {
                if (inventoryItem.ClickedLeft)
                {
                    if (transform.childCount == 0)
                    {
                        inventoryItem.ParentAfterDrag = transform;
                    }
                    else
                    {
                        InventoryItem thisInventory = transform.GetChild(0).GetComponent<InventoryItem>();

                        if (thisInventory.item.itemType == inventoryItem.item.itemType &&
                            thisInventory.count + inventoryItem.count <= inventoryItem.item.MaxAmount())
                        {
                            thisInventory.count += inventoryItem.count;
                            thisInventory.RefreshCount();
                            Destroy(inventoryItem.gameObject);
                            inventoryManager.DraggingItem = false;
                            inventoryManager.ItemDragged = null;
                        }
                    }
                }

                if (inventoryItem.ClickedRight)
                {
                    if (transform.childCount == 0)
                    {
                        GameObject halfStack = inventoryItem.halfStack;

                        halfStack.transform.SetParent(transform);
                        halfStack.GetComponent<Image>().raycastTarget = true;
                        GiveItem(halfStack, inventoryItem);

                        inventoryItem.halfStack = null;
                    }
                    else
                    {
                        GameObject halfStack = inventoryItem.halfStack;
                        GiveItem(halfStack, inventoryItem);

                        InventoryItem thisInventory = transform.GetChild(0).GetComponent<InventoryItem>();
                        InventoryItem halfStackInventoryItem = halfStack.GetComponent<InventoryItem>();

                        if (thisInventory.item.itemType == halfStackInventoryItem.item.itemType &&
                            thisInventory.count + halfStackInventoryItem.count <= halfStackInventoryItem.item.MaxAmount())
                        {
                            thisInventory.count += halfStackInventoryItem.count;
                            thisInventory.RefreshCount();
                            Destroy(halfStackInventoryItem.gameObject);
                            inventoryItem.halfStack = null;
                            inventoryManager.DraggingItem = false;
                            inventoryManager.ItemDragged = null;
                        }
                    }
                }
            }
        }
    }

    private void GiveItem(GameObject gameObject, InventoryItem information)
    {
        gameObject.AddComponent<InventoryItem>();

        InventoryItem halfStackInventoryItem = gameObject.GetComponent<InventoryItem>();
        halfStackInventoryItem.image = gameObject.GetComponent<Image>();
        halfStackInventoryItem.countText = gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        halfStackInventoryItem.item = new Item { itemType = information.item.itemType };
        halfStackInventoryItem.count = int.Parse(halfStackInventoryItem.countText.text);
        halfStackInventoryItem.inventoryManager = inventoryManager;
        halfStackInventoryItem.dropableParent = dropableParent;
        halfStackInventoryItem.player = player;
        halfStackInventoryItem.root = information.root;
        halfStackInventoryItem.RefreshCount();
    }
}
