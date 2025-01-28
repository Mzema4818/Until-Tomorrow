using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hotbar : MonoBehaviour
{
    public GameObject[] Tools;
    public GameObject[] Equipables;

    private void Awake()
    {
        foreach (var button in GetComponentsInChildren<HotbarButton>())
        {
            button.OnButtonClicked += ButtonOnOButtonClicked;
            button.gameObject.SetActive(false);
        }
    }

    private void ButtonOnOButtonClicked(int buttonNumber)
    {
        //Tools on
        if (buttonNumber == 1 && !Tools[buttonNumber - 1].activeSelf)
        {
            EnableTools(buttonNumber);
        }
        else if (buttonNumber == 2 && !Tools[buttonNumber - 1].activeSelf)
        {
            EnableTools(buttonNumber);
        }
        else if (buttonNumber == 3 && !Tools[buttonNumber - 1].activeSelf)
        {
            EnableTools(buttonNumber);
        }
        else if (buttonNumber == 4 && !Tools[buttonNumber - 1].activeSelf)
        {
            EnableTools(buttonNumber);
        }
        //Tools off
        else if (buttonNumber == 1 && Tools[buttonNumber - 1].activeSelf)
        {
            DisableTools();
        }
        else if (buttonNumber == 2 && Tools[buttonNumber - 1].activeSelf)
        {
            DisableTools();
        }
        else if (buttonNumber == 3 && Tools[buttonNumber - 1].activeSelf)
        {
            DisableTools();
        }
        else if (buttonNumber == 4 && Tools[buttonNumber - 1].activeSelf)
        {
            DisableTools();
        }
    }

    private void EnableTools(int num)
    {
        DisableTools();

        Tools[num - 1].SetActive(true);
    }

    private void DisableTools()
    {
        foreach (GameObject tool in Tools)
        {
            tool.SetActive(false);
        }

        foreach (GameObject equipables in Equipables)
        {
            equipables.SetActive(false);
        }
    }
}