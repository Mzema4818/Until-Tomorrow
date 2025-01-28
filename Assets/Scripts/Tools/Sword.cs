using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    Animator animator;

    private float time;
    private float initalTime;
    private bool canSwing;

    public bool shouldSwing;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == "SwordSwing")
            {
                initalTime = clip.length;
            }
        }

        canSwing = true;
        shouldSwing = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldSwing)
        {
            swing();
        }
    }

    private void swing()
    {
        if (Input.GetMouseButtonDown(0) && canSwing)
        {
            animator.SetTrigger("Swung");
            canSwing = false;
        }

        if (!canSwing)
        {
            time -= Time.deltaTime;
        }
        if (time < 0)
        {
            time = initalTime;
            canSwing = true;
        }
    }
}
