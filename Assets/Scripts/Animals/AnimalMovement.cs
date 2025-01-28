using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalMovement : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;
    private GameObject player;

    private float gravity = -9.81f;

    Vector3 velocity;
    bool isGrounded;

    private Transform groundCheck;
    private float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float moveSpeed = 3f;
    public float rotSpeed = 100f;
    public bool offset;

    private bool iswandering = false;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    private bool iswalking = false;
    private bool shouldWander = true;

    // Start is called before the first frame update
    void Start()
    {
        controller = transform.GetComponent<CharacterController>();
        animator = transform.GetComponent<Animator>();
        groundCheck = transform.Find("GroundCheck");
        player = GameObject.Find("Main Character");

        shouldWander = true;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (Vector3.Distance(player.transform.position, transform.position) < 10 && shouldWander)
        {
            StartCoroutine(RunAway());
        }

        if (shouldWander)
        {
            wanderAround();
        }
    }

    IEnumerator RunAway()
    {
        shouldWander = false;
        animator.SetBool("Walking", true);
        transform.position += -transform.right * 20 * Time.deltaTime;

        yield return new WaitForSeconds(5);

        animator.SetBool("Walking", false);
        shouldWander = true;
    }

    private void wanderAround()
    {
        if (iswandering == false)
        {
            StartCoroutine(Wander());
        }
        if (isRotatingRight == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * rotSpeed);
        }
        if (isRotatingLeft == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * -rotSpeed);
        }
        if (iswalking == true)
        {
            if (!offset)
            {
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
            }
            else
            {
                transform.position += -transform.right * moveSpeed * Time.deltaTime;
            }
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }
    }

    IEnumerator Wander()
    {
        int rotTime = Random.Range(1, 3);
        int rotateWait = Random.Range(1, 4);
        int rotateLorR = Random.Range(1, 2);
        int walkWait = Random.Range(1, 4);
        int walkTime = Random.Range(1, 5);

        iswandering = true;

        yield return new WaitForSeconds(walkWait);
        iswalking = true;
        yield return new WaitForSeconds(walkTime);
        iswalking = false;
        yield return new WaitForSeconds(rotateWait);
        if (rotateLorR == 1)
        {
            isRotatingRight = true;
        }
        yield return new WaitForSeconds(rotTime);
        {
            isRotatingRight = false;
        }
        if (rotateLorR == 2)
        {
            isRotatingLeft = true;
        }
        yield return new WaitForSeconds(rotTime);
        {
            isRotatingLeft = false;
        }
        iswandering = false;
    }
}
