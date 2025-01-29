using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerInput playerInput;
    PlayerInput.MainActions input;

    CharacterController controller;
    public Animator animator;

    [Header("Controller")]
    public float walkSpeed = 5;
    public float runSpeed = 10;
    public float gravity = -9.8f;
    public float jumpHeight = 1.2f;

    Vector3 _PlayerVelocity;

    bool isGrounded;
    bool isRunning;
    bool isCrouching;

    [Header("Camera")]
    public Camera cam;
    public float sensitivity;

    float xRotation = 0f;

    [Header("Global")]
    public bool shouldMove;
    public Vector3 respawnPoint;

    // Animations
    float maximumWalkVelocity = 0.5f;
    float maximumRunVelocity = 1.0f;
    float currentMaxVelocity = 0.0f;
    float VelocityX = 0;
    float VelocityY = 0;

    [Header("Crouch")]
    public float crouchHeight = 0.5f;  // Height when crouched
    public float standingHeight = 2.0f; // Default standing height

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = new PlayerInput();
        input = playerInput.Main;
        AssignInputs();

        // Set initial camera height
        controller.height = standingHeight;
        cam.transform.localPosition = new Vector3(0, standingHeight / 2, 0);
    }

    void Update()
    {
        isRunning = input.Run.IsPressed();
        isGrounded = controller.isGrounded;
    }

    void FixedUpdate()
    {
        MoveInput(input.Movement.ReadValue<Vector2>());
    }

    void LateUpdate()
    {
        if (shouldMove) LookInput(input.Look.ReadValue<Vector2>());
    }

    void MoveInput(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;

        float speed = isRunning ? runSpeed : walkSpeed;
        if (isCrouching) speed *= 0.5f; // Reduce speed when crouching

        if (shouldMove) controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        _PlayerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && _PlayerVelocity.y < 0)
            _PlayerVelocity.y = -2f;
        controller.Move(_PlayerVelocity * Time.deltaTime);

        if (shouldMove) Animate(input);
        else
        {
            VelocityX = 0;
            VelocityY = 0;
        }
    }

    void Animate(Vector2 input)
    {
        float targetVelocityX = input.x;
        float targetVelocityY = input.y;

        currentMaxVelocity = isRunning ? maximumRunVelocity : maximumWalkVelocity;

        VelocityX = targetVelocityX * currentMaxVelocity;
        VelocityY = targetVelocityY * currentMaxVelocity;

        if (Mathf.Abs(VelocityX) < 0.01f) VelocityX = 0f;
        if (Mathf.Abs(VelocityY) < 0.01f) VelocityY = 0f;

        animator.SetFloat("velocity X", VelocityX);
        animator.SetFloat("velocity Z", VelocityY);
    }

    void LookInput(Vector3 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        xRotation -= (mouseY * Time.deltaTime * sensitivity);
        xRotation = Mathf.Clamp(xRotation, -80, 80);

        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime * sensitivity));
    }

    void OnEnable()
    {
        input.Enable();
    }

    void OnDisable()
    {
        input.Disable();
    }

    void Jump()
    {
        if (isGrounded)
            _PlayerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
    }

    void AssignInputs()
    {
        input.Jump.performed += ctx => Jump();
        input.Crouch.performed += ctx => ToggleCrouch();  // Add crouch input
    }

    void ToggleCrouch()
    {
        if (isCrouching)
        {
            // Stand up
            controller.height = standingHeight;
            cam.transform.localPosition = new Vector3(0, standingHeight / 2, 0);
        }
        else
        {
            // Crouch
            controller.height = crouchHeight;
            cam.transform.localPosition = new Vector3(0, crouchHeight / 2, 0);
        }

        isCrouching = !isCrouching;
    }
}
