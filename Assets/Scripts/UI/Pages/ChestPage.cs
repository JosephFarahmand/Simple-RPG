using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChestPage : PageBase
{
    Inventory inventory;

    [Header("Slot")]
    [SerializeField] private ChestSlot chestSlotPrefab;
    ////////////////////////////// Inventory
    [Header("Inventory")]
    [SerializeField] private Transform inventoryItemsParent;
    [SerializeField] private ToggleGroup inventoryItemsToggleGroup;
    List<ChestSlot> inventorySlots;
    /// <summary>
    /// item that select from inventory
    /// </summary>
    Item dropItem;
    [SerializeField] private Button dropButton;
    ////////////////////////////// Chest Item
    [Header("Chest")]
    [SerializeField] private Transform chestItemsParent;
    [SerializeField] private ToggleGroup chestItemsToggleGroup;
    List<ChestSlot> chestSlots;
    /// <summary>
    /// item that select from chest
    /// </summary>
    Item selectedItem;
    [SerializeField] private Button selectButton;
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

        inventorySlots = new List<ChestSlot>();
        for (int i = 0; i < inventory.space; i++)
        {
            var newSlot = Instantiate(chestSlotPrefab, inventoryItemsParent);
            newSlot.SetType(ChestSlot.ItemType.InventoryItem);
            newSlot.SetActive(false);
            inventorySlots.Add(newSlot);
        }

        selectButton.onClick.RemoveAllListeners();
        selectButton.onClick.AddListener(() =>
        {
            if (selectedItem == null) return;

            // add item to inventory
            inventory.Add(selectedItem);

            // update ui --> add to inventory part
            UpdateUI();
        });

        dropButton.onClick.RemoveAllListeners();
        dropButton.onClick.AddListener(() =>
        {
            if (dropItem == null) return;

            // add item to inventory
            inventory.Remove(dropItem);

            // update ui --> add to chest part
            
        });
    }

    public void SetChestItems(List<Item> items)
    {
        chestSlots = new List<ChestSlot>();
        for (int i = 0; i < items.Count; i++)
        {
            var newSlot = Instantiate(chestSlotPrefab, chestItemsParent);
            newSlot.SetType(ChestSlot.ItemType.ChestItem);
            newSlot.AddItem(items[i]);
            chestSlots.Add(newSlot);
        }
    }

    private void UpdateUI()
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (i < inventory.items.Count)
            {
                inventorySlots[i].AddItem(inventory.items[i]);
            }
            else
            {
                inventorySlots[i].ClearSlot();
            }
        }
    }
    
    public void SetActiveChestItem(Item item)
    {
        if (item == null)
        {
            selectButton.interactable = false;
            return;
        }
        selectButton.interactable = true;
        selectedItem = item;
    }

    public void SetActiveInventoryItem(Item item)
    {
        if (item == null)
        {
            dropButton.interactable = false;
            return;
        }
        dropButton.interactable = true;
        dropItem = item;
    }
}
