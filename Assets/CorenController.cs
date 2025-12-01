using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorenController : MonoBehaviour // Script that has Coren Follow our main Player. I think i'll make coren as a prefab after i finish this :). She follows us throughout LV1 to LV 5
{

    public Transform player;  // Reference to the player's transform
    public float followSpeed = 2f;  // Speed at which Coren follows the player
    public Vector3 offSet = new Vector3(2f, 0f, 0f); /* the offSet distance between her and player. how does this work?
    ely by7sl enha it checks this offSet before following the player. if player is at 10,0,0 and offSet is 2,0,0 like this default value yb2a coren will stop at 8,0,0.
    */
    public SpriteRenderer sr; // SpriteRenderer for Coren
    public SpriteRenderer playerSR;// sprite renderer for player to check which direction he is facing
    public float teleportDistance = 6f; // if coren is  too far, she teleports behind the player

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        playerSR = player.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Follow();
    }
    //TBD:Does coren hit walls? if she does, then we need to add collider
    public void Follow()
    {
        if (!player)
        {
            Debug.Log("No player found for Coren ya gama3a please put player");
            return;
        }
        else
        {
            sr.flipX = playerSR.flipX;

            // dynamic offSet based on direction
            if (sr.flipX == true) // if player is facing left
            {
                offSet = new Vector3(-2f, 0f, 0f); // coren should be on the right side of player
            }
            else
            {
                offSet = new Vector3(2f, 0f, 0f); // coren should be on the left side of player
            }

            // Teleport behind player if too far
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance > teleportDistance)
            {
                transform.position = player.position - offSet;
                return;
            }

            Vector3 targetPos = player.position - offSet;
            transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);
        }
    }

}

