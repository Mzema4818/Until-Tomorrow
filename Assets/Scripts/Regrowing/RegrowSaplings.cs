using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegrowSaplings : MonoBehaviour
{
    public ForestGenerator forestGenerator;
    public float regrowTime = 2000f;
    public GameObject[] trees;
    public GameObject parent;
    public Color[] colors;
    public Color[] colorsSnow;
    public Shader shader;

    private bool treeSwitch;


    void Start()
    {
        InvokeRepeating("Grow", regrowTime, regrowTime);
    }

    // Update is called once per frame
    void Update()
    {
        trees = forestGenerator.elements[0].prefabs;
        colors = forestGenerator.colors;
        colorsSnow = forestGenerator.colorsSnow;
        shader = forestGenerator.shader;
    }

    private void Grow()
    {
        int treeNum;
        GameObject newElement;

        treeNum = Random.Range(0, trees.Length);

        if (transform.position.y >= 7)
        {
            treeSwitch = true;
        }
        else
        {
            treeSwitch = false;
        }

        float num = Random.Range(1.5f, 3.0f);
        Vector3 localScale = new Vector3(num, num, num);

        newElement = Instantiate(trees[treeNum]);
        newElement.transform.position = transform.position + new Vector3(0, -3, 0);
        newElement.transform.eulerAngles = transform.eulerAngles;
        newElement.transform.SetParent(parent.transform);
        newElement.transform.localScale = localScale;
        newElement.name = FixName(newElement.name);
        try
        {
            newElement.transform.Find("Canvas").transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        }
        catch
        {

        }

        //changeShaderRock(newElement);
        if (!treeSwitch)
        {
            changeShader(newElement);
        }
        else if (treeSwitch)
        {
            changeShaderSnow(newElement);
        }

        Destroy(gameObject);
    }

    public string FixName(string name)
    {
        int strSet = name.IndexOf("(");

        return name.Substring(0, strSet);
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
}
