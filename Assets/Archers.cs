using UnityEngine;

public class Archers : MonoBehaviour
{
    public GameObject arrowPrefab;          // was bulletPistolPrefab
    public Transform shootPoint;            // was firePoint
    public float arrowSpeed = 10f;          // was bulletPistolSpeed

    public float moveSpeed = 2f;
    public float shootingRange = 5f;

    [Header("Shooting")]
    public float timeBetweenShots = 5.0f;
    private float nextShotTime = 0f;

    private Animator anim;
    private Vector3 originalScale;
    private Transform player;

    void Start()
    {
        anim = GetComponent<Animator>();
        originalScale = transform.localScale;
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > shootingRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);

            if (Time.time >= nextShotTime)
            {
                Shoot();
                nextShotTime = Time.time + timeBetweenShots;
            }
        }

        FlipTowards(player.position);
    }

    public void Shoot()
    {
        anim.SetTrigger("isShooting");
    }

    // Animation Event
    public void FireArrow()
    {
        if (player == null) return;

        GameObject arrow = Instantiate(arrowPrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.gravityScale = 0;
            Vector2 direction = (player.position - shootPoint.position).normalized;
            rb.velocity = direction * arrowSpeed;
        }
    }

    public void FlipTowards(Vector3 targetPosition)
    {
        bool facingLeft = targetPosition.x < transform.position.x;

        transform.localScale = new Vector3(
            facingLeft ? -Mathf.Abs(originalScale.x) : Mathf.Abs(originalScale.x),
            originalScale.y,
            originalScale.z
        );
    }
}
