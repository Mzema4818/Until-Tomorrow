using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationChanger : StateMachineBehaviour
{
    public ResidentTools residentTools;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        residentTools = animator.GetComponent<ResidentTools>();
        if(residentTools != null)
        {
            Debug.Log("off");
            residentTools.TurnOffAll();
        }
    }
}
