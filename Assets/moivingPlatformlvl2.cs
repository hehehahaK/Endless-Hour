using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moivingPlatformlvl2 : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float moveSpeed;
    private Vector3 nextPosition;

    void Start()
    {
        nextPosition = pointB.position;
    }

    void Update()//update func dee ll frames tb efrdy comp slow 30FPS la 5leeha bel time
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, moveSpeed * Time.deltaTime);//v=d/t f last param is distance moved

        if (transform.position == nextPosition)
        {
            if (nextPosition == pointB.position)
            {
                nextPosition = pointA.position;
            }
            else
            {
                nextPosition = pointB.position;
            }
        }
    }
    //built in func
    private void OnCollisionEnter2D(Collision2D collision)//package of data of the obj that just hit us
    {
        if (collision.gameObject.CompareTag("Player"))
        //collisiondata el game object bta3ha(player) el transform bta3o(posiotion scale rotation) el parent bta3 atreas=transform el platform
        {
            collision.gameObject.transform.parent = transform;//position and rotation only homa elly inherited
        }
    }
    //built in func
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = null;//deattatch y ba4a
        }
    }
}