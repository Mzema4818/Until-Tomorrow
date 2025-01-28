using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tavern : MonoBehaviour
{
    public int sitting;
    public int maxSeats;
    public GameObject[] seats;

    // Start is called before the first frame update
    private void Awake()
    {
        if (transform.name.Contains("Level1")) maxSeats = 4;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
