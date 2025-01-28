using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResidentFood : MonoBehaviour
{
    [Header("Stats")]
    public ResidentStats residentStats;

    [Header("Other")]
    public Image FoodBar;
    public ResidentHealth residentHealth;
    public float DecayRate = 0f;

    void Start()
    {
        FoodBar.fillAmount = (residentStats.Stats[1] / 100.0f);

        //InvokeRepeating("naturalHungerDecrease", 0.0f, 1f);
        //InvokeRepeating("outOfHunger", 0.0f, 1f);
    }

    private void naturalHungerDecrease()
    {
        if(FoodBar.fillAmount > 0) ChangeValue(DecayRate);
    }

    private void outOfHunger()
    {
        if (FoodBar.fillAmount <= 0) residentHealth.ChangeValue(-10);
    }

    private IEnumerator changeToPct(float pct)
    {
        residentStats.Stats[1] += (int)pct; //stats[1] because its the food bar

        pct /= 100.0f;
        float preChangedPct = FoodBar.fillAmount;
        float elapsed = 0f;
        float amount = preChangedPct + pct;

        while (elapsed < 0.5f)
        {
            elapsed += Time.deltaTime;
            FoodBar.fillAmount = Mathf.Lerp(preChangedPct, amount, elapsed / 0.5f);
            yield return null;
        }
        FoodBar.fillAmount = amount;
    }

    public void ChangeValue(float pct)
    {
        StartCoroutine(changeToPct(pct));
        //print(FoodBar.fillAmount);
    }
}
