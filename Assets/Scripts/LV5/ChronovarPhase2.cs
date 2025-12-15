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
        chronovar.AirMove();
        HandleAttackLogic();
    }

    void HandleAttackLogic()
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0)
        {
            float dist = Vector2.Distance(chronovar.transform.position, chronovar.player.position);
            int AttackDecision = Random.Range(0, NumberOfAttacks);            
                switch (AttackDecision)
                {
                    case 0:
                        chronovar.anim.SetTrigger("Breath");
                        chronovar.Phase2Attack1();
                        break;

                    case 1:
                        chronovar.anim.SetTrigger("DiveBomb");
                        chronovar.Phase2Attack2();
                        break;

                    default:
                        chronovar.Phase2Attack1();
                        break;
                }
            }
            attackTimer = attackCooldown;
        }


    public override void Exit()
    {
    }
}