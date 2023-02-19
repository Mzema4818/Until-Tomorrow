using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ExportPrisoners : MonoBehaviour
{
    private ShipMovement shipMovement;
    public GameObject prisonerPrefab;
    public GameObject[] prisoners;
    public Transform residentParent;
    public Transform tempObjects;

    private bool activity;
    private Transform plankTransform;
    private int num;
    public int prisonersExported;
    // Start is called before the first frame update
    void Start()
    {
        num = UnityEngine.Random.Range(2, 4);
        prisoners = new GameObject[num];
        shipMovement = transform.GetComponent<ShipMovement>();
        plankTransform = transform.Find("Plank");
        activity = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (shipMovement.ReadyForActivity && activity)
        {
            for(int i = 0; i < num; i++)
            {
                GameObject prisoner = Instantiate(prisonerPrefab, new Vector3(plankTransform.position.x + i, plankTransform.position.y, plankTransform.position.z), Quaternion.identity);
                prisoner.name = "Prisoner";
                prisoner.transform.parent = tempObjects;
                prisoner.GetComponent<ResidentWander>().getOffBoat = true;
                prisoner.GetComponent<ResidentWander>().speed = 25;
                prisoner.GetComponent<ResidentWander>().exportPrisoners = transform.GetComponent<ExportPrisoners>();
                prisoner.transform.LookAt(new Vector3(0, 0, 0));
                prisoner.transform.eulerAngles = new Vector3(
                        23.79f,
                        prisoner.transform.eulerAngles.y,
                        0);
                prisoner.SetActive(true);
                prisoners[i] = prisoner;
                activity = false;
            }
        }

        if(prisonersExported == num)
        {
            shipMovement.leave = true;
        }
    }

    private void OnDestroy()
    {
        try
        {
            for (int i = 0; i < prisoners.Length; i++)
            {
                prisoners[i].transform.parent = residentParent;
            }
        }
        catch (Exception) { };
    }

    private string FixName(string name)
    {
        int strSet = name.IndexOf("(");

        return name.Substring(0, strSet);
    }
}
