using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JobCamera : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject TextBoxChoices;
    public GameObject[] JobLocations;
    public Vector3 offset;
    public GameObject jobBoard;
    public GameObject ResidentTalkingTo;
    public GameObject WorkersFull;

    public BuildMenuUpdater buildMenuUpdater;
    public TextMeshProUGUI Announcement;

    private int current = 0;
    //private int buttonClicked;

    private void Awake()
    {
        current = 0;
    }

    public void Next()
    {
        if (JobLocations.Length == 0) return;

        current++;
        if (current == JobLocations.Length) current = 0;

        transform.position = JobLocations[current].transform.position + offset;
        //buttonClicked = 1;
    }

    public void Back()
    {
        if (JobLocations.Length == 0) return;

        current--;
        if (current == -1) current = JobLocations.Length - 1;

        transform.position = JobLocations[current].transform.position + offset;
        //buttonClicked = 2;
    }

    public void BackToTalking()
    {
        jobBoard.SetActive(true);
        mainCamera.gameObject.SetActive(true);
        transform.parent.gameObject.SetActive(false);
    }

    public void Select()
    {
        Job job = JobLocations[current].GetComponent<Job>();

        //Check if mine was selected
        if (job != null && (job.Workers < job.MaxWorkers)) { SelectButton(); job.Workers++; job.WorkersActive[job.Workers - 1] = ResidentTalkingTo; }
        else { WorkersFull.SetActive(true); }
    }

    private void SelectButton()
    {
        mainCamera.gameObject.SetActive(true);
        transform.parent.gameObject.SetActive(false);

        ResidentTalkingTo.GetComponent<ResidentScheudle>().job = JobLocations[current];
        //ResidentTalkingTo.GetComponent<StatBar>().UpdateJob();
        TextBoxChoices.SetActive(true);

        if (!buildMenuUpdater.AccessToLevel3Buildings)
        {
            buildMenuUpdater.AccessToLevel2Buildings = false;
            buildMenuUpdater.AccessToLevel3Buildings = true;
            Announcement.gameObject.SetActive(true);
            Announcement.text = "New Buildings Unlocked";
        }
    }
}
