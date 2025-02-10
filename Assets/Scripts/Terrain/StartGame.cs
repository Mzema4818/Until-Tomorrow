using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;

public class StartGame : MonoBehaviour
{
    private MapGenerator mapGenerator;
    public NoiseData noiseData;
    public ForestGenerator forestGenerator;
    public PlayerController playerController;
    public PlayInventory playInventory;
    public Crosshair crosshair;
    public PlayerAttack playerAttack;
    public GetData getData;
    public BuildMenuUpdater buildMenuUpdater;
    public LightingManager lightingManager;
    public CollectableRandomizer collectableRandomizer;
    public CollectableRandomizer collectableRandomizer2;
    public NavMeshSurface navMeshSurface;
    public Builder builder;
    public TextBoxChoices choices;
    public GameObject HotBar;
    public GameObject CharacterStuff;
    public GameObject ToolBar;
    public GameObject TerrainObjects;
    public GameObject buildingsParent;
    public GameObject residentParent;
    public InventoryManager inventory;

    public GameObject townHallParent;
    public SpawnEnemies spawnEnemies;

    public GameObject player;
    public GameObject mainMenu;

    public TextMeshProUGUI dayUpdater;
    public GameObject terrain;
    public GameObject grass;
    private Mesh grassMesh;
    private Mesh mesh;
    private Vector3[] vertices;

    [Header("Sounds")]
    public AudioSource mainMenuMusic;
    [Header("Ambient")]
    public AudioSource noise1;
    public AudioSource noise2;
    public AudioSource noise3;

    [Header("Continue Game Check")]
    public GameObject continueGame;
    public bool wasContinueOn;

    void Awake()
    {
        mapGenerator = (MapGenerator)FindObjectOfType(typeof(MapGenerator));
        //Time.timeScale = 0;
        lightingManager.multiplier = 1;

        StartMusic(false);
    }

    private void Start()
    {
        CharacterStuff.SetActive(false);
    }

    public void EndGame()
    {
        Time.timeScale = 1;
        player.transform.position = new Vector3(0, 200, 0); //to turn off ocean sounds if you are close
        mainMenuMusic.Play();
        StartMusic(false);

        ClearGameObjects();
        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponent<PlayerController>().shouldMove = false;
        StartInventory();
        lightingManager.multiplier = 1;
        lightingManager.TimeOfDay = 11;
        lightingManager.numberOfDays = 0;
        lightingManager.countDaysStart = false;
        lightingManager.ResetSchedule();

        inventory.WipeInventory();
        inventory.StartOnItem();
        playerAttack.shouldAttack = false;

    }

    public void GenerateNewSeed()
    {
        mainMenuMusic.Stop();
        StartMusic(true);

        ClearGameObjects();
        player.GetComponent<PlayerController>().enabled = true;
        player.GetComponent<PlayerController>().shouldMove = true;
        StartInventory();
        lightingManager.multiplier = 30;
        lightingManager.TimeOfDay = 10;
        lightingManager.numberOfDays = 0;
        lightingManager.countDaysStart = true;
        lightingManager.ResetSchedule();
        StartGameMenu();
        noiseData.setNewSeed();
        mapGenerator.DrawMapInEditor();
        updateVertices();
        forestGenerator.Generate();
        turnOnPlayer();
        randomizePlayer();
        collectableRandomizer.Randomize();
        collectableRandomizer2.Randomize();
        DayStuff(0);
        buildMenuUpdater.AccessToLevel0Buildings = true;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        getData.SaveData();

        //inventory.StartOnSelectedItem();
        inventory.WipeInventory();
        inventory.StartOnItem();
        playerAttack.shouldAttack = true;

        navMeshSurface.BuildNavMesh();
        spawnEnemies.GetLocations();
    }

    public void GenerateLoadSeed()
    {
        mainMenuMusic.Stop();
        StartMusic(true);

        player.GetComponent<PlayerController>().enabled = true;
        player.GetComponent<PlayerController>().shouldMove = true;
        StartInventory();
        lightingManager.multiplier = 30;
        lightingManager.countDaysStart = true;
        lightingManager.ResetSchedule();
        StartGameMenu();
        mapGenerator.DrawMapInEditor();
        updateVertices();
        turnOnPlayer();
        DayStuff(transform.GetComponent<GetData>().GetNumberOfDays());
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;

        navMeshSurface.BuildNavMesh();
        spawnEnemies.GetLocations();

        //inventory.StartOnSelectedItem();
        inventory.StartOnItem();
        playerAttack.shouldAttack = true;

        if (townHallParent.transform.childCount != 0)
        {
            spawnEnemies.townhall = townHallParent.transform.GetChild(0).gameObject;
        }
    }

    public void updateVertices()
    {
        mesh = terrain.GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;

        MeshCollider mc = terrain.GetComponent<MeshCollider>();
        if (mc == null)
            mc = (MeshCollider)terrain.AddComponent(typeof(MeshCollider));
        else
        {
            mc.sharedMesh = null;
            mc.sharedMesh = mesh;
        }
        grass.transform.localScale = terrain.transform.localScale;
        grass.GetComponent<MeshFilter>();
        grassMesh = grass.GetComponent<MeshFilter>().mesh;
        grassMesh.vertices = terrain.GetComponent<MeshFilter>().mesh.vertices;
        grassMesh.RecalculateBounds();
    }

    public void turnOnPlayer()
    {
        playerController.shouldMove = true;
    }

    private void StartGameMenu()
    {
        player.SetActive(true);
        mainMenu.SetActive(false);
    }

    private void randomizePlayer()
    {
        Mesh mesh = terrain.GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;

        List<Vector3> newVertices = new List<Vector3>();

        for (int i = 0; i < vertices.Length; i += 1)
        {
            if (vertices[i].y >= 1.6)
            {
                newVertices.Add(vertices[i]);
            }
        }

        int randValue = Random.Range(0, newVertices.Count);
        player.transform.position = newVertices[randValue] * terrain.transform.localScale.y;
        playerController.respawnPoint = newVertices[randValue] * terrain.transform.localScale.y; 
    }

    public void StartInventory()
    {
        //getData.inventory = playInventory.ReturnInventory();
        //crosshair.inventory = playInventory.ReturnInventory();
        //builder.inventory = playInventory.ReturnInventory();
        //choices.inventory = playInventory.ReturnInventory();
    }

    public void DayStuff(int number)
    {
        dayUpdater.gameObject.SetActive(true);
        dayUpdater.text = "Day " + number;
    }

    public void ClearGameObjects()
    {
        TurnOnGameObjects[] turnOnGameObjects = residentParent.GetComponents<TurnOnGameObjects>();
        foreach (TurnOnGameObjects component in turnOnGameObjects) Destroy(component);

        for (int i = 0; i < TerrainObjects.transform.childCount - 2; i++)
        {
            foreach (Transform child in TerrainObjects.transform.GetChild(i))
            {
                Destroy(child.gameObject);
            }
        }

        foreach (Transform gameobject in buildingsParent.transform)
        {
            foreach (Transform child in gameobject)
            {
                Destroy(child.gameObject);
            }
        }

        //TurnOnGameObjects[] turnOnGameObjects = gameObject.GetComponents(typeof(TurnOnGameObjects));
    }

    public void WipeResidentParent()
    {
        TurnOnGameObjects[] turnOnGameObjects = residentParent.GetComponents<TurnOnGameObjects>();
        foreach (TurnOnGameObjects component in turnOnGameObjects) Destroy(component);
    }

    private void StartMusic(bool start)
    {
        if (start)
        {
            noise1.Play();
            noise2.Play();
            noise3.Play();
        }
        else
        {
            noise1.Stop();
            noise2.Stop();
            noise3.Stop();
        }
    }

    public void WipeInventory()
    {
        inventory.WipeInventory();
    }

    public void inventoryTest()
    {
        inventory.StartOnItem();
    }

    public void checkContinueGame()
    {
        wasContinueOn = continueGame.activeSelf;
    }

    public void turnOnOrOffContinueGame()
    {
        continueGame.SetActive(wasContinueOn);
    }
}
