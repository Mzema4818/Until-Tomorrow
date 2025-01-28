using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHeld : MonoBehaviour
{
    public Item item;
    public Inventory inventory;
    public string itemtype;
    public int amount;

    private void Update()
    {
        //amount = item.amount;
        itemtype = item.itemType.ToString();
    }

    private int ReturnIndex(string name)
    {
        return int.Parse(name.Substring(name.IndexOf("_") + 1));
    }
}
