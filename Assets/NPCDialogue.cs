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
              "Hold, warrior None may enter the arena unchallenged.",

"A monstrous guardian stands watch.",

"Seek the power potion nearby. You will need its strength to survive.",

"Defeat the beast, then face Hannibal to complete the First Trial.",

"Only then will you be worthy to wield Excalibur and challenge the Dragon."


            };

            dialogueManager.SetSentences(dialogue);
            dialogueManager.StartCoroutine(dialogueManager.TypeDialogue());

            // optional: prevent retrigger
            Destroy(GetComponent<Collider2D>(), 5f);
        }
    }
}
