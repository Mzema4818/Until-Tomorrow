using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [Header("Scripts")]
    public PlayerController playerController;
    public AudioMixer audioMixer;

    [Header("Texts")]
    public TextMeshProUGUI sensitivityValue;
    public TextMeshProUGUI volumeValue;

    [Header("Sliders")]
    public Slider sensitivitySlider;
    public Slider volumeSlider;

    [Header("Others")]
    private float volumeOutput;

    private void Awake()
    {
        //Sensitivity Set
        sensitivityValue.text = playerController.sensitivity.ToString();
        sensitivitySlider.value = playerController.sensitivity;

        //Volume Set
        audioMixer.GetFloat("volume", out volumeOutput);
        volumeValue.text = (volumeOutput + 80).ToString();
        volumeSlider.value = volumeOutput;
    }

    // Start is called before the first frame update
    public void SetSensitivity(float sensitivity)
    {
        playerController.sensitivity = sensitivity;
        sensitivityValue.text = sensitivity.ToString();
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
        audioMixer.GetFloat("volume", out volumeOutput);
        volumeValue.text = (volumeOutput + 80).ToString();
    }
}
