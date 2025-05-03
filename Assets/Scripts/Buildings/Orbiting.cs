using UnityEngine;

public class Orbiting : MonoBehaviour
{
    public Transform building;         // Reference to the building
    public Camera playerCamera;        // Reference to the player's camera
    public float distanceFromBuilding = 5f; // How far the object should float from the building
    public Vector3 offset;

    void Awake()
    {
        if (building == null)
            building = transform.parent;
    }

    void Update()
    {
        Vector3 directionToPlayer = (playerCamera.transform.position - building.position).normalized;
        transform.position = building.position + offset + directionToPlayer * distanceFromBuilding;

        transform.LookAt(playerCamera.transform);

        Vector3 eulerAngles = transform.eulerAngles;
        transform.rotation = Quaternion.Euler(0, eulerAngles.y, 0);
    }
}
