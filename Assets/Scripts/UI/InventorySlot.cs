using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    Item item;

    public Image icon;
    public Button removeButton;
    public Button slotButton;

    private void Start()
    {
        removeButton.onClick.AddListener(OnRemoveButton);

        slotButton.onClick.AddListener(UseItem);
    }

    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = newItem.icon;
        icon.enabled = true;

        removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;

        removeButton.interactable = false;
    }

    private void OnRemoveButton()
    {
        Inventory.Instance.Remove(item);
    }

    private void UseItem()
    {
        if (item == null) return;

        item.Use();
    }
}
