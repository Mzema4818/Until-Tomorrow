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
    private int maxHealth = 100;
    public int currentHealth;

    public event Action<float> onHealthPctChanged = delegate { };
    
    public Item.ItemType[] itemType;
    public int maxDrops;

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
        if(changeStat) residentStats.Stats[0] += amount;

        currentHealth += amount;

        float currentHealthPct = (float)currentHealth / (float)maxHealth;
        onHealthPctChanged(currentHealthPct);
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
            ParticleIndicator indicatorParticle = Instantiate(transform.GetComponent<ParticleHolder>().ParticleBreak, transform.position + new Vector3(0, 5, 0), Quaternion.identity).GetComponent<ParticleIndicator>();
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
}
