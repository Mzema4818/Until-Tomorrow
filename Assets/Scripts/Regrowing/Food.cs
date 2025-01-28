using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Food : MonoBehaviour
{
    public int food = 50;
    [SerializeField] private Slider foodBar;
    public InventoryManager inventory;
    public int itemIndex;

    public OpenMenus openMenus;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && openMenus.CheckIfMenusAreClosed() && openMenus.CheckIfOtherMenusAreClosed())
        {
            //print(itemIndex);
            Eat();
        }
    }

    public void Eat()
    {
        foodBar.value += food;
        bool Result = inventory.RemoveItemAtIndex(itemIndex, 1);
        if (Result) gameObject.SetActive(false);
    }
}
