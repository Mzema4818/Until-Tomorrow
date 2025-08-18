using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResidentChoices : MonoBehaviour
{
    public GameObject TextBox;
    public BuildMenuUpdater buildMenuUpdater;

    private TextMeshProUGUI text;
    private ResidentStats resident;
    private ResidentScheudle residentScheudle;

    public GameObject player;
    public GameObject stats;
    private GameObject canvas;

    [Header("Type Of Button")]
    public GameObject statsButton;
    public GameObject joinTownButton;
    public GameObject giveItemButton;
    public GameObject JoinJobButton;
    public GameObject JoinHomeButton;
    public GameObject AssignButton;

    [Header("Other")]
    public TextMeshProUGUI farmAttached;

    private void Awake()
    {
        TextBox = gameObject;
        text = TextBox.transform.Find("Canvas").Find("Background").Find("SpokenWords").GetComponent<TextMeshProUGUI>();
        resident = transform.parent.GetComponent<ResidentStats>();
        residentScheudle = transform.parent.GetComponent<ResidentScheudle>();
        canvas = gameObject;
    }

    private void OnEnable()
    {
        //turn on Farm Attached: if job is a messhall
        if(residentScheudle.job.GetComponent<Messhall>() != null)
        {
            farmAttached.gameObject.SetActive(true);
            if (residentScheudle.job.GetComponent<Messhall>().farm != null) farmAttached.text = "Farm Attached: Yes";
            else farmAttached.text = "Farm Attached: No";
        } else farmAttached.gameObject.SetActive(false);

        //regular text
        if (resident.transform.parent.name != "Resident")
        {
            text.text = "I just got here, leave me alone";
        }
        else
        {
            if (!resident.joinedTown)
            {
                if (buildMenuUpdater.AccessToLevel0Buildings)
                {
                    text.text = "You don't have anything for me right now, go away";
                }
                else
                {
                    text.text = "Hmm, what do you want?";
                    joinTownButton.SetActive(true);
                    giveItemButton.SetActive(false);
                }
            }
            else
            {
                if (residentScheudle.job != null)
                {
                    if (residentScheudle.job.GetComponent<Messhall>() != null)
                    {
                        AssignButton.SetActive(true);
                    }
                }

                text.text = "What can I do for you boss?";
                CheckIfHasJob();
                CheckIfHasHome();
            }
        }
    }

    private void CheckIfHasJob()
    {
        if (residentScheudle.job == null) JoinJobButton.SetActive(true);
        else text.text = "The " + GetJobName(residentScheudle.job.ToString()) + " has been running nicely lately";
    }

    private void CheckIfHasHome()
    {
        if (residentScheudle.home == null) JoinHomeButton.SetActive(true);
    }


    public void ViewStats()
    {
        print("hello");
    }

    private string GetJobName(string jobName)
    {
        return jobName.Substring(0, jobName.IndexOf("_"));
    }

}
