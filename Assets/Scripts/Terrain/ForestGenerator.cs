using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestGenerator : MonoBehaviour
{
    public Element[] elements;
    public Color[] colors;
    public Color[] colorsSnow;
    public Color[] colorsRock;
    public int treeDensity = 5;
    public int stumpDensity = 5;
    public int smallRockDensity = 5;
    public int deerDensity = 5;
    public int foxDensity = 5;
    public int bearDensity = 5;
    public int berryDensity = 5;
    public GameObject meshObject;
    public Shader shader;

    public void Generate()
    {
        Mesh mesh = meshObject.GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        Vector3[] normals = mesh.normals;

        //Trees
        GenerateObjects(vertices, normals, treeDensity, "Tree", 1.6f, 7f, 1.5f, 3f, -2f, 0.2f, false);
        GenerateObjects(vertices, normals, treeDensity, "SnowTree", 7f, 15f, 1.5f, 3f, -2f, 0.2f, false);
        GenerateObjects(vertices, normals, stumpDensity, "TreeStump", 1.6f, 25f, 1f, 2f, 2f, 0.2f, false);
        GenerateObjects(vertices, normals, stumpDensity, "FallenTree", 1.6f, 25f, 1f, 1.5f, 0.5f, 0.2f, false);

        //Rocks
        GenerateObjects(vertices, normals, treeDensity, "Rocks", 7f, 25f, 0.5f, 1f, 0.25f, 0.5f, false);
        GenerateObjects(vertices, normals, smallRockDensity, "RockSingle", 1.6f, 25f, 0.2f, 0.75f, 0.25f, 0.5f, false);

        //Flowers
        GenerateObjects(vertices, normals, stumpDensity, "Flowers", 1.6f, 7f, 1f, 1f, 1f, 0.2f, false);
        GenerateObjects(vertices, normals, stumpDensity, "Mushrooms", 1.6f, 7f, 2f, 3f, 0f, 0.1f, false);
        GenerateObjects(vertices, normals, stumpDensity, "Berries", 1.6f, 7f, 2f, 3f, 0f, 0.2f, true);

        //Animals
        GenerateAnimals(vertices, normals, foxDensity, "Fox", 1.6f, 7f, 1f, 3f, 0f, 0.2f);
        GenerateAnimals(vertices, normals, deerDensity, "Deer", 1.6f, 7f, 1f, 3f, 0f, 0.2f);
        GenerateAnimals(vertices, normals, bearDensity, "Bear", 1.6f, 7f, 1f, 3f, 0f, 0.2f);
    }

    public void GenerateObjects(Vector3[] vertices, Vector3[] normals, int ObjectDensity, string name, float minSpawnHeight, float maxSpawnHeight, float minScale, float maxScale, float heightOffset, float canvasSize, bool shouldTurn)
    {
        for (int i = 0; i < vertices.Length; i += 1)
        {
            int density = Random.Range(1, ObjectDensity);
            for (int i2 = 0; i2 < elements.Length; i2++)
            {
                Element element = elements[i2];
                Vector3 position = vertices[i] * meshObject.transform.localScale.y + new Vector3(0, heightOffset, 0);
                Vector3 offset = new Vector3(Random.Range(-0.75f, 0.75f), 0f, Random.Range(-0.75f, 0.75f));
                Vector3 rotation = new Vector3(Random.Range(0, 5f), Random.Range(0, 360f), Random.Range(0, 5f));
                Quaternion rotation2 = Quaternion.FromToRotation(Vector3.up, normals[i]);
                float num = Random.Range(minScale, maxScale);
                Vector3 localScale = new Vector3(num, num, num);

                if (element.CanPlace() && element.name == name)
                {
                    if (vertices[i].y >= minSpawnHeight && vertices[i].y <= maxSpawnHeight)
                    {
                        GameObject newElement = Instantiate(element.GetRandom());
                        newElement.transform.SetParent(element.parent);
                        newElement.transform.localScale = localScale;
                        newElement.transform.position = position + offset;
                        //newElement.transform.eulerAngles = rotation;
                        newElement.transform.rotation = rotation2;
                        if (shouldTurn)
                        {
                            newElement.transform.eulerAngles = new Vector3(
                                      newElement.transform.rotation.eulerAngles.y,
                                      Random.Range(0, 360f),
                                      newElement.transform.rotation.eulerAngles.y);
                        }
                        //newElement.transform.eulerAngles = new Vector3(newElement.transform.eulerAngles.x, Random.Range(0, 360f), newElement.transform.eulerAngles.z);
                        newElement.name = FixName(newElement.name);
                        try
                        {
                            newElement.transform.Find("Canvas").transform.localScale = new Vector3(canvasSize, canvasSize, canvasSize);
                        }
                        catch
                        {

                        }

                        //changeShaderRock(newElement);
                        if(name == "Tree")
                        {
                            changeShader(newElement);
                        }else if(name == "SnowTree")
                        {
                            changeShaderSnow(newElement);
                        }else if(name == "Rocks")
                        {
                            //changeShaderRock(newElement);
                        }

                        // Break out of this for loop to ensure we don't place another element at this position.
                        break;
                    }
                }
            }

            i += density;
        }
    }

    public void GenerateAnimals(Vector3[] vertices, Vector3[] normals, int ObjectDensity, string name, float minSpawnHeight, float maxSpawnHeight, float minScale, float maxScale, float heightOffset, float offset2)
    {
        for (int i = 0; i < vertices.Length; i += 1)
        {
            int density = Random.Range(1, ObjectDensity);
            for (int i2 = 0; i2 < elements.Length; i2++)
            {
                Element element = elements[i2];
                Vector3 position = vertices[i] * meshObject.transform.localScale.y + new Vector3(0, heightOffset, 0);

                if (element.CanPlace() && element.name == name)
                {
                    if (vertices[i].y >= minSpawnHeight && vertices[i].y <= maxSpawnHeight)
                    {
                        GameObject newElement = Instantiate(element.GetRandom(), position + new Vector3(0, 0, 0), Quaternion.identity);
                        newElement.transform.SetParent(element.parent);
                        //newElement.transform.position = position + new Vector3(0, 1, 0);
                        newElement.name = FixName(newElement.name);
                        try
                        {
                            newElement.transform.Find("Canvas").transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                        }
                        catch
                        {

                        }

                        // Break out of this for loop to ensure we don't place another element at this position.
                        break;
                    }
                }
            }

            i += density;
        }
    }

    public void changeShader(GameObject newElement)
    {
        int num = Random.Range(0, colors.Length);
        Color color = colors[num];

        for (int i = 0; i < newElement.transform.childCount; i++)
        {
            if (newElement.transform.GetChild(i).name.Contains("Leaves"))
            {
                Renderer rend = newElement.transform.GetChild(i).GetComponent<Renderer>();
                rend.material = new Material(shader);
                rend.material.SetColor("Color_8DC4F919", color);
                rend.material.SetFloat("Vector1_4FA07C5C", 20);
                rend.material.SetVector("Vector2_D588F14A", new Vector2(0.2f, 0.5f));
            }
        }
    }

    public void changeShaderSnow(GameObject newElement)
    {
        int num = Random.Range(0, colorsSnow.Length);
        Color color = colorsSnow[num];

        for (int i = 0; i < newElement.transform.childCount; i++)
        {
            if (newElement.transform.GetChild(i).name.Contains("Leaves"))
            {
                Renderer rend = newElement.transform.GetChild(i).GetComponent<Renderer>();
                rend.material = new Material(shader);
                rend.material.SetColor("Color_8DC4F919", color);
                rend.material.SetFloat("Vector1_4FA07C5C", 20);
                rend.material.SetVector("Vector2_D588F14A", new Vector2(0.2f, 0.5f));
            }
        }
    }

    public void changeShaderRock(GameObject newElement)
    {
        int num = Random.Range(0, colorsRock.Length);
        Color color = colorsRock[num];

        for (int i = 0; i < newElement.transform.childCount; i++)
        {
            if (newElement.transform.GetChild(i).name.Contains("Rock"))
            {
                Renderer rend = newElement.transform.GetChild(i).GetComponent<Renderer>();
                rend.material.color = color;
            }
        }
    }

    public string FixName(string name)
    {
        int strSet = name.IndexOf("(");

        return name.Substring(0,strSet);
    }

}

[System.Serializable]
public class Element
{

    public string name;
    [Range(1, 10)]
    public int density;
    public Transform parent;

    public GameObject[] prefabs;

    public bool CanPlace()
    {

        // Validation check to see if element can be placed. More detailed calculations can go here, such as checking perlin noise.

        if (Random.Range(0, 10) < density)
            return true;
        else
            return false;

    }

    public GameObject GetRandom()
    {

        // Return a random GameObject prefab from the prefabs array.

        return prefabs[Random.Range(0, prefabs.Length)];

    }

}