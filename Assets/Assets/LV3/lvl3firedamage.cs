using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lvl3firedamage : MonoBehaviour
{
    // Start is called before the first frame update

    public Collider2D dmgCollider;
    

    public int damageAmount = 10;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.tag=="Player"){
            FindObjectOfType<PlayerSuperclass>().TakeDamage(damageAmount);
        }
    }
}
