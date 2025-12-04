using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChronovarStateMachine : MonoBehaviour
{
    public ChronovarState activeState;
    public ChronovarPhase1 phase1State;
    public ChronovarPhase2 phase2State;
    public ChronovarPhase3 phase3State;

    public void ChangeState(ChronovarState newState)
    {
        if (activeState != null)
            activeState.Exit();
        
        activeState = newState;

        if (activeState != null)
            activeState.EnterState();
    }
    // Start is called before the first frame update
    void Start()
    {
        Chronovar chronovar = GetComponent<Chronovar>();
        phase1State.Initialize(chronovar, this);
        phase2State.Initialize(chronovar, this);
        phase3State.Initialize(chronovar, this);

        // Start Phase1
        ChangeState(phase1State);

    }

    // Update is called once per frame
    void Update()
    {
        if (activeState != null)
            activeState.UpdateState();
    }
}
