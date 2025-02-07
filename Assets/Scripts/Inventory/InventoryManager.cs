using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject[] ListOfHeldItems;
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;
    public Builder builder;
    public Animator armsAnimator;
    public PlayerAttack playerAttack;
    //public Animator armsAnimator;
    
    [HideInInspector] public bool DraggingItem;
    [HideInInspector] public GameObject ItemDragged;
    public OpenMenus openMenus;

    public bool firstLoadGame = false;
    public int selectedSlot = -1;
    public int selectedItem = 0;
    //public string currentAnimationState;

    //Change depending on how many hotbar slots we have (rn its 7)
    [HideInInspector] public KeyCode[] keyCodes = {
         KeyCode.Alpha1,
         KeyCode.Alpha2,
         KeyCode.Alpha3,
         KeyCode.Alpha4,
         KeyCode.Alpha5,
         KeyCode.Alpha6,
         KeyCode.Alpha7,
         //KeyCode.Alpha8,
         //KeyCode.Alpha9,
     };

    public GameObject gameOverScreen;

    private void Update()
    {
        if (gameOverScreen.activeSelf) return;

        for (int i = 0; i < keyCodes.Length; i++)
        {
            if (Input.GetKeyDown(keyCodes[i]))
            {
                int number = i + 1;
                if (openMenus.CheckIfMenusAreClosed() && openMenus.CheckIfOtherMenusAreClosed() && number - 1 != selectedSlot)
                {
                    ChangeSelectedSlot(number - 1);
                    ListOfHeldItems[selectedItem].SetActive(false);
                    if (GetSelectedItem() != null && GetSelectedItem().IsEquipable())
                    {
                        //ChangeAnimationState(GetSelectedItem().GetArmAnimation());
                        int num = ReturnNumByItem(GetSelectedItem().itemType);

                        if (ListOfHeldItems[num].GetComponent<Food>() != null) ListOfHeldItems[num].GetComponent<Food>().itemIndex = selectedSlot;
                        if (ListOfHeldItems[num].GetComponent<PlacingSeeds>() != null) ListOfHeldItems[num].GetComponent<PlacingSeeds>().itemIndex = selectedSlot;
                        ListOfHeldItems[num].SetActive(true);
                        selectedItem = num;
                    }
                    else
                    {
                        //When you are holding nothing, go back to idle animation for arms
                        armsAnimator.SetTrigger("Not Holding");
                        playerAttack.canAttack = false;
                        playerAttack.ATTACK1 = "";
                        playerAttack.ATTACK2 = "";
                        selectedItem = 0;
                    }

                }
            }
        }

        if (openMenus.CheckIfMenusAreClosed() && openMenus.CheckIfOtherMenusAreClosed() && !builder.isBuilding)
        {
            if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
            {
                if (selectedSlot + 1 > keyCodes.Length - 1) ChangeSelectedSlot(0);
                else ChangeSelectedSlot(selectedSlot + 1);

                ChangeItemSlotInHand();
            }
            else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
            {
                if (selectedSlot - 1 < 0) ChangeSelectedSlot(keyCodes.Length - 1);
                else ChangeSelectedSlot(selectedSlot - 1);

                ChangeItemSlotInHand();
            }
        }
        /*if(Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString, out int number);

        }*/
    }

    /*public void ChangeAnimationState(string newState)
    {
        // STOP THE SAME ANIMATION FROM INTERRUPTING WITH ITSELF //
        if (currentAnimationState == newState) return;

        // PLAY THE ANIMATION //
        currentAnimationState = newState;
        armsAnimator.SetBool(currentAnimationState, true);

        foreach (AnimatorControllerParameter parameter in armsAnimator.parameters)
        {
            if (parameter.name == currentAnimationState) continue;
            armsAnimator.SetBool(parameter.name, false);
        }
    }*/

    public void ChangeSlot(){
        if(selectedSlot == -1) return;

        ListOfHeldItems[selectedItem].SetActive(false);
        if (GetSelectedItem() != null && GetSelectedItem().IsEquipable())
        {
            int num = ReturnNumByItem(GetSelectedItem().itemType);

            if (ListOfHeldItems[num].GetComponent<Food>() != null) ListOfHeldItems[num].GetComponent<Food>().itemIndex = selectedSlot;
            if (ListOfHeldItems[num].GetComponent<PlacingSeeds>() != null) ListOfHeldItems[num].GetComponent<PlacingSeeds>().itemIndex = selectedSlot;
            ListOfHeldItems[num].SetActive(true);
            selectedItem = num;
        }

    }
    
    public void ChangeSelectedSlot(int newValue)
    {
        if (selectedSlot >= 0) inventorySlots[selectedSlot].Deselect();

        inventorySlots[newValue].Select();
        selectedSlot = newValue;
    }

    public bool AddItem(Item item)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot != null && 
                itemInSlot.item.itemType == item.itemType &&
                itemInSlot.item.IsStackable() &&
                itemInSlot.count < itemInSlot.item.MaxAmount())
            {
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if(itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                if(i == selectedSlot) ChangeSlot(); //Makes it so on pickup, if you are selecting the slot that an item "spawns" in, you hold that spawned in item
                return true;
            }
        }

        return false;
    }

    public int AddItemNum(Item item)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot != null && 
                itemInSlot.item.itemType == item.itemType &&
                itemInSlot.item.IsStackable() &&
                itemInSlot.count < itemInSlot.item.MaxAmount())
            {
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return i;
            }
        }

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if(itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return i;
            }
        }

        return -1;
    }

    public bool RemoveItemAtIndex(int index, int amount)
    {
        bool returnBool = false;

        InventoryItem itemInSlot = inventorySlots[index].GetComponentInChildren<InventoryItem>();
        if (itemInSlot.count - amount <= 0) returnBool = true;
        itemInSlot.count -= amount;
        itemInSlot.RefreshCount();

        return returnBool;
    }

    public void RemoveItem(Item.ItemType item)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if(itemInSlot != null && itemInSlot.item.itemType == item)
            {
                if (itemInSlot.count == 0) continue;
                itemInSlot.count--;
                itemInSlot.RefreshCount();
                return;
            }
        }
    }

    private void RemoveAllAtIndex(int index)
    {
        InventoryItem itemInSlot = inventorySlots[index].GetComponentInChildren<InventoryItem>();
        itemInSlot.count = -1;
        itemInSlot.RefreshCount();
    }

    private void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGameObject = Instantiate(inventoryItemPrefab, slot.transform);
        newItemGameObject.name = FixName(newItemGameObject.name);
        InventoryItem inventoryItem = newItemGameObject.GetComponent<InventoryItem>();
        inventoryItem.InitializeItem(item);
    }

    private Item GetSelectedItem()
    {
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

        if(itemInSlot != null)
        {
            return itemInSlot.item;
        }

        return null;
    }

    public void Test()
    {
        /*for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot != null)
            {
                print(itemInSlot.item.itemType);
            }
        }*/
    }

    private int ReturnNumByItem(Item.ItemType item)
    {
        switch (item)
        {
            default:
            case Item.ItemType.axe: return 0;
            case Item.ItemType.pickaxe: return 1;
            case Item.ItemType.sword: return 2;
            case Item.ItemType.hammer: return 3;
            case Item.ItemType.berry: return 4;
            case Item.ItemType.bush: return 5;
            case Item.ItemType.sapling: return 6;
            case Item.ItemType.rock: return 7;
            case Item.ItemType.wood: return 8;
            case Item.ItemType.flower: return 9;
            case Item.ItemType.mushroom: return 10;
            case Item.ItemType.charredBerry: return 11;
        }
    } 

    public void TurnOffItem()
    {
        ListOfHeldItems[selectedItem].SetActive(false);
        try { inventorySlots[selectedSlot].Deselect(); } catch{ };
        selectedSlot = -1;
    }

    public void SetItem(Item item, int amount, InventorySlot slot)
    {
        GameObject newItemGameObject = Instantiate(inventoryItemPrefab, slot.transform);
        newItemGameObject.name = FixName(newItemGameObject.name);
        InventoryItem inventoryItem = newItemGameObject.GetComponent<InventoryItem>();
        inventoryItem.InitializeItem(item);

        inventoryItem.count = amount;
        inventoryItem.RefreshCount();
    }

    public string[] ReturnItemListTypeString()
    {
        string[] itemList = new string[inventorySlots.Length];

        for(int i = 0; i < itemList.Length; i++)
        {
            if (inventorySlots[i].transform.childCount == 0) continue;

            InventoryItem inventoryItem = inventorySlots[i].transform.GetChild(0).GetComponent<InventoryItem>();
            itemList[i] = inventoryItem.item.itemType.ToString();
        }

        return itemList;
    }

    public int[] ReturnItemListAmount()
    {
        int[] itemAmount = new int[inventorySlots.Length];

        for (int i = 0; i < itemAmount.Length; i++)
        {
            if (inventorySlots[i].transform.childCount == 0) continue;

            TextMeshProUGUI inventoryAmount = inventorySlots[i].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
            itemAmount[i] = int.Parse(inventoryAmount.text);
        }

        return itemAmount;
    }

    public Item.ItemType[] ReturnItemListType()
    {
        Item.ItemType[] itemList = new Item.ItemType[inventorySlots.Length];

        for (int i = 0; i < itemList.Length; i++)
        {
            if (inventorySlots[i].transform.childCount == 0) continue;

            InventoryItem inventoryItem = inventorySlots[i].transform.GetChild(0).GetComponent<InventoryItem>();
            itemList[i] = inventoryItem.item.itemType;
        }

        return itemList;
    }

    public int GetAmountByName(Item.ItemType itemType)
    {
        int num = 0;

        int[] itemAmount = ReturnItemListAmount();
        Item.ItemType[] itemList = ReturnItemListType();

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (itemList[i] == itemType)
            {
                num += itemAmount[i];
            }
        }

        return num;
    }

    public void DisplayInventory()
    {
        int[] itemAmount = ReturnItemListAmount();
        string[] itemList = ReturnItemListTypeString();

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (itemList[i] == null)
            {
                print(i);
                continue;
            }
            print(i + "- Item: " + itemList[i] + " Amount: " + itemAmount[i]);
        }
    }

    public int NumberOfItems()
    {
        int num = 0;
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot != null) num++;
        }
            return num;
    }

    public void TransferInventory(Gravestone to)
    {
        int size = NumberOfItems();
        int index = 0;

        to.items = new Item[size];
        to.amounts = new int[size];
        to.size = size;

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot != null)
            {
                to.items[index] = itemInSlot.item;
                to.amounts[index] = itemInSlot.count;
                index++;

                itemInSlot.count = 0;
                itemInSlot.RefreshCount();
            }
        }
    }

    public void ClearInventory()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot != null)
            {
                itemInSlot.count = 0;
                itemInSlot.RefreshCount();
            }
        }
    }

    private string FixName(string name)
    {
        int strSet = name.IndexOf("(");

        return name.Substring(0, strSet);
    }

    private void ChangeItemSlotInHand()
    {
        ListOfHeldItems[selectedItem].SetActive(false);
        if (GetSelectedItem() != null && GetSelectedItem().IsEquipable())
        {
            int num = ReturnNumByItem(GetSelectedItem().itemType);

            if (ListOfHeldItems[num].GetComponent<Food>() != null) ListOfHeldItems[num].GetComponent<Food>().itemIndex = selectedSlot;
            if (ListOfHeldItems[num].GetComponent<PlacingSeeds>() != null) ListOfHeldItems[num].GetComponent<PlacingSeeds>().itemIndex = selectedSlot;
            ListOfHeldItems[num].SetActive(true);
            selectedItem = num;
        }
    }

    public void WipeInventory()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot != null) DestroyImmediate(itemInSlot.gameObject);
        }

    }

    //working here, when inventory wipes item im holding before hand is still on
    public void StartOnSelectedItem()
    {
        inventorySlots[0].Select();
        selectedSlot = 0;

        ListOfHeldItems[selectedItem].SetActive(false);
        selectedItem = 0;
        if (GetSelectedItem() != null && GetSelectedItem().IsEquipable())
        {
            //ChangeAnimationState(GetSelectedItem().GetArmAnimation());
            int num = ReturnNumByItem(GetSelectedItem().itemType);

            if (ListOfHeldItems[num].GetComponent<Food>() != null) ListOfHeldItems[num].GetComponent<Food>().itemIndex = selectedSlot;
            if (ListOfHeldItems[num].GetComponent<PlacingSeeds>() != null) ListOfHeldItems[num].GetComponent<PlacingSeeds>().itemIndex = selectedSlot;
            ListOfHeldItems[num].SetActive(true);
            selectedItem = num;
        }
    }

    public void StartOnItem()
    {
        //Reset arm animations
        ListOfHeldItems[selectedItem].SetActive(false);
        if (selectedSlot != -1) inventorySlots[selectedSlot].Deselect();
        armsAnimator.SetTrigger("Not Holding");
        playerAttack.canAttack = false;
        playerAttack.ATTACK1 = "";
        playerAttack.ATTACK2 = "";

        //Reset hand slots to first slot and turn on item if needed
        selectedSlot = 0;
        selectedItem = 0;
        inventorySlots[selectedSlot].Select();

        if (GetSelectedItem() != null && GetSelectedItem().IsEquipable())
        {
            //ChangeAnimationState(GetSelectedItem().GetArmAnimation());
            int num = ReturnNumByItem(GetSelectedItem().itemType);

            if (ListOfHeldItems[num].GetComponent<Food>() != null) ListOfHeldItems[num].GetComponent<Food>().itemIndex = selectedSlot;
            if (ListOfHeldItems[num].GetComponent<PlacingSeeds>() != null) ListOfHeldItems[num].GetComponent<PlacingSeeds>().itemIndex = selectedSlot;
            ListOfHeldItems[num].SetActive(true);
            selectedItem = num;
        }
    }

    public void ChangeSlotOnDrop(Item.ItemType item)
    {
        ListOfHeldItems[selectedItem].SetActive(false);
        armsAnimator.SetTrigger("Not Holding");
        playerAttack.canAttack = false;
        playerAttack.ATTACK1 = "";
        playerAttack.ATTACK2 = "";
        selectedItem = 0;
    }

    public void changeSlotOnDropSame(Item.ItemType item)
    {
        selectedItem = ReturnNumByItem(item);
        print(item);

        if (ListOfHeldItems[selectedItem].GetComponent<Food>() != null) ListOfHeldItems[selectedItem].GetComponent<Food>().itemIndex = selectedSlot;
        if (ListOfHeldItems[selectedItem].GetComponent<PlacingSeeds>() != null) ListOfHeldItems[selectedItem].GetComponent<PlacingSeeds>().itemIndex = selectedSlot;
        ListOfHeldItems[selectedItem].SetActive(true);
    }

    public void MakeSureUserHasItem()
    {
        
    }
}
