using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMenus : MonoBehaviour
{
    [Header("Player Menus")]
    public GameObject[] allMenus;
    public GameObject MainMenu;
    public GameObject Inventory;
    public GameObject PauseMenu;
    public GameObject BuildMenu;
    public GameObject CollectionMenu;
    public GameObject CharacterStuff;
    public GameObject ToolBar;
    public GameObject ChestInventory;
    public GameObject deathMenu;
    public GameObject gameOverScreen;
    public GameObject settingsMenu;

    [Header("Other Menus")]
    public GameObject ResidentMenus;
    public GameObject BuilderMenus;

    [Header("Player Scripts")]
    public PlayerController playerController;
    public LightingManager lightingManager;
    public Builder builder;
    public InventoryManager inventoryManager;
    public PlayerAttack playerAttack;
    public Book book;
    public Crosshair crosshair;

    [Header("Tool Scripts")]
    public Tool axe;
    public Tool pick;
    public Tool sword;

    [Header("Other")]
    public GameObject Camera;
    public GameObject MainMenuWorld;
    public GameObject ContinueSign;
    public GameObject ConintueWords;

    [Header("Tools")]
    public GameObject hammer;

    [Header("Animator")]
    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        if (!deathMenu.activeSelf && !gameOverScreen.activeSelf)
        {
            //CloseMenu(KeyCode.F);

            KeyPressed(KeyCode.Escape, PauseMenu, false);
            KeyPressed(KeyCode.Tab, Inventory, false);
            KeyPressed(KeyCode.B, BuildMenu, true);
            KeyPressed(KeyCode.U, CollectionMenu, false);
        }
    }

    private void KeyPressed(KeyCode keycode, GameObject menu, bool NeedHammer)
    {
        bool HammerOn = false;
        if (NeedHammer) HammerOn = hammer.activeSelf;

        if (Input.GetKeyDown(keycode))
        {
            if (MainMenuWorld.activeSelf) return;

            if (menu == PauseMenu && !menu.activeSelf)
            {
                CloseAllMenus();
                CloseAllOtherMenus();
                settingsMenu.SetActive(false);

                if (!Camera.activeSelf) Camera.SetActive(true);
                if (builder.isBuilding) hammer.SetActive(false);

                menu.SetActive(true);
                changePlayerState(false);
                //playerAttack.shouldAttack = false;
                return;
            }

            if (menu == Inventory && ChestInventory.activeSelf)
            {
                CloseChestInventory();
                menu.SetActive(false);
                changePlayerState(true);
                ChestInventory.SetActive(false);
                playerAttack.shouldAttack = !playerAttack.shouldAttack;
                return;
            }

            if ((CheckIfMenusAreClosed() && CheckIfOtherMenusAreClosed()) || menu.activeSelf)
            {
                //if (builder.isBuilding) hammer.SetActive(false);

                if (!NeedHammer || HammerOn)
                {
                    //playerAttack.shouldAttack = true;
                    if (builder.isBuilding){ builder.DestroyBuilding(); builder.isBuilding = false;}
                    if (!menu.activeSelf)
                    {
                        menu.SetActive(true);
                        changePlayerState(false);
                        //playerAttack.shouldAttack = !playerAttack.shouldAttack;
                    }
                    else
                    {
                        book.GoBackToPageOne();
                        menu.SetActive(false);
                        changePlayerState(true);
                        //print(settingsMenu.activeSelf);
                        //settingsMenu.SetActive(false);
                        //playerAttack.shouldAttack = !playerAttack.shouldAttack;
                    }
                }

                if (menu == Inventory)
                {
                    ToolBar.SetActive(true);
                }
            }
        }
    }

    private void CloseMenu(KeyCode keycode)
    {
        WhatInventory whatInventory = ChestInventory.GetComponent<WhatInventory>();

        if (Input.GetKeyDown(keycode) && whatInventory.inventoryOpen1 != null)
        {
            print(whatInventory.inventoryOpen1);
        }

        /*WhatInventory whatInventory = ChestInventory.GetComponent<WhatInventory>();

        if (Input.GetKeyDown(keycode))
        {
            if(whatInventory.inventoryOpen1 != null)
            {
                print("closed");
                CloseChestInventory();
                Inventory.SetActive(false);
                changePlayerState(true);
                ChestInventory.SetActive(false);
                playerAttack.shouldAttack = !playerAttack.shouldAttack;
                return;
            }
        }*/
    }

    public bool CheckIfMenusAreClosed()
    {
        foreach (GameObject objects in allMenus)
        {
            if (objects.activeSelf) return false;
        }

        return true;
    }

    public bool CheckIfOtherMenusAreClosed()
    {
        foreach (Transform children in ResidentMenus.transform)
        {
            if (children.gameObject.activeSelf) return false;
        }

        foreach (Transform children in BuilderMenus.transform)
        {
            if (children.gameObject.activeSelf) return false;
        }

        return true;
    }

    public void CloseAllMenus()
    {  
        foreach (GameObject objects in allMenus)
        {
            objects.SetActive(false);
        }
    }

    public void CloseAllOtherMenus()
    {
        foreach (Transform children in ResidentMenus.transform)
        {
            children.gameObject.SetActive(false);
        }

        foreach (Transform children in BuilderMenus.transform)
        {
            children.gameObject.SetActive(false);
        }

        CloseChestInventory();
    }

    public void CloseChestInventory()
    {
        WhatInventory inventory = ChestInventory.GetComponent<WhatInventory>();
        if (inventory.inventoryOpen1 != null)
        {
            inventory.inventoryOpen1.SetActive(false);
            inventory.inventoryOpen1 = null;
        }

        if (inventory.inventoryOpen2 != null)
        {
            inventory.inventoryOpen2.SetActive(false);
            inventory.inventoryOpen2 = null;
        }
    }

    public void TurnOffMenus()
    {
        CloseAllMenus();
        foreach (Transform children in BuilderMenus.transform)
        {
            children.gameObject.SetActive(false);
        }

        changePlayerState(true);
    }

    public void CloseBuildMenus()
    {
        TurnOffMenus();
    }

    public void MainMenuOpen()
    {
        MainMenuWorld.SetActive(true);
        TurnOffMenus();
        ContinueSign.SetActive(true);
        ConintueWords.SetActive(true);
        changePlayerState(false);
    }

    public void TurnOffContinue()
    {
        ContinueSign.SetActive(false);
        ConintueWords.SetActive(false);
    }

    public void MainMenuClose()
    {
        MainMenuWorld.SetActive(false);
        TurnOffMenus();
        ContinueSign.SetActive(false);
        ConintueWords.SetActive(false);
        changePlayerState(true);
        playerAttack.shouldAttack = true;
    }

    public void changePlayerState(bool state)
    {
        if (animator == null) return;

        playerController.shouldMove = state;
        animator.SetFloat("velocity X", 0);
        animator.SetFloat("velocity Z", 0);
        //axe.shouldSwing = state;
        //pick.shouldSwing = state;
        //sword.shouldSwing = state;
        CharacterStuff.SetActive(state);
        ToolBar.SetActive(state);
        playerAttack.shouldAttack = state;

        if (state)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }

    }
}
