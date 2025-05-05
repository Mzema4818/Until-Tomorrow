using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    public LightingManager lightingManager;

    [Header("Prefabs")]
    public GameObject MerchantShip;

    [Header("Game Objects Needed")]
    public Transform prisonShipParent;
    public SpawnEnemies spawnEnemies;
    public TextMeshProUGUI Announcement;
    public TextMeshProUGUI Error;
    public GameObject hotbar;
    private string direction;

    [Header("Story Moments")]
    public bool CanSpawnShips;
    public int SpawnShipDay;
    public int SpawnEnemyDay;

    [Header("Story Conditions")]
    public int toolsCollected;

    [Header("Bools per day")]
    private bool spawnedShip;
    private bool spawnedEnemy;

    [Header("Tests")]
    public bool prisonShipTest;
    public bool waveTest;

    // Update is called once per frame
    void Update()
    {
        //Spawn ships
        if(CanSpawnShips && lightingManager.numberOfDays >= SpawnShipDay && lightingManager.IsTimeApproximately(10) && !spawnedShip)
        {
            SpawnPrisonShip();
            SpawnShipDay += 2;
            spawnedShip = true;
        }
        else if (!lightingManager.IsTimeApproximately(10)) spawnedShip = false;

        //Spawn enemies;
        if (lightingManager.numberOfDays >= SpawnEnemyDay && lightingManager.IsTimeApproximately(18) && !spawnedEnemy){
            spawnEnemies.SpawnEnemyWave();
            SpawnEnemyDay += 2;
            spawnedEnemy = true;
        }
        else if (!lightingManager.IsTimeApproximately(18)) spawnedEnemy = false;

        if (prisonShipTest)
        {
            SpawnPrisonShip();
            prisonShipTest = false;
        }

        if (waveTest)
        {
            spawnEnemies.SpawnEnemyWave();
            waveTest = false;
        }
    }

    private void SpawnPrisonShip()
    {
        int[] coords = GetShipCords();
        GameObject ship = Instantiate(MerchantShip, new Vector3(coords[0], 10, coords[1]), Quaternion.identity);
        ship.transform.parent = prisonShipParent;
        ship.name = FixName(ship.name);

        Announcement.gameObject.SetActive(true);
        Announcement.text = "A ship is apporching from the " + direction;
    }

    private int[] GetShipCords()
    {
        int[] returnValues = new int[2];
        int num = Random.Range(0, 4); //Pick a direction: 0 = South, 1 = East, 2 = North, 3 = West

        if (num == 0) // South edge
        {
            direction = "South";
            returnValues[0] = Random.Range(-600, 601); // x
            returnValues[1] = -600;                    // y
        }
        else if (num == 1) // East edge
        {
            direction = "East";
            returnValues[0] = 600;                     // x
            returnValues[1] = Random.Range(-600, 601); // y
        }
        else if (num == 2) // North edge
        {
            direction = "North";
            returnValues[0] = Random.Range(-600, 601); // x
            returnValues[1] = 600;                     // y
        }
        else if (num == 3) // West edge
        {
            direction = "West";
            returnValues[0] = -600;                    // x
            returnValues[1] = Random.Range(-600, 601); // y
        }

        return returnValues;
    }

    private string FixName(string name)
    {
        int strSet = name.IndexOf("(");

        return name.Substring(0, strSet);
    }
}
