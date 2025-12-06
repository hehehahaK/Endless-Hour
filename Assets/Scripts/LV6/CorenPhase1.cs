using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorenPhase1 : CorenState
{
    public int NumberOfAttacks=4;
    public override void EnterState()
    {
        Debug.Log("Entered coren Phase 1");
        coren.anim.SetTrigger("Phase1");
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
        public void HandleAttackLogic()
    {
        int AttackDecision=Random.Range(0, NumberOfAttacks); // Randomly choose an attack from 0 to 3
        switch (AttackDecision)
        {
            case 0:
                coren.anim.SetTrigger("Attack1");
                break;
            case 1:
                coren.anim.SetTrigger("Attack2");
                break;
            case 2:
                coren.anim.SetTrigger("Attack3");
                break;
            case 3:
                coren.anim.SetTrigger("Attack4");
                break;
            default:
                coren.anim.SetTrigger("Attack1");
                break;
        }
    }

}
