using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider healthBar;
    public GameObject deathMenu;
    public OnDeath playerDeath;

    [Header("Player Scripts")]
    public OpenMenus openMenus;
    public PlayerMovement playerMovement;
    public PlayerAnimations playerAnimations;
    public MouseLook mouseLook;
    public GameObject weapons;
    public OnDeath onDeath;

    public bool isAlive;
    public bool test;

    private void Start()
    {
        isAlive = true;
        test = false;
    }

    public void changeHealth(float value)
    {
        healthBar.value += value;
    }

    private void Update()
    {
        if (test)
        {
            changeHealth(-100);
            test = false;
        }

        if (healthBar.value <= 0 && isAlive)
        {
            openMenus.CloseAllMenus();
            openMenus.CloseAllOtherMenus();
            openMenus.ToolBar.SetActive(false);

            Cursor.lockState = CursorLockMode.None;
            playerMovement.shouldMove = false;
            mouseLook.shouldLook = false;
            playerAnimations.shouldAnimating = false;
            deathMenu.SetActive(true);

            //onDeath.die();

            isAlive = false;
        }
    }
}
