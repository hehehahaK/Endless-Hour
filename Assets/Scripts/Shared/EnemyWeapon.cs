using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public int attackDamage = 10;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerSuperclass player = other.GetComponent<PlayerSuperclass>();
            if (player != null)
            {
                player.TakeDamage(attackDamage);
            }
        }
    }
}