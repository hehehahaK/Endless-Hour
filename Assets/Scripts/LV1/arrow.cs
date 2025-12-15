using UnityEngine;

public class arrow : MonoBehaviour
{
    public float moveSpeed = 3f;
    public Transform player;
    public int damage = 1;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    void FixedUpdate()
    {
        rb.velocity = -transform.right * moveSpeed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerSuperclass>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
