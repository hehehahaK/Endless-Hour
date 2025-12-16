using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChronovarPhase2 : ChronovarState
{

    public int NumberOfAttacks = 2;
    private float attackCooldown = 2.5f;
    private float attackTimer = 0f;

    public override void EnterState()
    {
        chronovar.isFlying = true;
        Debug.Log("Entered Chronovar Phase 2");
        chronovar.anim.SetTrigger("Phase2");
        attackTimer = attackCooldown;
    }

    public override void UpdateState()
    {
        if (!chronovar.isAttacking && !chronovar.isLanding)
        {
            chronovar.AirMove();
        }

        HandleAttackLogic();
    }


    void HandleAttackLogic()
    {
        if (chronovar.isAttacking) return;

        attackTimer -= Time.deltaTime;
        if (attackTimer > 0) return;

        int AttackDecision = Random.Range(0, NumberOfAttacks);

        switch (AttackDecision)
        {
            case 0:
                StartCoroutine(chronovar.FireBreathRoutine());

                break;
            case 1:
                StartCoroutine(chronovar.DiveBombRoutine());
                break;
        }

        attackTimer = attackCooldown; // Reset  starting attack
    }


    public override void Exit()
    {
        chronovar.StartCoroutine(chronovar.LandRoutine());
    }

}