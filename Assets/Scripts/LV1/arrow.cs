using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrow : MonoBehaviour
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
        Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
