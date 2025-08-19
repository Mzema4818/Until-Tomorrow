using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleeping : MonoBehaviour
{
    public GameObject building;
    public LightingManager time;
    public ResidentScheudle residentScheudle;

    private int TimeToWakeUp = -1;

    private void Start()
    {

        //weird glitch where if you start moving the building then stop right away the resident doesnt disapear
        //print("sleeping");
        residentScheudle = transform.GetComponent<ResidentScheudle>();

        FindNext(time.WhatTimeIsIt(), new int[] { 1, 2 });
        //print(TimeToWakeUp);

        transform.parent.gameObject.AddComponent<TurnOnGameObjects>().resident = gameObject;
        transform.parent.gameObject.GetComponent<TurnOnGameObjects>().time = time;
        transform.parent.gameObject.GetComponent<TurnOnGameObjects>().TimeToWakeUp = TimeToWakeUp;
        transform.parent.gameObject.GetComponent<TurnOnGameObjects>().building = building;

        gameObject.SetActive(false);
    }

    private void FindNext(int time, int[] TimesToNotFind)
    {
        for (int i = time; i < residentScheudle.Schedule.Length; i++)
        {
            if (residentScheudle.Schedule[i] == TimesToNotFind[0] || residentScheudle.Schedule[i] == TimesToNotFind[1])
            {
                TimeToWakeUp = i;
                break;
            }
        }

        if (TimeToWakeUp == -1)
        {
            for (int i = 0; i < time; i++)
            {
                if (residentScheudle.Schedule[i] == TimesToNotFind[0] || residentScheudle.Schedule[i] == TimesToNotFind[1])
                {
                    TimeToWakeUp = i;
                    break;
                }
            }
        }
    }
}
