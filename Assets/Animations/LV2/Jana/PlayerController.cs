using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour {
    public float moveSpeed;
    public float jumpHeight;
    //private bool IsFacingRight; //check if player is facing right
    public KeyCode Spacebar; 
    public KeyCode L;
    public KeyCode R;
    public KeyCode A;
    public KeyCode S;
    public Transform groundCheck;// usually an empty child positioned at the player’s feet. Used to detect if the player is touching ground.
    public float groundCheckRadius; 
    public LayerMask whatIsGround; //this variable stores what is considered a ground to the character,defines which physics layers count as ground.
    private bool grounded;
    private Animator anim;

	void Start () {
        anim = GetComponent<Animator>();
    }

	void Update () {
        if(Input.GetKeyDown(Spacebar) && grounded)
        {Jump(); }

        if (Input.GetKey(L))
        {
            Debug.Log("going left");
            GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y); 
            //player character moves horizontally to the left along the x-axis without disrupting jump
            if(GetComponent<SpriteRenderer>()!=null)
            {GetComponent<SpriteRenderer>().flipX = true;}           
        }

        if (Input.GetKey(R))
        {
            Debug.Log("going right");
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y); 
            //player character moves horizontally to the right along the x-axis without disrupting jump
            if(GetComponent<SpriteRenderer>()!=null)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }   
        }

        if (Input.GetKey(A))
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y); 
                    //player character moves horizontally to the left along the x-axis without disrupting jump
                    if(GetComponent<SpriteRenderer>()!=null)
                    {GetComponent<SpriteRenderer>().flipX = true;}           
                }

        if (Input.GetKey(S))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y); 
            //player character moves horizontally to the right along the x-axis without disrupting jump
            if(GetComponent<SpriteRenderer>()!=null)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }   
        }


        anim.SetFloat("Speed",Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
        anim.SetFloat("Height", GetComponent<Rigidbody2D>().velocity.y);
        anim.SetBool("Grounded", grounded);
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround); //this statement calculates when 
        //exactly the character is considered by Unity's engine to be standing on the ground. 
    }

    void Jump()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);    
    }
}

