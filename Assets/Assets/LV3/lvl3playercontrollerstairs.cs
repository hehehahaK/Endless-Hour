using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lvl3playercontrollerstairs : PlayerSuperclass
{
    // Start is called before the first frame update

    public bool isOnStairs = false;
    public float stairClimbBias = 0.75f;
    public float stairSpeedMultiplier = 0.5f;
    void Start()
    {
        
    }

    

    override public void Update(){
        float yVelocity = GetComponent<Rigidbody2D>().velocity.y;
        if (isOnStairs){
            moveSpeed *= stairSpeedMultiplier;

            if (Input.GetKey(R)) 
            {
                // Going UP: positive Y bias
                yVelocity = stairClimbBias; 
            }
            else if (Input.GetKey(L)) 
            {
                // Going DOWN: negative Y bias
                yVelocity = -stairClimbBias;
            }
            else{
                yVelocity = 0;
            }
        }
        else {
            yVelocity = GetComponent<Rigidbody2D>().velocity.y;
        }

        
    }
}
