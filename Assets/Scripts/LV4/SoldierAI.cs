using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAI : MonoBehaviour
{
    public Transform player;

    public float chaseSpeed = 3f;
    public float attackDistance = 1.5f;  // When soldier starts attacking
    public bool isActivated = false;

    public int damage = 10;
    public float attackCooldown = 1f;
    private float nextAttackTime = 0f;

    public int maxHealth = 1;   // dies in one hit
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
        if (!isActivated || isDead) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > attackDistance)
            RunTowardsPlayer();
        else
            AttackPlayer();
    }
    
    void RunTowardsPlayer()
    {
        anim.SetBool("isRunning", true);
        anim.SetBool("isAttacking", false);

        Vector2 direction = (player.position - transform.position).normalized;
        transform.position += (Vector3) direction * chaseSpeed * Time.deltaTime;

        // Flip sprite
        if (direction.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);
    }

    void AttackPlayer()
    {
        anim.SetBool("isRunning", false);
        anim.SetBool("isAttacking", true);

        if (Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + attackCooldown;

        // Damage the player (uses your existing script!)
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

    public void Die()
    {
        if (isDead) return;
        isDead = true;

        anim.SetBool("isRunning", false);
        anim.SetBool("isAttacking", false);
        anim.SetTrigger("Die");

        isActivated = false;

        // Stop physics
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Static;
        }

        // Disable collider so it can’t be triggered again
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