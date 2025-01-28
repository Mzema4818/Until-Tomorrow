using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpDecay : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(Decay());
    }

    IEnumerator Decay()
    {
        yield return new WaitForSeconds(5);
        gameObject.SetActive(false);
    }
}
