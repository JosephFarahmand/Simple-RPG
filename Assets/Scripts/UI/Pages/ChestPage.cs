using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChestPage : PageBase
{
    Inventory inventory;

    [Header("Slot")]
    [SerializeField] private InventorySlot inventorySlotPrefab;
    ////////////////////////////// Inventory
    [Header("Inventory")]
    [SerializeField] private Transform inventoryItemsParent;
    [SerializeField] private ToggleGroup inventoryItemsToggleGroup;
    InventorySlot[] slots;
    ////////////////////////////// Chest Item
    [Header("Chest")]
    [SerializeField] private Transform chestItemsParent;
    [SerializeField] private ToggleGroup chestItemsToggleGroup;
    InventorySlot[] chestSlots;
    //////////////////////////////
    
    [Header("Text")]
    [SerializeField] private TMP_Text inventorySpaceText;

    public override void SetValues()
    {
        UpdateUI();
    }

    public override void SetValuesOnSceneLoad()
    {
        inventory = Inventory.Instance;

        slots = new InventorySlot[inventory.space];
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = Instantiate(inventorySlotPrefab, inventoryItemsParent);
            slots[i].SetActive(false);
        }
    }

    public void SetChestItems(List<Item> items)
    {
        chestSlots = new InventorySlot[items.Count];
        for (int i = 0; i < chestSlots.Length; i++)
        {
            chestSlots[i] = Instantiate(inventorySlotPrefab, chestItemsParent);
            chestSlots[i].AddItem(items[i]);
        }
    }

    private void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
    
}
