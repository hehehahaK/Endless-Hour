using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenCannon : MonoBehaviour, InterfaceInteractable
{
    public Item requiredItemA;
    public Item requiredItemB;

    public bool IsFixed { get; private set; }
    private bool hasFired;

    public Sprite FixedCannonSprite;

    public BulletLvl4 cannonBall;              // Existing cannonball in scene
    public Vector2 shootDirection = Vector2.right;

    public ParticleSystem smokeEffect;
    public float smokeDelay = 1f;
    public GameObject ObstacleToDisappear;

    // ---------------- INVENTORY CHECK ----------------
    private bool HasItem(Item item, int amount = 1)
    {
        InventoryManager inv = FindObjectOfType<InventoryManager>();
        if (inv == null) return false;

        int count = 0;

        foreach (InventorySlot slot in inv.slots)
        {
            InventoryItem inventoryItem = slot.GetComponentInChildren<InventoryItem>();
            if (inventoryItem != null && inventoryItem.item == item)
            {
                count += inventoryItem.stackCount;
                if (count >= amount)
                    return true;
            }
        }
        return false;
    }

    // ---------------- CAN INTERACT ----------------
    public bool CanInteract()
    {
        // Broken → need items
        if (!IsFixed)
            return HasItem(requiredItemA, 1) && HasItem(requiredItemB, 1);

        // Fixed but not fired → can fire
        if (IsFixed && !hasFired)
            return true;

        // Already fired → no interaction
        return false;
    }

    // ---------------- INTERACT ----------------
    public void Interact()
    {
        if (!CanInteract()) return;

        // FIRST interaction → FIX
        if (!IsFixed)
        {
            RemoveItem(requiredItemA, 1);
            RemoveItem(requiredItemB, 1);
            FixCannon();
            Debug.Log("Cannon Fixed!");
            return;
        }

        // SECOND interaction → FIRE
        if (IsFixed && !hasFired)
        {
            FireCannon();
            hasFired = true;
            Debug.Log("Cannon Fired!");
        }
    }

    // ---------------- FIX CANNON ----------------
    private void FixCannon()
    {
        IsFixed = true;
        GetComponent<SpriteRenderer>().sprite = FixedCannonSprite;
    }

    // ---------------- FIRE CANNON ----------------
    private void FireCannon()
    {
        if (cannonBall == null)
        {
            Debug.LogWarning("CannonBall reference missing!");
            return;
        }

        cannonBall.Fire(shootDirection);

         // Trigger smoke AFTER delay
        if (smokeEffect != null)
            StartCoroutine(PlaySmokeAfterDelay());

        // Optional: disable interaction forever
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
            col.enabled = false;
    }

    // ---------------- REMOVE ITEMS ----------------
    private void RemoveItem(Item item, int amount = 1)
    {
        InventoryManager inv = FindObjectOfType<InventoryManager>();
        if (inv == null) return;

        foreach (InventorySlot slot in inv.slots)
        {
            InventoryItem inventoryItem = slot.GetComponentInChildren<InventoryItem>();
            if (inventoryItem != null && inventoryItem.item == item)
            {
                int remove = Mathf.Min(amount, inventoryItem.stackCount);
                inventoryItem.stackCount -= remove;
                amount -= remove;

                if (inventoryItem.stackCount <= 0)
                    Destroy(inventoryItem.gameObject);

                if (amount <= 0)
                    return;
            }
        }
    }

    private IEnumerator PlaySmokeAfterDelay()
    {
        // Wait for initial delay before smoke
        yield return new WaitForSeconds(smokeDelay);

        // Play smoke
        if (smokeEffect != null)
            smokeEffect.Play();

        // Disable the obstacle immediately
        if (ObstacleToDisappear != null)
            ObstacleToDisappear.SetActive(false);

        // Stop smoke after 2 seconds
        yield return new WaitForSeconds(0.5f);
        if (smokeEffect != null)
            smokeEffect.Stop();
    }
}
