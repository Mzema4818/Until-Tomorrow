using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegrowBerries : MonoBehaviour
{
    private GameObject berries;
    public float regrowTime = 120.0f;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).transform.name == "Berries")
            {
                berries = transform.GetChild(i).gameObject;
            }
        }

        InvokeRepeating("Regrow", regrowTime, regrowTime);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Regrow()
    {
        if (!berries.activeSelf)
        {
            berries.SetActive(true);
            transform.name = "BerryBush";
        }
    }
}
