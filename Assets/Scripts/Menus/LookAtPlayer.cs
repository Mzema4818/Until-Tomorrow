using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    private void LateUpdate()
    {
        try
        {
            transform.LookAt(Camera.main.transform);
        }
        catch { };
    }
}
