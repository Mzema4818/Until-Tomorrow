using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResidentFood : MonoBehaviour
{
    [SerializeField]
    public int maxFood = 100;

    public int currentFood;

    public event Action<float> onHleathPctChanged = delegate { };

    private ResidentStats residentStats;
    public ResidentHealth residentHealth;
    public int hungerRate = 1;

    private void Awake()
    {
        residentStats = transform.GetComponent<ResidentStats>();
    }
    // Start is called before the first frame update
    private void OnEnable()
    {
        currentFood = residentStats.Stats[1];
    }

    private void Start()
    {
        InvokeRepeating("naturalHungerDecrease", 0.0f, 1f);
        InvokeRepeating("OutOfFood", 0.0f, 1f);
    }

    public void ModifyFood(int amount)
    {
        if(currentFood > 0)
        {
            currentFood += amount;
            residentStats.Stats[1] += amount;

            float currentHealthPct = (float)currentFood / (float)maxFood;
            onHleathPctChanged(currentHealthPct);
        }
    }

    public void CanvasAlphaChangeOverTime(GameObject canvas, float speed)
    {
        float alphaColor = canvas.GetComponent<CanvasGroup>().alpha;

        alphaColor -= Time.deltaTime * speed;
        canvas.GetComponent<CanvasGroup>().alpha = alphaColor;

        if (alphaColor <= 0)
        {
            canvas.SetActive(false);
        }
    }

    private void naturalHungerDecrease()
    {
        ModifyFood(-hungerRate);
    }

    private void OutOfFood()
    {
        if (currentFood <= 0)
        {
            residentHealth.ModifyHealth(-10);
        }
    }
}
