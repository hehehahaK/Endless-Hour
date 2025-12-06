using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CorenState : MonoBehaviour
{
    public Transform player;
    protected CorenBoss coren;
    protected CorenStateMachine machine;
    public void Initialize(CorenBoss coren, CorenStateMachine machine)
    {
        this.coren = coren;
        this.machine = machine;
    }



    public virtual void EnterState() { }
    public virtual void UpdateState() { }
    public virtual void Exit() { }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
