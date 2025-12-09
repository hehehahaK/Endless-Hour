using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sheildsoliders : MonoBehaviour
{
    public Transform player;

    public float chaseSpeed = 3f;
   
    public bool isActivated = false;

    public int damage = 10;
    
   

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!isActivated) return;

        float distance = Vector2.Distance(transform.position, player.position);

   
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

    
}