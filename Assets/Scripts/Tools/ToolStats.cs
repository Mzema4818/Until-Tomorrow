using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolStats : MonoBehaviour
{
    [Header("Attacking Stats")]
    public bool canAttack;
    public float attackDistance = 3f;
    public float attackDelay = 0.4f;
    public float attackSpeed = 1f;
    public int attackDamage = 1;
    public LayerMask idealHit;

    [Header("Animations")]
    public bool heldHand; //false = 1 hand, true = 2 hand
    public string idleName = "";
    public string attackName = "";
    public string attackName2 = "";

    [Header("Arms")]
    public PlayerAttack playerAttack;

    private void OnEnable()
    {
        playerAttack.canAttack = canAttack;
        playerAttack.attackDistance = attackDistance;
        playerAttack.attackDelay = attackDelay;
        playerAttack.attackSpeed = attackSpeed;
        playerAttack.attackDamage = attackDamage;
        playerAttack.idealHit = idealHit;

        playerAttack.animator.ResetTrigger(idleName);
        playerAttack.animator.SetTrigger(idleName);
        playerAttack.heldHand = heldHand;
        playerAttack.ATTACK1 = attackName;
        playerAttack.ATTACK2 = attackName2;
    }
}
