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
    }

    public override void UpdateState()
    {
        float dist = Vector2.Distance(chronovar.transform.position, chronovar.player.position);

        // Only move if NOT attacking AND beyond stopping distance
        if (!chronovar.isAttacking && dist > chronovar.stoppingDistance)
        {
            chronovar.GroundMove();
        }
        else
        {
            // Stop movement when close enough or attacking
            chronovar.rb.velocity = Vector2.zero;
        }

        HandleAttackLogic();
    }

    public void HandleAttackLogic()
    {
        // Don't start a new attack if already attacking
        if (chronovar.isAttacking) return;

        attackTimer -= Time.deltaTime;
        
        if (attackTimer <= 0)
        {
            float dist = Vector2.Distance(chronovar.transform.position, chronovar.player.position);
            
            // Attack if within short range
            if (dist <= chronovar.shortRange)
            {
                int AttackDecision = Random.Range(0, NumberOfAttacks);
                
                switch (AttackDecision)
                {
                    case 0:
                        chronovar.anim.SetTrigger("Bite");
                        chronovar.Phase1Attack1();
                        break;

                    case 1:
                        chronovar.anim.SetTrigger("Tail");
                        chronovar.Phase1Attack2();
                        break;

                    case 2:
                        chronovar.anim.SetTrigger("Lunge");
                        chronovar.Phase1Attack3();
                        break;

                    default:
                        chronovar.anim.SetTrigger("Bite");
                        chronovar.Phase1Attack1();
                        break;
                }
                
                attackTimer = attackCooldown;
            }
        }
    }
}