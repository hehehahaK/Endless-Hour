using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lvl3playercontroller : PlayerSuperclass
{
    // Stairs Variables
    public bool isOnStairs = false;
    public float stairClimbBias = 4f;
    public float stairSpeedMultiplier = 0.2f;
    private float defaultGravity;

    // Combat Variables
    public bool hasDaggers = true; 
    private HashSet<GameObject> hitThisAttack = new HashSet<GameObject>(); 

    void Start()
    {
        base.Start();
        defaultGravity = rb.gravityScale;
    }

    void Update()
    {
        // 1. GRAVITY FIX
        if (isOnStairs) rb.gravityScale = 0f;
        else rb.gravityScale = defaultGravity;

        if (anim != null) anim.SetBool("HasDaggers", hasDaggers);

        // 2. ATTACK INPUT
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            PerformAttack();
        }

        // 3. JUMP
        if (Input.GetKeyDown(Spacebar) && grounded) Jump();

        // 4. MOVEMENT
        if (Input.GetKey(RunKey)){
           currentSpeed = moveSpeed*2;
        }
        else{
            currentSpeed = moveSpeed;
        }

        
        float yVelocity = rb.velocity.y;
        if (isOnStairs)
        {
            currentSpeed = moveSpeed;
            if (Input.GetKey(R)) yVelocity = stairClimbBias;
            else if (Input.GetKey(L)) yVelocity = -stairClimbBias;
            else yVelocity = 0;
        }

        if (Input.GetKey(L))
        {
            rb.velocity = new Vector2(-currentSpeed, yVelocity);
            if (sr != null) sr.flipX = true;
        }
        else if (Input.GetKey(R))
        {
            rb.velocity = new Vector2(currentSpeed, yVelocity);
            if (sr != null) sr.flipX = false;
        }
        else if (!Input.GetKey(L) && !Input.GetKey(R) && !isOnStairs)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else if (isOnStairs)
        {
            rb.velocity = new Vector2(0, yVelocity);
        }

        // --- 5. IMPORTANT: IMMUNITY TIMER (THIS WAS MISSING) ---
        if (isImmune)
        {
            SpriteFlicker();
            immunityTime += Time.deltaTime;
            if (immunityTime >= immunityDuration)
            {
                isImmune = false; // Player can take damage again
                sr.enabled = true; // Make sure sprite is visible
            }
        }
        

        

        // Animation Updates
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("Height", rb.velocity.y);
        anim.SetBool("Grounded", grounded);
        anim.SetBool("IsRunning", Input.GetKey(RunKey));  
    }

    void PerformAttack()
    {
        isAttacking = true;
        hitThisAttack.Clear(); 

        int randomAttack = Random.Range(0, 2); 
        if (randomAttack == 0) anim.SetTrigger("topstablvl3");
        else anim.SetTrigger("firehitlvl3");

        StartCoroutine(EndAttack());
    }

    IEnumerator EndAttack()
    {
        yield return new WaitForSeconds(attackDuration);
        isAttacking = false;
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    // --- COLLISION LOGIC ---
    void OnTriggerStay2D(Collider2D collision)
    {
        if (isAttacking) 
        {
            royalguard guard = collision.GetComponent<royalguard>();
            
            if (guard != null)
            {
                if (!hitThisAttack.Contains(collision.gameObject))
                {
                    guard.TakeDamage(AttackDamage);
                    hitThisAttack.Add(collision.gameObject);
                    Debug.Log("Hit Guard via Collision!");
                }
            }
        }
    }
}