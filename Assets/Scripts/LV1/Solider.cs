using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solider : NewEnemyController
{
    // Start is called before the first frame update
    bool facingRight;
    private Vector3 stopPosition; // where soldier will stop moving
    private bool hasDetectedPlayer = false;
    private bool stopped;
    void Start()
    {
        stopped = false;
        base.Start();
        facingRight = !spriteRenderer.flipX;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
    public override void MeleeAttack()
    {
        anim.SetTrigger("Attack");

    }
    public override void MoveTowardsPlayer(float distance)
    {
        // First detection: set the stop point
        if (!hasDetectedPlayer && distance <= detectionRange)
        {
            stopPosition = transform.position; // record current position
            hasDetectedPlayer = true;
        }

        // If already stopped, do nothing
        if (stopped)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        // Move toward stopPosition
        if (Vector2.Distance(transform.position, stopPosition) > 0.05f)
        {
            Vector2 direction = (stopPosition - transform.position).normalized;
            rb.velocity = direction * moveSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero;
            stopped = true; // freeze movement  forever
        }
    }
    public override void FlipSprite()
    {
    }
    public override void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        //player dmg enemy
        if (playerController.isAttacking && !hasRecentlyTakenDamage)
        {
            Vector2 attackDirection = (player.position - transform.position).normalized;
            if (facingRight && attackDirection.x > 0)
                return; // attack is from front, ignore

            // For left-facing soldier
            if (!facingRight && attackDirection.x < 0)
                return; // attack is from front, ignore

            TakeDamage(playerController.AttackDamage);
            StartCoroutine(DamageIntakeCooldown());
        }

        if (collision.gameObject.CompareTag("Player") && isAttacking)
        {
            playerController.TakeDamage(attackDamage);
        }
        isAttacking = false;

    }

}
