using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weilderistalking : MonoBehaviour
{
    public DMdaggers dialogueManager;
    public GameObject daggers;
    public GameObject lockpick;
    public Transform spawnLocation;

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger entered by: " + other.name);
        
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detected! Starting dialogue...");
            
            if (dialogueManager == null)
            {
                Debug.LogError("DialogueManager is NULL! Please assign it in the Inspector.");
                return;
            }
            
            string[] dialogue = {
                "Woah there, for a guy that is rumoured to be going up the oppressor, you sure do seem under equipped.", // Index 0
                "Here, a pair of sharp daggers should be of use to you.",  // Index 1 - Daggers spawn AFTER this
                "Good luc-... oh right", // Index 2
                "I also have this lockpick that was laying around.", // Index 3 - Lockpick spawns AFTER this
                "Everyone should always have a lockpick, you never know when you'll need it!", // Index 4
            };
            
            dialogueManager.SetSentences(dialogue);
            dialogueManager.SetPrefabToSpawn(daggers, lockpick, spawnLocation);
            dialogueManager.StartCoroutine(dialogueManager.TypeDialogue());
            Destroy(GetComponent<BoxCollider2D>(), 5f);
        }
    }
}