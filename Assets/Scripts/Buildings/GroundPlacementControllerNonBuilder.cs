using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPlacementControllerNonBuilder : MonoBehaviour
{
    public PlacingSeeds placingSeeds;
    public ForestGenerator forestGenerator;

    public GameObject currentPlaceableObject;
    public InventoryManager inventoryManager;
    public Transform parent;
    public Transform TreeParent;
    public int itemIndex;

    public float distance = 50;
    private float mouseWheelRotation;

    public bool tree;
    public bool berry;

    void Update()
    {
        if (currentPlaceableObject != null)
        {
            MoveCurrentObjectToMouse();
            RotateFromMouseWheel();
            ReleaseIfClicked();
        }
    }

    private void ReleaseIfClicked()
    {
        if (Input.GetMouseButtonDown(0) && !currentPlaceableObject.GetComponent<ColorChange>().touching)
        {
            Destroy(currentPlaceableObject.GetComponent<ColorChange>());
            currentPlaceableObject.GetComponent<PickUpPopUp>().enabled = true;
            currentPlaceableObject.transform.parent = parent;
            if (tree)
            {
                currentPlaceableObject.AddComponent<RegrowSaplings>().forestGenerator = forestGenerator;
                currentPlaceableObject.GetComponent<RegrowSaplings>().parent = TreeParent.gameObject;
                currentPlaceableObject.AddComponent<PickUp>();
            }
            if (berry)
            {
                currentPlaceableObject.AddComponent<RegrowBerries>();
                currentPlaceableObject.AddComponent<PickUp>();
            }
            currentPlaceableObject = null;
            Destroy(this);

            bool Result = inventoryManager.RemoveItemAtIndex(itemIndex, 1);
            if (Result)
            {
                placingSeeds.currentObject = null;
                placingSeeds.gameObject.SetActive(false);
            }
            else placingSeeds.SpawnObject();
        }
    }

    private void MoveCurrentObjectToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, distance, 1 << LayerMask.NameToLayer("Ground")))
        {
            currentPlaceableObject.SetActive(true);
            if (hitInfo.transform.name == "Terrain")
            {
                currentPlaceableObject.transform.position = hitInfo.point + new Vector3(0, 0, 0);
                currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
            }
            //currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
        }
        else
        {
            currentPlaceableObject.transform.position = new Vector3(0, -100, 0); //"fixes" spinning glitch 
        }
    }

    private void RotateFromMouseWheel()
    {
        //Debug.Log(Input.mouseScrollDelta);
        mouseWheelRotation += Input.mouseScrollDelta.y;
        currentPlaceableObject.transform.Rotate(Vector3.up, mouseWheelRotation * 10f);
    }
}
