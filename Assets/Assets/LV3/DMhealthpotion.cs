using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DMhealthpotion : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public GameObject dialogueBox;
    public GameObject continueButton;
    public float typingSpeed = 0.02f;
    public Rigidbody2D playerRB; 

    private string[] sentences;
    private int index;


    private GameObject itemToSpawn;
    private Transform locationToSpawn;
    private bool itemSpawned = false;

    void Start()
    {
        dialogueBox.SetActive(false);
        continueButton.SetActive(false);
    }

    public void SetSentences(string[] newSentences)
    {
        this.sentences = newSentences;
        index = 0;
    }

    // This function allows the Old Man script to pass the potion in
    public void SetPrefabToSpawn(GameObject prefab, Transform location)
    {
        itemToSpawn = prefab;
        locationToSpawn = location;
        itemSpawned = false; // Reset so it can spawn again
    }

    public IEnumerator TypeDialogue()
    {
        dialogueBox.SetActive(true);
        continueButton.SetActive(false);
        
        if(playerRB != null)
            playerRB.constraints = RigidbodyConstraints2D.FreezeAll;

        // --- SPAWN LOGIC ---
        // Spawn specifically at Index 2 (The 3rd line: "Here's a health potion...")
        if (index == 2 && itemToSpawn != null && !itemSpawned)
        {
            Instantiate(itemToSpawn, locationToSpawn.position, Quaternion.identity);
            itemSpawned = true;
            Debug.Log("Potion Spawned!");
        }
        // -------------------

        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        continueButton.SetActive(true);
    }

    public void NextSentence()
    {
        continueButton.SetActive(false);

        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(TypeDialogue());
        }
        else
        {
            textDisplay.text = "";
            dialogueBox.SetActive(false);
            
            // Clean up references
            itemToSpawn = null;
            itemSpawned = false;

            if(playerRB != null)
            {
                playerRB.constraints = RigidbodyConstraints2D.None;
                playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }
    }
}