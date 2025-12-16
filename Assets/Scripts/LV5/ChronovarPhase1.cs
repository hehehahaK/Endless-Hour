using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChronovarPhase1 : ChronovarState
{
    public int NumberOfAttacks = 3;
    private float attackCooldown = 1f;
    private float attackTimer = 0f;

    public override void EnterState()
    {
        Debug.Log("Entered Chronovar Phase 1");
        attackTimer = attackCooldown;
        attackTimer = 0f;
    }

    public override void UpdateState()
    {
        float dist = Vector2.Distance(chronovar.transform.position, chronovar.player.position);

        // Only move if NOT attacking AND beyond stopping distance
        if (chronovar.isAttacking)
        {
            chronovar.rb.velocity = Vector2.zero;
        }
        else if (dist > chronovar.stoppingDistance)
        {
            chronovar.GroundMove();
        }
        else
        {
            chronovar.rb.velocity = Vector2.zero;
        }

        HandleAttackLogic(); // << ADD THIS
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
                chronovar.StartCoroutine(chronovar.Phase1TailRoutine());
                break;
            case 1:
                chronovar.StartCoroutine(chronovar.Phase1LungeRoutine());
                break;
        }

        attackTimer = attackCooldown;
    }


}