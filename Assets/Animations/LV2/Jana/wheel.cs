using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wheel : EnemyController
{
    public Transform player;

    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, maxSpeed * Time.deltaTime);
    }

    public new void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<PlayerStats>().TakeDamage(damage);
        }
        else if (other.tag == "Wall")
        {
            Flip();
        }
    }
}