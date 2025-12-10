using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform Player;
    public float moveSpeed = 8f; 
    public int moveDirection = -1;
    public int damageAmount = 5;
    public  bool isActivated = false;
    private Rigidbody2D rb;
    private Animator anim;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        anim= GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isActivated)return;
        rb.velocity = new Vector2(moveSpeed * moveDirection, rb.velocity.y);
    }

    void OnTriggerEnter2D(Collider2D other){
        
        if (other.tag=="Player"){
            FindObjectOfType<PlayerSuperclass>().TakeDamage(damageAmount);
        }
    }

    
}
