using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnHover : MonoBehaviour
{
    public Builder builder;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Description;
    public TextMeshProUGUI Cost;
    public TextMeshProUGUI DescriptionCollection;
    public GameObject collectionMenu;
    public GameObject collectionObjectsMenu;
    public GameObject[] collectionObjects;
    public Transform costChecker;

    private void OnEnable()
    {
        if (name != null && Description != null && Cost != null)
        {
            Name.text = "";
            Description.text = "";
            Cost.text = "";
        }

        if (DescriptionCollection != null)
        {
            DescriptionCollection.text = "";
        }
    }

    private void Update()
    {
        PointerEventData pointer = new PointerEventData(EventSystem.current);
        pointer.position = Input.mousePosition;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, raycastResults);

        if (raycastResults.Count > 0)
        {
            foreach (var go in raycastResults)
            {
                switch (go.gameObject.name)
                {
                    case "Campfire":
                        Name.text = go.gameObject.name;
                        Description.text = "A nice campfire to keep you warm";
                        Cost.text = "10 stone, 10 wood";
                        ChangeCosts(builder.campfireCost);
                        break;
                    case "Tent":
                        Name.text = go.gameObject.name;
                        Description.text = "A warm tent to keep you safe from the elements";
                        Cost.text = "4 stone, 15 wood";
                        ChangeCosts(builder.tentCost);
                        break;
                    case "Mine":
                        Name.text = go.gameObject.name;
                        Description.text = "A small mine to start collecting stone";
                        Cost.text = "15 stone, 15 wood";
                        ChangeCosts(builder.mineCost);
                        break;
                    case "Lumbermill":
                        Name.text = go.gameObject.name;
                        Description.text = "A small lumbermill to start collecting wood";
                        Cost.text = "15 stone, 15 wood";
                        ChangeCosts(builder.lumberMillCost);
                        break;
                    case "Farm":
                        Name.text = go.gameObject.name;
                        Description.text = "A small Farm to start collecting food";
                        Cost.text = "10 stone, 10 wood";
                        ChangeCosts(builder.farmCost);
                        break;
                    case "Wall":
                        Name.text = go.gameObject.name;
                        Description.text = "A weak wall to keep out some of the elements";
                        Cost.text = "15 stone, 15 wood";
                        ChangeCosts(builder.wallCost);
                        break;
                    case "Door":
                        Name.text = go.gameObject.name;
                        Description.text = "A door to let you enter your settlement";
                        Cost.text = "15 stone, 15 wood";
                        ChangeCosts(builder.doorCost);
                        break;
                    case "Chest":
                        Name.text = go.gameObject.name;
                        Description.text = "To hold your spare items";
                        Cost.text = "15 stone, 15 wood";
                        ChangeCosts(builder.chestCost);
                        break;
                    case "Messhall":
                        Name.text = go.gameObject.name;
                        Description.text = "People gotta eat";
                        Cost.text = "15 stone, 15 wood";
                        ChangeCosts(builder.messhallCost);
                        break;
                    case "Tavern":
                        Name.text = go.gameObject.name;
                        Description.text = "Gotta have a way to make food";
                        Cost.text = "15 stone, 15 wood";
                        ChangeCosts(builder.tavernCost);
                        break;
                    case "Tower":
                        Name.text = go.gameObject.name;
                        Description.text = "The finest of snipers";
                        Cost.text = "15 stone, 15 wood";
                        ChangeCosts(builder.towerCost);
                        break;
                    case "KnightHut":
                        Name.text = go.gameObject.name;
                        Description.text = "The finest of knights";
                        Cost.text = "15 stone, 15 wood";
                        ChangeCosts(builder.knightHutCost);
                        break;
                    case "Opening Letter":
                        DescriptionCollection.text = go.gameObject.name;
                        if (Input.GetMouseButtonDown(0))
                        {
                            collectionMenu.SetActive(false);
                            collectionObjectsMenu.SetActive(true);
                            collectionObjects[0].SetActive(true);
                        }
                        break;
                }
            }
        }


        if (raycastResults.Count == 0)
        {
            if(name != null && Description != null && Cost != null)
            {
                Name.text = "";
                Description.text = "";
                Cost.text = "";
                RemoveCosts();
            }

            if(DescriptionCollection != null)
            {
                DescriptionCollection.text = "";
            }
        }
    }

    //The costChecker must line up with the cost[i] so cost[0] is wood and cost[1] is stone, then the first child of costchecker must be wood and second must be stone
    private void ChangeCosts(int[] cost)
    {
        for(int i = 0; i < cost.Length; i++)
        {
            GameObject child = costChecker.GetChild(i).gameObject;
            if (cost[i] == 0) child.SetActive(false);
            else child.SetActive(true);

            GameObject text = child.transform.GetChild(0).GetChild(0).gameObject;
            text.GetComponent<TextMeshProUGUI>().text = cost[i].ToString();
        }
    }

    private void RemoveCosts()
    {
        for (int i = 0; i < costChecker.childCount ; i++)
        {
            costChecker.GetChild(i).gameObject.SetActive(false);
        }
    }
}
