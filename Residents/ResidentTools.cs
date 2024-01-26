using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResidentTools : MonoBehaviour
{
    public GameObject axe;
    public GameObject sapling;
    public GameObject wood;
    public GameObject shovel;
    public GameObject can;
    public GameObject spoon;
    public GameObject[] heldItems;
    //0 - axe
    //1 - sapling
    //2 - wood
    //3 - shovel
    //4 - can
    //5 - spoon
    //6 - berry

    public void ChangeEnable(int num, bool enable)
    {
        heldItems[num].SetActive(enable);
    }

    public void TurnOffAll()
    {
        foreach(GameObject tool in heldItems)
        {
            tool.SetActive(false);
        }
    }
}
