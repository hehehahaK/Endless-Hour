using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : NewEnemyController
{
    public GameObject arrowPrefab;
    public override void Start()
    {
        base.Start();
        stoppingDistance = 6f;  // Archers stop far away
        attackRange = 7f;       // Archers shoot from RANGE
        isArcher = true;
    }

    // Update is called once per frame
    void Update()
    {
base.Update();
    }
    public override void RangeAttack()
    {

        anim.SetTrigger("Shoot");
        ShootArrow();
    }

    private void ShootArrow()
    {
        if (player == null) return;
        float offsetX = Random.Range(-5f, 5f); // random range for x offset
        Vector3 spawnPos = new Vector3(transform.position.x + offsetX, transform.position.y, 0f);
        GameObject arrow = Instantiate(arrowPrefab, spawnPos, Quaternion.Euler(0f, 0f, 0f));
        arrow.GetComponent<arrow>().player = player;
    }
}
