using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderValues : MonoBehaviour
{
    [Header("Slider Data")]
    public Slider sensitivitySlider;
    public TextMeshProUGUI sensitivityValue;

    [Header("Scripts")]
    public MouseLook mouseLook;
    // Start is called before the first frame update
    void OnEnable()
    {
        sensitivitySlider.value = mouseLook.mouseSensitivity;
    }

    // Update is called once per frame
    void Update()
    {
        sensitivityValue.text = sensitivitySlider.value.ToString();
        mouseLook.mouseSensitivity = sensitivitySlider.value;
    }
}
