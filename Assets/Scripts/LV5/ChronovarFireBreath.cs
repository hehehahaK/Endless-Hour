using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChronovarFireBreath : MonoBehaviour
{
    public int damage = 1;
    public PolygonCollider2D breathCollider;
    public float extendDuration = 0.5f;
    public float maxScaleX = 5f;    // How far the breath reaches
    public Transform player;

    private Vector3 initialScale;

    void Awake()
    {
        if (breathCollider == null)
            breathCollider = GetComponent<PolygonCollider2D>();

        initialScale = transform.localScale;
        transform.localScale = new Vector3(0f, initialScale.y, initialScale.z); // start small
        breathCollider.enabled = false;
    }

    public void ActivateBreath()
    {
        if (player != null)
        {
            Vector2 dir = (player.position - transform.position).normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        StartCoroutine(ExtendBreath());
    }

    private IEnumerator ExtendBreath()
    {
        breathCollider.enabled = true;

        float timer = 0f;
        while (timer < extendDuration)
        {
            timer += Time.deltaTime;
            float t = timer / extendDuration;

            // Scale X grows forward from pivot (mouth)
            float newScaleX = Mathf.Lerp(0f, maxScaleX, t);
            transform.localScale = new Vector3(Mathf.Abs(newScaleX), initialScale.y, initialScale.z);

            yield return null;
        }

        // Optional hold
        yield return new WaitForSeconds(0.3f);

        // Reset
        transform.localScale = new Vector3(0f, initialScale.y, initialScale.z);
        breathCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerSuperclass ps = other.GetComponent<PlayerSuperclass>();
        if (ps != null)
            ps.TakeDamage(damage);
    }
}
