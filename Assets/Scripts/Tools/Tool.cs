using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tool : MonoBehaviour
{
    Animator animator;
    BoxCollider boxCollider;
    public string optimalHit;
    bool canSwing;

    public GameObject damageText;
    int damage;
    bool crit;
    
    public int maxDamage;
    public int minDamage;

    public AudioSource swing;

    private void Awake()
    {
        animator = transform.GetComponent<Animator>();
        boxCollider = transform.GetComponent<BoxCollider>();

        boxCollider.enabled = false;
        canSwing = true;
    }

    private void OnEnable()
    {
        boxCollider.enabled = false;
        canSwing = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canSwing)
        {
            animator.SetTrigger("Swing");
            canSwing = false;
        }
    }

    public void woosh()
    {
        swing.Play();
    }

    public void EnableSwing()
    {
        canSwing = true;
    }

    public void AttackStart()
    {
        boxCollider.enabled = true;
    }

    public void AttackEnd()
    {
        boxCollider.enabled = false;
    }

    //maybe sometimes hits twice, add a boolean or test it first idc
    private void OnTriggerEnter(Collider collision)
    {
        /*if (collision.transform.name.Contains("LOD0") && collision.transform.parent.GetComponent<Health>() != null)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 4))

                if (collision.transform.parent.name.ToLower().Contains(optimalHit))
                {
                    damage = UnityEngine.Random.Range(minDamage, maxDamage);
                    crit = UnityEngine.Random.Range(1, 10) >= 9;
                    if (crit) damage *= 2;

                    //print("Hit " + damage + " optimalhit");
                    //print(collision.transform.parent.name);
                    collision.transform.parent.GetComponent<Health>().ModifyHealth(-damage);
                }
                else
                {
                    damage = UnityEngine.Random.Range(1, 5);

                    collision.transform.parent.GetComponent<Health>().ModifyHealth(-damage);
                    //print("non optimal");
                }

            DamageIndicator indicator = Instantiate(damageText, hit.point, Quaternion.identity).GetComponent<DamageIndicator>();
            indicator.SetDamageText(damage, crit);
        }*/
        try { OnHit(collision.transform.parent.GetComponent<Health>(), collision.transform.parent.GetComponent<ParticleHolder>(), collision.transform.name, "LOD0"); } catch { }
        try { OnHit(collision.transform.GetComponent<Health>(), collision.transform.GetComponent<ParticleHolder>(), collision.transform.name, "Animate"); } catch { }
    }

    private void OnHit(Health health, ParticleHolder particalHolder, string name, string optimalName)
    {
        if (name.Contains(optimalName) && health != null)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 4))

                if (name.ToLower().Contains(optimalHit.ToLower()))
                {
                    damage = UnityEngine.Random.Range(minDamage, maxDamage);
                    crit = UnityEngine.Random.Range(1, 10) >= 9;
                    if (crit) damage *= 2;

                    //print("Hit " + damage + " optimalhit");
                    //print(collision.transform.parent.name);
                    health.ModifyHealth(-damage);
                }
                else
                {
                    damage = UnityEngine.Random.Range(1, 5);

                    health.ModifyHealth(-damage);
                    //print("non optimal");
                }

            Instantiate(particalHolder.ParticleHit, hit.point, Quaternion.identity);
            DamageIndicator indicator = Instantiate(damageText, hit.point, Quaternion.identity).GetComponent<DamageIndicator>();
            indicator.SetDamageText(damage, crit);

            particalHolder.sound.Play();
        }
    }
}