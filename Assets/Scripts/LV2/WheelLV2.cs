using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelLV2 : MonoBehaviour
{
    public float moveSpeed = 3f;
    public Transform player;
    public int damage = 1;
    private SpriteRenderer sr;
    public Rigidbody2D rb;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(SelfDestructAfterTime(2f));

    }

    void FixedUpdate()
    {
        if (player != null)
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
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
            // Destroy the wheel when it hits a wall, depreciated
            Destroy(gameObject);
        }
    }
    private IEnumerator SelfDestructAfterTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}