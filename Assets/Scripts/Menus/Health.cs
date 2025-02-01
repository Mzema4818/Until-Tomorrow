using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public GameObject player;
    public Transform DropableParent;

    [SerializeField]
    public GameObject canvas;

    [SerializeField]
    private OnDeath onDeath;

    [Header("If resident")]
    public bool changeStat;
    public ResidentStats residentStats;

    [SerializeField]
    public int maxHealth = 100;
    public int currentHealth;

    public event Action<float> onHealthPctChanged = delegate { };
    
    public Item.ItemType[] itemType;
    public int maxDrops;
    public GameObject FallenTree;

    [Header("Arms stuff (for player only)")]
    public PlayerAttack playerAttack;
    public bool kill;

    private void Awake()
    {
        if(changeStat) residentStats = transform.parent.GetComponent<ResidentStats>();

        if(canvas == null)
        {
            canvas = transform.Find("Canvas").gameObject;
            canvas.SetActive(false);
        }

        currentHealth = maxHealth;
    }

    private void Start()
    {
        if (residentStats != null) currentHealth = residentStats.Stats[0];

        ModifyHealth(0); //this just sets the value when first opened.
    }

    public void ModifyHealth(int amount)
    {
        if (amount < 0)
            amount = Mathf.Max(amount, -currentHealth); // Ensure we don't go below 0
        else
            amount = Mathf.Min(amount, maxHealth - currentHealth); // Ensure we don't exceed maxHunger

        currentHealth += amount;

        // Ensure maxHunger stays within valid bounds
        maxHealth = Mathf.Clamp(maxHealth, 0, 100);

        // Calculate the current hunger percentage
        float currentHungerPct = (float)currentHealth / (float)maxHealth;
        onHealthPctChanged(currentHungerPct);
    }

    private void Update()
    {
        if (kill)
        {
            ModifyHealth(-20);
            kill = false;
        }


        if(player != null)
        {
            if (Vector3.Distance(player.transform.position, transform.position) < 15)
            {
                canvas.GetComponent<CanvasGroup>().alpha = 1;
                canvas.SetActive(true);
            }
            else
            {
                CanvasAlphaChangeOverTime(canvas, 2f);
            }

            if (currentHealth <= 0)
            {
                if (maxDrops != 0)
                {
                    ItemDropByType();
                    if(GetComponent<TreeLocation>() != null){ SpawnFallingTree(); }
                }

                if (!changeStat)
                {
                    Destroy(gameObject);
                }
                else
                {
                    Destroy(transform.parent.gameObject);
                }
            }
        }
        else
        {
            if (currentHealth <= 0)
            {
                onDeath.PlayerDied();

                playerAttack.canAttack = false;
                playerAttack.ATTACK1 = "";
                playerAttack.ATTACK2 = "";
                //To ensure that more gravestones down spawn
                ModifyHealth(maxHealth);
            }
        }
    }

    public void ItemDropByType()
    {
        int num;
        for (int i = 0; i < itemType.Length; i++)
        {
            num = UnityEngine.Random.Range(1, maxDrops);
            for(int j = 0; j < num; j++)
            {
                //Item item = new Item { itemType = itemType[i] };
                //Transform dropable = Instantiate(item.GetMesh(), transform.position, Quaternion.identity);
                //dropable.parent = DropableParent;
                ItemWorld.DropItem(transform, new Item { itemType = itemType[i] }, DropableParent);
            }
            //ItemWorld.SpawnItemWorld(transform.position + new Vector3(0, 5, 0), new Item { itemType = itemType[i], amount = num });
        }

        if(transform.GetComponent<ParticleHolder>() != null)
        {
            GameObject particles = Instantiate(transform.GetComponent<ParticleHolder>().ParticleBreak, transform.position + new Vector3(0, 15, 0), transform.rotation);
            //ParticleIndicator indicatorParticle = Instantiate(transform.GetComponent<ParticleHolder>().ParticleBreak, transform.position + new Vector3(0, 15, 0), Quaternion.identity).GetComponent<ParticleIndicator>();
        }
    }

    public void CanvasAlphaChangeOverTime(GameObject canvas, float speed)
    {
        float alphaColor = canvas.GetComponent<CanvasGroup>().alpha;

        alphaColor -= Time.deltaTime * speed;
        canvas.GetComponent<CanvasGroup>().alpha = alphaColor;

        if (alphaColor <= 0)
        {
            canvas.SetActive(false);
        }
    }

    public void SpawnFallingTree()
    {
        GameObject newTree = Instantiate(gameObject, transform.position, transform.rotation);
        newTree.layer = LayerMask.NameToLayer("Temp");
        Destroy(newTree.GetComponent<TreeLocation>());

        // Remove all components except LODGroup
        Component[] components = newTree.GetComponents<Component>();
        foreach (Component comp in components)
        {
            if (!(comp is Transform) && !(comp is LODGroup))
            {
                Destroy(comp);
            }
        }

        // Add Rigidbody
        newTree.AddComponent<Rigidbody>();

        // Collect children to remove before modifying hierarchy
        List<Transform> childrenToRemove = new List<Transform>();

        foreach (Transform child in newTree.transform)
        {
            if (child.name == "Canvas" || child.name == "Location")
            {
                childrenToRemove.Add(child); // Mark for removal
            }
        }

        // Remove children after iteration
        foreach (Transform child in childrenToRemove)
        {
            Destroy(child.gameObject);
        }

        // Wait until after deletion to modify remaining children
        foreach (Transform child in newTree.transform)
        {
            child.gameObject.layer = LayerMask.NameToLayer("Temp");
            MeshCollider meshCollider = child.GetComponent<MeshCollider>();
            if (meshCollider != null) // Ensure the component exists before using it
            {
                meshCollider.convex = true;
            }
        }
        Destroy(newTree, 5f);
    }
}
