using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;

    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector] public Item item;
    public int stackCount = 1;
    public Text stackText;
    public void initialize(Item newItem)
    {
        item = newItem;
        image.sprite = newItem.image;
        stackCount = 1;

        UpdateStackUI();
    }
    public void AddToStack(int amount)
    {
        stackCount += amount;
        UpdateStackUI();
    }
    void UpdateStackUI()
    {
        if (stackText != null)
        {
            stackText.text = stackCount > 1 ? stackCount.ToString() : "";
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
    }
}

