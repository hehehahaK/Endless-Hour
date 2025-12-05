using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lvl3playercontrollerstairs : MonoBehaviour
{
    // Start is called before the first frame update

    private float currentSpeed;
    public float moveSpeed;
    public float jumpHeight;
    public KeyCode Spacebar = KeyCode.Space;
    public KeyCode L = KeyCode.A;
    public KeyCode R = KeyCode.D;
    public KeyCode RunKey = KeyCode.LeftShift;
    public Transform groundCheck;// empty child positioned at the player's feet. Used to detect if the player is touching ground.
    public float groundCheckRadius;
    public LayerMask whatIsGround; //this variable stores what is considered a ground to the character,defines which physics layers count as ground.
    protected bool grounded;
    protected Animator anim; // ANIMATOR 
    protected SpriteRenderer sr; //SpritRenderer for flickering effect
    protected Rigidbody2D rb;

    // Health Variables
    public int health = 20;
    private int maxHealth = 20;
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


//new variables
    public bool isOnStairs = false;
    public float stairClimbBias = 0.3f;
    public float stairSpeedMultiplier = 0.2f;

    void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void Update()
    {
        /////////////new part is herererrereerer
        float currentMoveSpeed;
        if (Input.GetKey(RunKey))
        {
            currentMoveSpeed = moveSpeed * 1f;
        }
        else
        {
            currentMoveSpeed = moveSpeed;
        }

        float yVelocity = rb.velocity.y;

        if (isOnStairs)
        {
            currentMoveSpeed *= stairSpeedMultiplier;

            if (Input.GetKey(R)) 
            {
                yVelocity = stairClimbBias; // Going UP
            }
            else if (Input.GetKey(L)) 
            {
                yVelocity = -stairClimbBias; // Going DOWN
            }
            else
            {
                yVelocity = 0; // Standing still on stairs
            }
        }

        if (Input.GetKey(L))
        {
            rb.velocity = new Vector2(-currentMoveSpeed, yVelocity);
            if (sr != null)
            { sr.flipX = true; }
        }
        else if (Input.GetKey(R))
        {
            rb.velocity = new Vector2(currentMoveSpeed, yVelocity);
            if (sr != null)
            {
                sr.flipX = false;
            }
        }
        // Handle stopping movement
        else if (!Input.GetKey(L) && !Input.GetKey(R) && !isOnStairs)
        {
            // If NOT on stairs, stop horizontal movement
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else if (isOnStairs)
        {
            // If ON stairs, just stop horizontal movement but keep the set yVelocity
            rb.velocity = new Vector2(0, yVelocity);
        }

/////////////////////////////////////////////////////////////////////////////////////////

        

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

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround); //this statement calculates when 
        //exactly the character is considered by Unity's engine to be standing on the ground. 
    }

    protected void Jump()
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

    //Damage function, we send a DMG INT value to subtract from health. it also calls LevelManager to respawn player upon death.
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
        else
        {
            Debug.Log("Player took no damage.");
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

    public void AttackBoost(int boostAmount)
    {
        if (!isBoosted)
        {
            AttackDamage += boostAmount;
            isBoosted = true;
        }
        BoostTime = 0f;
        Debug.Log("Player Attack Damage:" + AttackDamage.ToString());
    }

    // Permanent damage upgrade (doesn't expire)
    public void SpecialBoost(int boostAmount)
    {
        normalAttackDamage += boostAmount;
        AttackDamage = normalAttackDamage;
        Debug.Log("Permanent Attack Damage Upgrade: " + AttackDamage.ToString());
    }
}
