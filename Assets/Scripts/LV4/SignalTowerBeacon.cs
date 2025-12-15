using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalTowerBeacon : MonoBehaviour, InterfaceInteractable
{
    public bool IsTriggered { get; private set; }

    public Animator animator;
    public GameObject SignalTowerToDisable;
    public CameraShakeLvl4 cameraShake;

    public bool CanInteract()
    {
        return !IsTriggered;
    }

    public void Interact()
    {
        if (!CanInteract()) return;

        IsTriggered = true;

        if (animator != null)
            animator.SetTrigger("TurnOn");

        if (SignalTowerToDisable != null)
            SignalTowerToDisable.SetActive(false);

        if (cameraShake != null)
            StartCoroutine(cameraShake.Shake(2.5f, 0.08f));

        Debug.Log("Signal Tower Activated!");
    }
}

