using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : MonoBehaviour
{
    public OpenMenus openMenus;
    public GameObject gameOverScreen;

    private void OnDestroy()
    {
        try
        {
            openMenus.CloseAllMenus();
            openMenus.MainMenuClose();
            openMenus.changePlayerState(false);
            gameOverScreen.SetActive(true);
        }
        catch { };
    }
}
