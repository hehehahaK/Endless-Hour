using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oldmanNPCdialogue : MonoBehaviour
{
    public Collider2D triggerZoneCollider;
    public DialogueManager dialogueManager;

//health potion stuffs
    public GameObject healthPotionPrefab; 
    public Transform spawnLocation;
    
    private bool hasTriggered = false; // Better than disabling the script

    void Start()
    {
        // Automatically find the child's collider if not manually assigned
        if (triggerZoneCollider == null)
        {
            triggerZoneCollider = GetComponentInChildren<Collider2D>();
        }
        
        // Add this script to the child's GameObject so it receives the trigger event
        if (triggerZoneCollider != null && triggerZoneCollider.gameObject != this.gameObject)
        {
            // Add a TriggerForwarder component to the child
            TriggerForwarder forwarder = triggerZoneCollider.gameObject.AddComponent<TriggerForwarder>();
            forwarder.parentDialogue = this;
        }
    }

    public void OnChildTriggerEnter(Collider2D other)
    {
        if (hasTriggered) return; // Prevent multiple triggers
        
        if (other.tag == "Player")
        {
            hasTriggered = true;
            
            Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();
            dialogueManager.playerRB = playerRb;
            
            string[] dialogue = {
                "Slow down there buddy, I hear you're going up Machiavelli, and quite frankly, I can't stand living under his rule.",
                "I like your passion and drive, so I'm going to help you in the best way I know how",
                "Here's a health potion, once you drink it, you'll be rejuvenated and prepared to face any mischievous souls!"
            };
            
            dialogueManager.SetSentences(dialogue);
            dialogueManager.StartCoroutine(dialogueManager.TypeDialogue());
            
            // Destroy the trigger collider
            if (triggerZoneCollider != null)
            {
                Destroy(triggerZoneCollider);
            }
        }
    }
}

// Helper component that forwards trigger events to the parent
public class TriggerForwarder : MonoBehaviour
{
    public oldmanNPCdialogue parentDialogue;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (parentDialogue != null)
        {
            parentDialogue.OnChildTriggerEnter(other);
        }
    }
}