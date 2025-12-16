using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HannibalScript : NewEnemyController
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
    public override void MeleeAttack()
    {
        anim.SetTrigger("Attack");
        StartCoroutine(ResetAttack());
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(0.5f); 
        isAttacking = false;
    }

    public override void OnCollisionStay2D(Collision2D collision)
    {
        base.OnCollisionStay2D(collision);
        if (collision.gameObject.CompareTag("Player") && isAttacking)
        {
            playerController.TakeDamage(attackDamage);
        }
        isAttacking = false;
    }
    public override void MoveTowardsPlayer(float distance)
    {
        base.MoveTowardsPlayer(distance);
        anim.SetTrigger("Run");

    }
}
