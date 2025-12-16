using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenspikeMovement : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 8f;
    public int moveDirection = -1; // -1 howa left, 1 howa  right
    public int damageAmount = 5;
    public bool isActivated = false;

    private Rigidbody2D rb;
    private Animator anim;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (!isActivated)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        rb.velocity = new Vector2(moveSpeed * Mathf.Sign(moveDirection), 0f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject
                .GetComponent<PlayerSuperclass>()
                .TakeDamage(damageAmount);
        }
    }
}
