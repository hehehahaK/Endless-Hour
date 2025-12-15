using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenspikeActivate : MonoBehaviour
{
    public WoodenspikeMovement woodenspike;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            woodenspike.isActivated = true;
        }
    }
}
