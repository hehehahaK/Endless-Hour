using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solider : NewEnemyController
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public override void MeleeAttack()
    {
        anim.SetTrigger("Attack");

    }
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.CompareTag("Player") && isAttacking)
        {
            playerController.TakeDamage(attackDamage);
        }
        isAttacking = false;
    }

}
