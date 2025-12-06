using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutAnkhAmoon : MonoBehaviour, IInteractableLvl2
{
    public bool IsTaken { get; private set; }

    public bool CanInteract()
    {
        return !IsTaken;
    }

    public void Interact()
    {
        if (!CanInteract()) return;

        // Mark TutAnkhAmoon as taken so it can't be interacted twice
        IsTaken = true;

        // Hide the object from the scene
        gameObject.SetActive(false);

        Debug.Log("TutAnkhAmoon collected!");
    }
}