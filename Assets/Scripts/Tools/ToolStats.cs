using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolStats : MonoBehaviour
{
    [Header("Stats")]
    public float attackDistance = 3f;
    public float attackDelay = 0.4f;
    public float attackSpeed = 1f;
    public int attackDamage = 1;
    public LayerMask idealHit;

    [Header("Arms")]
    public PlayerAttack playerAttack;

    private void OnEnable()
    {
        playerAttack.attackDistance = attackDistance;
        playerAttack.attackDelay = attackDelay;
        playerAttack.attackSpeed = attackSpeed;
        playerAttack.attackDamage = attackDamage;
        playerAttack.idealHit = idealHit;
    }
}
