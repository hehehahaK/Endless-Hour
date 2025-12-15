using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class royalguard : NewEnemyController
{
    public bool canFight = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        if (!canFight || isDead) return;
        base.Update();
    }

    public override void MeleeAttack()
    {
        if(isAttacking) return;
        
        anim.SetTrigger("Attack"); // Ensure you have this Trigger in Animator
        
        // Check for player in range
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(transform.position, attackRange);
        foreach (Collider2D playerHit in hitPlayers) // honestly i but its what works 
        {
            
            lvl3playercontroller p = playerHit.GetComponent<lvl3playercontroller>();
            if (p != null)
            {
                p.TakeDamage(attackDamage);
            }
        }
        StartCoroutine(ResetAttackCooldown());
    }
    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(0.3f); 
        isAttacking = false;
    }

    public override void Die()
    {
        if (isDead) return;
        isDead = true;
        rb.velocity = Vector2.zero; 
        GetComponent<Collider2D>().enabled = false; 

        StartCoroutine(FlashAndDie());
    }
    IEnumerator FlashAndDie()
    {
        // da flashing red mechanics 3shan el player el 3abeet y3raf en el guard bymoot 5alas bye bye
        for (int i = 0; i < 5; i++)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.clear;
            yield return new WaitForSeconds(0.1f);
        }

        Destroy(gameObject); // ya mayyet ya 3abeet 
    }
}
