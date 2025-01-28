using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFood : MonoBehaviour
{
    public Slider foodBar;
    public PlayerHealth playerHealth;
    public float hungerRate = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("naturalHungerDecrease", 0.0f, 5f);
        InvokeRepeating("outOfHunger", 0.0f, 5f);
    }

    private void naturalHungerDecrease()
    {
        foodBar.value -= hungerRate;
    }

    public void resetHunger()
    {
        foodBar.value = 100;
    }

    private void outOfHunger()
    {
        if (foodBar.value <= 0)
        {
            playerHealth.changeHealth(-10);
        }
    }
}
