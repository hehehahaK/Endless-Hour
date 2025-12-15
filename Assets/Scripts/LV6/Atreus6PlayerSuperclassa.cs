using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Atreus6PlayerSuperclassa : MonoBehaviour
{
    // Movement Variables incl. jump and its relation w/ ground and speed
    public GameObject slashVFX;
    private attackhitbox6 attackHitbox; // Reference to attack hitbox script
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
    private bool grounded;
    private Animator anim; // ANIMATOR 
    private SpriteRenderer sr; //SpritRenderer for flickering effect
    private Rigidbody2D rb;
    private float speedBoostTime = 0f;
    public float originalMoveSpeed = 5f;
    public float speedBoostDuration = 10f;
    // Health Variables
    public float health = 100;
    public float maxHealth = 100;
    public Healthbar healthBarUI;
    private int normalAttackDamage = 5;
    private float BoostDuration = 30f;
    private float BoostTime = 0f;
    public int AttackDamage = 5;
    private bool speedBoosted = false;
    private float flickerTime = 0f;
    private float flickerDuration = 0.1f;
    public bool isImmune = false;
    public bool isBoosted = false;
    private float immunityTime = 0f;
    public float immunityDuration = 1.5f;
    public float attackDuration = 1f; // How long the attack lasts
    public bool isAttacking = false;
    void Start()
    {
        moveSpeed = originalMoveSpeed;
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        attackHitbox = GetComponentInChildren<attackhitbox6>();
        // Get the attack hitbox component test if no work just comment all attachhitbox lines
    }

    void Attack()
    {
        isAttacking = true;
        anim.SetTrigger("isAttacking");
        StartCoroutine(ResetAttack());

    }
    public void OnAttackConnect()
    {
        attackHitbox.EnableDamage(); // Called from animation event at the right frame
    }
    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(attackDuration);
        attackHitbox.DisableDamage();// comment if no work
        isAttacking = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            Attack();
        }

        if (Input.GetKey(RunKey))
        {
            currentSpeed = moveSpeed * 2f;
        }
        else
        {
            currentSpeed = moveSpeed;
        }
        if (Input.GetKeyDown(Spacebar) && grounded)
        { Jump(); }

        if (!Input.GetKey(L) && !Input.GetKey(R))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if (Input.GetKey(L))
        {
            //Debug.Log("going left"); // note to anyone, remove later on!!
            rb.velocity = new Vector2(-currentSpeed, rb.velocity.y);
            //player character moves horizontally to the left along the x-axis without disrupting jump
            if (sr != null)
            {
                transform.localScale = new Vector3(-1, 1, 1); // Flip the whole player
            }
        }

        if (Input.GetKey(R))
        {
            //Debug.Log("going right");
            rb.velocity = new Vector2(currentSpeed, rb.velocity.y);
            //player character moves horizontally to the right along the x-axis without disrupting jump
            if (sr != null)
            {
                transform.localScale = new Vector3(1, 1, 1); // Flip back
            }
        }

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
        if (speedBoosted == true)
        {
            speedBoostTime += Time.deltaTime;
            if (speedBoostTime >= speedBoostDuration)
            {
                speedBoosted = false;
                moveSpeed = originalMoveSpeed;
            }
        }
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("Height", rb.velocity.y);
        anim.SetBool("Grounded", grounded);
        anim.SetBool("IsRunning", Input.GetKey(RunKey));
        anim.SetBool("isAttacking", isAttacking);
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround); //this statement calculates when 
        //exactly the character is considered by Unity's engine to be standing on the ground. 
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

    //Damage function, we send a DMG INT value to subtract from health. it also calls LevelManager to respawn player upon death.
    public void TakeDamage(int damage)
    {
        if (!isImmune)
        {
            health -= damage;
            health = Mathf.Clamp(health, 0f, maxHealth);
            //   healthBarUI.updateHealthBar();


            if (health <= 0)
            {
                FindObjectOfType<LevelManager>().RespawnPlayer();
                health = maxHealth;
                //                  healthBarUI.updateHealthBar();

            }
            //     Debug.Log("Player Health:" + health.ToString());
            isImmune = true;
            immunityTime = 0f;
        }
        else
        {
            //   Debug.Log("Player took no damage.");
        }
    }

    public void Heal(int healAmount)
    {
        health += healAmount;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        //Debug.Log("Player Health:" + health.ToString());
    }

    public void AttackBoost(int boostAmount)
    {
        if (!isBoosted)
        {
            AttackDamage += boostAmount;
            isBoosted = true;
        }
        BoostTime = 0f;
        // Debug.Log("Player Attack Damage:" + AttackDamage.ToString());
    }

    // Permanent damage upgrade (doesn't expire)
    public void speedBoost(int boostAmount)
    {
        if (!speedBoosted)
        {
            moveSpeed += boostAmount;
            speedBoosted = true;
        }

        Debug.Log("Speed Upgrade: " + moveSpeed.ToString());

    }

    public void SpecialBoost(int boostAmount)
    {
        normalAttackDamage += boostAmount;
        AttackDamage = normalAttackDamage;
        Debug.Log("Permanent Attack Damage Upgrade: " + AttackDamage.ToString());

    }
    public void PlaySlashVFX()
    {
        slashVFX.SetActive(true);
        StartCoroutine(DisableVFXAfterDelay(0.3f));
    }

    IEnumerator DisableVFXAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        slashVFX.SetActive(false);
    }

}