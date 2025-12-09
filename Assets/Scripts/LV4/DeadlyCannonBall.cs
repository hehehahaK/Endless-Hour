using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadlyCannonBall : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    // { This script must be on an object with a Collider2D set to Is Trigger = true! }
    // { The object entering must have a Rigidbody2D! }
    {
        if (other.tag == "Player") //Did the thing entering my trigger have the tag Player?
           
        
         FindObjectOfType<LevelManager>().RespawnPlayer(); //Finds the LevelManager script in the scene and Calls the RespawnPlayer() method on i
         // { A LevelManager exists in the scene, The LevelManager has a RespawnPlayer() method, and You spelled the script name exactly the same! }
    }
}
