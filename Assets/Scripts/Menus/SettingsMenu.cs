using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [Header("Scripts")]
    public PlayerController playerController;

    [Header("Texts")]
    public TextMeshProUGUI sensitivityValue;

    [Header("Sliders")]
    public Slider sensitivitySlider;

    private void Awake()
    {
        //Sensitivity Set
        sensitivityValue.text = playerController.sensitivity.ToString();
        sensitivitySlider.value = playerController.sensitivity;
    }

    // Start is called before the first frame update
    public void SetSensitivity(float sensitivity)
    {
        playerController.sensitivity = sensitivity;
        sensitivityValue.text = sensitivity.ToString();
    }
}
