using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMenus : MonoBehaviour
{
    [Header("Menu Items")]
    public Builder builder;
    public GameObject Inventory;
    public GameObject PauseMenu;
    public GameObject BuildMenu;
    public GameObject CollectionMenu;
    public GameObject CharacterStuff;
    public GameObject Builder;
    public GameObject player;
    public GameObject mainMenu;
    public GameObject settings;
    public GameObject CollectedMenu;
    public GameObject food;

    [Header("Player Scripts")]
    public PlayerMovement playerMovement;
    public PlayerAnimations playerAnimations;
    public MouseLook mouseLook;
    public LightingManager lightingManager;

    [Header("Tool Scripts")]
    public Tool axe;
    public Tool pick;
    public Tool sword;

    [Header("Tools")]
    public GameObject hammer;

    [Header("Builder Menu")]
    public GameObject buildings;
    public GameObject campfire;
    public GameObject tent;

    private bool campfirePlace;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !PauseMenu.activeSelf && !builder.isBuilding)
        {
            turnOffMenus();
            changePlayerState(false);
            PauseMenu.SetActive(true);
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && PauseMenu.activeSelf)
        {
            changePlayerState(true);
            PauseMenu.SetActive(false);

            for(int i = 0; i < PauseMenu.transform.childCount; i++)
            {
                PauseMenu.transform.GetChild(i).gameObject.SetActive(true);
            }
            settings.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.E) && !PauseMenu.activeSelf && !Inventory.activeSelf && !BuildMenu.activeSelf && !CollectionMenu.activeSelf && !CollectionMenu.activeSelf && !CollectedMenu.activeSelf)
        {
            turnOffMenus();
            changePlayerState(false);
            for(int i = 0; i < food.transform.childCount; i++)
            {
                food.transform.GetChild(i).gameObject.SetActive(false);
            }
            Inventory.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.E) && !PauseMenu.activeSelf && Inventory.activeSelf && !BuildMenu.activeSelf && !CollectionMenu.activeSelf && !CollectedMenu.activeSelf)
        {
            changePlayerState(true);
            Inventory.SetActive(false);
        }

        if(Input.GetKeyDown(KeyCode.B) && hammer.activeSelf && !PauseMenu.activeSelf && !Inventory.activeSelf && !BuildMenu.activeSelf && !builder.isBuilding && !CollectionMenu.activeSelf && !CollectedMenu.activeSelf)
        {
            turnOffMenus();
            changePlayerState(false);
            BuildMenu.SetActive(true);
        }
        else if(Input.GetKeyDown(KeyCode.B) && !PauseMenu.activeSelf && !Inventory.activeSelf && BuildMenu.activeSelf && !CollectionMenu.activeSelf && !CollectedMenu.activeSelf)
        {
            changePlayerState(true);
            BuildMenu.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.U) && !PauseMenu.activeSelf && !Inventory.activeSelf && !BuildMenu.activeSelf && !builder.isBuilding && !CollectionMenu.activeSelf && !CollectedMenu.activeSelf)
        {
            turnOffMenus();
            changePlayerState(false);
            CheckForBuildings();
            CollectionMenu.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.U) && !PauseMenu.activeSelf && !Inventory.activeSelf && !BuildMenu.activeSelf && CollectionMenu.activeSelf && !CollectedMenu.activeSelf)
        {
            changePlayerState(true);
            CollectionMenu.SetActive(false);
        }
    }

    public void turnOffMenus()
    {
        Inventory.SetActive(false);
        PauseMenu.SetActive(false);
        BuildMenu.SetActive(false);
        CollectionMenu.SetActive(false);
        CollectedMenu.SetActive(false);
    }

    public void changePlayerState(bool state)
    {
        playerMovement.shouldMove = state;
        mouseLook.shouldLook = state;
        playerAnimations.shouldAnimating = state;
        axe.shouldSwing = state;
        pick.shouldSwing = state;
        sword.shouldSwing = state;
        CharacterStuff.SetActive(state);

        if (state)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void turnOnMenu()
    {
        changePlayerState(true);
        PauseMenu.SetActive(false);
    }
    public void turnOffMenu()
    {
        changePlayerState(false);
        turnOffMenus();
        mainMenu.SetActive(true);
        lightingManager.multiplier = 1;
        lightingManager.TimeOfDay = 10;
        player.SetActive(false);

    }

    public void turnOffAllMenus()
    {
        changePlayerState(true);
        turnOffMenus();
    }

    public void CloseBuildMenus()
    {
        for(int i = 0; i < Builder.transform.childCount; i++)
        {
            Builder.transform.GetChild(i).gameObject.SetActive(false);
        }
        changePlayerState(true);
    }

    private void CheckForBuildings()
    {
        for(int i = 0; i < buildings.transform.childCount; i++)
        {
            if (buildings.transform.GetChild(i).name == "campfire")
            {
                campfirePlace = true;
            }
        }

        if (campfirePlace)
        {
            campfire.SetActive(false);
            tent.SetActive(true);
        }
        else
        {
            campfire.SetActive(true);
            tent.SetActive(false);
        }
    }
}
