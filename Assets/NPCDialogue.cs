using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public DialogueLv1 dialogueManager; // drag DialogueManager here in Inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            string[] dialogue = new string[]
            {
                "Hello Atreus.",
                "The Colosseum awaits you.",
                "Defeat Hannibal to pass the trial."
            };

            dialogueManager.SetSentences(dialogue);
            dialogueManager.StartCoroutine(dialogueManager.TypeDialogue());

            // optional: prevent retrigger
            Destroy(GetComponent<Collider2D>(), 5f);
        }
    }
}
