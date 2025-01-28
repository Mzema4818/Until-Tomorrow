using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hats : MonoBehaviour
{
    private ResidentScheudle residentScheudle;
    public GameObject hats;

    void Start()
    {
        residentScheudle = transform.GetComponent<ResidentScheudle>();
        if (residentScheudle.job != null) GetHat(residentScheudle.job.name);
    }

    private void OnEnable()
    {

    }

    public void GetHat(string job)
    {
        string jobName = job.Split('_')[0];
        switch (jobName) //child number the hats are
        {
            case "Farm":
                hats.transform.GetChild(2).gameObject.SetActive(true);
                break;
            case "Mine":
                hats.transform.GetChild(4).gameObject.SetActive(true);
                break;
            case "Lumbermill":
                hats.transform.GetChild(3).gameObject.SetActive(true);
                break;
            case "Messhall":
                hats.transform.GetChild(1).gameObject.SetActive(true);
                break;
            case "Tower":
                hats.transform.GetChild(0).gameObject.SetActive(true);
                break;
        }
    }

    public void RemoveHats()
    {
        foreach (Transform child in hats.transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
