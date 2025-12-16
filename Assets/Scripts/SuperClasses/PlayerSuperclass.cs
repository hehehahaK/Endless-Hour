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
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    protected bool grounded;
    protected Animator anim;
    protected SpriteRenderer sr;
    protected Rigidbody2D rb;
    protected float speedBoostTime = 0f;
    public float originalMoveSpeed = 5f;
    public float speedBoostDuration = 10f;

    // Health Variables
    public float health = 100;
    public float maxHealth = 100;
    public Healthbar healthBarUI;
    protected int normalAttackDamage = 5;
    protected float BoostDuration = 30f;
    protected float BoostTime = 0f;
    public int AttackDamage = 5;
    protected bool speedBoosted = false;
    protected float flickerTime = 0f;
    protected float flickerDuration = 0.1f;
    public bool isImmune = false;
    public bool isBoosted = false;
    protected float immunityTime = 0f;
    public float immunityDuration = 1.5f;
    public float attackDuration = 0.3f;
    public bool isAttacking = false;
    public bool isBlocking = false;

    void Start()
    {

        moveSpeed = originalMoveSpeed;
        currentSpeed = moveSpeed;
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Attack()
    {
        Debug.Log("ATTACK CALLED");
        isAttacking = true;
        anim.SetTrigger("Attack");
        StartCoroutine(ResetAttack());
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(attackDuration);
        isAttacking = false;
    }

    void Update()
    {
        if (Input.GetKey(RunKey)){

            currentSpeed=2*moveSpeed;
        }
        else{
            currentSpeed=moveSpeed;
        }
        if (Input.GetKeyDown(Spacebar) && grounded)
        {
            Jump();
        }

        if (!Input.GetKey(L) && !Input.GetKey(R))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if (Input.GetKey(L))
        {
            rb.velocity = new Vector2(-currentSpeed, rb.velocity.y);
            if (sr != null)
            {
                sr.flipX = true;
            }
        }

        if (Input.GetKey(R))
        {
            rb.velocity = new Vector2(currentSpeed, rb.velocity.y);
            if (sr != null)
            {
                sr.flipX = false;
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

                /*if (Input.GetMouseButton(0))
                {
                    isBlocking = true;
                    anim.SetBool("ShieldUp", true);
                }
                else
                {
                    isBlocking = false;
                    //anim.SetBool("ShieldUp", false);
                } */

        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("Height", rb.velocity.y);
        anim.SetBool("Grounded", grounded);
        anim.SetBool("IsRunning", Input.GetKey(RunKey));
        if (Input.GetMouseButton(0))
        {
            Attack();
        }


    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    protected void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
    }

    protected void SpriteFlicker()
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
            health = Mathf.Clamp(health, 0f, maxHealth);
            healthBarUI.updateHealthBar();

            if (health <= 0)
            {
                FindObjectOfType<LevelManager>().RespawnPlayer();
                health = maxHealth;
                healthBarUI.updateHealthBar();
            }

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
            healthBarUI.updateHealthBar();
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
    }

    public void speedBoost(int boostAmount)
    {
        if (!speedBoosted)
        {
            moveSpeed += boostAmount;
            speedBoosted = true;
            currentSpeed=moveSpeed;
        }

        Debug.Log("Speed Upgrade: " + moveSpeed.ToString());
    }

    public void SpecialBoost(int boostAmount)
    {
        normalAttackDamage += boostAmount;
        AttackDamage = normalAttackDamage;
        Debug.Log("Permanent Attack Damage Upgrade: " + AttackDamage.ToString());
    }
    void OnCollisionStay2D(Collision2D collision)
{
    // Player must be attacking and not blocking
    if (!isAttacking || isBlocking) return;

    // Check if we hit Hannibal
    HannibalScript hannibal =
        collision.gameObject.GetComponent<HannibalScript>();

    if (hannibal != null)
    {
        hannibal.TakeDamage(AttackDamage);
    }
}


}