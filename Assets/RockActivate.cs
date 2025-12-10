using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RockActivate : MonoBehaviour
{
    public RockMovement rock;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            rock.isActivated = true;
        }
    }
}
