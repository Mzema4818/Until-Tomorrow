using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PickUpPopUpAdvanced : MonoBehaviour
{
    public GameObject player;
    public GameObject canvas;
    public float distance;

    public bool shouldBeOn;

    private void Awake()
    {
        //canvas = gameObject;
        //player = GameObject.Find("Main Character");

        canvas.SetActive(false);
    }

    void Update()
    {
        if (shouldBeOn)
        {
            if (canvas.activeSelf && Vector3.Distance(player.transform.position, transform.position) < distance)
            {
                canvas.GetComponent<CanvasGroup>().alpha = 1;
                canvas.SetActive(true);
            }
            else
            {
                CanvasAlphaChangeOverTime(canvas, 2f);
            }
        }
        else
        {
            if (Vector3.Distance(player.transform.position, transform.position) < distance)
            {
                canvas.GetComponent<CanvasGroup>().alpha = 1;
                canvas.SetActive(true);
            }
            else
            {
                CanvasAlphaChangeOverTime(canvas, 2f);
            }
        }
    }

    private void LateUpdate()
    {
        try
        {
            Vector3 cameraPosition = Camera.main.transform.position;
            Vector3 lookAtPosition = new Vector3(cameraPosition.x, canvas.transform.position.y, cameraPosition.z);
            canvas.transform.LookAt(lookAtPosition);
        }
        catch { }
        //transform.parent.transform.Rotate(0, 180, 0);
    }

    public void CanvasAlphaChangeOverTime(GameObject canvas, float speed)
    {
        float alphaColor = canvas.GetComponent<CanvasGroup>().alpha;

        alphaColor -= Time.deltaTime * speed;
        canvas.GetComponent<CanvasGroup>().alpha = alphaColor;

        if (alphaColor <= 0)
        {
            canvas.SetActive(false);
        }
    }
}
