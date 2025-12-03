using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class child1 : MonoBehaviour
{
    public float moveSpeed;
    protected Rigidbody2D r;
    protected Animator a;
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Rigidbody2D>();
        a = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>(); 
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){
        if (r != null)
        {
            r.velocity = new Vector2(moveSpeed, r.velocity.y);
        }
    }

    public void Flip(){
        moveSpeed = moveSpeed * -1;
        sr.flipX= !sr.flipX;
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "lvl3Wall"){
            Flip();
        }
    }
}
