using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDeath : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject deathCamera;
    public GameObject deathMenu;
    public GameObject canvas;
    public GameObject mainCharacter;
    public GameObject food;
    public GameObject deathDrop;
    public bool dead = false;

    [Header("Player Scripts")]
    public InventoryManager inventoryManager;
    public PlayerHealth playerHealth;
    public PlayerMovement playerMovement;
    public PlayerAnimations playerAnimations;
    public PlayerFood playerFood;
    public PlayerBreath playerBreath;
    public MouseLook mouseLook;
    public GameObject weapons;

    public GameObject deathParent;
    private GameObject deathObject;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera.SetActive(true);
        deathCamera.SetActive(false);

        setRigidbodyState(true);
        setColliderState(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (dead)
        {
            deathCamera.transform.position = new Vector3(transform.position.x, transform.position.y + 30, transform.position.z);
            //mainCamera.SetActive(false);
            deathCamera.SetActive(true);
            die();
            dead = false;
        }
    }

    public void die()
    {
        deathCamera.transform.position = new Vector3(transform.position.x, transform.position.y + 30, transform.position.z);
        deathCamera.SetActive(true);

        GetComponent<Animator>().enabled = false;
        setRigidbodyState(false);
        setColliderState(true);
        TransferInventory();
    }

    void setRigidbodyState(bool state)
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

        foreach(Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = state;
        }

        //GetComponent<Rigidbody>().isKinematic = !state;
    }

    void setColliderState(bool state)
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach (Collider collider in colliders)
        {
            collider.enabled = state;
        }

        //GetComponent<Collider>().enabled = !state;
    }

    public void Respawn()
    {
        playerFood.resetHunger();
        playerHealth.changeHealth(100);
        playerBreath.restoreBreath();
        playerHealth.isAlive = true;

       // mainCamera.SetActive(true);
        deathCamera.SetActive(false);

        setRigidbodyState(true);
        setColliderState(false);

        deathMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        playerMovement.shouldMove = true;
        mouseLook.shouldLook = true;
        playerAnimations.shouldAnimating = true;
        weapons.SetActive(true);

        for (int i = 0; i < canvas.transform.childCount; i++)
        {
            if (canvas.transform.GetChild(i).transform.name == "Character Stuff")
            {
                canvas.transform.GetChild(i).gameObject.SetActive(true);
            }
        }

        GetComponent<Animator>().enabled = true;
        mainCharacter.transform.position = playerMovement.respawnPoint;
    }

    public void TransferInventory()
    {
        deathObject = Instantiate(deathDrop, transform.position + new Vector3(0, 0, 0), transform.rotation);
        deathObject.transform.parent = deathParent.transform;

        inventoryManager.TransferInventory(deathObject.GetComponent<Gravestone>());

        //Inventory Inventory = transform.parent.GetComponent<PlayInventory>().ReturnInventory();
        //Inventory DeathInventory = deathObject.GetComponent<DeathCollider>().inventory;

        //Inventory.TransferItems(Inventory, DeathInventory);

    }
}
