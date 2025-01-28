using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateData : MonoBehaviour
{
    [SerializeField]
    public Image foregroundImageFood;
    public Image foregroundImageHealth;

    [SerializeField]
    private float updateSpeedSeconds = 0.5f;
    // Start is called before the first frame update
    private void Awake()
    {
        GameObject starbar = transform.Find("Stats").Find("CanvasStats").gameObject;
        int num = starbar.transform.childCount;

        foregroundImageFood = starbar.transform.GetChild(num - 4).Find("Foreground").GetComponent<Image>();
        foregroundImageHealth = starbar.transform.GetChild(num - 3).Find("Foreground").GetComponent<Image>();

        if (GetComponentInParent<ResidentHealth>() != null)
        {
            //GetComponentInParent<ResidentHealth>().onHleathPctChanged += HandleHealthChanged;
        }

        if (GetComponentInParent<ResidentFood>() != null)
        {
            //GetComponentInParent<ResidentFood>().onHleathPctChanged += HandlefoodChanged;
        }

    }

    private void HandleHealthChanged(float pct)
    {
        if (gameObject.activeSelf) StartCoroutine(changeToPctHealth(pct));
    }

    private void HandlefoodChanged(float pct)
    {
        if(gameObject.activeSelf) StartCoroutine(changeToPctFood(pct));
    }

    private IEnumerator changeToPctHealth(float pct)
    {
        if (!gameObject.activeSelf) yield return null;

        float preChangedPct = foregroundImageHealth.fillAmount;
        float elapsed = 0f;

        while (elapsed < updateSpeedSeconds)
        {
            elapsed += Time.deltaTime;
            foregroundImageHealth.fillAmount = Mathf.Lerp(preChangedPct, pct, elapsed / updateSpeedSeconds);
            yield return null;
        }
        foregroundImageHealth.fillAmount = pct;
    }

    private IEnumerator changeToPctFood(float pct)
    {
        if (!gameObject.activeSelf) yield return null;

        float preChangedPct = foregroundImageFood.fillAmount;
        float elapsed = 0f;

        while (elapsed < updateSpeedSeconds)
        {
            elapsed += Time.deltaTime;
            foregroundImageFood.fillAmount = Mathf.Lerp(preChangedPct, pct, elapsed / updateSpeedSeconds);
            yield return null;
        }
        foregroundImageFood.fillAmount = pct;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
