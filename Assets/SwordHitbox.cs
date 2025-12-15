using UnityEngine;

public class SwordHitbox : MonoBehaviour
{
    public PlayerSuperclass player;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!player.isAttacking) return;

        HannibalScript hannibal = other.GetComponent<HannibalScript>();
        if (hannibal != null)
        {
            hannibal.TakeDamage(player.AttackDamage);
        }
    }
}
