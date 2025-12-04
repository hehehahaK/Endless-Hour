using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChronovarPhase1 : ChronovarState
{
    private float attackCooldown = 1.4f;
    private float attackTimer = 0f;
    // Start is called before the first frame update
    public override void EnterState()
    {
        Debug.Log("Entered Chronovar Phase 1");
        attackTimer = attackCooldown;
        chronovar.anim.SetTrigger("Phase1");
    }
    public override void UpdateState()
    {
        MoveTowardsPlayer();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void MoveTowardsPlayer()
    {
        chronovar.transform.position = Vector2.MoveTowards(chronovar.transform.position, player.position, chronovar.moveSpeed * Time.deltaTime); // normal movement towards player (refine)
    }

}
