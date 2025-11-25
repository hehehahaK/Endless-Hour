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
        CurrentCheckpoint = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RespawnPlayer()
    {
        if(CurrentCheckpoint != null)
        {
            FindObjectOfType<PlayerSuperclass>().transform.position = CurrentCheckpoint.transform.position;
            //CHANGETO YOUR PLAYERCONTROLLER NAME IMPORTANTTTT
        }
    }
}
