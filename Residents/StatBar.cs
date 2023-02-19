using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatBar : MonoBehaviour
{
    public int[] stats;
    public GameObject starbar;

    public bool test;
    [SerializeField]
    private int maxStats = 20;

    public event Action<float> onHleathPctChanged = delegate { };

    private void Awake()
    {
        starbar = transform.Find("Stats").Find("CanvasStats").gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (test)
        {
            UpdateStats();
            test = false;
        }
    }

    public void UpdateStats()
    {
        for(int i = 0; i < starbar.transform.childCount - 4; i++)
        {
            Image foregroundImage = starbar.transform.GetChild(i).Find("Foreground").GetComponent<Image>();
            foregroundImage.fillAmount = (float)stats[i + 3] / (float)maxStats; //+3 is because of we dont want to update the stats for health, food and moral because they arent "stats" really
        }
        UpdateHealthFood();
    }

    public void UpdateHealthFood()
    {
        int num = starbar.transform.childCount;
        Image foregroundImage = starbar.transform.GetChild(num - 3).Find("Foreground").GetComponent<Image>(); //Health
        foregroundImage.fillAmount = (float)stats[0] / (float)100; //0 because first stat is health
        GetComponent<ResidentHealth>().currentHealth = stats[0];

        Image foregroundImage2 = starbar.transform.GetChild(num - 4).Find("Foreground").GetComponent<Image>(); //Food
        foregroundImage2.fillAmount = (float)stats[1] / (float)100; //1 because secons stat is food
        GetComponent<ResidentFood>().currentFood = stats[1];

        Image foregroundImage3 = starbar.transform.GetChild(num - 2).Find("Foreground").GetComponent<Image>(); //Moral
        foregroundImage3.fillAmount = (float)stats[2] / (float)20; //3 because first stat is health
    }
}
