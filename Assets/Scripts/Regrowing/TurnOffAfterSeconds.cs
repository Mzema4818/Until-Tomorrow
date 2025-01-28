using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffAfterSeconds : MonoBehaviour
{
    public int time;

    private void OnEnable()
    {
        StartCoroutine(ExampleCoroutine());
    }

    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
