using UnityEngine;

public class CameraSway : MonoBehaviour
{
    public float swayAmountPositionX = 0.1f; // Maximum X position sway
    public float swayAmountPositionY = 0.1f; // Maximum Y position sway
    public float swayAmountPositionZ = 0.1f; // Maximum Z position sway
    public float swayAmountRotationX = 2f;   // Maximum X rotation sway (tilt)
    public float swayAmountRotationY = 2f;   // Maximum Y rotation sway
    public float swayAmountRotationZ = 2f;   // Maximum Z rotation sway
    public float swaySpeed = 1f;             // Speed of the sway

    private Vector3 startPosition;
    private Vector3 startRotation;

    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.eulerAngles;
    }

    void Update()
    {
        // Swaying position for X, Y, and Z axes
        float swayPositionX = Mathf.Sin(Time.time * swaySpeed) * swayAmountPositionX;
        float swayPositionY = Mathf.Cos(Time.time * swaySpeed) * swayAmountPositionY; // You can use Cos for variation
        float swayPositionZ = Mathf.Sin(Time.time * swaySpeed) * swayAmountPositionZ;

        // Swaying rotation for X, Y, and Z axes
        float swayRotationX = Mathf.Sin(Time.time * swaySpeed) * swayAmountRotationX;
        float swayRotationY = Mathf.Cos(Time.time * swaySpeed) * swayAmountRotationY;
        float swayRotationZ = Mathf.Sin(Time.time * swaySpeed) * swayAmountRotationZ;

        // Apply both position and rotation sway
        transform.position = startPosition + new Vector3(swayPositionX, swayPositionY, swayPositionZ); // Sway in all directions
        transform.rotation = Quaternion.Euler(startRotation.x + swayRotationX, startRotation.y + swayRotationY, startRotation.z + swayRotationZ); // Sway rotation in all axes
    }
}
