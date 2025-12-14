using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chronovar : MonoBehaviour
{
    // Dragon stats

    public int maxHealth = 100;
    public int currentHealth;
    public float stoppingDistance = 2f;
    public float moveSpeed = 3f;
    public int attackDamage = 10;
    public float shortRange = 3f;
    public float midRange = 6f;
    public float flyHeight = 3f; // desired Y offset above player
    public bool isDead = false;
    public bool isAttacking = false;
    public bool hasRecentlyTakenDamage = false;
    public float damageIntakeCooldown = 0.3f; // player can only hit every 0.3s
    protected PlayerSuperclass playerController;
    public SpriteRenderer spriteRenderer;
    public Animator anim;
    public Rigidbody2D rb;
    public ChronovarStateMachine stateMachine;
    public bool isFlying;
    public Transform player;

    // Phase thresholds
    public float phase2Threshold = 0.6f; // 60% health
    public float phase3Threshold = 0.35f; // 35% health
    public bool isEnraged = false;

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        stateMachine = GetComponent<ChronovarStateMachine>();
        playerController = player.GetComponent<PlayerSuperclass>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (currentHealth <= maxHealth * phase3Threshold && stateMachine.phase < 3)
        {
            stateMachine.ChangeState(stateMachine.phase3State);
            stateMachine.phase = 3;
        }
        else if (currentHealth <= maxHealth * phase2Threshold && stateMachine.phase < 2)
        {
            stateMachine.ChangeState(stateMachine.phase2State);
            stateMachine.phase = 2;
        }
        UpdateMovementAnim();

    }

    // MOVEMENT  LOGIC

    private void UpdateMovementAnim()
    {
        // If Chronovar is moving , play moving animation
        anim.SetBool("isMoving", rb.velocity.magnitude > 0.05f); // value of the velocity > 0.05f -> moves else howa idle
    }
    public void GroundMove()
    {
        if (!player) return;
        float direction;
        if ((player.position.x - transform.position.x) > 0) { direction = 1f; }
        else { direction = -1f; }
        spriteRenderer.flipX = direction > 0; // face left if player is on left   
        rb.velocity = new Vector2(direction * moveSpeed, 0f);
    }

    public void AirMove()
    {
        if (!player) return;

        Vector2 targetPos = new Vector2(player.position.x, player.position.y + flyHeight);
        Vector2 direction = (targetPos - (Vector2)transform.position).normalized;

        rb.velocity = direction * moveSpeed;

        spriteRenderer.flipX = direction.x > 0; // faces RIGHT when moving right  spriteRenderer.flipX = direction.x < 0;

        anim.SetBool("isFlying", true);
    }    
    // PHASE 1 ATTACKS
    public void Phase1Attack1() { Debug.Log("Chronovar Boss Phase 1 Attack 1 executed. chrono BITE"); }
    public void Phase1Attack2() { Debug.Log("Chronovar Boss Phase 1 Attack 2 executed. CHRONO LUNGE "); }
    public void Phase1Attack3() { Debug.Log("Chronovar Boss Phase 1 Attack 3 executed. Tail SWEEP"); }


    // PHASE 2 ATTACKS


    public void Phase2Attack1() { Debug.Log("Chronovar Boss Phase 2 Attack 1 executed. dragon BREATHH"); }
    public void Phase2Attack2() { Debug.Log("Chronovar Boss Phase 2 Attack 2 executed. dive BOMB"); }


    // PHASE 3 ATTACKS
    public void Phase3Attack1() { Debug.Log("Chronovar Boss Phase 3 Attack 1 executed. DRAGON WRATHH"); }

    public void TakeDamage(int damage)
    {

        if (isDead) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
            Die();
    }
    protected IEnumerator DamageIntakeCooldown() // dh cooldown l damage el enemy so that bro doesnt die in one sec
    {
        hasRecentlyTakenDamage = true;
        yield return new WaitForSeconds(damageIntakeCooldown);
        hasRecentlyTakenDamage = false;
    }
    public void Die()
    {
        isDead = true;

        rb.velocity = Vector2.zero;

        Destroy(gameObject, 0.1f);
    }
    public virtual void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        //player dmg enemy
        if (playerController.isAttacking && !hasRecentlyTakenDamage)
        {
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
