using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMenuUpdater : MonoBehaviour
{
    public GetData getdata;
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
        if (AccessToLevel0Buildings) Level0Access();
        if (AccessToLevel1Buildings) Level1Access();
        if (AccessToLevel2Buildings) Level2Access();
        if (AccessToLevel3Buildings) Level3Access();
    }

    private void OnDisable()
    {
        DisableBuildings();
    }

    private void Level0Access()
    {
        //transform.GetChild(num - 9).gameObject.SetActive(true); //wall TEST
        transform.GetChild(0).gameObject.SetActive(true); //campfire
    }

    private void Level1Access()
    {
        transform.GetChild(1).gameObject.SetActive(true); //tent
        transform.GetChild(7).gameObject.SetActive(true); //chest
    }

    private void Level2Access()
    {
        Level1Access();
        transform.GetChild(2).gameObject.SetActive(true); //mine
        transform.GetChild(3).gameObject.SetActive(true); //lumbermill
        transform.GetChild(4).gameObject.SetActive(true); //farm
        transform.GetChild(8).gameObject.SetActive(true); //messhall
        transform.GetChild(9).gameObject.SetActive(true); //Tavern
    }

    private void Level3Access()
    {
        Level2Access();
        transform.GetChild(5).gameObject.SetActive(true); //wall
        transform.GetChild(6).gameObject.SetActive(true); //door
        transform.GetChild(10).gameObject.SetActive(true); //Tower
        transform.GetChild(11).gameObject.SetActive(true); //Tower
    }

    private void DisableBuildings()
    {
        for(int i = 0; i < transform.childCount - 4; i++) //-4 because first 4 are descriptions
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void WipeAccess()
    {
        AccessToLevel0Buildings = true;
        AccessToLevel1Buildings = false;
        AccessToLevel2Buildings = false;
        AccessToLevel3Buildings = false;
    }
}
