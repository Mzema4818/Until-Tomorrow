using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Food : MonoBehaviour
{
    public Hunger hunger;
    public int food = 50;
    public InventoryManager inventory;
    public int itemIndex;

    public OpenMenus openMenus;

    private void Awake()
    {
        hunger = transform.root.GetComponent<Hunger>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && openMenus.CheckIfMenusAreClosed() && openMenus.CheckIfOtherMenusAreClosed())
        {
            Eat();
        }
    }

    public void Eat()
    {
        hunger.ModifyHunger(food);
        bool Result = inventory.RemoveItemAtIndex(itemIndex, 1);
        if (Result) gameObject.SetActive(false);
    }
}
