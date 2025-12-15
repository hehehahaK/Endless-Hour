using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lvl3playercontroller : MonoBehaviour
{
    private float currentSpeed;
    public float moveSpeed;
    public float jumpHeight;
    public KeyCode Spacebar = KeyCode.Space;
    public KeyCode L = KeyCode.A;
    public KeyCode R = KeyCode.D;
    public KeyCode RunKey = KeyCode.LeftShift;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround; 
    private bool grounded;
    private Animator anim; 
    private SpriteRenderer sr; 
    private Rigidbody2D rb;

    // Health Variables
    public int health = 20;
    public int maxHealth = 20;
    private int normalAttackDamage = 5;
    private float BoostDuration = 30f;
    private float BoostTime = 0f;
    public int AttackDamage = 5;

    private float flickerTime = 0f;
    private float flickerDuration = 0.1f;
    public bool isImmune = false;
    public bool isBoosted = false;
    private float immunityTime = 0f;
    public float immunityDuration = 1.5f;

    // New variables
    public bool isOnStairs = false;
    private float stairClimbBias = 4f;
    public float stairSpeedMultiplier = 0.2f;

    // --- COMBAT VARIABLES ---
    public bool hasDaggers = false;      // The variable the dagger script sets to TRUE
    public bool isAttacking = false;     // To prevent button spamming
    public float attackDuration = 0.4f;  // How long the attack lasts

    void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // --- 1. THE MISSING LOGIC TO SWITCH ANIMATIONS ---
        // This tells the Animator: "Hey, the hasDaggers variable changed!"
        if (anim != null)
        {
            anim.SetBool("HasDaggers", hasDaggers);
        }

        // --- 2. ATTACK LOGIC (Left Click) ---
        // Only attack if we have daggers and aren't already attacking
        if (Input.GetMouseButtonDown(0) && hasDaggers && !isAttacking)
        {
            Attack();
        }

        // --- MOVEMENT LOGIC (Existing Code) ---
        float currentMoveSpeed;
        if (Input.GetKey(RunKey) && isOnStairs)
        {
            currentMoveSpeed = moveSpeed;
        }
        else {
            currentMoveSpeed = moveSpeed;
        }

        float yVelocity = rb.velocity.y;

        if (isOnStairs)
        {
            currentMoveSpeed = moveSpeed;

            if (Input.GetKey(R)) 
            {
                yVelocity = stairClimbBias; 
            }
            else if (Input.GetKey(L)) 
            {
                yVelocity = -stairClimbBias; 
            }
            else
            {
                yVelocity = 0; 
            }
        }

        if (Input.GetKey(L))
        {
            rb.velocity = new Vector2(-currentMoveSpeed, yVelocity);
            if (sr != null) { sr.flipX = true; }
        }
        else if (Input.GetKey(R))
        {
            rb.velocity = new Vector2(currentMoveSpeed, yVelocity);
            if (sr != null) { sr.flipX = false; }
        }
        else if (!Input.GetKey(L) && !Input.GetKey(R) && !isOnStairs)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else if (isOnStairs)
        {
            rb.velocity = new Vector2(0, yVelocity);
        }

        if (Input.GetKeyDown(Spacebar) && grounded)
        { Jump(); }
        
        // --- BUFFS ---
        if (isImmune == true)
        {
            SpriteFlicker();
            immunityTime += Time.deltaTime;
            if (immunityTime >= immunityDuration)
            {
                isImmune = false;
                sr.enabled = true;
            }
        }
        if (isBoosted == true)
        {
            BoostTime += Time.deltaTime;
            if (BoostTime >= BoostDuration)
            {
                isBoosted = false;
                AttackDamage = normalAttackDamage;
            }
        }
        
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("Height", rb.velocity.y);
        anim.SetBool("Grounded", grounded);
        anim.SetBool("IsRunning", Input.GetKey(RunKey));
    }

 
    void Attack()
    {
        isAttacking = true;
        
        int randomAttack = Random.Range(0, 2);
        if (randomAttack == 0)
        {
            anim.SetTrigger("topstablvl3");
        }
        else
        {
            anim.SetTrigger("firehitlvl3");
        }

        StartCoroutine(ResetAttack());
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(attackDuration);
        isAttacking = false;
    }

    // --- PHYSICS & HEALTH (Existing Code) ---
    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround); 
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
    }

    void SpriteFlicker()
    {
        if (flickerTime < flickerDuration)
        {
            flickerTime += Time.deltaTime;
        }
        else if (flickerTime >= flickerDuration)
        {
            sr.enabled = !sr.enabled;
            flickerTime = 0;
        }
    }

    public void TakeDamage(int damage)
    {
        if (!isImmune)
        {
            health -= damage;
            if (health <= 0)
            {
                FindObjectOfType<LevelManager>().RespawnPlayer();
                health = maxHealth;
            }
            Debug.Log("Player Health:" + health.ToString());
            isImmune = true;
            immunityTime = 0f;
        }
    }

    public void Heal(int healAmount)
    {
        health += healAmount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        Debug.Log("Player Health:" + health.ToString());
    }


}