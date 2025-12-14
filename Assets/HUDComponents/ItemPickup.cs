using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemPickup : MonoBehaviour
{
    public Item item; // Assign your ScriptableObject here
    public KeyCode pickupKey = KeyCode.E;
    
    private bool playerNearby = false;
    public GameObject pickupPromptUI; // Optional: UI prompt showing "Press E"]

    void start()
    {

            pickupPromptUI.SetActive(false);
    }

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(pickupKey))
        {
            PickUp();
        }
    }

    void PickUp()
    {
        // Find the InventoryManager and add the item
        InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();
        
        if (inventoryManager != null)
        {
            inventoryManager.AddItem(item);
            Debug.Log("Picked up: " + item.itemName);
            Destroy(gameObject); // Remove item from world
        }
        else
        {
            Debug.LogError("InventoryManager not found!");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            Debug.Log("Player is nearby item: " + item.itemName);

            if (pickupPromptUI != null)
            {
                pickupPromptUI.SetActive(true);
            }
        }
    }   

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            
            if (pickupPromptUI != null)
            {
                pickupPromptUI.SetActive(false);
            }
        }
    }
}