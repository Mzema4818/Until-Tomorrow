using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreathBar : MonoBehaviour
{
    [SerializeField]
    private Image foregroundImage;

    [SerializeField]
    private Breath breath;

    [SerializeField]
    private float updateSpeedSeconds = 0.5f;
    // Start is called before the first frame update
    private void Awake()
    {
        breath.onBreathPctChanged += HandleBreathChanged;
    }

    private void HandleBreathChanged(float pct)
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
