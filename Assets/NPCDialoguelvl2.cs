using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialoguelvl2 : MonoBehaviour
{
    public DialogueLv1 dialogueManager; // drag DialogueManager here in Inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            string[] dialogue = new string[]
            {
                "Hello Atreus.",
                "The Temple awaits you.",
                "Solve the puzzle to enter the temple."
            };

            dialogueManager.SetSentences(dialogue);
            dialogueManager.StartCoroutine(dialogueManager.TypeDialogue());

            // optional: prevent retrigger
            Destroy(GetComponent<Collider2D>(), 5f);
        }
    }
}

