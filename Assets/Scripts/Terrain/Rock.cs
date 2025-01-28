using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public int numOfHits;

    void Update()
    {
        if (numOfHits == 3)
        {
            Destroy(gameObject);
        }
    }
}
