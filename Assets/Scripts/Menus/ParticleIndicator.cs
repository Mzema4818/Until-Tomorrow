using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ParticleIndicator : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject text;
    public float lifeTime = 0.6f;
    public float minDist = 2f;
    public float maxDist = 3f;
    public Transform parent;

    private Vector3 iniPos;
    private Vector3 targetPos;
    private float timer;

    void Start()
    {
        transform.LookAt(transform.position);

        float direction = Random.rotation.eulerAngles.z;
        iniPos = transform.position;
        float dist = Random.Range(minDist, maxDist);
        targetPos = iniPos + (Quaternion.Euler(0, 0, direction) * new Vector3(dist, dist, 0f));
        transform.localScale = Vector3.zero;
        transform.parent = parent;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        float fraction = lifeTime / 2;

        if (timer > lifeTime) Destroy(gameObject);

        transform.position = Vector3.Lerp(iniPos, targetPos, Mathf.Sin(timer / lifeTime));
        transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, Mathf.Sin(timer / lifeTime));
    }
}
