using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ResidentConditions : MonoBehaviour
{
    private ResidentStats residentStats;

    public GameObject tents;

    [Header("Conditions")]
    public bool hasHome;

    // Start is called before the first frame update
    void Start()
    {
        tents = GameObject.Find("Tents");
        residentStats = transform.GetComponent<ResidentStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasHome && residentStats.joinedTown) CheckForEmptyHomes();
    }

    private void CheckForEmptyHomes()
    { 
        List<int> emptyHomes = new List<int>();

        if (tents.transform.childCount == 0) return;

        for(int i = 0; i < tents.transform.childCount; i++)
        {
            if(tents.transform.GetChild(i).GetComponent<Tent>().Residents < tents.transform.GetChild(i).GetComponent<Tent>().MaxResidents)
            {
                emptyHomes.Add(i);
                //print(tents.transform.GetChild(i).name);
            }
        }

        if(emptyHomes.Count != 0)
        {
            int num = emptyHomes[Random.Range(0, emptyHomes.Count)];

            hasHome = true;
            transform.GetComponent<ResidentScheudle>().home = tents.transform.GetChild(num).gameObject;
            Tent tent = tents.transform.GetChild(num).GetComponent<Tent>();

            tent.Residents++;
            tent.ResidentsActive[tent.Residents - 1] = gameObject;
        }
    }
}
