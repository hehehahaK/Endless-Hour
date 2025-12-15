using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // if the collider of the object whose name is Player GameObject touches the checkpoint circle collider
            FindObjectOfType<LevelManager>().SetCheckpoint(transform);
            //go to the level manager script and update the value of currentcheckpoint to become the new checkpoint the player has just pass through. this is necessary when you have several checkpoints in a level.
        }
    }
}
