using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject CurrentCheckpoint; // So we can update the current checkpoint from within Unity
    public Transform player;
 
    // Start is called before the first frame update
    void Start()
    {
          CurrentCheckpoint = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     public void RespawnPlayer()
    {
        // Search for the asset/object called PlayerController (or whatever your player's script name is)
        // Once you've found it, change its player game object's position to be at the last checkpoint
        // the player passed through before dying
        GameObject.Find("MC").transform.position = CurrentCheckpoint.transform.position;
    }
}
