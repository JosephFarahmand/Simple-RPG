using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryPage : PageBase
{
    [Header("Bottons")]
    [SerializeField] private Button selectButton;
    [SerializeField] private Button deleteButton;

    [Header("Text")]
    [SerializeField] private TMP_Text inventorySpaceText;
    private int activeSlotCount = 0;

    [Header("Slot")]
    [SerializeField] private InventoryItemCard itemCardPrefab;
    [SerializeField] private Transform itemsParent;

    Inventory inventory;

    //public Transform itemsParent;
    //List<InventorySlot> _slots;
    List<InventoryItemCard> slots;

    public override void SetValues()
    {
        SetValueCards();
    }

    public override void SetValuesOnSceneLoad()
    {
        //inventory = Inventory.Instance;
        //inventory.onItemChangedCallback += UpdateUI;

        //slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        //slots = new List<InventoryItemCard>();
        //for (int i = 0; i < inventory.space; i++)
        //{
        //    InstantiateSlot();
        //}

        SetInventorySpaceText();
    }

    private void SetInventorySpaceText()
    {
        inventorySpaceText.text = $"{activeSlotCount} / {inventory.space}";
    }

    //private InventoryItemCard GetDisableSlot()
    //{
    //    return slots.Find(x => x.HasItem == false);
    //}

    //private void UpdateUI()
    //{
    //    foreach (var item in inventory.items)
    //    {
    //        bool found = false;
    //        foreach (var slot in slots)
    //        {
    //            if (slot == null) continue;
    //            if (slot.slotItem.Id == item.Id)
    //            {
    //                slot.SetActive(true);
    //                slot.SetValue(item);
    //                found = true;
    //                break;
    //            }
    //        }
    //        if (found) continue;
    //        AddNewItem(item);
    //    }

    //    activeSlotCount = slots.FindAll(x => x.HasItem == true).Count;
    //    SetInventorySpaceText();

    //    foreach (var slot in slots)
    //    {
    //        if (inventory.items.Contains(slot.slotItem))
    //        {

    //        }
    //    }

    //    //for (int i = 0; i < _slots.Count; i++)
    //    //{
    //    //    if (i < inventory.items.Count)
    //    //    {
    //    //        _slots[i].AddItem(inventory.items[i]);
    //    //    }
    //    //    else
    //    //    {
    //    //        _slots[i].ClearSlot();
    //    //    }
    //    //}
    //}

    //private void AddNewItem(Item item)
    //{
    //    var slot = GetDisableSlot();
    //    if (slot == null)
    //    {
    //        slot = InstantiateSlot();
    //    }
    //    slot.SetActive(true);
    //    slot.SetValue(item);
    //}

    //private InventoryItemCard InstantiateSlot()
    //{
    //    var newSlot = Instantiate(itemCardPrefab, itemsParent);
    //    slots.Add(newSlot);
    //    newSlot.SetActive(false);
    //    return newSlot;
    //}


    private void InstantiateSlots(int value)
    {
        for (int i = 0; i < value; i++)
        {
            var newSlot = Instantiate(itemCardPrefab, itemsParent);
            slots.Add(newSlot);
        }
    }

    private void SetValueCards()
    {
        var playerItems = SaveOrLoadManager.instance.Player.Inventory.Items;

        if (playerItems.Count > slots.Count)
            InstantiateSlots(playerItems.Count - slots.Count);

        activeSlotCount = 0;
        for (int i = 0; i < playerItems.Count; i++)
        {
            slots[i].SetActive(true);
            slots[i].SetValue(GameData.GetItem(playerItems[i].Id));
            activeSlotCount++;
        }
        SetInventorySpaceText();
    }

    private void OnDisable()
    {
        if (slots.Count <= 0)
            return;
        foreach (var item in slots)
            item.SetActive(false);
    }
}