using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorenPhase2 : CorenState
{
      public override void EnterState()
    {
        Debug.Log("Entered Coren Phase 2");
        coren.anim.SetTrigger("Phase2");
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
