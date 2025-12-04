using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class woodenlogwithspikespawner : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed = 8f; 
    public int moveDirection = -1;
    public int damageAmount = 3;
    
    private Rigidbody2D rb;
    

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(moveSpeed * moveDirection, rb.velocity.y);
    }

    void OnTriggerEnter2D(Collider2D other){
        
        if (other.tag=="Player"){
            FindObjectOfType<PlayerSuperclass>().TakeDamage(damageAmount);
        }
    }

    
}
