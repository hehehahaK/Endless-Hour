using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChronovarPhase1 : ChronovarState
{
    public int NumberOfAttacks = 3;
    private float attackCooldown = 3f;
    private float attackTimer = 0f;
    // Start is called before the first frame update
    public override void EnterState()
    {
        Debug.Log("Entered Chronovar Phase 1");
        attackTimer = attackCooldown;
    }
    public override void UpdateState()
    {
        chronovar.MoveTowardsPlayer();

        HandleAttackLogic();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void HandleAttackLogic()
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer > 0) return;
        float dist = Vector2.Distance(chronovar.transform.position, chronovar.player.position);
        int AttackDecision = Random.Range(0, NumberOfAttacks); // Randomly choose an attack from 0 to 2
        if (dist < chronovar.shortRange)
        {
            switch (AttackDecision)
            {
                case 0:
                    chronovar.anim.SetTrigger("Attack_Bite");
                    chronovar.Phase1Attack1();
                    break;

                case 1:
                    chronovar.anim.SetTrigger("Attack_TailSwipe");
                    chronovar.Phase1Attack2();
                    break;

                case 2:
                    chronovar.anim.SetTrigger("Attack_Lunge");
                    chronovar.Phase1Attack3();
                    break;
                default:
                    chronovar.anim.SetTrigger("Attack_Bite");
                    chronovar.Phase1Attack1();
                    break;
            }
        }
        attackTimer = attackCooldown; // reset cooldown

    }



}
