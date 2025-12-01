using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Transform CurrentCheckpoint; // so we can update the current checkpoint from within unity
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        CurrentCheckpoint = player;
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void RespawnPlayer()
    {
        player.position = CurrentCheckpoint.position;
    }
}
