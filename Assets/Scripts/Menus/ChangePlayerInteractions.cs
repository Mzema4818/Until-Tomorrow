using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayerInteractions : MonoBehaviour
{
    public PlayerInteractions playerInteractions;

    private void OnDisable()
    {
        playerInteractions.residentTalkingTo = null;
    }
}
