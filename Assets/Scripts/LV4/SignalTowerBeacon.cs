using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalTowerBeacon : MonoBehaviour, InterfaceInteractable
{
    public bool IsTriggered { get; private set; }
    public Sprite SignalTowerBeaconOnSprite;

    // GameObjects to switch
    public GameObject SignalTowerOffToDisable;
    public GameObject SignalTowerOnToEnable;

    public bool CanInteract()
    {
        return !IsTriggered;
    }

    public void Interact()
    {
        if (!CanInteract()) return;

        SignalTowerBeaconOn(true);

        // Switch GameObjects
        if (SignalTowerOffToDisable != null)
            SignalTowerOffToDisable.SetActive(false);

        if (SignalTowerOnToEnable != null)
            SignalTowerOnToEnable.SetActive(true);
        Debug.Log("Signal Tower Activated!");
    }

    public void SignalTowerBeaconOn(bool beaconOnSignalTower)
    {
        IsTriggered = beaconOnSignalTower;

        if (IsTriggered)
        {
            GetComponent<SpriteRenderer>().sprite = SignalTowerBeaconOnSprite;
        }
    }
}
