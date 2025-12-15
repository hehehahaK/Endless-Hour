using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gladiatorspeaks : MonoBehaviour{

    public dialogueLv1 dialogueManager;

    public GameObject powerPotionPrefab;
    

    void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Player"){
            string[] dialogue ={

    "Easy there, warrior. I died in this arena, Hannibal himself took me down.",
    "A beast guards the gate now. Beat it, and you’ll earn the right to face him inside.",
    "Take this potion—my last strength. It’ll help you survive what I couldn’t.",
};

            
            dialogueManager.SetSentences(dialogue);
            dialogueManager.StartCoroutine(dialogueManager.TypeDialogue());
            Destroy(GetComponent<BoxCollider2D>(), 5f);
        }
    }
}