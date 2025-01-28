using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMenu : MonoBehaviour
{
    [Header("Menus")]
    public GameObject PauseMenu;
    public GameObject CharacterStuff;

    [Header("Player Scripts")]
    public PlayerMovement playerMovement;
    public PlayerAnimations playerAnimations;
    public MouseLook mouseLook;
    public LightingManager lightingManager;

    [Header("Tool Scripts")]
    public Tool axe;
    public Tool pick;
    public Tool sword;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        KeyPressed(KeyCode.Escape, PauseMenu);
    }

    private void KeyPressed(KeyCode keycode, GameObject menu)
    {
        if (Input.GetKeyDown(keycode) && !menu.activeSelf)
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

    public void changePlayerState(bool state)
    {
        playerMovement.shouldMove = state;
        mouseLook.shouldLook = state;
        playerAnimations.shouldAnimating = state;
        //axe.shouldSwing = state;
        //pick.shouldSwing = state;
        //sword.shouldSwing = state;
        CharacterStuff.SetActive(state);

        if (state) Cursor.lockState = CursorLockMode.Locked;
        else Cursor.lockState = CursorLockMode.None;

    }
}