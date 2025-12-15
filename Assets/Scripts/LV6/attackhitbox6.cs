using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackhitbox6 : MonoBehaviour
{
    private Atreus6PlayerSuperclassa player;
    private bool canDamage = false;

    void Start()
    {
        player = GetComponentInParent<Atreus6PlayerSuperclassa>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        DealDamage(collision);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        DealDamage(collision);
    }

    private void DealDamage(Collider2D collision)
    {
        if (canDamage && collision.CompareTag("CorenEnemy"))
        {
            CorenBoss coren = collision.GetComponent<CorenBoss>();
            if (coren != null)
            {
                coren.TakeDamage(player.AttackDamage);
                HitStop6.Execute(0.6f,0.15f); 
                canDamage = false;
            }
        }
    }

    public void EnableDamage()
    {
        canDamage = true;
    }

    public void DisableDamage()
    {
        canDamage = false;
    }
}