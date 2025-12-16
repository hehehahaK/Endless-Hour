using System.Collections;
using UnityEngine;
using TMPro;

public class DMmach : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public float typingSpeed = 0.02f; 
    public GameObject continueButton;
    public GameObject dialogueBox; 
    public Rigidbody2D playerRB;

    private string[] dialogueSentences; 
    private int index = 0;
    private machcontroller currentNPC; // Reference to Machiavelli

    void Start()
    {
        dialogueBox.SetActive(false);
        continueButton.SetActive(false);
    }

    // Call this to start everything
    public void StartDialogue(string[] sentences, machcontroller npc)
    {
        dialogueSentences = sentences;
        currentNPC = npc;
        index = 0;

        dialogueBox.SetActive(true);
        
        // Freeze Player
        if(playerRB)
            playerRB.constraints = RigidbodyConstraints2D.FreezeAll;

        StartCoroutine(Type());
    }

    IEnumerator Type()
    {
        continueButton.SetActive(false);
        textDisplay.text = ""; // Clear old text
        
        foreach (char letter in dialogueSentences[index].ToCharArray())
        {
            textDisplay.text += letter; 
            yield return new WaitForSeconds(typingSpeed);
        }
        
        continueButton.SetActive(true);
    }

    public void NextSentence()
    {
        continueButton.SetActive(false);

        if (index < dialogueSentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        textDisplay.text = "";
        dialogueBox.SetActive(false);
        
        // Unfreeze Player (Keep rotation frozen if needed)
        if(playerRB)
            playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;

        // Tell Machiavelli to walk away
        if (currentNPC != null)
        {
            currentNPC.FinishDialogueAndWalk();
        }
    }
}