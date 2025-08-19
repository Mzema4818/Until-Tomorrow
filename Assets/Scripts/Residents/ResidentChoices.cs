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
        SetupFarmInfo();
        SetupDialogue();
    }

    public void SetupFarmInfo()
    {
        if (residentScheudle.job != null && residentScheudle.job.TryGetComponent(out Messhall messhall))
        {
            farmAttached.gameObject.SetActive(true);
            farmAttached.text = messhall.farm != null ? "Farm Attached: Yes" : "Farm Attached: No";
        }
        else
        {
            farmAttached.gameObject.SetActive(false);
        }
    }

    public void SetupDialogue()
    {
        if (resident.transform.parent.name != "Resident")
        {
            text.text = "I just got here, leave me alone";
            return;
        }

        if (!resident.joinedTown)
        {
            SetupPreTownDialogue();
        }
        else
        {
            SetupPostTownDialogue();
        }
    }

    private void SetupPreTownDialogue()
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

    private void SetupPostTownDialogue()
    {
        if (residentScheudle.job != null && residentScheudle.job.GetComponent<Messhall>() != null)
        {
            AssignButton.SetActive(true);
        }

        text.text = "What can I do for you boss?";
        CheckIfHasJob();
        CheckIfHasHome();
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
