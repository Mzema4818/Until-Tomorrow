using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using TMPro;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attacking")]
    public bool canAttack;
    public float attackDistance = 3f;
    public float attackDelay = 0.4f;
    public float attackSpeed = 1f;
    public int attackDamage = 1;
    public LayerMask idealHit;
    public LayerMask treeHit;

    public AudioSource audioSource;
    public GameObject hitEffect;
    public AudioClip swingSound;

    public bool shouldAttack = true;
    bool attacking = false;
    bool readyToAttack = true;
    int attackCount;

    [Header("Animations")]
    public Animator animator;
    public GameObject[] heldItems;
    string currentAnimationState;
    public bool heldHand; //false = 1 hand, true = 2 hand
    public string ATTACK1 = "Attack 1";
    public string ATTACK2 = "Attack 2";

    [Header("Camera")]
    public Camera cam;
    PlayerInput playerInput;
    PlayerInput.MainActions input;

    private void Awake()
    {
        audioSource = transform.root.GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        playerInput = new PlayerInput();
        input = playerInput.Main;
        AssignInputs();
    }

    void Update()
    {
        //Dont think i need this because of AssignInputs()
        /*if (canAttack && input.Attack.IsPressed())
        {
            print(canAttack);
            Attack();
            //print("hi");
        }*/
    }

    public void Attack()
    {
        if (!shouldAttack || !readyToAttack || attacking) return;

        readyToAttack = false;
        attacking = true;

        Invoke(nameof(ResetAttack), attackSpeed);
        Invoke(nameof(AttackRaycast), attackDelay);

        //audioSource.pitch = Random.Range(0.9f, 1.1f);
        //audioSource.PlayOneShot(swordSwing);

        if (attackCount == 0)
        {
            ChangeAnimationState(ATTACK1);
            attackCount++;
        }
        else
        {
            ChangeAnimationState(ATTACK2);
            attackCount = 0;
        }
    }

    void ResetAttack()
    {
        attacking = false;
        readyToAttack = true;
    }

    void AttackRaycast()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, attackDistance, idealHit))
        {
            //HitTarget(hit.point, hit.transform.parent.GetComponent<ParticleHolder>());
            ParticleHolder particle = hit.transform.parent.GetComponent<ParticleHolder>();
            if(particle != null) ParticleHit(hit.transform.parent.GetComponent<ParticleHolder>());

            if (hit.transform.gameObject.layer == treeHit) StartCoroutine(ScaleTreeEffect(hit.transform.parent.localScale, hit.transform.parent.gameObject));

            if (hit.transform.parent.TryGetComponent(out Health T))
            { T.ModifyHealth(-attackDamage); }

            audioSource.PlayOneShot(hit.transform.parent.GetComponent<ParticleHolder>().sound);
        }
        else if(canAttack)
        {
            audioSource.PlayOneShot(swingSound);
        }
    }

    private IEnumerator ScaleTreeEffect(Vector3 originalScale, GameObject hit)
    {
        float scaleUpFactor = 0.95f; //Scale factor when the tree grows
        float scaleDuration = 0.1f; //Duration of the scale effect

        Vector3 targetScale = originalScale * scaleUpFactor;
        float timeElapsed = 0f;

        // Smoothly scale up
        while (timeElapsed < scaleDuration)
        {
            //Check if the object is destroyed
            if (hit == null) yield break; //Exit the coroutine if the object is destroyed

            hit.transform.localScale = Vector3.Lerp(originalScale, targetScale, timeElapsed / scaleDuration);
            timeElapsed += Time.deltaTime;
            yield return null; //Wait until next frame
        }

        //Ensure the final scale is set exactly
        if (hit != null) hit.transform.localScale = targetScale;

        // Wait for the scale-up effect to finish before returning to original size
        yield return new WaitForSeconds(0.1f);

        //Smoothly scale back to the original size
        timeElapsed = 0f;
        while (timeElapsed < scaleDuration)
        {
            //Check if the object is destroyed
            if (hit == null) yield break; //Exit the coroutine if the object is destroyed

            hit.transform.localScale = Vector3.Lerp(targetScale, originalScale, timeElapsed / scaleDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        //Ensure the scale is exactly the original size
        if (hit != null) hit.transform.localScale = originalScale;
    }

    void OnEnable()
    { input.Enable(); }

    void OnDisable()
    { input.Disable(); }

    void HitTarget(Vector3 pos, ParticleHolder particleHolder)
    {
        //audioSource.pitch = 1;
        //audioSource.PlayOneShot(hitSound);
        //Instantiate(particleHolder.ParticleHit, pos, Quaternion.identity);

        //GameObject GO = Instantiate(hitEffect, pos, Quaternion.identity);
        //GO.GetComponentInChildren<TextMeshProUGUI>().text = attackDamage.ToString();
        //Destroy(GO, 20);
    }

    public void ParticleHit(ParticleHolder particleHolder)
    {
        //Get the center of the screen
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);

        //Cast a ray from the camera through the screen center
        Ray ray = cam.ScreenPointToRay(screenCenter);
        RaycastHit hit;

        //Perform the raycast
        if (Physics.Raycast(ray, out hit))
        {
            //Get the hit position and normal
            Vector3 hitPosition = hit.point;
            Vector3 hitNormal = hit.normal;

            //Instantiate the particle effect at the hit position
            GameObject hitEffect = Instantiate(particleHolder.ParticleHit, hitPosition, Quaternion.identity);

            //Align the particle effect with the surface normal
            hitEffect.transform.rotation = Quaternion.LookRotation(hitNormal);

            //Optionally add a slight offset so particles aren't inside the object
            hitEffect.transform.position += hitNormal * 0.01f;
        }
    }




    public void ChangeAnimationState(string newState)
    {
        // STOP THE SAME ANIMATION FROM INTERRUPTING WITH ITSELF //
        if (currentAnimationState == newState) return;

        // PLAY THE ANIMATION //
        currentAnimationState = newState;
        animator.CrossFadeInFixedTime(currentAnimationState, 0.2f);
    }

    void AssignInputs()
    {
        input.Attack.started += ctx => Attack();
    }

    public void SwingSound()
    {
        //might need this later, if i switch to a weapon mid swing, it still plays the sound
    }

}
