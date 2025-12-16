using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chronovar : MonoBehaviour
{

    // Dragon stats
    private bool hasDealtDamage = false;
    public int maxHealth = 100;
    public bool isLanding = false;

    public int currentHealth;
    public float stoppingDistance = 22f;
    public float moveSpeed = 3f;
    public int attackDamage = 10;
    public float shortRange = 30f;
    public float midRange = 200f;
    public float flyHeight = 3f; // desired Y offset above player
    public bool isDead = false;
    public bool isAttacking = false;
    public bool hasRecentlyTakenDamage = false;
    public float damageIntakeCooldown = 0.3f; // player can only hit every 0.3s
    protected PlayerSuperclass playerController;
    public SpriteRenderer spriteRenderer;
    public bool phase3AttackStarted = false;

    public Animator anim;
    public Rigidbody2D rb;
    public ChronovarStateMachine stateMachine;
    public bool isFlying;
    public Transform player;
    public float hoverDistance = 12f;
    public float hoverTolerance = 2f;
    public GameObject lavaProjectilePrefab;
    public float lavaFallSpeed = 10f; // how fast it falls


    // Phase thresholds
    public float phase2Threshold = 0.6f; // 60% health
    public float phase3Threshold = 0.35f; // 35% health
    public bool isEnraged = false;
    public ChronovarFireBreath fireBreath;
    public Transform fireSpawnPoint;
    public float diveSpeed = 15f;
    public float impactRadius = 8f;
    public int diveBombDamage = 35;
    public LayerMask groundLayer; // bro needs access to layer :SOb:
    public float groundCheckDistance = 5f;
    public bool isGrounded = false;
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
        if (isAttacking)
        {
            rb.velocity = Vector2.zero;
        }
    }

    // MOVEMENT  LOGIC
    public IEnumerator FireBreathRoutine()
    {
        anim.SetTrigger("Breath");

        isAttacking = true;

        fireBreath.ActivateBreath();

        yield return new WaitForSeconds(1.2f); // breath duration

        isAttacking = false;
    }


    private void UpdateMovementAnim()
    {
        // If Chronovar is moving , play moving animation
        anim.SetBool("isMoving", rb.velocity.magnitude > 0.05f); // value of the velocity > 0.05f -> moves else howa idle
    }
    public void GroundMove()
    {
        if (!player) return;

        // STOP moving 
        if (isAttacking || isLanding)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        float direction;
        if ((player.position.x - transform.position.x) > 0) { direction = 1f; }
        else { direction = -1f; }

        spriteRenderer.flipX = direction > 0; // face left if player is on left   

        rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y); // keep current Y velocity (gravity)
    }

    public void AirMove()
    {
        if (!player || isLanding || isAttacking) return;

        Vector2 target = new Vector2(player.position.x, player.position.y + flyHeight);
        Vector2 toTarget = target - (Vector2)transform.position;
        float distance = toTarget.magnitude;

        if (distance < hoverDistance - hoverTolerance)
        {
            rb.velocity = -toTarget.normalized * moveSpeed;
        }
        // TOO FAR
        else if (distance > hoverDistance + hoverTolerance)
        {
            rb.velocity = toTarget.normalized * moveSpeed;
        }
        // IDEAL ZONE 
        else
        {
            rb.velocity = Vector2.zero;
        }

        spriteRenderer.flipX = toTarget.x > 0;
        anim.SetBool("isFlying", true);
    }
    // PHASE 1 ATTACKS
    public IEnumerator LandRoutine()
    {
        isLanding = true;
        isFlying = false;
        isAttacking = true;

        rb.gravityScale = 1f;
        anim.SetTrigger("Land");
        //  downward movement
        rb.velocity = Vector2.down * 12f;

        // Wait until ground hit
        while (!isGrounded)
        {
            yield return null;
        }

        // Clamp cleanly
        rb.velocity = Vector2.zero;
        rb.gravityScale = 1f;

        anim.SetBool("isFlying", false);

        isLanding = false;
        isAttacking = false;
    }

    public void Phase1Attack1()
    {
        Debug.Log("Chronovar Boss Phase 1 Attack 2 executed. CHRONO LUNGE ");
        StartCoroutine(StopAttack(4f));
    }
    public void Phase1Attack2()
    {
        Debug.Log("Chronovar Boss Phase 1 Attack 2 executed. Tail SWEEP");
        StartCoroutine(StopAttack(5f));
    }


    // PHASE 2 ATTACKS


    public void Phase2Attack1()
    {
        Debug.Log("Cdragon BREATHH");

        StartCoroutine(StopAttack(5f));
    }
    public void Phase2Attack2()
    {
        Debug.Log("Chronovar Dive Bomb");

        StartCoroutine(DiveBombRoutine());
    }
    public IEnumerator DiveBombRoutine()
    {
        isAttacking = true;
        isLanding = true;
        rb.velocity = Vector2.zero;

        // Hover above player
        Vector2 hoverPos = new Vector2(player.position.x, player.position.y + 2f); // tweak hover
        float hoverSpeed = 3f;
        while ((Vector2)rb.position != hoverPos)
        {
            rb.MovePosition(Vector2.MoveTowards(rb.position, hoverPos, hoverSpeed * Time.fixedDeltaTime));
            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(0.2f); // slight telegraph

        // Dive down
        Vector2 diveTarget = new Vector2(player.position.x, player.position.y); // dive target
        float diveSpeedLocal = 8f;
        while ((Vector2)rb.position != diveTarget)
        {
            rb.MovePosition(Vector2.MoveTowards(rb.position, diveTarget, diveSpeedLocal * Time.fixedDeltaTime));
            yield return new WaitForFixedUpdate();
        }

        anim.SetTrigger("DiveBomB");

        // Slight pause on the ground
        yield return new WaitForSeconds(0.8f);

        // Reset state
        isAttacking = false;
        isLanding = false;
    }



    public IEnumerator Phase3Attack1Routine()
    {
        if (isAttacking) yield break;   // safety lock

        isAttacking = true;
        isFlying = false;

        anim.SetTrigger("Wrath");

        yield return new WaitForSeconds(0.5f);

        // SPAWN ONE LAVA
        Vector2 spawnPos = new Vector2(player.position.x, player.position.y + 5f);
        GameObject lava = Instantiate(
            lavaProjectilePrefab,
            spawnPos,
            Quaternion.Euler(0f, 0f, 90f)
        );

        Rigidbody2D lavaRb = lava.GetComponent<Rigidbody2D>();
        lavaRb.velocity = Vector2.down * lavaFallSpeed;

        Destroy(lava, 5f);

        // WAIT A BIT FOR IMPACT FEEL
        yield return new WaitForSeconds(0.6f);

        // 🔥 THIS IS WHAT YOU WERE MISSING
        yield return StartCoroutine(LandRoutine());

        // HARD LOCK: NEVER FLY AGAIN
        isFlying = false;
        rb.gravityScale = 1f;

        isAttacking = false;
    }


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
    public void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        // Player damages Chronovar
        if (playerController.isAttacking && !hasRecentlyTakenDamage)
        {
            TakeDamage(playerController.AttackDamage);
            StartCoroutine(DamageIntakeCooldown());
        }

        // Chronovar damages Player (ONCE per attack)
        if (!isAttacking || hasDealtDamage) return;

        playerController.TakeDamage(attackDamage);
        hasDealtDamage = true;
    }


    public IEnumerator StopAttack(float duration)
    {
        isAttacking = false;
        yield return new WaitForSeconds(duration);
        anim.SetTrigger("Phase2");

    }
    public IEnumerator Phase1TailRoutine()
    {
        isAttacking = true;
        rb.velocity = Vector2.zero;

        anim.SetTrigger("Tail");

        yield return new WaitForSeconds(1f); // duration of tail sweep

        isAttacking = false;
    }

    public IEnumerator Phase1LungeRoutine()
    {
        isAttacking = true;
        rb.velocity = Vector2.zero;

        anim.SetTrigger("Lunge");

        yield return new WaitForSeconds(2f); // duration of lunge

        isAttacking = false;
    }

    /*TBD - > make it so that chronovar goes down,
    dive bomb, 2 fire prefab -> one lava and one is breath along with lava ground,
    fire done
    linking animations is kinda done
    divebomb 80% done
    timer, loop
    phase 3 uh

    timer coroutine ->ez actually
    link some animations,
 phase 1 finished
 phase 2 needs logic to go down
 phase 3 will just be a random check! MYFN3SH
    */
}
