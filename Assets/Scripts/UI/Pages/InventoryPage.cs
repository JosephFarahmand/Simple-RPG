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
    private int activeSlot = 0;

    [Header("Slot")]
    [SerializeField] private InventoryItemCard itemCardPrefab;
    [SerializeField] private Transform itemsParent;

    Inventory inventory;

    //public Transform itemsParent;
    //List<InventorySlot> _slots;
    List<InventoryItemCard> slots;

    public override void SetValues()
    {

    }

    public override void SetValuesOnSceneLoad()
    {
        inventory = Inventory.Instance;
        inventory.onItemChangedCallback += UpdateUI;

        //slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        slots = new List<InventoryItemCard>();
        for (int i = 0; i < inventory.space; i++)
        {
            InstantiateSlot();
        }

        SetInventorySpaceText();
    }

    private void SetInventorySpaceText()
    {
        inventorySpaceText.text = $"{activeSlot} / {inventory.space}";
    }

    private InventoryItemCard GetDisableSlot()
    {
        return slots.Find(x => x.gameObject.activeSelf == false);
    }

    private void UpdateUI()
    {
        foreach (var item in inventory.items)
        {
            bool found = false;
            foreach (var slot in slots)
            {
                if (slot == null) continue;
                if (slot.slotItem.Id == item.Id)
                {
                    slot.gameObject.SetActive(true);
                    slot.SetValue(item);
                    found = true;
                    break;
                }
            }
            if (found) continue;
            AddNewItem(item);
        }

        activeSlot = slots.FindAll(x => x.gameObject.activeSelf == true).Count;
        SetInventorySpaceText();
        //for (int i = 0; i < _slots.Count; i++)
        //{
        //    if (i < inventory.items.Count)
        //    {
        //        _slots[i].AddItem(inventory.items[i]);
        //    }
        //    else
        //    {
        //        _slots[i].ClearSlot();
        //    }
        //}
    }

    private void AddNewItem(Item item)
    {
        var slot = GetDisableSlot();
        if (slot == null)
        {
            slot = InstantiateSlot();
        }
        slot.gameObject.SetActive(true);
        slot.SetValue(item);
    }

    private InventoryItemCard InstantiateSlot()
    {
        var newSlot = Instantiate(itemCardPrefab, itemsParent);
        slots.Add(newSlot);
        newSlot.gameObject.SetActive(false);
        return newSlot;
    }
}