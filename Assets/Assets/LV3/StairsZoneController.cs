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
        
        lvl3playercontrollerstairs player = FindObjectOfType<lvl3playercontrollerstairs>();

        if (other.tag=="Player"){
            player.isOnStairs = true;
        }
    }

    void OnTriggerExit2D(Collider2D other){

        lvl3playercontrollerstairs player = FindObjectOfType<lvl3playercontrollerstairs>();

        if (other.tag=="Player"){
            player.isOnStairs = false;
        }
    }
}
