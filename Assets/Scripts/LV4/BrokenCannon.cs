using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenCannon : MonoBehaviour, InterfaceInteractable
{
    public Item requiredItemA;
    public Item requiredItemB;

    public bool IsFixed { get; private set; }
    public Sprite FixedCannonSprite;

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

    public bool CanInteract()
    {
        if (IsFixed) return true;

        return HasItem(requiredItemA, 1) && HasItem(requiredItemB, 1);
    }

    public void Interact()
    {
        if (!CanInteract()) return;

        RemoveItem(requiredItemA, 1);
        RemoveItem(requiredItemB, 1);

        FixedCannon(true);

        Debug.Log("Broken Cannon Fixed!");
    }

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
                else
                    inventoryItem.SendMessage("UpdateStackUI");

                if (amount <= 0)
                    return;
            }
        }
    }

    private void FixedCannon(bool CannonFixed)
    {
        IsFixed = CannonFixed;
        if (IsFixed)
        {
            GetComponent<SpriteRenderer>().sprite = FixedCannonSprite;
        }
    }
}
