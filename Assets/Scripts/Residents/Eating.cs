using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Eating : MonoBehaviour
{
    public GameObject location;
    public Vector3 locationEntered;

    public Animator animator;
    public NavMeshAgent agent;
    public ResidentTools residentTools;
    public Tavern tavern;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        residentTools = GetComponent<ResidentTools>();

        tavern = location.GetComponent<Tavern>();
        locationEntered = transform.position;
        transform.position = tavern.seats[tavern.sitting].transform.position;
        transform.LookAt(location.transform);
        tavern.sitting++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        agent.enabled = true;
        transform.position = locationEntered;
        tavern.sitting--;
    }
}
