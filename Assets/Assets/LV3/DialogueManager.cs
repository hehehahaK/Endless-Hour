using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{


    public TextMeshProUGUI textDisplay; //a special variable that holds the TextMeshPro - Text for manipulation
    private string[] dialogueSentences; //an array that stores all the sentences to be displayed
    private int index = 0; //a variable that signifies which sentence is being printed or to be printed
    public float typingSpeed; //a variable to control the speed of the typewriter effect
    public GameObject continueButton; //a variable that holds the continue button
    public GameObject dialogueBox; //a variable that holds the panel (dialogue box)
    public Rigidbody2D playerRB; //a variable that holds the player's/character's Rigidbody2D component
    // Start is called before the first frame update


    private GameObject prefabToSpawn;
    private Transform spawnLocation;
    private int spawnAtSentenceIndex = -1; // -1 means don't spawn anything
    private bool hasSpawned = false;

    
    void Start()
    {
        dialogueBox.SetActive(false);
        continueButton.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSentences(string[] sentences){
        this.dialogueSentences = sentences;
    }

    public IEnumerator TypeDialogue(){
        dialogueBox.SetActive(true); //enables the dialogue box
        playerRB.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY; //freezing the player in place

        foreach (char letter in dialogueSentences[index]. ToCharArray()){
            textDisplay.text += letter; //adding each char to the displayed text

            yield return new WaitForSeconds(typingSpeed); //this special type of return is used with the IEnumerator and StartCoroutine() function

            if (textDisplay.text == dialogueSentences [index]){
                continueButton.SetActive(true);
            }
        }
    }


    //increment fel sentences arrraaayyy
    public void NextSentence() {
        Debug. Log("Inside NextSentence");
        dialogueBox.SetActive(true);

        continueButton.SetActive(false); //disables the continue button to avoid bugs
        if (index < dialogueSentences.Length - 1){
            index++; //move to the next sentence
            textDisplay.text = ""; //clear the displayed text
            StartCoroutine(TypeDialogue());
        }
        else{
            textDisplay.text = ""; //clear the displayed text
            //continueButton.SetActive(false); //disable the continue button
            dialogueBox.SetActive(false); //disable the dialogue box
            this.dialogueSentences = null; //clear the sentences array
            index = 0; //reset the index
            playerRB.constraints = RigidbodyConstraints2D.None; //unfreeze the player
            playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }}
