using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class ResidentHealth : MonoBehaviour
{
    [SerializeField]
    public int maxHealth = 100;
    public int currentHealth;

    public event Action<float> onHealthPctChanged = delegate { };

    public ResidentStats residentStats;
    private ResidentScheudle residentScheudle;
    private Coroutine attackCoroutine;

    public bool damage;
    public Transform whoDamaged;

    public Transform tempObject;
    private Rigidbody[] rigidbodies;
    private Collider[] colliders;

    private void Awake()
    {
        residentScheudle = transform.GetComponent<ResidentScheudle>();
        currentHealth = maxHealth;

        rigidbodies = GetComponentsInChildren<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();
    }

    private void Start()
    {
        ModifyHealth(0, null); //this just sets the value when first opened.

        //turn off all colliders and on all rigidbodies for ragdoll, then turn on just the one we need to interact with
        setRigidbodyState(true);
        setColliderState(false);

        gameObject.GetComponent<BoxCollider>().enabled = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }

    private void Update()
    {
        if (damage)
        {
            ModifyHealth(-20, null);
            damage = false;
        }

        if (currentHealth <= 0)
        {
            setRigidbodyState(false);
            setColliderState(true);

            StopAllMovementAndInteraction();
            residentScheudle.dead = true;
            Destroy(gameObject, 5f);
        }
    }

    public void ModifyHealth(int amount, Transform whoDamaged)
    {
        if (amount < 0)
        {
            amount = Mathf.Max(amount, -currentHealth); // Ensure we don't go below 0
            this.whoDamaged = whoDamaged;
            if(whoDamaged != null) TriggerBeingAttacked();
        }
        else
            amount = Mathf.Min(amount, maxHealth - currentHealth); // Ensure we don't exceed maxHealth

        currentHealth += amount;

        //Ensure maxHealth stays within valid bounds
        maxHealth = Mathf.Clamp(maxHealth, 0, 100);

        // Calculate the current health percentage
        float currentHungerPct = (float)currentHealth / (float)maxHealth;
        onHealthPctChanged(currentHungerPct);

        residentStats.Stats[0] += amount;
    }

    private void TriggerBeingAttacked()
    {
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
        }

        //turn off menus if they are on
        ResidentStats resident = gameObject.GetComponent<ResidentStats>();
        if (resident.textBox.activeSelf)
        {
            resident.textBox.SetActive(false);
            resident.schedule.SetActive(false);
            resident.StatObject.SetActive(false);
        }

        residentScheudle.recentlyAttacked = true;
        residentScheudle.runAwayFrom = whoDamaged;
        residentScheudle.shouldRun = true;
        residentScheudle.agent.speed = residentScheudle.runSpeed;
        residentScheudle.RemoveJobs();
        attackCoroutine = StartCoroutine(ResetBeingAttackedAfterDelay(5f));
    }

    private IEnumerator ResetBeingAttackedAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        residentScheudle.recentlyAttacked = false;
        residentScheudle.runAwayFrom = null;
        residentScheudle.shouldRun = false;
        residentScheudle.agent.speed = residentScheudle.speed;
    }

    void setRigidbodyState(bool state)
    {
        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = state;
        }
    }

    void setColliderState(bool state)
    {
        foreach (Collider collider in colliders)
        {
            collider.enabled = state;
        }
    }


    private void StopAllMovementAndInteraction()
    {
        ResidentStats resident = gameObject.GetComponent<ResidentStats>();
        if (resident.textBox.activeSelf)
        {
            resident.textBox.SetActive(false);
            resident.schedule.SetActive(false);
            resident.StatObject.SetActive(false);
        }

        transform.parent = tempObject;
        transform.GetComponent<Animator>().enabled = false;
        NavMeshAgent navMeshAgent = transform.GetComponent<NavMeshAgent>();
        if (navMeshAgent.enabled)
        {
            navMeshAgent.isStopped = true;
            navMeshAgent.speed = 0;
        }
    }
}
