using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotEnough : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        StartCoroutine(turnOff());
    }

    void OnDisable()
    {
        gameObject.SetActive(false);
    }


    IEnumerator turnOff()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
}
