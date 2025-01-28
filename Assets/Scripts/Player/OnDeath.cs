using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class OnDeath : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject deathCamera;
    public GameObject deathMenu;
    public GameObject canvas;
    public GameObject mainCharacter;
    public GameObject food;
    public GameObject deathDrop;

    [Header("Player Scripts")]
    public Animator animator;
    public OpenMenus openMenus;
    public InventoryManager inventoryManager;
    public PlayerController playerController;
    public Hunger hunger;
    public PlayerBreath playerBreath;
    public GameObject weapons;

    public GameObject character;
    public GameObject deathParent;

    private Rigidbody[] rigidbodies;
    private Collider[] colliders;


    //Remember animator has to set to false to have it so ragdolls work

    // Start is called before the first frame update
    private void Awake()
    {
        animator = GetComponent<Animator>();

        character = transform.parent.gameObject;
        openMenus = character.GetComponent<OpenMenus>();
        
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();
    }

    void Start()
    {
        mainCamera.SetActive(true);
        deathCamera.SetActive(false);

        setRigidbodyState(true);
        setColliderState(false);
    }

    public void PlayerDied()
    {
        Cursor.lockState = CursorLockMode.None;

        openMenus.CloseAllMenus();
        openMenus.CloseAllOtherMenus();

        openMenus.ToolBar.SetActive(false);
        deathMenu.SetActive(true);

        playerController.shouldMove = false;
        animator.enabled = false;

        deathCamera.transform.position = new Vector3(transform.position.x, transform.position.y + 30, transform.position.z);
        deathCamera.SetActive(true);

        animator.SetFloat("velocity X", 0);
        animator.SetFloat("velocity Z", 0);

        TransferInventory();
        setRigidbodyState(false);
        setColliderState(true);
    }

    void setRigidbodyState(bool state)
    {
        foreach(Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = state;
        }
    }

    void setColliderState(bool state)
    {
        foreach (Collider collider in colliders)
        {
            collider.enabled = state;
        }

    }

    public void Respawn()
    {
        hunger.ModifyHunger(hunger.maxHunger);
        //playerHealth.changeHealth(100);
        playerBreath.restoreBreath();
        //playerHealth.isAlive = true;

       // mainCamera.SetActive(true);
        deathCamera.SetActive(false);

        setRigidbodyState(true);
        setColliderState(false);

        deathMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        playerController.shouldMove = true;
        weapons.SetActive(true);

        for (int i = 0; i < canvas.transform.childCount; i++)
        {
            if (canvas.transform.GetChild(i).transform.name == "Character Stuff")
            {
                canvas.transform.GetChild(i).gameObject.SetActive(true);
            }
        }

        GetComponent<Animator>().enabled = true;

        mainCharacter.transform.position = playerController.respawnPoint;
    }

    public void TransferInventory()
    {
        GameObject deathObject = Instantiate(deathDrop, transform.position + new Vector3(0, 0, 0), transform.rotation);
        deathObject.transform.parent = deathParent.transform;

        inventoryManager.TransferInventory(deathObject.GetComponent<Gravestone>());

        //Inventory Inventory = transform.parent.GetComponent<PlayInventory>().ReturnInventory();
        //Inventory DeathInventory = deathObject.GetComponent<DeathCollider>().inventory;

        //Inventory.TransferItems(Inventory, DeathInventory);

    }
}
