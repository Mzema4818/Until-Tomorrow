using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCollider : MonoBehaviour
{
    public Inventory inventory;
    public bool test = false;
    // Start is called before the first frame update
    private void Awake()
    {
        inventory = new Inventory(35);
    }

    // Update is called once per frame
    void Update()
    {
        if (test)
        {
            foreach(Item item in inventory.GetItemList())
            {
                //print(item.itemType + "    " + item.amount);
            }
            test = false;
        }
    }
}
