using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShipMovement : MonoBehaviour
{
    public bool orientation;
    public float moveSpeed;
    public float rotateSpeed = 1.0f;

    private bool move;
    private bool rotate;
    private bool returnHome;

    [Header("Ship Activities")]
    public bool leave;
    public bool ReadyForActivity;
    public NavMeshSurface navMeshSurface;
    public NavMeshLink navMeshLink;
    public bool destroyed;

    private Quaternion target;
    private Quaternion original;
    private Animator animator;

    public bool test;

    void Awake()
    {
        animator = transform.Find("Plank").GetComponent<Animator>();
        navMeshLink = transform.Find("Plank").Find("Cube").GetComponent<NavMeshLink>();
        navMeshSurface = transform.Find("Plank").GetComponent<NavMeshSurface>();
        transform.LookAt(new Vector3(0, 10, 0));
    }

    void Start()
    {
        move = true;
        target = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 90, transform.rotation.eulerAngles.z);
        original = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 180, transform.rotation.eulerAngles.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (test)
        {
            navMeshSurface.BuildNavMesh();
            test = false;
        }

        if (move)
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }

        if (rotate)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * rotateSpeed);
            //print("Transform: " + Mathf.Floor(transform.rotation.eulerAngles.y) + "| Target: " + Mathf.Floor(target.eulerAngles.y));
            if (Mathf.Floor(transform.rotation.eulerAngles.y) == Mathf.Floor(target.eulerAngles.y))
            {
                rotate = false;
                animator.SetBool("Extend", true);
                StartCoroutine(ReadyForExport());
            }
        }

        if (leave)
        {
            animator.SetBool("Extend", false);
            animator.SetTrigger("deExtend");
            StartCoroutine(RetractPlank());

            if (returnHome)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, original, Time.deltaTime * rotateSpeed);
            }
            if (Mathf.Floor(transform.rotation.eulerAngles.y) == Mathf.Floor(original.eulerAngles.y))
            {
                move = true;
                returnHome = false;
                StartCoroutine(DestroyObject());
            }
        }
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y - 10, transform.position.z), transform.TransformDirection(Vector3.forward), out hit, 20))
        {
            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y - 10, transform.position.z), transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            if (hit.transform.name == "Terrain")
            {
                move = false;
                rotate = true;
            }
        }
    }

    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(10);
        destroyed = true;
        Destroy(gameObject);
    }

    IEnumerator RetractPlank()
    {
        yield return new WaitForSeconds(2);
        returnHome = true;
    }

    IEnumerator ReadyForExport()
    {
        yield return new WaitForSeconds(2);
        navMeshSurface.BuildNavMesh();
        navMeshLink.UpdateLink();
        ReadyForActivity = true;
    }
}
