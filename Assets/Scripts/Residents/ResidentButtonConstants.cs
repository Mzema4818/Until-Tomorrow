using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResidentButtonConstants : MonoBehaviour
{
    [Header("GameObjects")]
    public TextMeshProUGUI text;
    public GameObject StatsObject;
    public GameObject ScheduleObject;
    public GameObject resident;
    public BuildMenuUpdater buildMenuUpdater;
    public TextMeshProUGUI Announcement;
    public InventoryManager inventoryManager;
    public GameObject player;

    public GameObject jobBoard;
    public GameObject Jobs;
    public GameObject buildings;
    public GameObject actions;

    [Header("Buttons")]
    public GameObject GiveItemButton;
    public GameObject JoinTownButton;
    public GameObject GiveJobButton;
    public GameObject GiveHomeButton;
    public GameObject AssignButton;
}
