using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Pickaxe : MonoBehaviour
{
    Animator animator;

    private float time;
    private float initalTime;
    private bool canSwing;
    private bool canHit;

    public bool shouldSwing;

    public TextMeshProUGUI stone;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == "PickSwing")
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 12 && AnimatorIsPlaying() && canHit)
        {
            canHit = false;
            collision.transform.parent.gameObject.GetComponent<Rock>().numOfHits++;
            int num = int.Parse(stone.GetComponent<TextMeshProUGUI>().text) + 1;
            stone.GetComponent<TextMeshProUGUI>().text = num.ToString();
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
            canHit = true;
        }
    }

    bool AnimatorIsPlaying()
    {
        return animator.GetCurrentAnimatorStateInfo(0).length >
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
}