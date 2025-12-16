using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcken: MonoBehaviour
{
    public DialogueLv1 dialogueManager; // drag DialogueManager here in Inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            string[] dialogue = new string[]
            {
              
              "you should run before its too- is that the legendary excalibur? Humanity might actually have a chance.",
"Hurry up , cross the platforms and fight chronovar, before it releases it's earth shattering breath"


            };

            dialogueManager.SetSentences(dialogue);
            dialogueManager.StartCoroutine(dialogueManager.TypeDialogue());

            // optional: prevent retrigger
            Destroy(GetComponent<Collider2D>(), 5f);
        }
    }
}
