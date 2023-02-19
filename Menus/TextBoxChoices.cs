using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextBoxChoices : MonoBehaviour
{
    public BuildMenuUpdater buildMenuUpdater;
    public TextMeshProUGUI Announcement;
    public GameObject ResidentTalkingTo;
    public TextMeshProUGUI TextBox;
    public Inventory inventory;

    public PlayerMovement playerMovement;
    public MouseLook mouseLook;

    //GetChild(transform.childCount - 1) = "bye"
    //GetChild(0) = "want to join?"
    //GetChild(1) = "Give item"

    private void OnEnable()
    {
        CloseChildren();
        transform.GetChild(transform.childCount - 1).gameObject.SetActive(true); //should always be on
        if (!ResidentTalkingTo.GetComponent<ResidentStats>().joinedTown && ResidentTalkingTo.transform.parent.name == "Resident" && buildMenuUpdater.AccessToLevel1Buildings)
        {
            transform.GetChild(0).gameObject.SetActive(true); //"want to join" option
        }
    }

    public void JoinSettlement()
    {
        int amount = 1;
        
        if (inventory.GetAmountByName(ResidentTalkingTo.GetComponent<ResidentStats>().FavoriteItem) >= amount)
        {
            inventory.RemoveItem(new Item { itemType = ResidentTalkingTo.GetComponent<ResidentStats>().FavoriteItem, amount = amount });
            ResidentTalkingTo.GetComponent<ResidentStats>().joinedTown = true;
            ResidentTalkingTo.GetComponent<ResidentStats>().namebar.color = Color.green;
            transform.GetChild(1).gameObject.SetActive(false);
            TextBox.text = "Thank you kindly";

            if (!buildMenuUpdater.AccessToLevel2Buildings)
            {
                buildMenuUpdater.AccessToLevel1Buildings = false;
                buildMenuUpdater.AccessToLevel2Buildings = true;
                Announcement.gameObject.SetActive(true);
                Announcement.text = "New Buildings Unlocked";
            }
        }
        else
        {
            TextBox.text = "You can't even do that right? Get out of here";
        }
    }

    public void JoinOption()
    {
        TextBox.text = "Hmm I'll join you for lets say a " + ReturnName(ResidentTalkingTo.GetComponent<ResidentStats>().FavoriteItem);
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Give " + ReturnName(ResidentTalkingTo.GetComponent<ResidentStats>().FavoriteItem);
    }

    public void Bye()
    {
        CloseMenus();
    }

    private void CloseMenus()
    {
        TextBox.gameObject.SetActive(false);
        gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        playerMovement.shouldMove = true;
        mouseLook.shouldLook = true;
    }

    private void CloseChildren()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    private string ReturnName(Item.ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case Item.ItemType.berry: return "berry";
            case Item.ItemType.bush: return "bush";
            case Item.ItemType.flower: return "flower";
            case Item.ItemType.mushroom: return "mushroom";
            case Item.ItemType.rock: return "rock";
            case Item.ItemType.sapling: return "sapling";
            case Item.ItemType.wood: return "wood";
        }
    }
}
