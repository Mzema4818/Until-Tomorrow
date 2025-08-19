using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JobOptions : MonoBehaviour
{
    public GameObject workersDisplay;
    public GameObject resident;
    //public int num; 

    public void Fire(Job job, int num)
    {
        resident = job.WorkersActive[num];

        //removing TurnOnGameObjects
        var scripts = resident.transform.parent.GetComponents<TurnOnGameObjects>();
        foreach (var s in scripts)
        {
            if (s.resident == resident)
            {
                Destroy(s);
                break;
            }
        }

        //Job job = resident.GetComponent<ResidentScheudle>().job.GetComponent<Job>();
        //Messhall messhall = resident.GetComponent<ResidentScheudle>().job.GetComponent<Messhall>();

        job.RemoveJob(num);
        job.Workers--;
        job.WorkersActive[num] = null;
        job.WorkersActive = ReorganizeArray();

        //resident.GetComponent<ResidentScheudle>().RemoveJobs();
        //RemoveJob();
        //UpdateWorkersHere(job);
        resident.GetComponent<ResidentScheudle>().AtLocation = false;
        resident.GetComponent<ResidentScheudle>().job = null;
        resident.GetComponent<StatBar>().UpdateJob();
        //if (messhall != null) messhall.farm = null;
        resident.SetActive(true);
        //resident.GetComponent<StatBar>().UpdateJob();
        resident.GetComponent<Hats>().RemoveHats();
        ReorganizeText();
    }

    private GameObject[] ReorganizeArray()
    {
        //when deleting a resident from an array theres an empty space there which messes things up, this just gets rid of that space
        Job job = resident.GetComponent<ResidentScheudle>().job.GetComponent<Job>();
        GameObject[] returnArray = new GameObject[job.MaxWorkers];

        int index = 0;
        for(int i = 0; i < returnArray.Length; i++)
        {
            if(job.WorkersActive[i] != null)
            {
                returnArray[index] = job.WorkersActive[i];
                index++;
            }
        }

        return returnArray;
    }

    private void ReorganizeText()
    {
        //workersDisplay.transform.GetChild(num).GetComponent<TextMeshProUGUI>().text = ""; //gets the text and makes it nothing
        //Destroy(workersDisplay.transform.GetChild(num).GetChild(0).gameObject); //gets the "fire" button of the bar and destroys it
        
    }

    private void UpdateWorkersHere(Job job)
    {
        Mine mine = job.GetComponent<Mine>();
        Farm farm = job.GetComponent<Farm>();
        Lumbermill lumbermill = job.GetComponent<Lumbermill>();
        Messhall messhall = job.GetComponent<Messhall>();

        print("hi job options");
        //resident.GetComponent<ResidentScheudle>().UpdateWorkers(mine, farm, lumbermill, messhall, 1);
    }

    private void RemoveJob()
    {

    }
}
