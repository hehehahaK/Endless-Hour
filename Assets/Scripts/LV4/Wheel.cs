using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour, InterfaceInteractable
{
    public bool IsTaken { get; private set; }

    public bool CanInteract()
    {
        return !IsTaken;
    }

    public void Interact()
    {
        if (!CanInteract()) return;

        // Mark wheel as taken so it can't be interacted twice
        IsTaken = true;

        // Hide the object from the scene
        gameObject.SetActive(false);

        Debug.Log("Cannon wheel picked up!");
    }
}
