using System.Collections;
using UnityEngine;

public class guardmanager : MonoBehaviour
{
    public DMGuard dialogueManager; 
    public royalguard guard1;
    public royalguard guard2;
    public GameObject nextscenecollider;

    
    private bool sequenceStarted = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !sequenceStarted)
        {
            StartCoroutine(FightSequence());
            
            // Destroy the trigger immediately so it doesn't happen twice
            Destroy(GetComponent<BoxCollider2D>());
        }
    }

    IEnumerator FightSequence()
    {
        SpriteRenderer guardSR = guard1.GetComponent<SpriteRenderer>();
        guardSR.flipX = true;
        sequenceStarted = true;

        
        DialogueLine[] dialogue = {
            new DialogueLine("Guard 1:", "Halt! You're not supposed to be here. You think you can just enter the castle that easily by walking in?"),
            new DialogueLine("Guard 2:", "Who do you think you are?"),
            new DialogueLine("Guard 1:", "Let me get rid of you real quick.")
        };
       

        
        dialogueManager.SetDialogue(dialogue);
        dialogueManager.StartCoroutine(dialogueManager.TypeDialogue());

        
        yield return new WaitForSeconds(0.5f); 
        while (dialogueManager.dialogueBox.activeSelf)
        {
            yield return null;
        }

        
        Debug.Log("Guard 1 Attacking!");
        if(guard1 != null) guard1.canFight = true;

        
        while (guard1 != null)
        {
            yield return null;
        }

        
        Debug.Log("Guard 1 Dead (byebyeee). Guard 2 Attacking!");
        if(guard2 != null) guard2.canFight = true;

        
        while (guard2 != null)
        {
            yield return null;
        }

        
        Debug.Log(" yaayyy Both Dead");
        nextscenecollider.SetActive(true); 
        
        
        Destroy(gameObject);
    }
}