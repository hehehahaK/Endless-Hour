using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewEnemyController : MonoBehaviour
{
    protected bool hasDealtDamage = false;

    public int maxHealth = 20;
    public int currentHealth = 20;
    public PlayerSuperclass playerController;
    public float moveSpeed = 3f;
    public float stoppingDistance = 1.5f;
    public float detectionRange = 10f;
    public float attackRange = 2f;
    public float attackCooldown = 1f;
    public bool isArcher;
    public float nextAttackTime = 0f;
    public Transform player;
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    public bool isAttacking = false;
    public bool isDead = false;
    public Animator anim;
    public int attackDamage = 5;
    protected bool hasRecentlyTakenDamage = false;
    public float damageIntakeCooldown = 0.3f;

    protected IEnumerator DamageIntakeCooldownRoutine()
    {
        hasRecentlyTakenDamage = true;
        yield return new WaitForSeconds(damageIntakeCooldown);
        hasRecentlyTakenDamage = false;
    }

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

    public virtual void Update()
    {
        if (isDead) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (!isAttacking && distanceToPlayer <= detectionRange)
        {
            MoveTowardsPlayer(distanceToPlayer);
            FlipSprite();
        }

        if (!isAttacking && distanceToPlayer <= attackRange && Time.time >= nextAttackTime)
        {
            Attack();
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    public virtual void FlipSprite()
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
            isAttacking = true;
            hasDealtDamage = false;
            MeleeAttack();
        }
    }

    public virtual void RangeAttack() { }
    public virtual void MeleeAttack() { }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
            Die();
    }

    public virtual void Die()
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
            rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y); // freeze Y movement
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y); // stop horizontal movement
        }
    }

    public virtual void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        // player damages enemy
        if (playerController.isAttacking && !hasRecentlyTakenDamage)
        {
            TakeDamage(playerController.AttackDamage);
            StartCoroutine(DamageIntakeCooldownRoutine());
        }
    }
}
