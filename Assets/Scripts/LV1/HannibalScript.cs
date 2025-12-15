using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HannibalScript : MonoBehaviour
{
    public int maxHealth = 100;
    public int attackDamage = 15;

    private int currentHealth;
    private bool isDead = false;
    private bool isAttacking = false;

    private Animator anim;
    private Rigidbody2D rb;
    private Collider2D col;
    private PlayerSuperclass player;

    void Start()
    {
        currentHealth = maxHealth;

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        player = FindObjectOfType<PlayerSuperclass>();
    }

    void Update()
    {
        if (isDead) return;

        anim.SetBool("Run", true);
    }

    // =======================
    // DAMAGE & DEATH
    // =======================
    

   private void Die()
{
    if (isDead) return;

    isDead = true;

    rb.velocity = Vector2.zero;
    rb.simulated = false;
    col.enabled = false;

    anim.SetTrigger("Die");
    StartCoroutine(
   
    Camera.main.GetComponent<CameraShakeLv1>()
    .Shake(3.5f, 0.15f)
);

}


    // =======================
    // ATTACK
    // =======================
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (isDead) return;

        if (collision.gameObject.CompareTag("Player") && isAttacking)
        {
            player.TakeDamage(attackDamage);
        }
    }

    // Call this from animation event
    public void StartAttack()
    {
        if (isDead) return;

        isAttacking = true;
        anim.SetTrigger("Attack");
    }

    // Call this from animation event
    public void EndAttack()
    {
        isAttacking = false;
    }

    public void TakeDamage(int damage)
    {
    if (isDead) return;

    currentHealth -= damage;
    if (currentHealth <= 0)
    {
        Die();
    }
}

}
