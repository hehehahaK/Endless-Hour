// DMdaggers.cs
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

    private GameObject daggers;
    private GameObject lockpick;
    private Transform spawnLocation;
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

    public void SetPrefabToSpawn(GameObject prefab, GameObject prefabLP, Transform location)
    {
        daggers = prefab;
        lockpick = prefabLP;
        spawnLocation = location;
        daggersSpawned = false;
        lockpickSpawned = false;
    }

    public IEnumerator TypeDialogue()
    {
        dialogueBox.SetActive(true);
        playerRB.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        
        Debug.Log("TypeDialogue - Current index: " + index);
        
        // Check daggers spawn at index 1
        if (index == 1 && daggers != null && spawnLocation != null) 
        {
            Debug.Log("Spawning DAGGERS at position: " + spawnLocation.position);
            GameObject spawnedDagger = Instantiate(daggers, spawnLocation.position, Quaternion.identity);
            Debug.Log("Daggers spawned! GameObject name: " + spawnedDagger.name + " | Active: " + spawnedDagger.activeSelf);
            daggers = null;
        }
        else if (index == 1)
        {
            Debug.LogWarning("Daggers spawn failed - daggers null: " + (daggers == null) + " | spawnLocation null: " + (spawnLocation == null));
        }
        
        // Check lockpick spawn at index 4
        if (index == 4 && lockpick != null && spawnLocation != null) 
        {
            Debug.Log("Spawning LOCKPICK at position: " + spawnLocation.position);
            GameObject spawnedLockpick = Instantiate(lockpick, spawnLocation.position, Quaternion.identity);
            Debug.Log("Lockpick spawned! GameObject name: " + spawnedLockpick.name + " | Active: " + spawnedLockpick.activeSelf);
            lockpick = null;
        }
        else if (index == 4)
        {
            Debug.LogWarning("Lockpick spawn failed - lockpick null: " + (lockpick == null) + " | spawnLocation null: " + (spawnLocation == null));
        }

        foreach (char letter in dialogueSentences[index].ToCharArray())
        {
            textDisplay.text += letter; 
            yield return new WaitForSeconds(typingSpeed);
            
            if (textDisplay.text == dialogueSentences[index])
            {
                continueButton.SetActive(true);
            }
        }
    }

    public void NextSentence()
    {
        Debug.Log("NextSentence called. Current index: " + index);
        continueButton.SetActive(false);
        
        // Spawn items AFTER the sentence is displayed (when clicking continue)
        // Daggers spawn after sentence 1: "Here, a pair of sharp daggers..."
        if (index == 1 && daggers != null && !daggersSpawned) 
        {
            Debug.Log("Spawning daggers!");
            Instantiate(daggers, spawnLocation.position, Quaternion.identity);
            daggersSpawned = true;
        }
        
        // Lockpick spawns after sentence 3: "I also have this lockpick..."
        if (index == 2 && lockpick != null && !lockpickSpawned) 
        {
            Debug.Log("Spawning lockpick!");
            Instantiate(lockpick, spawnLocation.position, Quaternion.identity);
            lockpickSpawned = true;
        }

        if (index < dialogueSentences.Length - 1)
        {
            index++; 
            textDisplay.text = ""; 
            StartCoroutine(TypeDialogue());
        }
        else 
        {
            Debug.Log("Dialogue finished. Closing dialogue box.");
            textDisplay.text = ""; 
            dialogueBox.SetActive(false);
            this.dialogueSentences = null; 
            index = 0;
            daggers = null;
            lockpick = null;
            spawnLocation = null;
            daggersSpawned = false;
            lockpickSpawned = false;
            playerRB.constraints = RigidbodyConstraints2D.None; 
            playerRB.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
}

