using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsABuilding : MonoBehaviour
{
    public int distance;
    public bool beingMoved;
    public GameObject actions;
    public GameObject canvas;

    private void Awake()
    {
        int distance = transform.GetComponent<ProximitySettings>().maxDistance;

        transform.GetComponent<PickUpPopUpAdvanced>().distance = distance + 5;
        actions.GetComponent<Orbiting>().distanceFromBuilding = distance;
    }

    public void SetPosition()
    {
        Orbiting orbiting = actions.GetComponent<Orbiting>();

        Vector3 directionToPlayer = (orbiting.playerCamera.transform.position - transform.position).normalized;
        actions.transform.position = transform.position + orbiting.offset + directionToPlayer * distance;
    }
}
