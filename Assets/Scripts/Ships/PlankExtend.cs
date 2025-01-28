using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlankExtend : MonoBehaviour
{
    public bool Extend;
    public bool unExtend;
    public Vector3 scale;
    private Vector3 originalScale;
    private ShipMovement shipMovement;

    // Start is called before the first frame update
    private void Start()
    {
        originalScale = transform.localScale;
        shipMovement = transform.parent.GetComponent<ShipMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Extend) transform.localScale = Vector3.Lerp(transform.localScale, scale, 0.1f * Time.deltaTime);
        if (unExtend) transform.localScale = Vector3.Lerp(transform.localScale, originalScale, 1.8f * Time.deltaTime);

        if (unExtend && Vector3.Distance(transform.localScale, originalScale) < 0.1f)
        {
            shipMovement.destroyed = true;
            shipMovement.rotate = true;
            unExtend = false;
        }
    }
}
