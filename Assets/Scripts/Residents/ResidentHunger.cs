using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResidentHunger : MonoBehaviour
{
    [SerializeField]
    public int maxHunger = 100;
    public ResidentHealth health;
    public int currentHunger;

    public event Action<float> onHungerPctChanged = delegate { };

    public ResidentStats residentStats;

    private void Awake()
    {
        health = GetComponent<ResidentHealth>();

        currentHunger = maxHunger;
    }

    void Start()
    {
        ModifyHunger(0); //this just sets the value when first opened.

        InvokeRepeating("naturalHungerDecrease", 0.0f, 1f);
        InvokeRepeating("outOfHunger", 0.0f, 1f);
    }

    private void naturalHungerDecrease()
    {
        if (currentHunger >= 0)
        {
            ModifyHunger(0);
        }
    }

    private void outOfHunger()
    {
        if (currentHunger <= 0)
        {
            health.ModifyHealth(-10, null);
        }
    }

    public void ModifyHunger(int amount)
    {
        if (amount < 0)
            amount = Mathf.Max(amount, -currentHunger); // Ensure we don't go below 0
        else
            amount = Mathf.Min(amount, maxHunger - currentHunger); // Ensure we don't exceed maxHunger

        currentHunger += amount;

        // Ensure maxHunger stays within valid bounds
        maxHunger = Mathf.Clamp(maxHunger, 0, 100);

        // Calculate the current hunger percentage
        float currentHungerPct = (float)currentHunger / (float)maxHunger;
        onHungerPctChanged(currentHungerPct);

        residentStats.Stats[1] += amount;
    }
}
