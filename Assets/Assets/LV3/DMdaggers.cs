using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DMdaggers : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    private string[] dialogueSentences; 
    private int index = 0;
    public float typingSpeed; 
    public GameObject continueButton;
    public GameObject dialogueBox; 
    public Rigidbody2D playerRB;

    // Prefabs and Locations
    private GameObject daggers;
    private GameObject lockpick;
    private Transform spawnLocationdag;
    private Transform spawnlocationlockpick;
    
    // Track if items have been given so we don't spawn them twice
    private bool daggersSpawned = false;
    private bool lockpickSpawned = false;
    
    void Start()
    {
        dialogueBox.SetActive(false);
        continueButton.SetActive(false);
    }

    public void SetSentences(string[] sentences)
    {
        this.dialogueSentences = sentences;
    }

    public void SetPrefabToSpawn(GameObject prefab, GameObject prefabLP, Transform location, Transform lockloc)
    {
        daggers = prefab;
        lockpick = prefabLP;
        spawnLocationdag = location;
        spawnlocationlockpick = lockloc;
        
        // Reset spawning flags
        daggersSpawned = false;
        lockpickSpawned = false;
    }

    public IEnumerator TypeDialogue()
    {
        dialogueBox.SetActive(true);
        // Freeze X and Y movement, keep rotation frozen
        playerRB.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        
        // --- SPAWN LOGIC (Inside TypeDialogue) ---
        // Spawn Daggers at Index 1 (Second sentence)
        if (index == 1 && daggers != null && !daggersSpawned) 
        {
            // Fixed: changed 'spawnLocation' to 'spawnLocationdag'
            Instantiate(daggers, spawnLocationdag.position, Quaternion.identity);
            daggersSpawned = true;
            Debug.Log("Daggers spawned!");
        }
        
        
        if (index == 3 && lockpick != null && !lockpickSpawned) 
        {
            Instantiate(lockpick, spawnlocationlockpick.position, Quaternion.identity);
            lockpickSpawned = true;
            Debug.Log("Lockpick spawned!");
        }
        // -----------------------------------------

        // Type the text
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
            // End of dialogue
            textDisplay.text = ""; 
            dialogueBox.SetActive(false);
            index = 0;
            
            // Unfreeze movement, but keep rotation frozen so player doesn't tip over
            playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
}