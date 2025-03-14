using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawning : MonoBehaviour
{
    public float despawnTime = 300f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Despawn", despawnTime, despawnTime);
    }

    private void Despawn()
    {
        Destroy(gameObject);
    }
}
