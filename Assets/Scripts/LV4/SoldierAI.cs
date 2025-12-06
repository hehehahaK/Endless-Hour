using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAI : MonoBehaviour
{
    public Transform player;
    public float chaseSpeed = 3f;
    public float stopDistance = 6f;   // Stop chasing when player is this far
    public bool isActivated = false;  // Trigger activates this

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isActivated) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance < stopDistance)
        {
            RunTowardsPlayer();
        }
        else
        {
            StopRunning();
        }
    }

    void RunTowardsPlayer()
    {
        anim.SetBool("isRunning", true);

        Vector2 direction = (player.position - transform.position).normalized;
        transform.position += (Vector3) direction * chaseSpeed * Time.deltaTime;

        // Flip sprite based on direction
        if (direction.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);
    }

    void StopRunning()
    {
        anim.SetBool("isRunning", false);
    }
}
