using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class royalguard : NewEnemyController
{
    public bool canFight = false;
    private bool hasHitPlayerThisAttack = false;

    public override void Start()
    {
        // Call base start to ensure variables from the parent class are set up
        base.Start(); 
        
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;

        // Ensure we have the player script reference
        if (player != null && playerController == null)
        {
            playerController = player.GetComponent<PlayerSuperclass>();
        }

        nextAttackTime = 0f;
    }

    public override void Update()
    {
        if (!canFight || isDead) return;
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // 1. Movement Logic
        if (distanceToPlayer <= detectionRange)
        {
            MoveTowardsPlayer(distanceToPlayer);
            FlipSprite();
        }

        // 2. Start Attack Logic
        if (distanceToPlayer <= attackRange && Time.time >= nextAttackTime && !isAttacking)
        {
            MeleeAttack();
            nextAttackTime = Time.time + attackCooldown;
        }

        // 3. DAMAGE LOGIC (The Fix)
        // Instead of OnTriggerStay, we check:
        // A. Are we currently attacking?
        // B. Have we hit the player yet in this specific swing?
        // C. Is the player still close enough to get hit?
        if (isAttacking && !hasHitPlayerThisAttack && distanceToPlayer <= attackRange)
        {
            if (playerController != null)
            {
                playerController.TakeDamage(attackDamage);
                hasHitPlayerThisAttack = true; // Mark true so we don't hit 60 times a second
                Debug.Log("Guard hit player for " + attackDamage + " damage!");
            }
        }
    }

    public override void MeleeAttack()
    {
        if (isAttacking) return;

        isAttacking = true;
        hasHitPlayerThisAttack = false; // Reset hit flag for the new attack
        anim.SetBool("isAttacking", true);

        StartCoroutine(AttackDuration());
    }

    IEnumerator AttackDuration()
    {
        yield return new WaitForSeconds(0.3f); // Length of the attack animation
        anim.SetBool("isAttacking", false);
        isAttacking = false;
        // Do NOT reset hasHitPlayerThisAttack here, wait for the next MeleeAttack() call
    }

    // REMOVED OnTriggerStay2D entirely to prevent the bug

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        Debug.Log("Guard Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public override void Die()
    {
        if (isDead) return;
        isDead = true;
        rb.velocity = Vector2.zero;
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(FlashAndDie());
    }

    IEnumerator FlashAndDie()
    {
        for (int i = 0; i < 5; i++)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.clear;
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(gameObject);
    }
}