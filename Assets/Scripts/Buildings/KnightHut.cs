using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KnightHut : MonoBehaviour
{
    public FieldOfView fieldOfView;
    public Job job;

    // Start is called before the first frame update
    void Start()
    {
        fieldOfView = GetComponent<FieldOfView>();
        job = GetComponent<Job>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fieldOfView.canSeePlayer)
        {
            EnemiesFound();
        }
        else
        {
            NoEnemies();
        }
    }

    private void EnemiesFound()
    {
        foreach(GameObject resident in job.WorkersActive)
        {
            if (resident == null || resident.activeSelf) continue;
            resident.GetComponent<Knight>().attackEnemies = true;
            resident.SetActive(true);
        }
    }

    private void NoEnemies()
    {
        foreach (GameObject resident in job.WorkersActive)
        {
            if (resident == null || !resident.activeSelf || !resident.GetComponent<ResidentScheudle>().AtLocation) continue;
            resident.GetComponent<NavMeshAgent>().ResetPath();
            resident.GetComponent<Knight>().enemyToAttack = fieldOfView.objectSeen;
            resident.GetComponent<Knight>().attackEnemies = false;
        }
    }
}
