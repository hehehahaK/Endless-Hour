using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAI : MonoBehaviour
{
    public Transform player;

    public float chaseSpeed = 3f;
    public float attackDistance = 1.5f;  // When soldier starts attacking
    public bool isActivated = false;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isActivated) return;

        float distance = Vector2.Distance(transform.position, player.position);

        // If far from player → RUN
        if (distance > attackDistance)
        {
            RunTowardsPlayer();
        }
        else
        {
            AttackPlayer();
        }
    }

    void RunTowardsPlayer()
    {
        anim.SetBool("isRunning", true);
        anim.SetBool("isAttacking", false);

        Vector2 direction = (player.position - transform.position).normalized;
        transform.position += (Vector3) direction * chaseSpeed * Time.deltaTime;

        // Flip sprite
        if (direction.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);
    }

    void AttackPlayer()
    {
        anim.SetBool("isRunning", false);
        anim.SetBool("isAttacking", true);

        // Optional: stop moving when attacking
        // (No movement code here)
    }
}