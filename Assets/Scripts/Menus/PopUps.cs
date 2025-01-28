using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUps : MonoBehaviour
{
    public GameObject PopUpSaveGame;
    private bool startPopUp;

    private void Update()
    {
        if (startPopUp)
        {
            StartCoroutine(SaveGame());
            startPopUp = false;
        }
    }

    public void StartPopUp()
    {
        startPopUp = true;
    }

    public IEnumerator SaveGame()
    {
        PopUpSaveGame.SetActive(true);
        yield return new WaitForSeconds(2);
        PopUpSaveGame.SetActive(false);
    }
}
