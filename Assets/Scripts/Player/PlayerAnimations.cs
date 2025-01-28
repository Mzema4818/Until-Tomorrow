using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    Animator animator;
    CharacterController characterController;
    public float velocityZ = 0.0f;
    public float velocityX = 0.0f;
    public float acceleration = 2.0f;
    public float deceleration = 2.0f;
    public bool runPressed;
    public float maximumWalkVelocity = 0.5f;
    public float maximumRunVelocity = 1.0f;
    public bool shouldAnimating;


    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        shouldAnimating = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldAnimating)
        {
            bool forwardPressed = Input.GetKey("w");
            bool leftPressed = Input.GetKey("a");
            bool backPressed = Input.GetKey("s");
            bool rightPressed = Input.GetKey("d");
            bool runPressed = Input.GetKey("left shift");

            /*if (Input.GetKey(KeyCode.LeftControl))
            {
                animator.SetBool("isCrouching", true);
            }
            else
            {
                animator.SetBool("isCrouching", false);
            }*/

            float currentMaxVelocity = runPressed ? maximumRunVelocity : maximumWalkVelocity;

            if (forwardPressed && velocityZ < currentMaxVelocity)
            {
                velocityZ += Time.deltaTime * acceleration;
            }

            if (backPressed && velocityZ > -currentMaxVelocity)
            {
                velocityZ -= Time.deltaTime * acceleration;
            }

            if (leftPressed && velocityX > -currentMaxVelocity)
            {
                velocityX -= Time.deltaTime * acceleration;
            }

            if (rightPressed && velocityX < currentMaxVelocity)
            {
                velocityX += Time.deltaTime * acceleration;
            }

            //
            if (!forwardPressed && velocityZ > 0.0f)
            {
                velocityZ -= Time.deltaTime * deceleration;
            }

            if (!backPressed && velocityZ < 0.0f)
            {
                velocityZ += Time.deltaTime * deceleration;
            }

            if (!leftPressed && velocityX < 0.0f)
            {
                velocityX += Time.deltaTime * deceleration;
            }

            if (!rightPressed && velocityX > 0.0f)
            {
                velocityX -= Time.deltaTime * deceleration;
            }

            if (!leftPressed && !rightPressed && !forwardPressed && !backPressed && velocityX != 0.0f && (velocityX > -0.05f && velocityX < 0.0f))
            {
                velocityX = 0.0f;
            }

            //fixes when you stopped pressing sprint but still walking, it goes back to walking speed
            if (!runPressed && velocityZ > maximumWalkVelocity)
            {
                velocityZ -= Time.deltaTime * deceleration;
            }

            if (!runPressed && velocityZ < -maximumWalkVelocity)
            {
                velocityZ += Time.deltaTime * deceleration;
            }

            if (!runPressed && velocityX < -maximumWalkVelocity)
            {
                velocityX += Time.deltaTime * deceleration;
            }

            if (!runPressed && velocityX > maximumWalkVelocity)
            {
                velocityX -= Time.deltaTime * deceleration;
            }


            animator.SetFloat("velocity X", velocityX);
            animator.SetFloat("velocity Z", velocityZ);

            /*if (tools[0].activeSelf)
            {
                axeAnimator.SetFloat("velocity X", velocityX);
                axeAnimator.SetFloat("velocity Z", velocityZ);
            }

            if (tools[1].activeSelf)
            {
                pickAnimator.SetFloat("velocity X", velocityX);
                pickAnimator.SetFloat("velocity Z", velocityZ);
            }

            if (tools[2].activeSelf)
            {
                swordAnimator.SetFloat("velocity X", velocityX);
                swordAnimator.SetFloat("velocity Z", velocityZ);
            }

            if (tools[3].activeSelf)
            {
                hammerAnimator.SetFloat("velocity X", velocityX);
                hammerAnimator.SetFloat("velocity Z", velocityZ);
            }*/
        }
    }
}
