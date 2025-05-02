using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildingMenuUpdater : MonoBehaviour
{
    public Builder builder;
    public TextMeshProUGUI buildingName;
    public BuildingHealth buildingHealth;
    public HealthBar healthBar;
    public Transform costChecker;
    public GameObject residentsBar;
    public GameObject namesPrefab;

    private void Awake()
    {
        //Set building name
        buildingName.text = FixName(transform.parent.name);

        //Set building health
        healthBar.SetHealth();
        buildingHealth.ModifyHealth(0);

        //Set repair costs
        ChangeRepairCost(builder.CheckRepairCost(buildingName.text.ToString()));

        //Check if there are residents
        if (transform.parent.GetComponent<Job>() == null) residentsBar.SetActive(false);
        else ChangeResidents();
    }

    private void OnEnable()
    {
        buildingHealth.ModifyHealth(0);
    }

    public void ChangeResidents()
    {
        Job job = transform.parent.GetComponent<Job>();
        for (int i = 0; i < job.WorkersActive.Length; i++)
        {
            if (job.WorkersActive[i] != null)
            {
                GameObject prefab = Instantiate(namesPrefab, residentsBar.transform.GetChild(0));
                prefab.GetComponent<TextMeshProUGUI>().text = job.WorkersActive[i].name;
            }
        }
    }

    private void ChangeRepairCost(int[] amounts)
    {
        //How this works is that we have all the materials listed under the repair bar, we go through the repair cost (build cost / 3) and change the numbers
        //accordingly, if its 0 we just turn it off, and because its a grid they get hidden

        for(int i = 0; i < costChecker.childCount; i++)
        {
            TextMeshProUGUI cost = costChecker.GetChild(i).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
            int amount = amounts[i] / 3;

            if (amount == 0) costChecker.GetChild(i).gameObject.SetActive(false);
            else cost.text = amount.ToString();
        }
    }

    private string FixName(string name)
    {
        int strSet = name.IndexOf("_");

        return name.Substring(0, strSet);
    }
}
