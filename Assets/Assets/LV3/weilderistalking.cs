using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weilderistalking : MonoBehaviour
{
    public DMdaggers dialogueManager;

    public GameObject daggers;



    public Transform spawnLocation;      


    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider is actually the player
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detected! Starting dialogue...");
            
            if (dialogueManager == null)
            {
                Debug.LogError("DialogueManager is NULL! Please assign it in the Inspector.");
                return;
            }
            
            string[] dialogue = {
                "Woah there, for a guy that is rumoured to be going up the oppressor, you sure do seem under equipped.", 
                "Here, a pair of sharp daggers should be of use to you.", 
                "Good luck stranger!", 
            };
            
            dialogueManager.SetSentences(dialogue);

            // UPDATED LINE: We now pass 4 arguments, including the new lockpick location
            dialogueManager.SetPrefabToSpawn(daggers, spawnLocation);
            
            dialogueManager.StartCoroutine(dialogueManager.TypeDialogue());
            
            // Destroy the trigger so dialogue doesn't loop
            Destroy(GetComponent<BoxCollider2D>());
        }
    }
}