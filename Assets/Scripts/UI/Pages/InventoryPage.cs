using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class InventoryPage : PageBase
{
    InventoryController inventory;

    [Header("Slot")]
    [SerializeField] private InventorySlot inventorySlotPrefab;

    [SerializeField] private Transform itemsParent;
    [SerializeField] private ToggleGroup itemsToggleGroup;
    InventorySlot[] slots;

    [Header("Equipment Display")]
    private List<InventoryEquipSlot> equipSlots;

    [Header("Bottons")]
    [SerializeField] private Button selectButton;
    [SerializeField] private Button deleteButton;

    [Header("Text")]
    [SerializeField] private TMP_Text inventorySpaceText;


    [Header("State")]
    [SerializeField] private StateElement hp;
    [SerializeField] private StateElement attack;
    [SerializeField] private StateElement damage;
    [SerializeField] private StateElement defence;

    Item selectedItem;

    public ToggleGroup ItemsToggleGroup => itemsToggleGroup;

    public override void SetValues()
    {
        UpdateUI();
    }

    public override void SetValuesOnSceneLoad()
    {
        inventory = PlayerManager.InventoryController;
        inventory.onItemChangedCallback += UpdateUI;

        PlayerManager.EquipController.onEquipmentChanged += onEquip;

        SetInventorySpaceText();

        slots = new InventorySlot[inventory.space];
        for (int i = 0; i < slots.Length; i++)
        {
            //in
            slots[i] = Instantiate(inventorySlotPrefab, itemsParent);
            slots[i].SetActive(false);
        }

        selectButton.onClick.RemoveAllListeners();
        selectButton.onClick.AddListener(() =>
        {
            if (selectedItem == null) return;

            selectedItem.Use();

            //if is equipment item, display in slot!!

            SetActiveButtons(false);
        });


        deleteButton.onClick.RemoveAllListeners();
        deleteButton.onClick.AddListener(() =>
        {
            if (selectedItem == null) return;

            inventory.Remove(selectedItem);

            //drop item also!!

            SetActiveButtons(false);
        });

        SetActiveButtons(false);

        this.equipSlots = new List<InventoryEquipSlot>();
        var equipSlots = GetComponentsInChildren<InventoryEquipSlot>();
        foreach (var slot in equipSlots)
        {
            slot.SetIcon(null);
            this.equipSlots.Add(slot);
        }
    }

    private void onEquip(Equipment newItem, Equipment oldItem)
    {
        InventoryEquipSlot equipSlot;
        if (newItem == null)
        {
            equipSlot = equipSlots.Find(x => x.Slot == oldItem.equipSlot);
            equipSlot.SetIcon(oldItem.Icon);
        }
        else
        {
            equipSlot = equipSlots.Find(x => x.Slot == newItem.equipSlot);
            equipSlot.SetIcon(newItem.Icon);
        }
    }

    private void SetActiveButtons(bool value)
    {
        selectButton.interactable = value;
        deleteButton.interactable = value;
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

        SetInventorySpaceText();
    }

    public void SetActiveItem(Item item)
    {
        if (item == null)
        {
            SetActiveButtons(false);
            return;
        }
        SetActiveButtons(true);
        selectedItem = item;
    }

    private void SetInventorySpaceText()
    {
        inventorySpaceText.text = $"{inventory.items.Count} / {inventory.space}";
    }


    private void UpdatePlayerState(Item item)
    {
        if (item is Equipment equipment)
        {
            hp.SetNewValue(equipment.itemModifier.HP);
            attack.SetNewValue(equipment.itemModifier.Attack);
            damage.SetNewValue(equipment.itemModifier.Damage);
            defence.SetNewValue(equipment.itemModifier.Armor);
        }
    }

    [System.Serializable]
    public struct StateElement
    {
        [SerializeField] private Slider currentValue;
        [SerializeField] private Slider newValue;

        public void SetCurrentValue(float value)
        {
            currentValue.value = value;
        }

        public void SetNewValue(float changedValue)
        {
            newValue.value += changedValue;
        }

        public Slider CurrentValue { get => currentValue; set => currentValue = value; }
        public Slider NewValue { get => newValue; set => newValue = value; }
    }

    //[Header("Text")]
    //[SerializeField] private TMP_Text inventorySpaceText;
    //private int activeSlotCount = 0;

    //[Header("Slot")]
    //[SerializeField] private InventoryItemCard itemCardPrefab;
    //[SerializeField] private Transform itemsParent;

    //Inventory inventory;

    ////public Transform itemsParent;
    ////List<InventorySlot> _slots;
    //List<InventoryItemCard> slots;

    //public override void SetValues()
    //{
    //    SetValueCards();
    //}

    //public override void SetValuesOnSceneLoad()
    //{
    //    //inventory = Inventory.Instance;
    //    //inventory.onItemChangedCallback += UpdateUI;

    //    //slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    //    //slots = new List<InventoryItemCard>();
    //    //for (int i = 0; i < inventory.space; i++)
    //    //{
    //    //    InstantiateSlot();
    //    //}

    //    SetInventorySpaceText();
    //}



    ////private InventoryItemCard GetDisableSlot()
    ////{
    ////    return slots.Find(x => x.HasItem == false);
    ////}

    ////private void UpdateUI()
    ////{
    ////    foreach (var item in inventory.items)
    ////    {
    ////        bool found = false;
    ////        foreach (var slot in slots)
    ////        {
    ////            if (slot == null) continue;
    ////            if (slot.slotItem.Id == item.Id)
    ////            {
    ////                slot.SetActive(true);
    ////                slot.SetValue(item);
    ////                found = true;
    ////                break;
    ////            }
    ////        }
    ////        if (found) continue;
    ////        AddNewItem(item);
    ////    }

    ////    activeSlotCount = slots.FindAll(x => x.HasItem == true).Count;
    ////    SetInventorySpaceText();

    ////    foreach (var slot in slots)
    ////    {
    ////        if (inventory.items.Contains(slot.slotItem))
    ////        {

    ////        }
    ////    }

    ////    //for (int i = 0; i < _slots.Count; i++)
    ////    //{
    ////    //    if (i < inventory.items.Count)
    ////    //    {
    ////    //        _slots[i].AddItem(inventory.items[i]);
    ////    //    }
    ////    //    else
    ////    //    {
    ////    //        _slots[i].ClearSlot();
    ////    //    }
    ////    //}
    ////}

    ////private void AddNewItem(Item item)
    ////{
    ////    var slot = GetDisableSlot();
    ////    if (slot == null)
    ////    {
    ////        slot = InstantiateSlot();
    ////    }
    ////    slot.SetActive(true);
    ////    slot.SetValue(item);
    ////}

    ////private InventoryItemCard InstantiateSlot()
    ////{
    ////    var newSlot = Instantiate(itemCardPrefab, itemsParent);
    ////    slots.Add(newSlot);
    ////    newSlot.SetActive(false);
    ////    return newSlot;
    ////}


    //private void InstantiateSlots(int value)
    //{
    //    for (int i = 0; i < value; i++)
    //    {
    //        var newSlot = Instantiate(itemCardPrefab, itemsParent);
    //        slots.Add(newSlot);
    //    }
    //}

    //private void SetValueCards()
    //{
    //    var playerItems = SaveOrLoadManager.instance.Player.Inventory.Items;

    //    if (playerItems.Count > slots.Count)
    //        InstantiateSlots(playerItems.Count - slots.Count);

    //    activeSlotCount = 0;
    //    for (int i = 0; i < playerItems.Count; i++)
    //    {
    //        slots[i].SetActive(true);
    //        slots[i].SetValue(GameData.GetItem(playerItems[i].Id));
    //        activeSlotCount++;
    //    }
    //    SetInventorySpaceText();
    //}

    //private void OnDisable()
    //{
    //    if (slots.Count <= 0)
    //        return;
    //    foreach (var item in slots)
    //        item.SetActive(false);
    //}
}