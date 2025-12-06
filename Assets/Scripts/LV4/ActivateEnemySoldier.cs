using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateEnemySoldier : MonoBehaviour
{
    public SoldierAI soldier;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            soldier.isActivated = true;
        }
    }
}
