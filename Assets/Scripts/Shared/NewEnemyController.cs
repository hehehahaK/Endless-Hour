using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemyController : MonoBehaviour
{
    public int maxHealth = 20;
    public int currentHealth;
    public PlayerSuperclass playerController;
    public float moveSpeed = 3f;
    public float stoppingDistance = 1.5f;
    public float detectionRange = 10f;
    public float attackRange = 2f;
    public float attackCooldown = 1f;
    public bool isArcher;
    public float nextAttackTime = 0f; // plz work
    public Transform player;
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public bool isAttacking=true;
    public bool isDead = false;
    public Animator anim;
    public int attackDamage = 5;
    public virtual void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player == null)
        {
            Debug.LogError("Player not found! Assign the Player tag to your player object.");
            enabled = false;
            return;
        }
        playerController = player.GetComponent<PlayerSuperclass>();
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isDead) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            MoveTowardsPlayer(distanceToPlayer);
            FlipSprite();
        }

        if (distanceToPlayer <= attackRange && Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    public void FlipSprite()
    {
        if (spriteRenderer)
            spriteRenderer.flipX = player.position.x < transform.position.x;
    }

    public void Attack()
    {
        if (isArcher)
        {
            RangeAttack();
        }
        else
        {
            isAttacking=true;
            MeleeAttack();
        }
    }
    public virtual void RangeAttack()
    {
        // Ranged attack logic here
    }
    public virtual void MeleeAttack()
    {
        // Attack logic here
    }

    public void TakeDamage(int damage)
    {

        if (isDead) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
            Die();
    }

    public void Die()
    {
        isDead = true;

        rb.velocity = Vector2.zero;

        Destroy(gameObject, 0.1f);
    }

    public virtual void MoveTowardsPlayer(float distance)
    {
        if (distance > stoppingDistance)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (playerController.isAttacking)
            {
                TakeDamage(playerController.AttackDamage); // takes damage from player attack, should also do damage if they're attacking
         }
         
        }
    }
}

