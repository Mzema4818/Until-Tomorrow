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

    [SerializeField]
    private float updateSpeedSeconds = 0.5f;
    // Start is called before the first frame update
    private void Awake()
    {
        hunger.onHungerPctChanged += HandleHungerChanged;
    }

    private void HandleHungerChanged(float pct)
    {
        StartCoroutine(changeToPct(pct));
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
}
