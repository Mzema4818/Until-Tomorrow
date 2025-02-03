using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
using static UnityEngine.GraphicsBuffer;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject meshObject;
    public float minSpawnHeight = 1.6f;
    public float edgeThreshold = 10.0f;
    public List<Vector3> spawnLocations = new List<Vector3>();

    public LightingManager lightingManager;
    public TextMeshProUGUI notifiation;
    public Transform enemyParent;
    public GameObject[] EnemyPrefabs;
    public GameObject townhall;

    public bool spawn = false;

    private void Update()
    {
        if (spawn)
        {
            SpawnEnemy(EnemyPrefabs[0]);
            spawn = false;
        }
    }

    public void SpawnEnemyWave()
    {
        if (townhall == null) return;

        int spawnMin = lightingManager.numberOfDays <= 7
                ? 5
                : 5 + (int)Math.Log(lightingManager.numberOfDays - 6);


        int num = UnityEngine.Random.Range(spawnMin, 10);
        for(int i = 0; i < num; i++)
        {
            SpawnEnemy(EnemyPrefabs[0]);
        }

        notifiation.gameObject.SetActive(true);
    }

    public void SpawnEnemy(GameObject enemyPrefab)
    {
        GameObject enemy = Instantiate(enemyPrefab, spawnLocations[UnityEngine.Random.Range(0, spawnLocations.Count)], Quaternion.identity, enemyParent);
        //enemy.transform.LookAt(townhall.transform); //test
        //enemy.GetComponent<Enemy>().townhall = townhall;
    }

    public void GetLocations()
    {
        Mesh mesh = meshObject.GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;

        // Calculate the bounds of the map
        float minX = -200f;
        float maxX = 200f;
        float minZ = -200f;
        float maxZ = 200f;

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 position = vertices[i];
            position = meshObject.transform.TransformPoint(position); // Transform to world coordinates

            if (position.y > minSpawnHeight && IsNearEdge(position, minX, maxX, minZ, maxZ, edgeThreshold))
            {
                spawnLocations.Add(position);
            }
        }
    }

    private bool IsNearEdge(Vector3 position, float minX, float maxX, float minZ, float maxZ, float threshold)
    {
        // Check if the position is within the edge threshold of the map boundaries
        return (Mathf.Abs(position.x - minX) <= threshold || Mathf.Abs(position.x - maxX) <= threshold ||
                Mathf.Abs(position.z - minZ) <= threshold || Mathf.Abs(position.z - maxZ) <= threshold);
    }
}
