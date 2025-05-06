using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private void Awake()
    {
        residentScheudle = transform.GetComponent<ResidentScheudle>();
        currentHealth = maxHealth;
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
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ModifyHealth(0, null); //this just sets the value when first opened.
    }

    public void ModifyHealth(int amount, Transform whoDamaged)
    {
        if (amount < 0)
        {
            amount = Mathf.Max(amount, -currentHealth); // Ensure we don't go below 0
            this.whoDamaged = whoDamaged;
            TriggerBeingAttacked();
        }
        else
            amount = Mathf.Min(amount, maxHealth - currentHealth); // Ensure we don't exceed maxHunger

        currentHealth += amount;

        // Ensure maxHunger stays within valid bounds
        maxHealth = Mathf.Clamp(maxHealth, 0, 100);

        // Calculate the current hunger percentage
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

        residentScheudle.runAwayFrom = whoDamaged;
        residentScheudle.shouldRun = true;
        residentScheudle.agent.speed = residentScheudle.runSpeed;
        residentScheudle.RemoveJobs();
        attackCoroutine = StartCoroutine(ResetBeingAttackedAfterDelay(5f));
    }

    private IEnumerator ResetBeingAttackedAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        residentScheudle.runAwayFrom = null;
        residentScheudle.shouldRun = false;
        residentScheudle.agent.speed = residentScheudle.speed;
    }
}
