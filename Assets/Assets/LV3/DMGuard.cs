using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DialogueLine
{
    public string speakerName;
    [TextArea(3, 10)]
    public string sentence;

    public DialogueLine(string name, string text)
    {
        this.speakerName = name;
        this.sentence = text;
    }
}

public class DMGuard : MonoBehaviour
{
    public TextMeshProUGUI nameDisplay; 
    public TextMeshProUGUI textDisplay; 
    public GameObject dialogueBox;
    public GameObject continueButton;
    public float typingSpeed = 0.02f;
    public Rigidbody2D playerRB; 

    private DialogueLine[] lines; 
    private int index;

    void Start()
    {
        dialogueBox.SetActive(false);
        continueButton.SetActive(false);
    }

    public void SetDialogue(DialogueLine[] newLines)
    {
        this.lines = newLines;
        index = 0;
    }

    public IEnumerator TypeDialogue()
    {
        dialogueBox.SetActive(true);
        continueButton.SetActive(false);
        
        if(playerRB != null)
            playerRB.constraints = RigidbodyConstraints2D.FreezeAll;

        if (nameDisplay != null)
        {
            nameDisplay.text = lines[index].speakerName;
        }

        textDisplay.text = ""; 
        foreach (char letter in lines[index].sentence.ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        continueButton.SetActive(true);
    }

    public void NextSentence()
    {
        continueButton.SetActive(false);

        if (index < lines.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(TypeDialogue());
        }
        else
        {
            textDisplay.text = "";
            dialogueBox.SetActive(false);
            
            if(playerRB != null)
            {
                playerRB.constraints = RigidbodyConstraints2D.None;
                playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }
    }
}