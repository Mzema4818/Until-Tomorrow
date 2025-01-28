using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameBar : MonoBehaviour
{
    public GameObject player;
    private GameObject canvas;

    private void Awake()
    {
        canvas = transform.Find("Name").gameObject;
    }
}
