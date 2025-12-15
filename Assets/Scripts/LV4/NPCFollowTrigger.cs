using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFollowTrigger : MonoBehaviour
{
    public NPCFollow npc;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            npc.ActivateFollow();
        }
    }
}