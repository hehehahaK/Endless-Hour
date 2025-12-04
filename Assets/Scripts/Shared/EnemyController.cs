
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int maxHealth = 20;
    public GameObject deathEffect;
    private float currentHealth;
    public float moveSpeed = 3f;

    private Collider2D weaponHitbox;
    public float stoppingDistance = 1.5f;
    public float detectionRange = 10f;
    public float attackRange = 2f;
    public float attackCooldown = 1f;
    public GameObject attackEffect;
    public Transform attackPoint;
    private float nextAttackTime = 0f;

    public Transform player;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool isDead = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        weaponHitbox = transform.Find("WeaponHitbox").GetComponent<Collider2D>();
        if (weaponHitbox != null)
        {
            weaponHitbox.enabled = false;
        }
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            MoveTowardsPlayer(distanceToPlayer);
            FlipSprite();

            if (distanceToPlayer <= attackRange && Time.time >= nextAttackTime)
            {
                Attack();
                nextAttackTime = Time.time + attackCooldown;
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void MoveTowardsPlayer(float distance)
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

    private void FlipSprite()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = player.position.x < transform.position.x;
        }
    }

    public void Attack()
    {
        // slash/whatever weapon effect
        if (attackEffect != null && attackPoint != null)
        {
            Instantiate(attackEffect, attackPoint.position, attackPoint.rotation);
        }

        // Make weapon hitbox active just for a bit so it doesnt keep hitting without attack
        if (weaponHitbox != null)
        {
            weaponHitbox.enabled = true;
            Invoke("DisableWeaponHitbox", 0.5f);
        }
    }
    private void DisableWeaponHitbox()
    {
        if (weaponHitbox != null)
            weaponHitbox.enabled = false;
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
        }

        rb.velocity = Vector2.zero;
        Destroy(gameObject, 0.1f);
    }

}