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

    public bool activity;
    private Transform plankTransform;
    private int num;
    public int prisonersExported;
    // Start is called before the first frame update
    void Start()
    {
        num = UnityEngine.Random.Range(2, 4);
        prisoners = new GameObject[num];
        shipMovement = transform.GetComponent<ShipMovement>();
        plankTransform = transform.Find("Spawn");
        activity = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (shipMovement.ReadyForActivity && activity)
        {
            NavMeshHit myNavHit;
            if (NavMesh.SamplePosition(transform.position, out myNavHit, 100, -1))
            {
                //transform.position = myNavHit.position;
                //print(myNavHit);
            }

            for (int i = 0; i < num; i++)
            {
                GameObject prisoner = Instantiate(prisonerPrefab, new Vector3(myNavHit.position.x, myNavHit.position.y, myNavHit.position.z), Quaternion.identity);
                prisoner.name = "Prisoner"; //NEED THIS HERE TO GET WHICH TYPE THE RESIDENT IS, if "prisoner" is a prisoner, if "Merchant" is a merchant, so on.... it gives stats depending on the name
                prisoner.transform.parent = tempObjects;
                prisoner.SetActive(true);
                prisoners[i] = prisoner;
                activity = false;
            }

            activity = false;
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
                //prisoners[i].AddComponent<ResidentConditions>();
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
