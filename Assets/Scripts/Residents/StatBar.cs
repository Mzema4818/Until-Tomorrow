using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatBar : MonoBehaviour
{
    public ResidentStats residentStats;
    public GameObject statBar;
    public GameObject jobName;
    public GameObject homeName;

    private void Start()
    {
        UpdateStatBar();
    }

    private void UpdateStatBar()
    {
        int Index = 3; //this is where the strength stat starts (first stat besides health and food and stuff)

        for(int i = 1; i < statBar.transform.childCount - 5; i++) //strength is the first actual stat, the last children are stuff we dont change
        {
            statBar.transform.GetChild(i).GetChild(0).GetComponent<Image>().fillAmount = (residentStats.Stats[Index] * 5) / 100.0f; //this is because stats have a max of 20.  So we make it out of 100 first then we divide it by 100 to get a decimal
            Index++;
        }

        UpdateJob();
        UpdateHome();
    }

    public void UpdateJob()
    {
        TextMeshProUGUI text = jobName.GetComponent<TextMeshProUGUI>();
        if (transform.GetComponent<ResidentScheudle>().job != null)
        {
            string name = transform.GetComponent<ResidentScheudle>().job.name;
            text.text = "Job: " + name.Substring(0, name.IndexOf("_"));
        }
        else { text.text = "Job: None"; }
    }

    public void UpdateHome()
    {
        TextMeshProUGUI text = homeName.GetComponent<TextMeshProUGUI>();
        if (transform.GetComponent<ResidentScheudle>().home != null) text.text = "Home: Yes";
        else text.text = "Home: No";
    }
}