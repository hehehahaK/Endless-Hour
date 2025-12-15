using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NapoleonController : MonoBehaviour
{
    public Transform player;

    public float attackDistance = 2f;

    public int damage = 10;
    public float attackCooldown = 1.2f;
    private float nextAttackTime;

    public int maxHealth = 1;
    private int currentHealth;
    private bool isDead = false;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (isDead || player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= attackDistance)
        {
            AttackPlayer();
        }
        else
        {
            Idle();
        }
    }

    void Idle()
    {
        anim.SetBool("isAttacking", false);
    }

    void AttackPlayer()
    {
        anim.SetBool("isAttacking", true);

        if (Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + attackCooldown;

            PlayerSuperclass playerHealth = player.GetComponent<PlayerSuperclass>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }

    public void TakeDamage(int damageAmount)
    {
        if (isDead) return;

        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;

        anim.SetBool("isAttacking", false);
        anim.SetTrigger("Die");

        // Stop physics
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static;
        }

        // Disable collider
        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("PlayerWeapon"))
        {
            Die();
        }
    }
}

