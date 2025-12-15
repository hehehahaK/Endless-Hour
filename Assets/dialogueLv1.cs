using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueLv1 : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    private string[] dialogueSentences;
    private int index = 0;

    public float typingSpeed = 0.02f;
    public GameObject continueButton;
    public GameObject dialogueBox;
    public Rigidbody2D playerRB;

    void Start()
    {
        dialogueBox.SetActive(false);
        continueButton.SetActive(false);
    }

    public void SetSentences(string[] sentences)
    {
        dialogueSentences = sentences;
    }

    public IEnumerator TypeDialogue()
    {
        dialogueBox.SetActive(true);

        // Freeze player position while talking
        playerRB.constraints = RigidbodyConstraints2D.FreezePositionX |
                               RigidbodyConstraints2D.FreezePositionY |
                               RigidbodyConstraints2D.FreezeRotation;

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
            StartCoroutine(TypeDialogue());
        }
        else
        {
            // End dialogue
            textDisplay.text = "";
            dialogueBox.SetActive(false);
            dialogueSentences = null;
            index = 0;

            // Unfreeze player (keep rotation frozen)
            playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
    public void StartDialogue(string[] sentences)
{
    SetSentences(sentences);
    StopAllCoroutines();
    textDisplay.text = "";
    StartCoroutine(TypeDialogue());
}

}
