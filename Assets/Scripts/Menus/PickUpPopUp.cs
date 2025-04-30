using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPopUp : MonoBehaviour
{
    public GameObject player;
    private GameObject canvas;

    // Start is called before the first frame update
    private void Awake()
    {
        canvas = transform.Find("Canvas").gameObject;
        //player = GameObject.Find("Main Character");

        canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 15)
        {
            canvas.GetComponent<CanvasGroup>().alpha = 1;
            canvas.SetActive(true);
        }
        else
        {
            CanvasAlphaChangeOverTime(canvas, 2f);
        }
    }

    private void LateUpdate()
    {
        try
        {
            canvas.transform.LookAt(Camera.main.transform);
        }
        catch { };
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
            //print("hi");
        }
    }

    private void OnDisable()
    {
        canvas.SetActive(false);
    }
}
