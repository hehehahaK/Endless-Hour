using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Vector3 CurrentCheckpoint;
    public Transform player;

    void Start()
    {
        CurrentCheckpoint = player.position;
    }

    public void RespawnPlayer()
    {
        player.position = CurrentCheckpoint;
    }

    public void SetCheckpoint(Vector3 newCheckpoint)
    {
        CurrentCheckpoint = newCheckpoint;
    }
        public void SetCheckpoint(Transform checkpointTransform)
    {
        CurrentCheckpoint = checkpointTransform.position;
    }
}
