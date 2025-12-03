using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelLV2 : MonoBehaviour
{
    public float moveSpeed = 3f;
    public Transform player;
    public int damage = 1;
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                player.position,
                moveSpeed * Time.deltaTime
            );
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerSuperclass>().TakeDamage(damage);
        }

        if (other.CompareTag("Wall"))
        {
            // Destroy the wheel when it hits a wall
            Destroy(gameObject);
        }
    }
}