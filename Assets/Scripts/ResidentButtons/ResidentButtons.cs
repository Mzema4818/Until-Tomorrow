using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class ResidentButtons : MonoBehaviour
{
    [Header("Type Of Button")]
    public bool ViewStats;
    public bool Invite;
    public bool GiveItem;
    public bool GiveJob;
    public bool ChangeSchedule;
    public bool Assign;

    private ResidentButtonConstants constant;

    private void Awake()
    {
        constant = transform.parent.GetComponent<ResidentButtonConstants>();
    }

    public void ButtonClick()
    {
        if (ViewStats)
        {
            if(!constant.StatsObject.activeSelf) constant.StatsObject.SetActive(true);
            else constant.StatsObject.SetActive(false);
        }
        else if (Invite)
        {
            constant.text.text = "Hmm I'll join you for lets say a " + ReturnName(constant.resident.GetComponent<ResidentStats>().FavoriteItem);
            constant.GiveItemButton.SetActive(true);
            constant.JoinTownButton.SetActive(false);
        }
        else if (GiveItem)
        {
            if (constant.inventoryManager.GetAmountByName(constant.resident.GetComponent<ResidentStats>().FavoriteItem) >= 1)
            {
                //inventory.RemoveItem(new Item { itemType = ResidentTalkingTo.GetComponent<ResidentStats>().FavoriteItem, amount = amount, index = inventory.GetLastLocationOfItem(ResidentTalkingTo.GetComponent<ResidentStats>().FavoriteItem) });
                constant.inventoryManager.RemoveItem(constant.resident.GetComponent<ResidentStats>().FavoriteItem);
                constant.resident.GetComponent<ResidentStats>().joinedTown = true;
                constant.resident.GetComponent<ResidentStats>().namebar.color = constant.resident.GetComponent<ResidentStats>().color;
                constant.GiveItemButton.SetActive(false);
                //transform.GetChild(1).gameObject.SetActive(false);
                //transform.GetChild(2).gameObject.SetActive(true);
                constant.text.text = "Thank you kindly";

                //disables item held by player
                constant.inventoryManager.ChangeSlotOnDrop(constant.resident.GetComponent<ResidentStats>().FavoriteItem);

                if (!constant.buildMenuUpdater.AccessToLevel2Buildings)
                {
                    constant.buildMenuUpdater.AccessToLevel1Buildings = false;
                    constant.buildMenuUpdater.AccessToLevel2Buildings = true;
                    constant.Announcement.gameObject.SetActive(true);
                    constant.Announcement.text = "New Buildings Unlocked";
                }
            }
            else
            {
                constant.text.text = "You can't even do that right? Get out of here";
            }
        }
        else if (GiveJob)
        {
            constant.GiveJobButton.SetActive(false);
            constant.resident.GetComponent<NavMeshAgent>().stoppingDistance = 5;
            constant.resident.GetComponent<BoxCollider>().enabled = false;
            constant.text.text = "Okay following you!";

            constant.player.GetComponent<PlayerInteractions>().residentFollowing = true;
            constant.player.GetComponent<PlayerInteractions>().resident = constant.resident;
            constant.player.GetComponent<PlayerInteractions>().residentText = constant.text;

            constant.resident.GetComponent<ResidentScheudle>().followPlayer = true;
            constant.resident.GetComponent<ResidentScheudle>().isBeingTalkedTo = false;
        }
        else if (ChangeSchedule)
        {
            if (!constant.ScheduleObject.activeSelf) constant.ScheduleObject.SetActive(true);
            else constant.ScheduleObject.SetActive(false);
        }
        else if (Assign)
        {
            Destroy(constant.resident.GetComponent<Chef>());
            constant.AssignButton.SetActive(false);
            constant.resident.GetComponent<NavMeshAgent>().stoppingDistance = 5;
            constant.resident.GetComponent<BoxCollider>().enabled = false;
            constant.text.text = "Please tell me what farm to go to!";

            constant.player.GetComponent<PlayerInteractions>().resident = constant.resident;
            constant.player.GetComponent<PlayerInteractions>().residentText = constant.text;
            constant.player.GetComponent<PlayerInteractions>().assign = true;
            constant.player.GetComponent<PlayerInteractions>().resident = constant.resident;

            constant.resident.GetComponent<ResidentScheudle>().followPlayer = true;
            constant.resident.GetComponent<ResidentScheudle>().isBeingTalkedTo = false;
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
