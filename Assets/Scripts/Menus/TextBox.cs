using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextBox : MonoBehaviour
{
    private TextMeshProUGUI textBox;
    public BuildMenuUpdater buildMenuUpdater;
    public GameObject ResidentTalkingTo;
    public GameObject Choices;

    public PlayerMovement playerMovement;
    public MouseLook mouseLook;

    private void Awake()
    {
        textBox = transform.GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        if (ResidentTalkingTo.transform.parent.name != "Resident")
        {
            textBox.text = "Please don't talk to me yet";
            TurnMovementOff();
        }
        else if (ResidentTalkingTo.transform.parent.name == "Resident" && ResidentTalkingTo.transform.GetComponent<ResidentStats>().joinedTown && !buildMenuUpdater.AccessToLevel0Buildings)
        {
            textBox.text = "What can I do for you boss?";
            TurnMovementOff();
        }
        else if (ResidentTalkingTo.transform.parent.name == "Resident" && buildMenuUpdater.AccessToLevel0Buildings)
        {
            textBox.text = "You don't have anything for me right now, go away";
            TurnMovementOff();
        }
        else if(ResidentTalkingTo.transform.parent.name == "Resident")
        {
            textBox.text = "Whats up?";
            TurnMovementOff();
        }

    }

    private void OnDisable()
    {
        try
        {
            ResidentTalkingTo.GetComponent<ResidentWander>().BeingTalkedTo = false;
            ResidentTalkingTo.GetComponent<ResidentWander>().resume();
        }
        catch { };
    }

    private void TurnMovementOff()
    {
        Choices.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        playerMovement.shouldMove = false;
        mouseLook.shouldLook = false;
    }
}
