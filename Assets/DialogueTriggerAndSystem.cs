using UnityEngine;

public class DialogueTriggerAndSystem : MonoBehaviour
{
    [Header("Enemy Spawning")]
    public string npcTag = "NPC";    // tag of the existing NPC in the scene
    public float spawnOffsetX = 2f;  // optional offset if you want to move it

    private GameObject npcToEnable;

    private string[] dialogueSentences = new string[]
    {
        "Hello hero!",
        "This is a simple dialogue system.",
        "An enemy will appear after the dialogue.",
        "Get ready!"
    };

    private int index = 0;
    private bool isTalking = false;
    private bool hasTriggered = false;


    private void Start()
    {
        // find the NPC in the scene and deactivate it initially
        npcToEnable = GameObject.FindGameObjectWithTag(npcTag);
        if (npcToEnable != null)
        {
            npcToEnable.SetActive(false);
        }
        else
        {
            Debug.LogWarning("No NPC with tag '" + npcTag + "' found in the scene!");
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;
            isTalking = true;
            index = 0;

            // Enable NPC instead of instantiating
            if (npcToEnable != null)
            {
                npcToEnable.SetActive(true);

            

                Debug.Log("NPC Enabled!");
            }

            Debug.Log("Dialogue Started:");
            ShowLine();
        }
    }


    private void Update()
    {
        if (isTalking && Input.GetKeyDown(KeyCode.Space))
        {
            NextLine();
        }
    }


    private void ShowLine()
    {
        Debug.Log(dialogueSentences[index]);
    }


    private void NextLine()
    {
        if (index < dialogueSentences.Length - 1)
        {
            index++;
            ShowLine();
        }
        else
        {
            Debug.Log("Dialogue Finished.");
            isTalking = false;
        }
    }
}
