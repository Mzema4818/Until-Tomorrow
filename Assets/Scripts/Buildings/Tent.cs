using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tent : MonoBehaviour
{
    public int MaxNumberOfWorkers;
    public int MaxResidents = 0;
    public int Residents;
    public GameObject[] ResidentsActive;
    public LightingManager time;
    public GameObject heartParticles;
    public Transform tempParent;

    public Vector3 originalPosition;

    public bool enableResidents;

    // Start is called before the first frame update
    private void Awake()
    {
        if (name.Contains("Level0")) MaxResidents = MaxNumberOfWorkers;
        ResidentsActive = new GameObject[MaxResidents];

        originalPosition = transform.position;
    }

    private void Start()
    {
        InvokeRepeating("HealResidents", 0.0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        //if (!time.sleepTime && enableResidents) WakeUpResidents();
        //WakeUpSleepingResidents();

        if (transform.position != originalPosition) { WakeUpResidents(); originalPosition = transform.position; }
    }

    private void OnDestroy()
    {
        try
        {
            WakeUpResidents();
            for (int i = 0; i < ResidentsActive.Length; i++)
            {
                ResidentsActive[i].GetComponent<ResidentScheudle>().home = null;
                ResidentsActive[i].GetComponent<StatBar>().UpdateHome();
                //ResidentsActive[i].GetComponent<ResidentWander>().ResetSchedule();
                //ResidentsActive[i].GetComponent<ResidentConditions>().hasHome = false;
            }
        }
        catch { };
    }

    //for "firing"
    public void RemoveResident(int residentNum)
    {
        //removing TurnOnGameObjects and sleeping
        var scripts = ResidentsActive[residentNum].transform.parent.GetComponents<TurnOnGameObjects>();
        foreach (var s in scripts)
        {
            if (s.resident == ResidentsActive[residentNum])
            {
                Destroy(s);
                break;
            }
        }
        if (ResidentsActive[residentNum].TryGetComponent<Sleeping>(out var sleeping)) Destroy(sleeping);

        ResidentsActive[residentNum].GetComponent<ResidentScheudle>().home = null;
        ResidentsActive[residentNum].GetComponent<StatBar>().UpdateHome();
        ResidentsActive[residentNum].GetComponent<ResidentScheudle>().AtLocation = false;
        ResidentsActive[residentNum].SetActive(true);

        Residents--;
        ResidentsActive[residentNum] = null;
        ResidentsActive = ReorganizeArray();
    }

    //for when resident dies
    public void RemoveResident(GameObject gameobject)
    {
        var scripts = gameobject.transform.parent.GetComponents<TurnOnGameObjects>();
        foreach (var s in scripts)
        {
            if (s.resident == gameobject)
            {
                Destroy(s);
                break;
            }
        }

        int num = FindMatchIndex(ResidentsActive, gameobject);

        Residents--;
        ResidentsActive[num] = null;
        ResidentsActive = ReorganizeArray();
    }

    int FindMatchIndex(GameObject[] list, GameObject target)
    {
        for (int i = 0; i < list.Length; i++)
        {
            if (list[i] == target)
            {
                return i;
            }
        }
        return -1;
    }

    private void HealResidents()
    {
        if (ResidentsActive[0] == null) return;

        foreach (GameObject resident in ResidentsActive)
        {
            if(resident == null) continue;
            //ResidentScheudle residentScheudle = resident.GetComponent<ResidentScheudle>();
            ResidentHealth residentHealth = resident.GetComponent<ResidentHealth>();
            Sleeping sleeping = resident.GetComponent<Sleeping>();

            //if the resident is sleeping and has a health bar, heal health
            if (sleeping != null && residentHealth != null && residentHealth.currentHealth != residentHealth.maxHealth)
            {
                residentHealth.ModifyHealth(10, null);

                //adding heart particles
                GameObject heart = Instantiate(heartParticles, transform.position, heartParticles.transform.rotation);
                heart.transform.parent = tempParent;
                Destroy(heart, 5);
            }
        }
    }

    private void WakeUpResidents()
    {
        foreach (GameObject resident in ResidentsActive)
        {
            if (resident != null) resident.SetActive(true);
        }
        enableResidents = false;
    }

    private GameObject[] ReorganizeArray()
    {
        //when deleting a resident from an array theres an empty space there which messes things up, this just gets rid of that space
        GameObject[] returnArray = new GameObject[MaxResidents];

        int index = 0;
        for (int i = 0; i < returnArray.Length; i++)
        {
            if (ResidentsActive[i] != null)
            {
                returnArray[index] = ResidentsActive[i];
                index++;
            }
        }

        return returnArray;
    }
}
