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
    public InventoryManager inventoryManager;

    public PlayerMovement playerMovement;
    public MouseLook mouseLook;

    public GameObject jobBoard;
    public GameObject Jobs;
    public GameObject buildings;

    //GetChild(transform.childCount - 1) = "bye"
    //GetChild(0) = "want to join?"
    //GetChild(1) = "Give item"
    //GetChild(2) = "join job"

    private void OnEnable()
    {
        CloseChildren();
        transform.GetChild(transform.childCount - 1).gameObject.SetActive(true); //should always be on
        if (!ResidentTalkingTo.GetComponent<ResidentStats>().joinedTown && ResidentTalkingTo.transform.parent.name == "Resident" && !buildMenuUpdater.AccessToLevel0Buildings)
        {
            transform.GetChild(0).gameObject.SetActive(true); //"want to join" option
        }

        if(ResidentTalkingTo.GetComponent<ResidentWander>().job == null && ResidentTalkingTo.GetComponent<ResidentStats>().joinedTown)
        {
            transform.GetChild(2).gameObject.SetActive(true);
        }
    }

    public void JoinSettlement()
    {
        int amount = 1;
        
        if (inventoryManager.GetAmountByName(ResidentTalkingTo.GetComponent<ResidentStats>().FavoriteItem) >= amount)
        {
            //inventory.RemoveItem(new Item { itemType = ResidentTalkingTo.GetComponent<ResidentStats>().FavoriteItem, amount = amount, index = inventory.GetLastLocationOfItem(ResidentTalkingTo.GetComponent<ResidentStats>().FavoriteItem) });
            inventoryManager.RemoveItem(ResidentTalkingTo.GetComponent<ResidentStats>().FavoriteItem);
            ResidentTalkingTo.GetComponent<ResidentStats>().joinedTown = true;
            ResidentTalkingTo.GetComponent<ResidentStats>().namebar.color = Color.green;
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(true);
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

    public void JobBoard()
    {
        CloseJobs();

        jobBoard.SetActive(true);
        //TextBox.gameObject.SetActive(false);
        gameObject.SetActive(false);

        //enabling which jobs are open
        for(int i = 0; i < buildings.transform.childCount; i++)
        {
            if (buildings.transform.GetChild(i).GetComponent<IsAJob>() == null) continue;

            if(buildings.transform.GetChild(i).childCount != 0)
            {
                Jobs.transform.GetChild(ReturnJob(i)).gameObject.SetActive(true);
            }
        }
    }

    public void BackFromJobBoard()
    {
        jobBoard.SetActive(false);
        //TextBox.gameObject.SetActive(true);
        gameObject.SetActive(true);
    }

    public void Bye()
    {
        CloseMenus();
    }

    private void CloseMenus()
    {
        jobBoard.SetActive(false);
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

    private void CloseJobs()
    {
        foreach (Transform child in Jobs.transform)
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

    private int ReturnJob(int i)
    {
        //case is the position of the Job in the building spawn location
        //return is the position on the job board;
        switch (i)
        {
            default:
            case 2: return 0;
            case 3: return 1;
            case 4: return 2;
        }
    }
}
