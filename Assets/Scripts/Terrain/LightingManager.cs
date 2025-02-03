using System.Collections;
using TMPro;
using UnityEngine;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    //Scene References
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private LightingPreset Preset;
    //Variables
    [SerializeField, Range(0, 24)] public float TimeOfDay;

    public bool countDaysStart;
    public int numberOfDays;
    public bool countDays;
    public bool startSun;
    public float multiplier = 30;
    public TextMeshProUGUI dayUpdater;
    public UnderWater underWater;

    [Header("Resident Schedule")]
    public bool sleepTime;
    public bool workTime;
    public bool wanderTime;

    public bool[] Hours = new bool[24];

    [Header("Story")]
    public StoryManager storyManager;

    private void Start()
    {
        countDays = true;
        startSun = true;
        countDaysStart = false;

        sleepTime = false;
        workTime = false;
        wanderTime = false;
    }

    private void Update()
    {
        if (startSun)
        {
            if (Preset == null)
                return;

            if (Application.isPlaying)
            {
                //(Replace with a reference to the game time)
                TimeOfDay += Time.deltaTime / multiplier; //Number 2 here makes day twice as long.  Without 2 its 24 second days.  With 2 its 48 second days ||| number 30 makes 12 minute days


                if (TimeOfDay >= 23.99 && countDays && countDaysStart)
                {
                    numberOfDays++;
                    dayUpdater.gameObject.SetActive(true);
                    dayUpdater.text = "Day " + numberOfDays;
                    countDays = false;
                }

                if (TimeOfDay <= 0.5 && !countDays)
                {
                    countDays = true;
                }


                SetResidentSchedules();
                if ((TimeOfDay >= 20 || TimeOfDay < 6) && !sleepTime) //sleepy time
                {
                    sleepTime = true;
                    wanderTime = false;
                    workTime = false;

                }

                if (TimeOfDay >= 6 && TimeOfDay < 8 && !wanderTime) //wandering time (morning)
                {
                    sleepTime = false;
                    wanderTime = true;
                    workTime = false;
                }

                if (TimeOfDay >= 8 && TimeOfDay < 18 && !workTime) //work time
                {
                    sleepTime = false;
                    wanderTime = false;
                    workTime = true;
                }

                if (TimeOfDay >= 18 && TimeOfDay < 20 && !wanderTime) //wanderirng time (night)
                {
                    if(storyManager.ShouldSpawnEnemies) storyManager.DoSpawnEnemies = true;
                    sleepTime = false;
                    wanderTime = true;
                    workTime = false;
                }


                TimeOfDay %= 24; //Modulus to ensure always between 0-24
                UpdateLighting(TimeOfDay / 24f);
            }
            else
            {
                UpdateLighting(TimeOfDay / 24f);
            }
        }
    }

    private void UpdateLighting(float timePercent)
    {
        //Set ambient and fog
        RenderSettings.ambientLight = Preset.AmbientColor.Evaluate(timePercent);

        if (!underWater.isUnderwater)
        {
            RenderSettings.fogColor = Preset.FogColor.Evaluate(timePercent);
        }

        //If the directional light is set then rotate and set it's color, I actually rarely use the rotation because it casts tall shadows unless you clamp the value
        if (DirectionalLight != null)
        {
            DirectionalLight.color = Preset.DirectionalColor.Evaluate(timePercent);

            DirectionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
        }

    }

    //Try to find a directional light to use if we haven't set one
    private void OnValidate()
    {
        if (DirectionalLight != null)
            return;

        //Search for lighting tab sun
        if (RenderSettings.sun != null)
        {
            DirectionalLight = RenderSettings.sun;
        }
        //Search scene for light that fits criteria (directional)
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    DirectionalLight = light;
                    return;
                }
            }
        }
    }

    public void ResetSchedule()
    {
        for(int i = 0; i < Hours.Length; i++)
        {
            Hours[i] = false;
        }
    }

    public int WhatTimeIsIt()
    {
        for (int i = 0; i < Hours.Length; i++)
        {
            if (Hours[i]) return i;
        }

        return -1;
    }

    private void SetResidentSchedules()
    {
        if (TimeOfDay >= 0 && TimeOfDay < 1) { Hours[0] = true; Hours[23] = false; }
        else if (TimeOfDay >= 1 && TimeOfDay < 2) { Hours[1] = true; Hours[0] = false; }
        else if (TimeOfDay >= 2 && TimeOfDay < 3) { Hours[2] = true; Hours[1] = false; }
        else if (TimeOfDay >= 3 && TimeOfDay < 4) { Hours[3] = true; Hours[2] = false; }
        else if (TimeOfDay >= 4 && TimeOfDay < 5) { Hours[4] = true; Hours[3] = false; }
        else if (TimeOfDay >= 5 && TimeOfDay < 6) { Hours[5] = true; Hours[4] = false; }
        else if (TimeOfDay >= 6 && TimeOfDay < 7) { Hours[6] = true; Hours[5] = false; }
        else if (TimeOfDay >= 7 && TimeOfDay < 8) { Hours[7] = true; Hours[6] = false; }
        else if (TimeOfDay >= 8 && TimeOfDay < 9) { Hours[8] = true; Hours[7] = false; }
        else if (TimeOfDay >= 9 && TimeOfDay < 10) { Hours[9] = true; Hours[8] = false; }
        else if (TimeOfDay >= 10 && TimeOfDay < 11) { Hours[10] = true; Hours[9] = false; }
        else if (TimeOfDay >= 11 && TimeOfDay < 12) { Hours[11] = true; Hours[10] = false; }
        else if (TimeOfDay >= 12 && TimeOfDay < 13) { Hours[12] = true; Hours[11] = false; }
        else if (TimeOfDay >= 13 && TimeOfDay < 14) { Hours[13] = true; Hours[12] = false; }
        else if (TimeOfDay >= 14 && TimeOfDay < 15) { Hours[14] = true; Hours[13] = false; }
        else if (TimeOfDay >= 15 && TimeOfDay < 16) { Hours[15] = true; Hours[14] = false; }
        else if (TimeOfDay >= 16 && TimeOfDay < 17) { Hours[16] = true; Hours[15] = false; }
        else if (TimeOfDay >= 17 && TimeOfDay < 18) { Hours[17] = true; Hours[16] = false; }
        else if (TimeOfDay >= 18 && TimeOfDay < 19) { Hours[18] = true; Hours[17] = false; }
        else if (TimeOfDay >= 19 && TimeOfDay < 20) { Hours[19] = true; Hours[18] = false; }
        else if (TimeOfDay >= 20 && TimeOfDay < 21) { Hours[20] = true; Hours[19] = false; }
        else if (TimeOfDay >= 21 && TimeOfDay < 22) { Hours[21] = true; Hours[20] = false; }
        else if (TimeOfDay >= 22 && TimeOfDay < 23) { Hours[22] = true; Hours[21] = false; }
        else if (TimeOfDay >= 23 && TimeOfDay < 24) { Hours[23] = true; Hours[22] = false; }


        //trying to automatic above, it doesnt work, almost does.  to tired rn.
        /*for(int i = 0; i < 24; i++)
        {
            int time = i;
            int timePlus1 = i + 1;
            int timeMinus1 = i - 1;
            if (timeMinus1 == -1) timeMinus1 = 23;

            if (TimeOfDay >= time && TimeOfDay < timePlus1) { 
                    Hours[timePlus1] = true; 
                    Hours[timeMinus1] = false; 
             }
        }*/
    }
}