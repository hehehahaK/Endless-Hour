using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionDetectorLvl2 : MonoBehaviour
{
    private IInteractableLvl2 interactableInRange = null;
    public GameObject interactionIconn;

    // Start is called before the first frame update
    void Start()
    {
        interactionIconn.SetActive(false);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            interactableInRange?.Interact();
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractableLvl2 interactable) && interactable.CanInteract())
        {
            interactableInRange = interactable;
            interactionIconn.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractableLvl2 interactable) && interactable == interactableInRange)
        {
            interactableInRange = null;
            interactionIconn.SetActive(false);
        }
    }
}