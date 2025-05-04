using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResidentStats : MonoBehaviour
{
    public Resident resident;
    public int[] Stats;
    public bool joinedTown;
    public Color color;
    public Item.ItemType FavoriteItem;
    public GameObject textBox;
    public GameObject schedule;

    private string ResidentName;
    public TextMeshProUGUI namebar;
    public GameObject StatObject;

    void Awake()
    {
        ResidentName = transform.name;
        resident = new Resident {residentType = GetTypeByName()};
        Stats = resident.GetStats();
        FavoriteItem = GetFavoriteItem(Random.Range(1, 7));
        //transform.GetComponent<StatBar>().stats = Stats;
        //transform.GetComponent<StatBar>().UpdateStats();
        textBox = transform.Find("Actions").gameObject;
        schedule = textBox.transform.Find("Schedule").gameObject;
    }

    private void Start()
    {
        if (transform.name == "Prisoner" || transform.name == "Merchant")
        {
            transform.name = resident.ReturnNewName();
        }

        namebar.text = transform.name;

        if (joinedTown)
        {
            namebar.color = color;
        }
    }

    private Resident.ResidentType GetTypeByName()
    {
        switch (ResidentName)
        {
            default:
            case "Prisoner": return Resident.ResidentType.prisoner;
            case "Merchant": return Resident.ResidentType.merchant;
        }
    }

    private Item.ItemType GetFavoriteItem(int num)
    {
        switch (num)
        {
            default:
            case 1: return Item.ItemType.berry;
            case 2: return Item.ItemType.bush;
            case 3: return Item.ItemType.flower;
            case 4: return Item.ItemType.mushroom;
            case 5: return Item.ItemType.rock;
            case 6: return Item.ItemType.sapling;
            case 7: return Item.ItemType.wood;
        }
    }
}
