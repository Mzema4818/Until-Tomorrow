using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Image foregroundImage;

    [SerializeField]
    private Health health;
    public BuildingHealth buildingHealth;
    public ResidentHealth residentHealth;

    [SerializeField]
    private float updateSpeedSeconds = 0.5f;
    // Start is called before the first frame update
    private void Awake()
    {
        if (health != null) health.onHealthPctChanged += HandleHealthChanged;
        else if (GetComponentInParent<Health>() != null) GetComponentInParent<Health>().onHealthPctChanged += HandleHealthChanged;
        else if (GetComponentInParent<ResidentHealth>() != null) GetComponentInParent<ResidentHealth>().onHealthPctChanged += HandleHealthChanged;
    }

    private void HandleHealthChanged(float pct)
    {
        if (gameObject.activeInHierarchy) StartCoroutine(changeToPct(pct));
        else changeToPctDisabled(pct);
    }

    private IEnumerator changeToPct(float pct)
    {
        float preChangedPct = foregroundImage.fillAmount;
        float elapsed = 0f;

        while( elapsed < updateSpeedSeconds)
        {
            elapsed += Time.deltaTime;
            foregroundImage.fillAmount = Mathf.Lerp(preChangedPct, pct, elapsed / updateSpeedSeconds);
            yield return null;
        }
        foregroundImage.fillAmount = pct;
    }

    private void changeToPctDisabled(float pct)
    {
        foregroundImage.fillAmount = pct;
    }

    public void SetHealth()
    {
        buildingHealth.onHealthPctChanged += HandleHealthChanged;
    }

    public void SetHeathResident()
    {
        GetComponentInParent<ResidentHealth>().onHealthPctChanged += HandleHealthChanged;
    }
}
