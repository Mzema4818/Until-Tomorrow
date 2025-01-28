using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularPopUps : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        StartCoroutine(PopUp());
    }

    public IEnumerator PopUp()
    {
        yield return new WaitForSeconds(5);
        gameObject.SetActive(false);
    }
}
