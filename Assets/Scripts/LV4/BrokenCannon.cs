using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenCannon : MonoBehaviour, InterfaceInteractable
{
    public bool IsFixed { get; private set; }
    public Sprite FixedCannonSprite;

    public ParticleSystem smokeEffect; // Assign your particle system in the Inspector
    public float smokeDelay = 0f;      // Delay before smoke starts, e.g., 1.5f

    public bool CanInteract()
    {
        return !IsFixed;
    }

    public void Interact()
    {
        if (!CanInteract()) return;

        FixedCannon(true);

        // Trigger particle system after a delay
        if (smokeEffect != null)
        {
            StartCoroutine(TriggerSmokeAfterDelay(smokeDelay));
        }

        Debug.Log("Broken Cannon Fixed!");
    }

    private void FixedCannon(bool CannonFixed)
    {
        IsFixed = CannonFixed;
        if (IsFixed)
        {
            GetComponent<SpriteRenderer>().sprite = FixedCannonSprite;
        }
    }

    private IEnumerator TriggerSmokeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        smokeEffect.Play(); // Play the particle system
    }
}
