﻿using System.Collections;
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

    [Header("Other Menus")]
    public GameObject ResidentMenus;
    public GameObject BuilderMenus;

    [Header("Player Scripts")]
    public PlayerMovement playerMovement;
    public PlayerAnimations playerAnimations;
    public MouseLook mouseLook;
    public LightingManager lightingManager;
    public Builder builder;
    public InventoryManager inventoryManager;

    [Header("Tool Scripts")]
    public Tool axe;
    public Tool pick;
    public Tool sword;

    [Header("Other")]
    public GameObject Camera;
    public GameObject MainMenuWorld;

    [Header("Tools")]
    public GameObject hammer;

    // Update is called once per frame
    void Update()
    {
        KeyPressed(KeyCode.Escape, PauseMenu, false);
        KeyPressed(KeyCode.E, Inventory, false);
        KeyPressed(KeyCode.B, BuildMenu, true);
        KeyPressed(KeyCode.U, CollectionMenu, false);
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

                if (!Camera.activeSelf) Camera.SetActive(true);
                if (builder.isBuilding) hammer.SetActive(false);

                menu.SetActive(true);
                changePlayerState(false);
                return;
            }

            if (menu == Inventory && ChestInventory.activeSelf)
            {
                CloseChestInventory();
                menu.SetActive(false);
                changePlayerState(true);
                ChestInventory.SetActive(false);
                return;
            }

            if ((CheckIfMenusAreClosed() && CheckIfOtherMenusAreClosed()) || menu.activeSelf)
            {
                //if (builder.isBuilding) hammer.SetActive(false);

                if (!NeedHammer || HammerOn)
                {
                    if(builder.isBuilding){ builder.DestroyBuilding(); builder.isBuilding = false;}
                    if (!menu.activeSelf)
                    {
                        menu.SetActive(true);
                        changePlayerState(false);
                    }
                    else
                    {
                        menu.SetActive(false);
                        changePlayerState(true);
                    }
                }

                if (menu == Inventory)
                {
                    ToolBar.SetActive(true);
                    inventoryManager.TurnOffItem();
                }
            }
        }
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
        changePlayerState(false);
    }

    public void changePlayerState(bool state)
    {
        playerMovement.shouldMove = state;
        mouseLook.shouldLook = state;
        playerAnimations.shouldAnimating = state;
        //axe.shouldSwing = state;
        //pick.shouldSwing = state;
        //sword.shouldSwing = state;
        CharacterStuff.SetActive(state);
        ToolBar.SetActive(state);

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