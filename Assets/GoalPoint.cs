using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPoint : MonoBehaviour
{
    public TimerScript timer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            timer.StopTimer();
            Debug.Log("Reached goal in time!");
            // SUCCESS LOGIC HERE
        }
    }
}
