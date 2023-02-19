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
    }

    private void OnDisable()
    {
        DisableBuildings();
    }

    private void Level0Access()
    {
        transform.GetChild(num - 8).gameObject.SetActive(true); //campfire
    }

    private void Level1Access()
    {
        transform.GetChild(num - 7).gameObject.SetActive(true); //tent
    }

    private void Level2Access()
    {
        Level1Access();
        transform.GetChild(num - 5).gameObject.SetActive(true); //lumbermill
        transform.GetChild(num - 6).gameObject.SetActive(true); //mine
    }

    private void DisableBuildings()
    {
        for(int i = 0; i < transform.childCount - 4; i++) //-4 because first 4 are descriptions
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
