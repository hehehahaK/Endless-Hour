using UnityEngine;
using System.Collections;

public class machcontroller3 : MonoBehaviour
{
    public DMmach3 dialogueManager;
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
            
            if (spriteRenderer) spriteRenderer.flipX = false; 
            
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
                "I have decided to present you with a moral dilemma instead of mindlessly throwing guards at you.", 
                "Soon, you shall see a contraption of justice, where a civillian must die for their sins.", 
                "One, a woman who stole bread for her hungry child. The other, an escape prisoner who critisized of the law.", 
                "You must personally pick one to face their consequences, and deem the other's life more valuable", 
                "Now, feel free to explore. Let's see if the ends justify the means...", 
            };
            
            
            dialogueManager.StartDialogue(dialogue, this);
            
            
            Destroy(GetComponent<BoxCollider2D>());
        }
    }

}