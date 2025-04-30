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

    [Header("Bars")]
    public Slider sensitivitySlider;
    public Slider volumeSlider;
    public TMP_Dropdown resolutionDropdown;

    [Header("Others")]
    public GameObject fps;
    private float volumeOutput;
    private Resolution[] resolutions;

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

    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for(int i = 0; i < resolutions.Length; i++)
        {
            options.Add(resolutions[i].width + " x " + resolutions[i].height);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height) currentResolutionIndex = i;
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

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

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetFPS(bool showFPS)
    {
        fps.SetActive(showFPS);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
