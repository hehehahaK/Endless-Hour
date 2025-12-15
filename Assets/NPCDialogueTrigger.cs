using UnityEngine;

public class NPCDialogueTrigger : MonoBehaviour
{
    [TextArea(2, 4)]
    public string[] dialogueLines;

    private bool hasTalked = false;
    private DialogueLv1 dialogueManager;

    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueLv1>();

        if (dialogueManager == null)
        {
            Debug.LogError("DialogueLv1 NOT found in scene!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTalked) return;
        if (!other.CompareTag("Player")) return;

        hasTalked = true;
        dialogueManager.StartDialogue(dialogueLines);
    }
}
