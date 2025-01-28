using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    public bool shouldLook;
    public GameObject weaponCamera;
    public Vector3 offset;

    float xRotation = 0f;

    // Start is called before the first frame update
    void Awake()
    {
        shouldLook = false;
        //weaponCamera.transform.position = transform.position;
        //weaponCamera.transform.position += offset;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldLook)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90, 60f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}
