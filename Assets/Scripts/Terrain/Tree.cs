using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
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
