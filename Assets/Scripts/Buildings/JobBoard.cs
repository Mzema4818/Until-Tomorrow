using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobBoard : MonoBehaviour
{
    public GameObject JobCameraHolder;
    public Camera jobCamera;
    public Camera MainCamera;

    public GameObject buildings;
    public GameObject[] JobLocations;
    public Vector3 offset;

    public void Mine()
    {
        FillJobLocations(2); //number 2 is where Mines spawn
    }
    public void Lumbermill()
    {
        FillJobLocations(3); //number 3 is where Lumbermills spawn
    }

    public void Farm()
    {
        FillJobLocations(4); //number 4 is where farms spawn
    }

    private void FillJobLocations(int BuildingLocation)
    {
        JobLocations = new GameObject[buildings.transform.GetChild(BuildingLocation).childCount];

        for(int i = 0; i < JobLocations.Length; i++)
        {
            JobLocations[i] = buildings.transform.GetChild(BuildingLocation).GetChild(i).gameObject;
        }

        jobCamera.GetComponent<JobCamera>().JobLocations = JobLocations;
        jobCamera.GetComponent<JobCamera>().offset = offset;
        gameObject.SetActive(false);
        MainCamera.gameObject.SetActive(false);
        JobCameraHolder.gameObject.SetActive(true);
        jobCamera.gameObject.transform.position = JobLocations[0].transform.position + offset;
    }
}
