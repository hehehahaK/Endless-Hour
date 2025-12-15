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
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
