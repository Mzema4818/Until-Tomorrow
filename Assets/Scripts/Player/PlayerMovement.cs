using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector3 respawnPoint;
    public CharacterController controller;
    public Transform playerCamera = null;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public LayerMask groundMaskRocks;
    public LayerMask groundMaskTrees;
    public LayerMask groundMaskBuildings;

    public bool shouldMove;

    [Header("Crouch")]
    public float standHeight = 2.0f;
    public float crouchHeight = 1.0f;
    public float crouchSpeed = 0.3f;
    public bool crouching;

    Vector3 velocity;
    bool isGrounded;
    bool isGroundedRocks;
    bool isGroundedTrees;
    bool isGroundedBuildings;

    public bool first;

    public GameObject dust;

    private void Awake()
    {
        shouldMove = false;
    }

    void Update()
    {
        crouching = Input.GetKey(KeyCode.LeftControl);

        if (shouldMove)
        {
            if (Input.GetKey("left shift"))
            {
                speed = 15;
            }
            else if (Input.GetKey(KeyCode.LeftControl))
            {
                speed = 3;
            }
            else
            {
                speed = 7;
            }

            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            isGroundedRocks = Physics.CheckSphere(groundCheck.position, groundDistance, groundMaskRocks);
            isGroundedTrees = Physics.CheckSphere(groundCheck.position, groundDistance, groundMaskTrees);
            isGroundedBuildings = Physics.CheckSphere(groundCheck.position, groundDistance, groundMaskBuildings);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(speed * Time.deltaTime * move);

            if (Input.GetButtonDown("Jump") && (isGrounded || isGroundedRocks || isGroundedTrees || isGroundedBuildings))
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }

        if (controller.enabled)
        {
            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        var desiredHeight = crouching ? crouchHeight : standHeight;

        if (controller.height != desiredHeight)
        {
            AdjustHeight(desiredHeight);

            var camPos = playerCamera.transform.localPosition;
            camPos.y = controller.height;

            playerCamera.transform.localPosition = camPos;
        }
    }

    private void AdjustHeight(float height)
    {
        float center = height / 2;
        controller.height = Mathf.Lerp(controller.height, height, crouchSpeed);
        controller.center = Vector3.Lerp(controller.center, new Vector3(0, center, 0), crouchSpeed);
    }
}
