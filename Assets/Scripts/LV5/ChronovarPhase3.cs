using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChronovarPhase3 : ChronovarState
{
    // Start is called before the first frame update
    public override void EnterState()
    {
        Debug.Log("Entered Chronovar Phase 3");
        chronovar.anim.SetTrigger("Phase3");
    }
    public override void UpdateState()
    {
        if (!chronovar.phase3AttackStarted)
        {
            chronovar.phase3AttackStarted = true;
            StartCoroutine(chronovar.Phase3Attack1Routine());
        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
