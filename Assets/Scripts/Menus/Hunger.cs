using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class Hunger : MonoBehaviour
{
    [SerializeField]
    public int maxHunger = 100;
    public Health health;
    public int currentHunger;

    [Header("If resident")]
    public bool changeStat;
    public ResidentStats residentStats;

    public event Action<float> onHungerPctChanged = delegate { };

    private void Awake()
    {
        health = GetComponent<Health>();
        if (changeStat) residentStats = transform.parent.GetComponent<ResidentStats>();

        currentHunger = maxHunger;
    }

    void Start()
    {
        if (residentStats != null) currentHunger = residentStats.Stats[1];
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
            health.ModifyHealth(-10);
        }
    }

    public void ModifyHunger(int amount)
    {
        if (changeStat) residentStats.Stats[1] += amount;
        currentHunger += amount;

        float currentHungerPct = (float)currentHunger / (float)maxHunger;
        onHungerPctChanged(currentHungerPct);
    }
}
