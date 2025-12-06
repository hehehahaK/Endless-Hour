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
    public float attackDamage = 10f;
    public float shortRange = 3f;
    public float midRange = 6f;

    public Animator anim;
    public Rigidbody2D rb;
    public ChronovarStateMachine stateMachine;

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
    }

    void Update()
    {
        UpdateMovementAnim();
    }

    // MOVEMENT ANIMATION LOGIC

    private void UpdateMovementAnim()
    {
        // If Chronovar is moving , play moving animation
        anim.SetBool("isMoving", rb.velocity.magnitude > 0.05f);
    }

    // PHASE 1 ATTACKS

    public void Phase1Attack1() { Debug.Log("Chronovar Boss Phase 1 Attack 1 executed."); }
    public void Phase1Attack2() { Debug.Log("Chronovar Boss Phase 1 Attack 2 executed."); }
    public void Phase1Attack3() { Debug.Log("Chronovar Boss Phase 1 Attack 3 executed."); }
    public void Phase1Attack4() { Debug.Log("Chronovar Boss Phase 1 Attack 4 executed."); }


    // PHASE 2 ATTACKS


    public void Phase2Attack1() { Debug.Log("Chronovar Boss Phase 2 Attack 1 executed."); }
    public void Phase2Attack2() { Debug.Log("Chronovar Boss Phase 2 Attack 2 executed."); }
    public void Phase2Attack3() { Debug.Log("Chronovar Boss Phase 2 Attack 3 executed."); }


    // PHASE 3 ATTACKS
    public void Phase3Attack1() { Debug.Log("Chronovar Boss Phase 3 Attack 1 executed."); }
    public void Phase3Attack2() { Debug.Log("Chronovar Boss Phase 3 Attack 2 executed."); }
    public void MoveTowardsPlayer()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);


        if (distance <= stoppingDistance) // within stopping distance
        {
            rb.velocity = Vector2.zero;
            return;
        }

        // Otherwise move normally
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;

    }

}
