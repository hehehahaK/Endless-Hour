using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenCannon : MonoBehaviour, InterfaceInteractable
{
    public bool IsFixed { get; private set; }
    public Sprite FixedCannonSprite;

    public bool CanInteract()
    {
        return !IsFixed;
    }

    public void Interact()
    {
        if (!CanInteract()) return;
        FixedCannon(true);
        // Mark wheel as taken so it can't be interacted twice
  

        Debug.Log("Broken Cannon Fixed!");
    }

    public void FixedCannon(bool CannonFixed)
    {
        if (IsFixed = CannonFixed)
        {
            GetComponent<SpriteRenderer>().sprite = FixedCannonSprite;
        }
    }
}

