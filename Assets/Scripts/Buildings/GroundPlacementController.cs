using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class GroundPlacementController : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public int[] Cost;

    public SpawnEnemies spawnEnemies;
    public TextMeshProUGUI Announcement;
    public BuildMenuUpdater buildMenuUpdater;
    public ProximityShader proximityShader;
    [SerializeField]
    public GameObject placeableObjectPrefab;

    public float distance;
    public float offset;

    public GameObject currentPlaceableObject;
    public Transform parent;

    private float mouseWheelRotation;
    public GameObject hammer;
    public Builder builder;
    public bool moving;
    public bool shouldPlace;

    public AudioSource audioSource;
    public AudioClip placingItem;

    private Vector3 originalPosition;

    private void Start()
    {
        if (moving)
        {
            originalPosition = transform.position;
            transform.GetComponent<IsABuilding>().beingMoved = true;
        }
    }

    private void Update()
    {
        //HandleNewObjectHotkey();

        if (currentPlaceableObject != null) //gotta fix this a bit, its here because if object isnt on screen(you look away) it goes underground
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
                builder.isBuilding = false;
            }
            else
            {
                moveBack();
            }
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
                currentPlaceableObject.transform.position = hitInfo.point + new Vector3(0, offset, 0);
                currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
            }
            shouldPlace = true;
        }
        else
        {
            currentPlaceableObject.transform.position = new Vector3(0, -100, 0); //"fixes" spinning glitch 
            shouldPlace = false;
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
        if (Input.GetMouseButtonDown(0) && !currentPlaceableObject.GetComponent<ColorChange>().touching && shouldPlace)
        {
            Destroy(currentPlaceableObject.GetComponent<ColorChange>());
            currentPlaceableObject.transform.parent = parent;
            try { currentPlaceableObject.transform.name = FixName(currentPlaceableObject.transform.name); } catch { }
            if(currentPlaceableObject.name.Contains("Campfire") && !moving)
            {
                buildMenuUpdater.AccessToLevel0Buildings = false;
                buildMenuUpdater.AccessToLevel1Buildings = true;

                Announcement.gameObject.SetActive(true);
                Announcement.text = "New Buildings Unlocked";
                spawnEnemies.townhall = currentPlaceableObject;
            }

            transform.GetComponent<IsABuilding>().beingMoved = false;
            transform.GetComponent<NavMeshObstacle>().enabled = true;

            //Array index to type
            //0 - wood
            //1 - rock
            if (inventoryManager != null)
            {
                RemoveItem(Item.ItemType.wood, Cost[0]);
                RemoveItem(Item.ItemType.rock, Cost[1]);
            }

            if (currentPlaceableObject.GetComponent<PickUpPopUp>() != null) currentPlaceableObject.GetComponent<PickUpPopUp>().enabled = true;
            currentPlaceableObject = null;
            builder.isBuilding = false;

            audioSource.PlayOneShot(placingItem);

            proximityShader.AddBuilding(gameObject);
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

        transform.GetComponent<IsABuilding>().beingMoved = false;
        transform.GetComponent<NavMeshObstacle>().enabled = true;
        Destroy(this);
    }

    private int CheckNumber()
    {
        int num = 1;
        for (int i = 0; i < parent.childCount - 1; i++)
        {
            string number = parent.GetChild(i).name.Substring(parent.GetChild(i).name.Length - 1);
            if (int.Parse(number) != num) return num;
            num++;
        }

        return -1;
    }

    private string FixName(string name)
    {
        int strSet = name.IndexOf("(");
        int num = CheckNumber();

        if (num != -1)
        {
            parent.GetChild(parent.childCount - 1).SetSiblingIndex(num - 1);
            return name.Substring(0, strSet) + num;
        }

        return name.Substring(0, strSet) + parent.childCount;
    }

    private void RemoveItem(Item.ItemType item, int cost)
    {
        for (int i = 0; i < cost; i++)
        {
            inventoryManager.RemoveItem(item);
        }
    }
}