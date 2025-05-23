using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Breath : MonoBehaviour
{
    public UnderWater underWater;
    public GameObject breath;

    [SerializeField]
    public int maxBreath = 100;
    public Health health;
    public int currentBreath;

    public event Action<float> onBreathPctChanged = delegate { };

    private void Awake()
    {
        health = GetComponent<Health>();

        currentBreath = maxBreath;
    }

    void Start()
    {
        ModifyBreath(0); //this just sets the value when first opened.

        InvokeRepeating("losingBreath", 0.0f, 1f);
        InvokeRepeating("gainingBreath", 0.0f, 1f);
        InvokeRepeating("outOfBreath", 0.0f, 1f);
    }

    private void Update()
    {
        if (currentBreath == 100 && !underWater.isUnderwater)
        {
            breath.gameObject.SetActive(false);
        }
    }

    private void losingBreath()
    {
        if (underWater.isUnderwater && currentBreath >= 0)
        {
            ModifyBreath(-10);
        }
    }

    public void gainingBreath()
    {
        if (!underWater.isUnderwater && currentBreath != 100)
        {
            ModifyBreath(10);
        }
    }

    private void outOfBreath()
    {
        if (currentBreath <= 0)
        {
            health.ModifyHealth(-10);
        }
    }

    public void ModifyBreath(int amount)
    {
        if (amount < 0)
            amount = Mathf.Max(amount, -currentBreath); // Ensure we don't go below 0
        else
            amount = Mathf.Min(amount, maxBreath - currentBreath); // Ensure we don't exceed maxHunger

        currentBreath += amount;

        // Ensure maxHunger stays within valid bounds
        maxBreath = Mathf.Clamp(maxBreath, 0, 100);

        // Calculate the current hunger percentage
        float currentHungerPct = (float)currentBreath / (float)maxBreath;
        onBreathPctChanged(currentHungerPct);
    }
}
