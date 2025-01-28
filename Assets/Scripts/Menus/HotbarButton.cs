using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HotbarButton : MonoBehaviour
{
    public event Action<int> OnButtonClicked;

    private TMP_Text text;
    private KeyCode keyCode;
    private int keyNumber;

    private void OnValidate()
    {
        keyNumber = transform.GetSiblingIndex() + 1;
        keyCode = KeyCode.Alpha0 + keyNumber;

        if (text == null)
        {
            text = GetComponentInChildren<TMP_Text>();
        }

        //text.SetText(keyNumber.ToString());
        gameObject.name = "Hotbar Button " + keyNumber;
    }

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(HandleClick);
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            try { HandleClick(); } catch (Exception) { }
            //HandleClick();
        }
    }

    private void HandleClick()
    {
        OnButtonClicked.Invoke(keyNumber);
    }
}
