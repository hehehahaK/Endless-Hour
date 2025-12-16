using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLvl4 : MonoBehaviour
{
    public float speed = 15f;
    public float lifetime = 6f;

    private Vector2 direction;
    private bool isMoving = false;

    public void Fire(Vector2 dir)
    {
        direction = dir.normalized;
        isMoving = true;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        if (!isMoving) return;

        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
            Destroy(gameObject);
    }
}

