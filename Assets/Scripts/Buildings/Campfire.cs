using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : MonoBehaviour
{
    public OpenMenus openMenus;
    public GameObject gameOverScreen;
    public bool gameOver;

    private void OnDestroy()
    {
        try
        {
            if (gameOver)
            {
                openMenus.CloseAllMenus();
                openMenus.MainMenuClose();
                openMenus.changePlayerState(false);
                gameOverScreen.SetActive(true);
            }
        }
        catch { };
    }
}
