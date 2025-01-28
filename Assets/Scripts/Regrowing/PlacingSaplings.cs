using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacingSaplings : MonoBehaviour
{
    public float distance = 50;

    public Inventory inventory;
    public Item item;

    [SerializeField]
    private GameObject placeableObjectPrefab;

    [SerializeField]
    private GameObject parent;


    private GameObject currentPlaceableObject;

    private float mouseWheelRotation;
    private bool start = true;

    private void Update()
    {
        HandleNewObjectHotkey(ref start);

        if (currentPlaceableObject != null)
        {
            MoveCurrentObjectToMouse();
            RotateFromMouseWheel();
            ReleaseIfClicked();
        }
    }

    private void HandleNewObjectHotkey(ref bool start)
    {
        if (start)
        {
            if (currentPlaceableObject != null)
            {
                Destroy(currentPlaceableObject);
            }
            else
            {
                currentPlaceableObject = Instantiate(placeableObjectPrefab);
                currentPlaceableObject.AddComponent<ColorChange>();
            }
        }
        start = false;
    }
    private void MoveCurrentObjectToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, distance, 1 << LayerMask.NameToLayer("Ground")))
        {
            if (hitInfo.transform.name == "Terrain")
            {
                currentPlaceableObject.transform.position = hitInfo.point;
                currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
            }
            //currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
        }
    }

    private void RotateFromMouseWheel()
    {
        mouseWheelRotation += Input.mouseScrollDelta.y;
        currentPlaceableObject.transform.Rotate(Vector3.up, mouseWheelRotation * 10f);
    }

    private void ReleaseIfClicked()
    {
        if (Input.GetMouseButtonDown(0) && !currentPlaceableObject.GetComponent<ColorChange>().touching)
        {
            //currentPlaceableObject.AddComponent<PickUpPopUp>();
            currentPlaceableObject.AddComponent<PickUp>();
            currentPlaceableObject.AddComponent<RegrowSaplings>();
            Destroy(currentPlaceableObject.GetComponent<ColorChange>());
            Destroy(currentPlaceableObject.GetComponent<BoxCollider>());
            currentPlaceableObject.transform.Find("Canvas").gameObject.SetActive(true);
            try { currentPlaceableObject.transform.name = FixName(currentPlaceableObject.transform.name); } catch { }
            currentPlaceableObject.transform.parent = parent.transform;
            currentPlaceableObject = null;
            //inventory.RemoveItem(new Item { itemType = item.itemType, amount = 1 });
            if (inventory.CheckIfEmpty(item))
            {
                gameObject.SetActive(false);
            }
            else
            {
                start = true;
            }
        }
    }

    private void OnDisable()
    {
        Destroy(currentPlaceableObject);
        start = true;
    }

    private string FixName(string name)
    {
        int strSet = name.IndexOf("(");

        return name.Substring(0, strSet);
    }
}
