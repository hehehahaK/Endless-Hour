using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFollow : MonoBehaviour
{
    public Transform player;       // Drag your player here
    public float speed = 2f;       // NPC walking speed
    public float stopDistance = 3.2f; // Distance at which NPC stops moving

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        // If player is far away → walk toward player
        if (distance > stopDistance)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)direction * speed * Time.deltaTime;
            
            anim.SetBool("Walking", true);
        }
        else
        {
            // Player is close → stop moving
            anim.SetBool("Walking", false);
        }
    }
}