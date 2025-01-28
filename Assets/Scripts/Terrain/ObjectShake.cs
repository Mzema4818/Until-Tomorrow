using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShake : MonoBehaviour
{
    public float speed;
    public float amount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Mathf.Sin(Time.time * speed) * amount;
        transform.position = new Vector3(transform.position.x + x, transform.position.y + x, transform.position.z + x);
    }
}
