using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResidentTools : MonoBehaviour
{
    public Animator animator;
    public GameObject[] heldItems;
    //0 - axe
    //1 - sapling
    //2 - wood
    //3 - shovel
    //4 - can
    //5 - spoon
    //6 - berry
    //7 - bow
    //8 - sword

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

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

    public void TurnOffAllAnimations()
    {
        for (int i = 2; i < animator.parameterCount; i++) 
        {
            animator.SetBool(animator.GetParameter(i).name, false);
        }
    }
}
