using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changecam : MonoBehaviour
{

    public CameraFollow camfollow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D other)
{

    if (other.CompareTag("Player")){

camfollow.minY=35;

    }


}}
