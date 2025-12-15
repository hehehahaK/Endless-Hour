using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsZoneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnTriggerEnter2D(Collider2D other){
        
        
        lvl3playercontroller player = other.GetComponent<lvl3playercontroller>();
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();

        if (player != null && rb != null)
        {
            player.isOnStairs = true;
            
            // CRITICAL FIX: Disable gravity so the vertical bias works
            rb.gravityScale = 0f; 
            
            // Also zero out the current Y velocity to prevent instant sinking/flying
            rb.velocity = new Vector2(rb.velocity.x, 0f);
        }
    }

    void OnTriggerExit2D(Collider2D other){

        if (other.CompareTag("Player"))
    {
        lvl3playercontroller player = other.GetComponent<lvl3playercontroller>();
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>(); // Get the Rigidbody2D

        if (player != null && rb != null)
        {
            player.isOnStairs = false;
            
            // CRITICAL FIX: Restore gravity when the player leaves the zone
            // Set this back to your game's default gravity scale (usually 1 or more)
            rb.gravityScale = 3f; // Use your default gravity scale here 
        }
    }
    }
}