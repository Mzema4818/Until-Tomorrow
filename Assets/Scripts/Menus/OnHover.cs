using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnHover : MonoBehaviour
{
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Description;
    public TextMeshProUGUI Cost;
    public TextMeshProUGUI DescriptionCollection;
    public GameObject collectionMenu;
    public GameObject collectionObjectsMenu;
    public GameObject[] collectionObjects;

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
                if(go.gameObject.name == "Campfire")
                {
                    Name.text = go.gameObject.name;
                    Description.text = "A nice campfire to keep you warm";
                    Cost.text = "10 stone, 10 wood";
                }
                if (go.gameObject.name == "Tent")
                {
                    Name.text = go.gameObject.name;
                    Description.text = "A warm tent to keep you safe from the elements";
                    Cost.text = "4 stone, 15 wood";
                }
                if (go.gameObject.name == "Mine")
                {
                    Name.text = go.gameObject.name;
                    Description.text = "A small mine to start collecting stone";
                    Cost.text = "15 stone, 15 wood";
                }
                if (go.gameObject.name == "Lumbermill")
                {
                    Name.text = go.gameObject.name;
                    Description.text = "A small lumbermill to start collecting wood";
                    Cost.text = "15 stone, 15 wood";
                }
                if (go.gameObject.name == "Farm")
                {
                    Name.text = go.gameObject.name;
                    Description.text = "A small Farm to start collecting food";
                    Cost.text = "10 stone, 10 wood";
                }
                if (go.gameObject.name == "Wall")
                {
                    Name.text = go.gameObject.name;
                    Description.text = "A weak wall to keep out some of the elements";
                    Cost.text = "15 stone, 15 wood";
                }
                if (go.gameObject.name == "Door")
                {
                    Name.text = go.gameObject.name;
                    Description.text = "A door to let you enter your settlement";
                    Cost.text = "15 stone, 15 wood";
                }
                if (go.gameObject.name == "Opening Letter")
                {
                    DescriptionCollection.text = go.gameObject.name;
                    if (Input.GetMouseButtonDown(0))
                    {
                        collectionMenu.SetActive(false);
                        collectionObjectsMenu.SetActive(true);
                        collectionObjects[0].SetActive(true);
                    }
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
            }

            if(DescriptionCollection != null)
            {
                DescriptionCollection.text = "";
            }
        }
    }
}
