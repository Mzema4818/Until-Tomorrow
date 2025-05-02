using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TurnOffFollow : MonoBehaviour
{
    public GameObject player;

    private void OnDisable()
    {
        try
        {
            transform.parent.GetComponent<ResidentScheudle>().followPlayer = false;
            transform.parent.GetComponent<ResidentScheudle>().followPlayerHome = false;
            transform.parent.GetComponent<ResidentScheudle>().isBeingTalkedTo = false;
            transform.parent.GetComponent<NavMeshAgent>().stoppingDistance = 0;
            transform.parent.GetComponent<BoxCollider>().enabled = true;

            //player.GetComponent<PlayerInteractions>().assign = false;
            player.GetComponent<PlayerInteractions>().residentFollowing = false;
            player.GetComponent<PlayerInteractions>().assign = false;
            player.GetComponent<PlayerInteractions>().resident = null;
            player.GetComponent<PlayerInteractions>().residentText = null;
        }
        catch { };
    }
}
