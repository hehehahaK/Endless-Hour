using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Atreus5 : PlayerSuperclass
{
    //attacker and vfx
    public GameObject slashVFX;             // Visual effect for attack
    private attackhitbox6 attackHitbox;

    public override void Start()
    {
        attackDuration=1f;
        
        base.Start();                        // Initialize everything from PlayerSuperclass
        attackHitbox = GetComponentInChildren<attackhitbox6>();
    }

    public override void Attack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            anim.SetTrigger("isAttacking");
            PlaySlashVFX();
            StartCoroutine(ResetAttack());
        }
    }

    public void OnAttackConnect()
    {
        if (attackHitbox != null)
        {
            attackHitbox.EnableDamage();    // Called from animation event
        }
    }

    private IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(attackDuration);
        if (attackHitbox != null)
            attackHitbox.DisableDamage();

        isAttacking = false;
    }

    public void PlaySlashVFX()
    {
        if (slashVFX != null)
        {
            slashVFX.SetActive(true);
            StartCoroutine(DisableVFXAfterDelay(0.3f));
        }
    }

    private IEnumerator DisableVFXAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (slashVFX != null)
            slashVFX.SetActive(false);
    }
}
