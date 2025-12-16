using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solider : NewEnemyController
{
    private bool facingRight;
    private Vector3 stopPosition;
    private bool hasDetectedPlayer = false;
    private bool stopped = false;
    public float attackDuration = 0.5f;
    private float attackTimer = 0f;

    void Start()
    {
        base.Start();
        stopped = false;
        facingRight = !spriteRenderer.flipX;
    }

    void Update()
    {
        base.Update();

        // Attack duration timer
        if (isAttacking)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0f)
            {
                isAttacking = false;
                hasDealtDamage = false;
                rb.velocity = Vector2.zero;
            }
        }
    }

    public override void MeleeAttack()
    {
        anim.SetTrigger("Attack");
        isAttacking = true;
        hasDealtDamage = false;
        attackTimer = attackDuration;
    }

    public override void MoveTowardsPlayer(float distance)
    {
        if (!hasDetectedPlayer && distance <= detectionRange)
        {
            stopPosition = transform.position;
            hasDetectedPlayer = true;
        }

        if (stopped) { rb.velocity = Vector2.zero; return; }

        if (Vector2.Distance(transform.position, stopPosition) > 0.05f)
        {
            Vector2 direction = (stopPosition - transform.position).normalized;
            rb.velocity = direction * moveSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero;
            stopped = true;
        }
    }

    public override void FlipSprite() { /* do nothing */ }

    public override void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        // Player damages soldier
        if (playerController.isAttacking && !hasRecentlyTakenDamage)
        {
            Vector2 attackDir = (player.position - transform.position).normalized;
            if ((facingRight && attackDir.x < 0) || (!facingRight && attackDir.x > 0))
            {
                TakeDamage(playerController.AttackDamage);
                StartCoroutine(DamageIntakeCooldownRoutine());
            }
        }

        // Soldier damages player
        if (isAttacking && !hasDealtDamage)
        {
            playerController.TakeDamage(attackDamage);
            hasDealtDamage = true;
        }
    }
public override void Die(){
    base.Die();
}
}
