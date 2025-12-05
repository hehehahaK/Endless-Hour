using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChronovarState : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    protected Chronovar chronovar;
    protected ChronovarStateMachine machine;
    public void Initialize(Chronovar chronovar, ChronovarStateMachine machine)
    {
        this.chronovar = chronovar;
        this.machine = machine;
    }


    public virtual void EnterState() { }
    public virtual void UpdateState() { }
    public virtual void Exit() { }

}
