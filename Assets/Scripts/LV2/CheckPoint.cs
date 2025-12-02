using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    void Start()
    {
        
    }
    void Update()
    {
        
    }
public void OnTriggerEnter2D(Collider2D other)
    {
       if(other.tag=="Player"){
         FindObjectOfType<LevelManager>().CurrentCheckpoint =this.gameObject;
    }
    }
}
