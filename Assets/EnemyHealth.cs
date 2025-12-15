using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 3;
    int currentHealth;

    [Header("Invincible + Flicker")]
    public float invincibleTime = 0.6f;
    public float flickerInterval = 0.08f;

    bool invincible;
    SpriteRenderer[] sprites;

    void Awake()
    {
        currentHealth = maxHealth;
        sprites = GetComponentsInChildren<SpriteRenderer>(true);
        foreach (var s in sprites) s.enabled = true;
    }

    public void TakeDamage(int amount)
    {
        if (invincible) return;
        if (amount <= 0) return;

        currentHealth -= amount;
        StartCoroutine(Flicker());

        if (currentHealth <= 0)
            Destroy(gameObject);
    }

    IEnumerator Flicker()
    {
        invincible = true;
        float t = 0f;

        while (t < invincibleTime)
        {
            t += flickerInterval;
            foreach (var s in sprites) s.enabled = !s.enabled;
            yield return new WaitForSeconds(flickerInterval);
        }

        foreach (var s in sprites) s.enabled = true;
        invincible = false;
    }
}