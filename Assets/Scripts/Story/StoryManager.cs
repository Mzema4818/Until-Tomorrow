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
    public TextMeshProUGUI Announcement;
    public GameObject hotbar;

    [Header("Story Moments")]
    public bool SpawnPrisonShips;
    public bool CheckSpawnShips;
    public int SpawnPrisonShipDay;
    public Transform prisonShipParent;
    public bool test;

    private string direction;

    // Update is called once per frame
    void Update()
    {
        //Checking to spawn prisonships
        if (!SpawnPrisonShips && CheckSpawnShips)
        {
            int num = 0;
            foreach(Transform child in hotbar.transform)
            {
                if (!child.gameObject.activeSelf)
                {
                    //print(child.name);
                    num++;
                }
            }

            if(num == 4)
            {
                SpawnPrisonShips = true;
                SpawnPrisonShipDay = lightingManager.numberOfDays + 1;
            }

            CheckSpawnShips = false;
        }

        //Spawning Prison Ship
        if (SpawnPrisonShips)
        {
            if (lightingManager.numberOfDays >= SpawnPrisonShipDay && lightingManager.TimeOfDay > 10)
            {
                SpawnPrisonShip();
                SpawnPrisonShipDay += 2;
            }
        }

        if (test)
        {
            int[] coords = GetShipCords();
            GameObject ship = Instantiate(MerchantShip, new Vector3(coords[0], 10, coords[1]), Quaternion.identity);
            ship.transform.parent = prisonShipParent;
            ship.name = FixName(ship.name);

            Announcement.gameObject.SetActive(true);
            Announcement.text = "A ship is apporching from the " + direction;
            test = false;
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

        //int num = Random.Range(1, 99);

        //if (Random.Range(0, 2) == 1) num += -600;
        //else num += 500;

        //returnValues[0] = num;
        //returnValues[1] = Random.Range(-500, 500);

        int num = Random.Range(0, 4);
        //4 conditions

        //conditon 1
        if(num == 0)
        {
            direction = "South";
            returnValues[0] = Random.Range(-600, 600);
            returnValues[1] = -600;
        }

        //conditon 2
        if (num == 1)
        {
            direction = "East";
            returnValues[0] = -600;
            returnValues[1] = Random.Range(-600, 600);
        }

        //conditon 3
        if(num == 2)
        {
            direction = "North";
            returnValues[0] = Random.Range(-600, 600);
            returnValues[1] = 600;
        }

        //conditon 4
        if(num == 3)
        {
            direction = "West";
            returnValues[0] = 600;
            returnValues[1] = Random.Range(-600, 600);
        }

        return returnValues;
    }

    private string FixName(string name)
    {
        int strSet = name.IndexOf("(");

        return name.Substring(0, strSet);
    }
}
