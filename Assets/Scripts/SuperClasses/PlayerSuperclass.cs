using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSuperclass : MonoBehaviour
{
    // Movement Variables incl. jump and its relation w/ ground and speed
    public float currentSpeed;
    public float moveSpeed;
    public float jumpHeight;
    public KeyCode Spacebar = KeyCode.Space;
    public KeyCode L = KeyCode.A;
    public KeyCode R = KeyCode.D;
    public KeyCode RunKey = KeyCode.LeftShift;
    public Transform groundCheck; // empty child positioned at the player's feet. Used to detect if the player is touching ground.
    public float groundCheckRadius;
    public LayerMask whatIsGround; //this variable stores what is considered a ground to the character,defines which physics layers count as ground.
    public bool grounded; // made public so subclass can read

    // Components
    public Animator anim; // ANIMATOR 
    public SpriteRenderer sr; //SpritRenderer for flickering effect
    public Rigidbody2D rb;

    // Speed Boost
    public float speedBoostTime = 0f;
    public float originalMoveSpeed = 5f;
    public float speedBoostDuration = 10f;
    public bool speedBoosted = false;

    // Health Variables
    public float health = 100;
    public float maxHealth = 100;
    public Healthbar healthBarUI;
    public int normalAttackDamage = 5;
    public int AttackDamage = 5;
    public float BoostDuration = 30f;
    public float BoostTime = 0f;
    public bool isBoosted = false;

    // Immunity
    public bool isImmune = false;
    public float flickerTime = 0f;
    public float flickerDuration = 0.1f;
    public float immunityTime = 0f;
    public float immunityDuration = 1.5f;

    // Attack
    public float attackDuration = 0.3f; // How long the attack lasts
    public bool isAttacking = false;

    // Shield
    public bool isBlocking = false;

    // Start is called before the first frame update
    public virtual void Start()
    {
        moveSpeed = originalMoveSpeed;
        currentSpeed = moveSpeed;
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Attack function, can be overridden by subclasses
    public virtual void Attack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            anim.SetTrigger("Attack");
            StartCoroutine(ResetAttack());
        }
    }

    // Reset attack after attackDuration
    public virtual IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(attackDuration);
        isAttacking = false;
    }

    // Main update loop
    public virtual void Update()
    {
        // Handle running
        if (Input.GetKey(RunKey))
            currentSpeed = 2 * moveSpeed;
        else
            currentSpeed = moveSpeed;

        // Handle jumping
        if (Input.GetKeyDown(Spacebar) && grounded)
            Jump();

        // Stop movement if no horizontal key pressed
        if (!Input.GetKey(L) && !Input.GetKey(R))
            rb.velocity = new Vector2(0, rb.velocity.y);

        // Move left
        if (Input.GetKey(L))
        {
            rb.velocity = new Vector2(-currentSpeed, rb.velocity.y);
            if (sr != null) sr.flipX = true;
        }

        // Move right
        if (Input.GetKey(R))
        {
            rb.velocity = new Vector2(currentSpeed, rb.velocity.y);
            if (sr != null) sr.flipX = false;
        }

        // Handle immunity flicker
        if (isImmune)
        {
            SpriteFlicker();
            immunityTime += Time.deltaTime;
            if (immunityTime >= immunityDuration)
            {
                isImmune = false;
                sr.enabled = true;
            }
        }

        // Handle attack boost
        if (isBoosted)
        {
            BoostTime += Time.deltaTime;
            if (BoostTime >= BoostDuration)
            {
                isBoosted = false;
                AttackDamage = normalAttackDamage;
            }
        }

        // Handle speed boost
        if (speedBoosted)
        {
            speedBoostTime += Time.deltaTime;
            if (speedBoostTime >= speedBoostDuration)
            {
                speedBoosted = false;
                moveSpeed = originalMoveSpeed;
            }
        }

        // Update animator parameters
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("Height", rb.velocity.y);
        anim.SetBool("Grounded", grounded);
        anim.SetBool("IsRunning", Input.GetKey(RunKey));

        // Attack input
        if (Input.GetMouseButton(0))
        {
            Attack();
        }
        if (Input.GetMouseButton(1)) { isBlocking = true;
         anim.SetBool("ShieldUp", true); }
        else
        {
            isBlocking = false; 
            anim.SetBool("ShieldUp", false); } 
        }
    

    // Physics update
    public virtual void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround); //this statement calculates when exactly the character is considered by Unity's engine to be standing on the ground. 
    }

    // Jump function, can be overridden
    public virtual void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
    }

    // Sprite flicker for immunity
    public virtual void SpriteFlicker()
    {
        if (flickerTime < flickerDuration)
            flickerTime += Time.deltaTime;
        else
        {
            sr.enabled = !sr.enabled;
            flickerTime = 0f;
        }
    }

    //Damage function, we send a DMG INT value to subtract from health. it also calls LevelManager to respawn player upon death.
    public virtual void TakeDamage(int damage)
    {
        if (!isImmune)
        {
            health -= damage;
            health = Mathf.Clamp(health, 0f, maxHealth);
            healthBarUI?.updateHealthBar();

            if (health <= 0)
            {
                FindObjectOfType<LevelManager>()?.RespawnPlayer();
                health = maxHealth;
                healthBarUI?.updateHealthBar();
            }
            isImmune = true;
            immunityTime = 0f;
        }
    }

    public virtual void Heal(int healAmount)
    {
        health += healAmount;
        if (health > maxHealth) health = maxHealth;
    }

    public virtual void AttackBoost(int boostAmount)
    {
        if (!isBoosted)
        {
            AttackDamage += boostAmount;
            isBoosted = true;
        }
        BoostTime = 0f;
    }

    // Permanent speed upgrade
    public virtual void speedBoost(int boostAmount)
    {
        if (!speedBoosted)
        {
            moveSpeed += boostAmount;
            speedBoosted = true;
            currentSpeed = moveSpeed;
        }
        Debug.Log("Speed Upgrade: " + moveSpeed);
    }

    // Permanent attack upgrade
    public virtual void SpecialBoost(int boostAmount)
    {
        normalAttackDamage += boostAmount;
        AttackDamage = normalAttackDamage;
        Debug.Log("Permanent Attack Damage Upgrade: " + AttackDamage);
    }
}
