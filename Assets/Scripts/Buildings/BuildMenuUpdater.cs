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
        book.pageToBuilding.Sort(); //Might not be the best, but without it, the pages arent in the same order as index
    }

    private void OnDisable()
    {
        DisableBuildings();
    }

    private void Level0Access()
    {
        TurnOnBuildings(0); //campfire
    }

    private void Level1Access()
    {
        TurnOnBuildings(1); //tent
        TurnOnBuildings(7); //chest
    }

    private void Level2Access()
    {
        Level1Access();
        TurnOnBuildings(2); //mine
        TurnOnBuildings(3); //lumbermill
        TurnOnBuildings(4); //farm
        TurnOnBuildings(8); //messhall
        TurnOnBuildings(9); //Tavern
    }

    private void Level3Access()
    {
        Level2Access();
        TurnOnBuildings(5); //wall
        TurnOnBuildings(6); //door
        TurnOnBuildings(10); //Tower
        TurnOnBuildings(11); //Knighthut
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
