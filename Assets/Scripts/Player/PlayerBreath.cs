using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBreath : MonoBehaviour 
{
    public UnderWater underWater;
    public Slider breathBar;
    public PlayerHealth playerHealth;
    public float breathRate = 10f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("losingBreath", 0.0f, 1f);
        InvokeRepeating("gainingBreath", 0.0f, 1f);
        InvokeRepeating("outOfBreath", 0.0f, 1f);
    }

    private void Update()
    {
        if(breathBar.value == 100 && !underWater.isUnderwater)
        {
            breathBar.gameObject.SetActive(false);
        }
    }

    private void losingBreath()
    {
        if (underWater.isUnderwater)
        {
            breathBar.value -= breathRate;
        }
    }

    public void gainingBreath()
    {
        if (!underWater.isUnderwater && breathBar.value != 100)
        {
            breathBar.value += breathRate;
        }
    }

    private void outOfBreath()
    {
        if (breathBar.value <= 0)
        {
            playerHealth.changeHealth(-10);
        }
    }

    public void restoreBreath()
    {
        breathBar.value = 100;
    }
}
