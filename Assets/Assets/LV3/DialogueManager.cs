using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    private string[] dialogueSentences; 
    private int index = 0;
    public float typingSpeed; 
    public GameObject continueButton;
    public GameObject dialogueBox; 
    public Rigidbody2D playerRB;


    public GameObject prefabToSpawn;
    public Transform spawnLocation;
    private int spawnAtSentenceIndex = -1;
    int healthsentenceIndex = 2;
//-1 means dont spawn       
    void Start()
    {
        dialogueBox.SetActive(false);
        continueButton.SetActive(false);
    }

    

    public void SetSentences(string[] sentences)
    {
        this.dialogueSentences = sentences;
        
    }

    public void SetPrefabToSpawn(GameObject prefab, Transform location)
    {
        prefabToSpawn = prefab;
        spawnLocation = location;
        spawnAtSentenceIndex = healthsentenceIndex;
    }

    public IEnumerator TypeDialogue()
    {
        dialogueBox.SetActive(true); 
        playerRB. constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        foreach (char letter in dialogueSentences [index]. ToCharArray()){
            textDisplay.text += letter; 
            yield return new WaitForSeconds(typingSpeed);
            if (textDisplay.text == dialogueSentences [index]){
                continueButton.SetActive(true);
            }

        }
    }

    public void NextSentence()
    {

        if (index == spawnAtSentenceIndex && prefabToSpawn != null && spawnLocation != null)
        {
            Debug.Log("Spawning item: " + prefabToSpawn.name);
            Instantiate(prefabToSpawn, spawnLocation.position, Quaternion.identity);
        }

        Debug. Log("Inside NextSentence");
        continueButton.SetActive(false); 
        if (index < dialogueSentences. Length - 1){
            index++; 
            textDisplay. text = ""; 
            StartCoroutine(TypeDialogue());
    }
    else {
        textDisplay. text = ""; 

//resetting everything so pls  dont mess with the 2nd npc stuff
//update: that wasnt the problem lol
        dialogueBox.SetActive(false);
        this.dialogueSentences = null; 
        index = 0; 
        playerRB.constraints = RigidbodyConstraints2D.None; 
        playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    }
    
}