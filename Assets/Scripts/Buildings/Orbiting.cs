using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbiting : MonoBehaviour
{
    private Transform building;  // Reference to the building's transform
    public Camera playerCamera; // Reference to the player's camera
    public float orbitSpeed = 30f; // Speed of orbit around the building
    public float distanceFromBuilding = 5f; // Distance of the canvas from the building

    private Vector3 offset;

    void Start()
    {
        // Initial offset from the building
        building = transform.parent;
        offset = transform.position - building.position;
    }

    private void OnEnable()
    {
        Vector3 directionToPlayer = (playerCamera.transform.position - building.position).normalized;
        Vector3 targetPosition = building.position + directionToPlayer * distanceFromBuilding;

        transform.position = targetPosition;
    }

    void Update()
    {
        // Position the canvas in front of the player, maintaining the distance
        Vector3 directionToPlayer = (playerCamera.transform.position - building.position).normalized;
        Vector3 targetPosition = building.position + directionToPlayer * distanceFromBuilding;

        // Update the canvas position while maintaining its offset relative to the building
        transform.position = targetPosition;

        // Make the canvas orbit around the building based on the player's position
        transform.RotateAround(building.position, Vector3.up, orbitSpeed * Time.deltaTime);

        // Ensure the canvas always faces the player (optional)
        transform.LookAt(playerCamera.transform);
    }
}
