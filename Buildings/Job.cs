using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Job : MonoBehaviour
{
    public int MaxNumberOfWorkers;
    public int MaxWorkers = 0;
    public int Workers;
    public GameObject[] WorkersActive;

    public LightingManager time;

    public bool beingMoved;

    private void Awake()
    {
        if (name.Contains("Level0")) MaxWorkers = MaxNumberOfWorkers;
        WorkersActive = new GameObject[MaxWorkers];
    }

    private void OnDestroy()
    {
        for (int i = 0; i < WorkersActive.Length; i++)
        {
            if (WorkersActive[i] != null)
            {
                WorkersActive[i].GetComponent<ResidentScheudle>().job = null;
                WorkersActive[i].GetComponent<StatBar>().UpdateJob();
                //try { WorkersActive[i].GetComponent<NavMeshAgent>().ResetPath(); } catch { };
                //WorkersActive[i].GetComponent<StatBar>().UpdateJob();
            }
        }
    }
}
