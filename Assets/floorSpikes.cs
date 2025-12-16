using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floorSpikes : MonoBehaviour
{
    public int AttackDamage=14;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
private void OnCollisionEnter2D(Collision2D other){

    if (other.gameObject.tag=="Player"){
        other.gameObject.GetComponent<PlayerSuperclass>().TakeDamage(AttackDamage);

    }

}

}
