using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationChanger2 : StateMachineBehaviour
{
    public ResidentTools residentTools;
    public bool switchBool;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        residentTools = animator.GetComponent<ResidentTools>();

        if (switchBool)
        {
            residentTools.ChangeEnable(3, false);
            residentTools.ChangeEnable(4, true);
        }
        else
        {
            residentTools.ChangeEnable(3, true);
            residentTools.ChangeEnable(4, false);
        }
    }
}
