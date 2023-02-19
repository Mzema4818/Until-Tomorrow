using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public GameObject player;
    private GameObject canvas;

    [SerializeField]
    private int maxHealth = 100;

    private int currentHealth;

    public event Action<float> onHleathPctChanged = delegate { };
    public Item.ItemType[] itemType;
    public int maxDrops;

    private void Awake()
    {
        canvas = transform.Find("Canvas").gameObject;
        //player = GameObject.Find("Main Character");

        canvas.SetActive(false);
    }
    // Start is called before the first frame update
    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    public void ModifyHealth(int amount)
    {
        currentHealth += amount;

        float currentHealthPct = (float)currentHealth / (float)maxHealth;
        onHleathPctChanged(currentHealthPct);
    }

    private void Update()
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

        if(currentHealth < 0)
        {
            if(maxDrops != 0)
            {
                ItemDropByType();
            }
            Destroy(gameObject);  
        }

    }

    public string FixName(string name)
    {
        int strSet = name.IndexOf("(");

        return name.Substring(0, strSet);
    }

    public void ItemDropByType()
    {
        int num;
        for (int i = 0; i < itemType.Length; i++)
        {
            num = UnityEngine.Random.Range(1, maxDrops);
            ItemWorld.SpawnItemWorld(transform.position + new Vector3(0, 5, 0), new Item { itemType = itemType[i], amount = num });
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
