using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorenStateMachine : MonoBehaviour
{
    // Start is called before the first frame update
    public CorenState activeState;
    public CorenPhase1 phase1State;
    public CorenPhase2 phase2State;
    private CorenBoss coren;

    void Start()
    {
        CorenBoss Coren = GetComponent<CorenBoss>();
        phase1State = GetComponent<CorenPhase1>();
        phase2State = GetComponent<CorenPhase2>();
        
        phase1State.Initialize(Coren, this);
        phase2State.Initialize(Coren, this);

        // Start Phase1
        ChangeState(phase1State);

    }
    public void ChangeState(CorenState newState)
    {
        if (activeState != null)
            activeState.Exit();

        activeState = newState;

        if (activeState != null)
            activeState.EnterState();
    }

    // Update is called once per frame
    public void Update()
    {
        if (activeState != null)
            activeState.UpdateState();
    }
}
