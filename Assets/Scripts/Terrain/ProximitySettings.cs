using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximitySettings : MonoBehaviour
{
    public ProximityShader proximityShader;
    public int maxDistance;

    private void OnDestroy()
    {
        if(gameObject != null) proximityShader.RemoveBuilding(gameObject);
    }
}
