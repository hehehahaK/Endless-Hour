using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] slots;
    public GameObject inventoryItemPrefab;

    public void AddItem(Item item)
    {

        if (item.stackable)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                InventorySlot slot = slots[i];
                InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
                

                if (itemInSlot != null && itemInSlot.item == item)
                {
                    itemInSlot.AddToStack(1);
                    return;
                }
            }
        }


        for (int i = 0; i < slots.Length; i++)
        {
            InventorySlot slot = slots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                return;
            }
        }


        Debug.Log("Inventory is full!");
    }

    void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.initialize(item);
    }
}
