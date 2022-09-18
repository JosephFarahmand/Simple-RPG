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
    [SerializeField] private StateElement damage;
    [SerializeField] private StateElement armor;
    [SerializeField] private StateElement attackSpeed;

    Item selectedItem;

    public override void SetValues()
    {
        UpdateUI();

        damage.SetCurrentValue(PlayerManager.Stats.Damage.GetValue());
        armor.SetCurrentValue(PlayerManager.Stats.Armor.GetValue());
        attackSpeed.SetCurrentValue(PlayerManager.Stats.AttackSpeed.GetValue());
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
            slots[i] = Instantiate(inventorySlotPrefab, itemsParent);
            slots[i].SetActive(false);

            slots[i].OnToggleChange += InventoryPage_OnToggleChange;
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

    private void InventoryPage_OnToggleChange(bool arg1, Item arg2)
    {
        if (arg1)
        {
            if (arg2 == null) return;
            SetActiveItem(arg2);

            if (arg2 is Equipment equipment)
            {
                UpdatePlayerState(equipment);
            }
        }
        else
        {
            SetActiveItem(null);

            if (arg2 is Equipment equipment)
            {
                var tempEquipment = new Equipment(new Equipment.ItemModifier(-equipment.Modifier.Damage, -equipment.Modifier.Armor, -equipment.Modifier.AttackSpeed));
                UpdatePlayerState(tempEquipment);
            }
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

        damage.SetCurrentValue(PlayerManager.Stats.Damage.GetValue());
        armor.SetCurrentValue(PlayerManager.Stats.Armor.GetValue());
        attackSpeed.SetCurrentValue(PlayerManager.Stats.AttackSpeed.GetValue());
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


    private void UpdatePlayerState(Equipment equipment)
    {
        damage.SetNewValue(equipment.Modifier.Damage);
        armor.SetNewValue(equipment.Modifier.Armor);
        attackSpeed.SetNewValue(equipment.Modifier.AttackSpeed);
    }

    [System.Serializable]
    public struct StateElement
    {
        [SerializeField] private Slider currentValue;
        [SerializeField] private Slider newValue;

        public void SetCurrentValue(float value)
        {
            currentValue.value = value;
            newValue.value = value;
        }

        public void SetNewValue(float changedValue)
        {
            newValue.value += changedValue;
        }

        public Slider CurrentValue { get => currentValue; set => currentValue = value; }
        public Slider NewValue { get => newValue; set => newValue = value; }
    }
}