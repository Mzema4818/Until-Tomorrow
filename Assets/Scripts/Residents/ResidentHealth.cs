using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResidentHealth : MonoBehaviour
{
    [Header("Stats")]
    public ResidentStats residentStats;

    [Header("Other")]
    public Image HealthBar;

    private void Start()
    {
        HealthBar.fillAmount = (residentStats.Stats[0] / 100.0f);
    }

    private void Update()
    {
        if (HealthBar.fillAmount == 0) Destroy(gameObject);
    }

    private IEnumerator changeToPct(float pct)
    {
        residentStats.Stats[0] += (int)pct; //stats[0] because its the health bar

        pct /= 100.0f;
        float preChangedPct = HealthBar.fillAmount;
        float elapsed = 0f;
        float amount = preChangedPct + pct;

        while (elapsed < 0.5f)
        {
            elapsed += Time.deltaTime;
            HealthBar.fillAmount = Mathf.Lerp(preChangedPct, amount, elapsed / 0.5f);
            yield return null;
        }
        HealthBar.fillAmount = amount;
    }

    public void ChangeValue(float pct)
    {
        StartCoroutine(changeToPct(pct));
    }
}
