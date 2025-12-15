using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oldmanNPCdialogue : MonoBehaviour{

    public DMhealthpotion dialogueManager;

    public GameObject healthPotionPrefab;
    
    public Transform spawnLocation;

    void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Player"){
            string[] dialogue ={
                "Slow down there buddy, I hear you're going up machiavelli, and quite frankly, I can't stand living under his rule.",
                "I like your passion and drive, so I'm going to help you in the best way I know how",
                "Here's a health potion, once you drink it, you'll be rejuvenated and prepared to face any mischievous souls!",
            };
            dialogueManager.SetSentences(dialogue);
            dialogueManager.SetPrefabToSpawn(healthPotionPrefab, spawnLocation);
            dialogueManager.StartCoroutine(dialogueManager.TypeDialogue());
            Destroy(GetComponent<BoxCollider2D>(), 5f);
        }
    }
}