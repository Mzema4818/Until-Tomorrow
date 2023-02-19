using TMPro;
using UnityEngine;

public class GroundPlacementController : MonoBehaviour
{
    public TextMeshProUGUI Announcement;
    public BuildMenuUpdater buildMenuUpdater;
    [SerializeField]
    public GameObject placeableObjectPrefab;

    public float distance;

    public GameObject currentPlaceableObject;
    public Transform parent;

    private float mouseWheelRotation;
    public GameObject hammer;
    public Builder builder;
    public bool moving;
    public MaterialManager materialManager;

    private Vector3 originalPosition;

    private void Start()
    {
        if (moving)
        {
            originalPosition = transform.position;
        }
    }

    private void Update()
    {
        //HandleNewObjectHotkey();

        if (currentPlaceableObject != null)
        {
            MoveCurrentObjectToMouse();
            RotateFromMouseWheel();
            ReleaseIfClicked();
        }

        if (!hammer.activeSelf)
        {
            if (!moving)
            {
                DestroyObject();
            }
            else
            {
                moveBack();
            }
        }
    }

    private void HandleNewObjectHotkey()
    {
        /*if ()
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
            canSpawn = false;
        }*/
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
        //Debug.Log(Input.mouseScrollDelta);
        mouseWheelRotation += Input.mouseScrollDelta.y;
        currentPlaceableObject.transform.Rotate(Vector3.up, mouseWheelRotation * 10f);
    }

    private void ReleaseIfClicked()
    {
        if (Input.GetMouseButtonDown(0) && !currentPlaceableObject.GetComponent<ColorChange>().touching)
        {
            Destroy(currentPlaceableObject.GetComponent<ColorChange>());
            currentPlaceableObject.transform.parent = parent;
            try{currentPlaceableObject.transform.name = FixName(currentPlaceableObject.transform.name);}catch{}
            if(currentPlaceableObject.name.Contains("Campfire") && !moving)
            {
                buildMenuUpdater.AccessToLevel0Buildings = false;
                buildMenuUpdater.AccessToLevel1Buildings = true;

                Announcement.gameObject.SetActive(true);
                Announcement.text = "New Buildings Unlocked";
            }
            currentPlaceableObject = null;
            builder.isBuilding = false;
            materialManager.placed = true;
            Destroy(this);
        }
    }

    private void DestroyObject()
    {
        Destroy(currentPlaceableObject);
    }

    private void moveBack()
    {
        transform.position = originalPosition;
        Destroy(currentPlaceableObject.GetComponent<ColorChange>());
        currentPlaceableObject.transform.parent = parent;
        try { currentPlaceableObject.transform.name = FixName(currentPlaceableObject.transform.name); } catch { }
        currentPlaceableObject = null;
        builder.isBuilding = false;
        Destroy(this);
    }

    private string FixName(string name)
    {
        int strSet = name.IndexOf("(");

        return name.Substring(0, strSet) + parent.childCount;
    }
}