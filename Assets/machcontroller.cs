using UnityEngine;
using System.Collections;

public class machcontroller : MonoBehaviour
{
    public DMmach dialogueManager;
    public Transform player;
    public SpriteRenderer spriteRenderer;

    public float walkSpeed = 2f;

    private bool isWalkingAway = false;
    private bool hasTriggered = false;

    public Animator animator;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        if (!player && GameObject.FindWithTag("Player")) 
            player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        if (animator)
        {
            animator.SetBool("isWalkingAway", isWalkingAway);
        }
        if (isWalkingAway)
        {
            // Face Right
            if (spriteRenderer) spriteRenderer.flipX = false; 
            // Move Right
            transform.Translate(Vector2.right * walkSpeed * Time.deltaTime);
        }
        else
        {
            FlipSprite();
        }
    }

    public virtual void FlipSprite()
    {
        if (spriteRenderer && player)
            spriteRenderer.flipX = player.position.x < transform.position.x;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;

            string[] dialogue = {
                "Ah! at last we meet...", 
                "You're here to 'defeat' me I assume? I'd hate to disappoint you but, there is no such thing.", 
                "I'm not like any other ruler you'll meet, I don't care for brawns or fighting skill.", 
                "I only respect the mind.", 
                "That being said, Come, Follow me, for I shall test your true brain power", 
            };
            
            // Pass 'this' (the script itself) so the manager knows who to tell to walk away
            dialogueManager.StartDialogue(dialogue, this);
            
            // Destroy the trigger so it doesn't happen again
            Destroy(GetComponent<BoxCollider2D>());
        }
    }

    // The Dialogue Manager will call this when the text is all done
    public void FinishDialogueAndWalk()
    {
        isWalkingAway = true;
    }
}