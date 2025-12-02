using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour, InterfaceInteractable
{
    public bool IsTaken { get; private set; }
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public bool CanInteract()
    {
        return !IsTaken;
    }
    
    public void Interact()
    {
        if (!CanInteract()) return;
        // Logic for taking the wheel
    }
}
