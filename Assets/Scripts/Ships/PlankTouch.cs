using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlankTouch : MonoBehaviour
{
    private PlankExtend plank;
    private ShipMovement shipMovement;
    // Start is called before the first frame update
    private void Awake()
    {
        plank = transform.parent.GetComponent<PlankExtend>();
        shipMovement = transform.parent.parent.GetComponent<ShipMovement>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Terrain")
        {
            plank.Extend = false;
            shipMovement.readyForExport = true;
        }
    }

}
