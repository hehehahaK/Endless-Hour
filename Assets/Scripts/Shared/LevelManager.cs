using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject CurrentCheckpoint; // so we can update the current checkpoint from within unity
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        CurrentCheckpoint = FindObjectOfType<PlayerSuperclass>().transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Transform CurrentCheckpoint;   // where the player will respawn
    public Transform player;              // drag your Player into this slot in the Inspector

    public void RespawnPlayer()
    {
    player.position = CurrentCheckpoint.position;
    }
}
