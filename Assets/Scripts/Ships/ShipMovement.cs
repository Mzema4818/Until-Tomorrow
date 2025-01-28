using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public float moveSpeed;

    private float originalSpeed;

    public bool readyForExport;
    private bool move;
    public bool rotate;
    public bool slowSpeed;
    public bool docked;

    [Header("Ship Activities")]
    public bool leave;
    public bool ReadyForActivity;
    public bool destroyed;

    private Quaternion targetAngle_90 = Quaternion.Euler(0, 0, 0);
    private Quaternion targetAngle_180 = Quaternion.Euler(0, 0, 0);

    private GameObject plank;
    private GameObject cube;

    public bool test;

    void Awake()
    {
        docked = false;
        originalSpeed = moveSpeed;

        plank = transform.Find("Plank").gameObject;
        cube = transform.Find("Plank").Find("Cube").gameObject;

        transform.LookAt(new Vector3(0, 10, 0));

        targetAngle_90 = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + 90, transform.eulerAngles.z);
        targetAngle_180 = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + 180, transform.eulerAngles.z);
    }

    void Start()
    {
        move = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }

        if (rotate && !destroyed)
        {
            if (Quaternion.Angle(transform.rotation, targetAngle_90) > 5.0F)
            {
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetAngle_90, 2 * Time.deltaTime);
            }
            else
            {
                StartCoroutine(ExtendPlank());
                rotate = false;
            }
        }

        if (slowSpeed)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, 0, 1.5f * Time.deltaTime);
            if (moveSpeed < 1)
            {
                slowSpeed = false;
                move = false;
                rotate = true;
                moveSpeed = originalSpeed;
            }
        }

        if (readyForExport)
        {
            StartCoroutine(ExportResidents());
            readyForExport = false;
        }

        if (leave)
        {
            StartCoroutine(UnExtendPlank());
            leave = false;
        }

        if (rotate && destroyed)
        {
            if (Quaternion.Angle(transform.rotation, targetAngle_180) > 5.0F)
            {
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetAngle_180, 2 * Time.deltaTime);
            }
            else
            {
                move = true;
                rotate = false;
                StartCoroutine(DestroyObject());
            }
        }
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y - 10, transform.position.z), transform.TransformDirection(Vector3.forward), out hit, 45) && !docked)
        {
            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y - 10, transform.position.z), transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            if (hit.transform.name == "Terrain")
            {
                slowSpeed = true;
                docked = true;
            }
        }
    }

    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(10);
        destroyed = true;
        Destroy(gameObject);
    }

    IEnumerator ExtendPlank()
    {
        yield return new WaitForSeconds(2);
        plank.GetComponent<PlankExtend>().Extend = true;
    }

    IEnumerator ExportResidents()
    {
        yield return new WaitForSeconds(2);
        ReadyForActivity = true;
    }

    IEnumerator UnExtendPlank()
    {
        yield return new WaitForSeconds(2);
        plank.GetComponent<PlankExtend>().unExtend = true;
    }
}
