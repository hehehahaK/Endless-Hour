using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSuperclass : MonoBehaviour
{
    // Movement Variables incl. jump and its relation w/ ground and speed
    public float moveSpeed;
    public float jumpHeight;
    public KeyCode Spacebar = KeyCode.Space;
    public KeyCode L = KeyCode.A;
    public KeyCode R = KeyCode.D;
    public KeyCode RunKey = KeyCode.LeftShift;
    public Transform groundCheck;// empty child positioned at the player’s feet. Used to detect if the player is touching ground.
    public float groundCheckRadius;
    public LayerMask whatIsGround; //this variable stores what is considered a ground to the character,defines which physics layers count as ground.
    private bool grounded;
    private Animator anim; // ANIMATOR 
    private SpriteRenderer sr; //SpritRenderer for flickering effect

    // Health Variables
    public int health = 20;
    private float maxHealth = 20f;
    private float normalAttackDamage = 5f;
    private float BoostDuration = 30f;
    private float BoostTime = 0f;
    public float AttackDamage = 5f;

    private float flickerTime = 0f;
    private float flickerDuration = 0.1f;
    public bool isImmune = false;
    public bool isBoosted = false;
    private float immunityTime = 0f;
    public float immunityDuration = 1.5f;

    void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

    }

    void Update()
    {
        if (Input.GetKeyDown(Spacebar) && grounded)
        { Jump(); }

        if (Input.GetKey(L))
        {
            
            GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
            //player character moves horizontally to the left along the x-axis without disrupting jump
            if (GetComponent<SpriteRenderer>() != null)
            { GetComponent<SpriteRenderer>().flipX = true; }
        }

        if (Input.GetKey(R))
        {
            
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
            //player character moves horizontally to the right along the x-axis without disrupting jump
            if (GetComponent<SpriteRenderer>() != null)
            {
                GetComponent<SpriteRenderer>().flipX = false;
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
        anim.SetFloat("Speed", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
        anim.SetFloat("Height", GetComponent<Rigidbody2D>().velocity.y);
        anim.SetBool("Grounded", grounded);
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround); //this statement calculates when 
        //exactly the character is considered by Unity's engine to be standing on the ground. 
    }

    void Jump()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
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
                health = 20;
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
        Debug.Log("Player Health:" + health.ToString());
    }
    public void AttackBoost(float boostAmount)
    {
        if (!isBoosted)
        {
            AttackDamage += boostAmount;
            Debug.Log("Player Attack Damage:" + AttackDamage.ToString());
            BoostTime = 0f;
            isBoosted = true;
        }
    }

}



