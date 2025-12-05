using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChronovarPhase2 : ChronovarState
{
    
    public override void EnterState()
    {
        Debug.Log("Entered Chronovar Phase 2");
        chronovar.anim.SetTrigger("Phase2");
    }
    public override void UpdateState()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
