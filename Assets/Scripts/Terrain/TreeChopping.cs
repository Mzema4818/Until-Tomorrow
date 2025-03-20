using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeChopping : MonoBehaviour, IHitable
{
    [SerializeField] private GameObject treeChopped;

    public void Execute()
    {
        Instantiate(treeChopped, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
