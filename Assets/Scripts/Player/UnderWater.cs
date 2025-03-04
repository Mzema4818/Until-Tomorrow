using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderWater : MonoBehaviour
{
    public float waterLevel;
    public bool isUnderwater;
    public Color normalColor;
    public Color underWaterColor;
    public GameObject waterBar;

    // Start is called before the first frame update
    void Start()
    {
        //normalColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        underWaterColor = new Color(0.22f, 0.65f, 0.77f, 0.5f);
    }

    //Update is called once per frame
    void Update()
    {
        if((transform.position.y < waterLevel) != isUnderwater)
        {
            isUnderwater = transform.position.y < waterLevel;
            if (isUnderwater) SetUnderwater();
            if (!isUnderwater) SetNormal();
        }
    }

    private void SetNormal()
    {
        //RenderSettings.fogColor = normalColor;
        RenderSettings.fogDensity = 0.015f;
        //waterBar.SetActive(false);
    }

    private void SetUnderwater()
    {
        RenderSettings.fogColor = underWaterColor;
        RenderSettings.fogDensity = 0.15f;
        waterBar.SetActive(true);
    }
}
