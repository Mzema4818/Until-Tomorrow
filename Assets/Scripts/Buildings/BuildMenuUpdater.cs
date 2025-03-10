using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMenuUpdater : MonoBehaviour
{
    public GetData getdata;
    public Book book;
    public BuildMenuFirstPage buildMenuFirstPage;
    private int num;

    public bool AccessToLevel0Buildings;
    public bool AccessToLevel1Buildings;
    public bool AccessToLevel2Buildings;
    public bool AccessToLevel3Buildings;


    private void Awake()
    {
        DisableBuildings();

        num = transform.childCount;
    }

    private void OnEnable()
    {
        book.RemovePages();
        if (AccessToLevel0Buildings) Level0Access();
        if (AccessToLevel1Buildings) Level1Access();
        if (AccessToLevel2Buildings) Level2Access();
        if (AccessToLevel3Buildings) Level3Access();
        buildMenuFirstPage.RenameGridChildren();
    }

    private void OnDisable()
    {
        DisableBuildings();
    }

    private void Level0Access()
    {
        TurnOnBuildings(0);
        //transform.GetChild(0).gameObject.SetActive(true); //campfire
    }

    private void Level1Access()
    {
        TurnOnBuildings(1);
        TurnOnBuildings(7);
        //transform.GetChild(1).gameObject.SetActive(true); //tent
        //transform.GetChild(7).gameObject.SetActive(true); //chest
    }

    private void Level2Access()
    {
        Level1Access();
        TurnOnBuildings(2);
        TurnOnBuildings(3);
        TurnOnBuildings(4);
        TurnOnBuildings(8);
        TurnOnBuildings(9);
        //transform.GetChild(2).gameObject.SetActive(true); //mine
        //transform.GetChild(3).gameObject.SetActive(true); //lumbermill
        //transform.GetChild(4).gameObject.SetActive(true); //farm
        //transform.GetChild(8).gameObject.SetActive(true); //messhall
        //transform.GetChild(9).gameObject.SetActive(true); //Tavern
    }

    private void Level3Access()
    {
        Level2Access();
        TurnOnBuildings(5);
        TurnOnBuildings(6);
        TurnOnBuildings(10);
        TurnOnBuildings(11);
        //transform.GetChild(5).gameObject.SetActive(true); //wall
        //transform.GetChild(6).gameObject.SetActive(true); //door
        //transform.GetChild(10).gameObject.SetActive(true); //Tower
        //transform.GetChild(11).gameObject.SetActive(true); //Knighthut
    }

    private void DisableBuildings()
    {
        for(int i = 0; i < transform.childCount; i++) 
        {
            transform.GetChild(1).GetChild(i).gameObject.SetActive(false);
        }
    }

    private void TurnOnBuildings(int num)
    {
        book.AddPage(num);
        transform.GetChild(1).GetChild(num).gameObject.SetActive(true);
    }

    public void WipeAccess()
    {
        AccessToLevel0Buildings = true;
        AccessToLevel1Buildings = false;
        AccessToLevel2Buildings = false;
        AccessToLevel3Buildings = false;
    }
}
