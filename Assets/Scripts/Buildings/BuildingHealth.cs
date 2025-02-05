using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHealth : MonoBehaviour
{
    [SerializeField]
    public int maxHealth = 100;
    public int currentHealth;

    public event Action<float> onHealthPctChanged = delegate { };

    public OpenMenus openMenus;

    public bool damage;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (damage)
        {
            ModifyHealth(-20);
            damage = false;
        }

        if(currentHealth <= 0)
        {
            openMenus.CloseBuildMenus();
            openMenus.CloseChestInventory();

            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ModifyHealth(0); //this just sets the value when first opened.
    }

    public void ModifyHealth(int amount)
    {
        if (amount < 0)
            amount = Mathf.Max(amount, -currentHealth); // Ensure we don't go below 0
        else
            amount = Mathf.Min(amount, maxHealth - currentHealth); // Ensure we don't exceed maxHunger

        currentHealth += amount;

        // Ensure maxHunger stays within valid bounds
        maxHealth = Mathf.Clamp(maxHealth, 0, 100);

        // Calculate the current hunger percentage
        float currentHungerPct = (float)currentHealth / (float)maxHealth;
        onHealthPctChanged(currentHungerPct);
    }
}
