using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerAttack : MonoBehaviour
{
    public PlayerInput playerInput;
    public PlayerInput.MainActions input;

    [Header("Attacking")]
    public float attackDistance = 3f;
    public float attackDelay;
    public float attackSpeed;
    public int attackDamage;
    public LayerMask idealHit;
    public LayerMask attackLayer;

    public GameObject hitEffect;
    public AudioClip swordSwing;
    public AudioClip hitSound;

    public string IDLE = "Arms Idle";
    public string attackOneHand1 = "Swing One Hand 1";
    public string attackOneHand2 = "Swing One Hand 2";
    public string attackTwoHand1 = "Swing Two Hand 1";
    public string attackTwoHand2 = "Swing Two Hand 2";

    public bool attacking = false;
    public bool readyToAttack = true;
    public int attackCountOne;
    public int attackCountTwo;

    string currentAnimationState;
    Animator animator;
    AudioSource audioSource;

    [Header("Camera")]
    public Camera cam;

    void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        playerInput = new PlayerInput();
        input = playerInput.Main;
        AssignInputs();
    }

    void Update()
    {
        if (input.Attack.IsPressed() && readyToAttack)
        {
            Attack(); 
        }

        SetAnimations();
    }

    public void ChangeAnimationState(string newState)
    {
        // STOP THE SAME ANIMATION FROM INTERRUPTING WITH ITSELF //
        if (currentAnimationState == newState) return;

        // PLAY THE ANIMATION //
        currentAnimationState = newState;
        animator.CrossFadeInFixedTime(currentAnimationState, 0.2f);
    }

    public void Attack()
    {
        if (!readyToAttack || attacking || attackDamage == 0) return;

        readyToAttack = false;
        attacking = true;

        Invoke(nameof(ResetAttack), attackSpeed);
        Invoke(nameof(AttackRaycast), attackDelay);

        //audioSource.pitch = Random.Range(0.9f, 1.1f);
        //audioSource.PlayOneShot(swordSwing);

        if (animator.GetBool("Holding"))
        {
            if (attackCountOne == 0)
            {
                ChangeAnimationState(attackOneHand1);
                attackCountOne++;
            }
            else
            {
                ChangeAnimationState(attackOneHand2);
                attackCountOne = 0;
            }
        }
        else if(animator.GetBool("Holding 2"))
        {
            if (attackCountTwo == 0)
            {
                ChangeAnimationState(attackTwoHand1);
                attackCountTwo++;
            }
            else
            {
                ChangeAnimationState(attackTwoHand2);
                attackCountTwo = 0;
            }
        }

    }

    void ResetAttack()
    {
        attacking = false;
        readyToAttack = true;
    }

    void AttackRaycast()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, attackDistance, attackLayer))
        {
            //hit.transform.parent.GetComponent<Health>().ModifyHealth(-15);
            //HitTarget(hit.point);

            print(idealHit.value);
            print(hit.transform.gameObject.layer);

            if(idealHit == hit.transform.gameObject.layer)
            {
                if (hit.transform.TryGetComponent(out Health itself)) itself.ModifyHealth(-attackDamage);
                else if (hit.transform.parent.TryGetComponent(out Health parent)) parent.ModifyHealth(-attackDamage);
            }
            else
            {
                if (hit.transform.TryGetComponent(out Health itself)) itself.ModifyHealth(-1);
                else if (hit.transform.parent.TryGetComponent(out Health parent)) parent.ModifyHealth(-1);
            }
        }
    }

    void SetAnimations()
    {
        // If player is not attacking
        if (!attacking)
        {
            if (animator.GetBool("Holding"))
            {
                ChangeAnimationState("Arms holding");
            }
            else if (animator.GetBool("Holding 2"))
            {
                ChangeAnimationState("Arms holding 2");
            }
        }
    }

    void OnEnable()
    { input.Enable(); }

    void OnDisable()
    { input.Disable(); }

    void HitTarget(Vector3 pos)
    {
        audioSource.pitch = 1;
        audioSource.PlayOneShot(hitSound);

        GameObject GO = Instantiate(hitEffect, pos, Quaternion.identity);
        Destroy(GO, 20);
    }

    void AssignInputs()
    {
        input.Attack.started += ctx => Attack();
    }
}
