using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class daggersscript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
       
        lvl3playercontroller player = other.GetComponent<lvl3playercontroller>();

        if (player != null)
        {
            
            player.hasDaggers = true;
            Debug.Log("Daggers Equipped!");

           
            Destroy(gameObject);
        }
    }
}
