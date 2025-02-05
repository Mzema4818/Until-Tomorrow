using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungerBar : MonoBehaviour
{
    [SerializeField]
    private Image foregroundImage;

    [SerializeField]
    private Hunger hunger;
    public ResidentHunger residentHunger;

    [SerializeField]
    private float updateSpeedSeconds = 0.5f;
    // Start is called before the first frame update
    private void Awake()
    {
        if (hunger != null) hunger.onHungerPctChanged += HandleHungerChanged;
        else if (GetComponentInParent<ResidentHunger>() != null) GetComponentInParent<ResidentHunger>().onHungerPctChanged += HandleHungerChanged;
    }

    private void HandleHungerChanged(float pct)
    {
        if (gameObject.activeInHierarchy) StartCoroutine(changeToPct(pct));
        else changeToPctDisabled(pct);
    }

    private IEnumerator changeToPct(float pct)
    {
        float preChangedPct = foregroundImage.fillAmount;
        float elapsed = 0f;

        while (elapsed < updateSpeedSeconds)
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

    public void SetHungerResident()
    {
        GetComponentInParent<ResidentHunger>().onHungerPctChanged += HandleHungerChanged;
    }
}
