using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCFollow : MonoBehaviour
{
    [Header("References")]
    public Transform player;               // drag your player here
    public Button interactButton;          // drag InteractButton (UI) here
    public GameObject dialoguePanel;       // drag DialoguePanel here
    public TextMeshProUGUI dialogueText;   // drag DialogueText (UI Text) here (or TMP Text)

    [Header("Movement")]
    public float speed = 2f;               // NPC walking speed
    public float stopDistance = 3.2f;      // distance at which NPC stops moving

    [Header("Dialogue & Death")]
    public string[] dialogueLines;         // optional: lines to show when interacting
    public float delayBeforeDie = 2f;      // seconds after interaction to start dying

    private Animator anim;
    private bool isDead = false;
    private bool isInRange = false;
    private bool canFollow = false;
    private int currentDialogueIndex = 0;

    void Start()
    {
        anim = GetComponent<Animator>();

        // ensure UI references are set
        if (interactButton != null)
        {
            interactButton.onClick.RemoveAllListeners();
            interactButton.onClick.AddListener(OnInteractButtonPressed);
            interactButton.gameObject.SetActive(false);
        }

        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }
    }

    void Update()
    {
        if (isDead) return; // no movement if dead

        if (!canFollow)
        {
            anim.SetBool("Walking", false);
            return;
        }

        float distance = Vector2.Distance(transform.position, player.position);

        // If player far away → walk toward player
        if (distance > stopDistance)
        {
            // hide interact UI
            if (interactButton != null && interactButton.gameObject.activeSelf)
                interactButton.gameObject.SetActive(false);

            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)direction * speed * Time.deltaTime;

            anim.SetBool("Walking", true);
            isInRange = false;
        }
        else
        {
            // Player is close → stop moving and show interact button
            anim.SetBool("Walking", false);

            if (!isInRange)
            {
                isInRange = true;
                // Optionally snap exactly to stopping distance
                // Vector3 toPlayer = player.position - transform.position;
                // transform.position += (Vector3)toPlayer.normalized * Mathf.Max(0, toPlayer.magnitude - stopDistance);
            }

            if (interactButton != null && !isDead)
            {
                interactButton.gameObject.SetActive(true);
            }
        }
    }

    // Called by the UI Button's OnClick
    public void OnInteractButtonPressed()
    {
        if (isDead) return;

        // open dialogue
        if (dialoguePanel != null)
        {
            currentDialogueIndex = 0;
            dialoguePanel.SetActive(true);
            ShowCurrentDialogueLine();
        }

        // hide interact button while dialogue open (optional)
        if (interactButton != null)
            interactButton.gameObject.SetActive(false);

        // start the death timer (2 seconds before dying)
        StartCoroutine(DeathSequenceCoroutine());
    }

    private void ShowCurrentDialogueLine()
    {
        if (dialogueText == null) return;

        if (dialogueLines != null && dialogueLines.Length > 0)
        {
            dialogueText.text = dialogueLines[currentDialogueIndex];
        }
        else
        {
            dialogueText.text = "Hello."; // fallback
        }
    }

    // If you want "next" or closing from dialogue UI, add UI button(s) to call this:
    public void CloseDialogue()
    {
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);

        // If you want the NPC to remain dead even if dialogue closed earlier, we already set isDead later.
        // If not dead yet and still in range, show interact button again:
        if (!isDead && isInRange && interactButton != null)
            interactButton.gameObject.SetActive(true);
    }

    private IEnumerator DeathSequenceCoroutine()
    {
        // Wait the configured delay (2s by default)
        yield return new WaitForSeconds(delayBeforeDie);

        // trigger animator to play dying animation
        anim.SetTrigger("Die"); // make sure this trigger exists

        // prevent movement and further interactions
        isDead = true;

        // hide interact button
        if (interactButton != null)
            interactButton.gameObject.SetActive(false);

        // (Optional) close dialogue panel immediately or leave it open during dying
        // If you want to leave it open while NPC dies, remove/modify the next line:
        // if (dialoguePanel != null) dialoguePanel.SetActive(false);

        // Wait for the dying clip length then freeze on last frame
        float dieClipLength = GetAnimationClipLength(anim, "deathT");
        if (dieClipLength <= 0f)
        {
            // fallback: use a default wait
            dieClipLength = 1.0f;
        }

        // Wait for the dying animation to finish playing
        yield return new WaitForSeconds(dieClipLength);

        // Freeze animator so it stays on the final frame
        anim.speed = 0f;

        // Optionally set a boolean to mark fully dead (for other systems)
        anim.ResetTrigger("Die");
        anim.SetBool("IsDead", true); // optional param; create if desired
    }

    // Utility: find clip length by name in animator
    private float GetAnimationClipLength(Animator animator, string clipName)
    {
        if (animator == null) return 0f;
        RuntimeAnimatorController rac = animator.runtimeAnimatorController;
        if (rac == null) return 0f;
        foreach (var clip in rac.animationClips)
        {
            if (clip.name == clipName)
            {
                return clip.length;
            }
        }
        return 0f;
    }

    public void ActivateFollow()
    {
        canFollow = true;
    }
}