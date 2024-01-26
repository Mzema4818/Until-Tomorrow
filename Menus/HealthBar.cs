using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Image foregroundImage;
    public bool health;

    [SerializeField]
    private float updateSpeedSeconds = 0.5f;
    // Start is called before the first frame update
    private void Awake()
    {
        if (health)
        {
            if (GetComponentInParent<Health>() != null)
            {
                GetComponentInParent<Health>().onHleathPctChanged += HandleHealthChanged;
            }
            else if (GetComponentInParent<ResidentHealth>() != null)
            {
                //GetComponentInParent<ResidentHealth>().onHleathPctChanged += HandleHealthChanged;
            }
        }
        else
        {
            if (GetComponentInParent<ResidentFood>() != null)
            {
                //GetComponentInParent<ResidentFood>().onHleathPctChanged += HandleHealthChanged;
            }
        }
        
    }

    private void HandleHealthChanged(float pct)
    {
        StartCoroutine(changeToPct(pct));
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
}
