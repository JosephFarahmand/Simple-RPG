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
    [SerializeField] private StatsElement damage;
    [SerializeField] private StatsElement armor;
    [SerializeField] private StatsElement attackSpeed;

    public override void SetValues()
    {
        UpdateUI();

        damage.SetValue(PlayerManager.Stats.Damage.GetValue());
        armor.SetValue(PlayerManager.Stats.Armor.GetValue());
        attackSpeed.SetValue(PlayerManager.Stats.AttackSpeed.GetValue());
    }

    public override void SetValuesOnSceneLoad()
    {
        inventory = PlayerManager.InventoryController;
        inventory.onItemChangedCallback += UpdateUI;

        damage.Initialize(PlayerManager.Stats.Damage.GetValue());
        armor.Initialize(PlayerManager.Stats.Armor.GetValue());
        attackSpeed.Initialize(PlayerManager.Stats.AttackSpeed.GetValue());

        PlayerManager.EquipController.onEquipmentChanged += onEquip;

        SetInventorySpaceText();

        slots = new InventorySlot[StaticData.inventorySpace];
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = Instantiate(inventorySlotPrefab, itemsParent);
            slots[i].SetActive(false);

            slots[i].OnSlotSelectedHandler += Slot_OnSlotSelectedHandler;
        }

        SetActionButtonInteractable(false);

        equipSlots = new List<InventoryEquipSlot>(GetComponentsInChildren<InventoryEquipSlot>());
        foreach (var slot in equipSlots)
        {
            slot.SetIcon(null);
        }
    }

    private void Slot_OnSlotSelectedHandler(bool isEquiped, Item item)
    {
        if (isEquiped)
        {
            if (item == null) return;

            SetActionButtonInteractable(true);

            SetSelectButtonAction(item);
            SetDeleteButtonAction(item);

            if (item is Equipment equipment)
            {
                var damageValue = PlayerManager.Stats.Damage.GetValue() + equipment.Modifier.Damage;
                var armorValue = PlayerManager.Stats.Armor.GetValue() + equipment.Modifier.Armor;
                var attackSpeedValue = PlayerManager.Stats.AttackSpeed.GetValue() + equipment.Modifier.AttackSpeed;

                damage.SetNewValue(damageValue);
                armor.SetNewValue(armorValue);
                attackSpeed.SetNewValue(attackSpeedValue);
            }
        }
        else
        {
            SetActionButtonInteractable(false);

            if (item is Equipment)
            {
                damage.SetNewValue();
                armor.SetNewValue();
                attackSpeed.SetNewValue();
            }
        }
    }

    private void SetSelectButtonAction(Item item)
    {
        selectButton.onClick.RemoveAllListeners();
        selectButton.onClick.AddListener(() =>
        {
            if (item == null) return;

            item.Use();

            //if is equipment item, display in slot!!

            SetActionButtonInteractable(false);
        });
    }

    private void SetDeleteButtonAction(Item item)
    {
        deleteButton.onClick.RemoveAllListeners();
        deleteButton.onClick.AddListener(() =>
        {
            if (item == null) return;

            inventory.Remove(item);

            //drop item also!!

            SetActionButtonInteractable(false);
        });
    }

    private void onEquip(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            var equipSlot = equipSlots.Find(x => x.Slot == newItem.equipSlot);
            equipSlot.SetIcon(newItem.Icon);
        }

        damage.SetValue(PlayerManager.Stats.Damage.GetValue());
        armor.SetValue(PlayerManager.Stats.Armor.GetValue());
        attackSpeed.SetValue(PlayerManager.Stats.AttackSpeed.GetValue());
    }

    private void SetActionButtonInteractable(bool value)
    {
        selectButton.interactable = value;
        deleteButton.interactable = value;
    }

    private void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < AccountController.Profile.InventoryItems.Count)
            {
                slots[i].AddItem(AccountController.Profile.InventoryItems[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }

        SetInventorySpaceText();
    }

    private void SetInventorySpaceText()
    {
        inventorySpaceText.text = $"{AccountController.Profile.InventoryItems.Count} / {StaticData.inventorySpace}";
    }

    [Serializable]
    public struct StatsElement
    {
        [Header("Slider")]
        [SerializeField] private Slider currentValue;
        private Image currentValueFill;
        [SerializeField] private Slider newValue;
        private Image newValueFill;

        [Header("Color")]
        [SerializeField] private Color defaultColor;

        public void Initialize(float value)
        {
            currentValue.maxValue = 100;
            currentValue.minValue = 0;
            currentValueFill = currentValue.fillRect.GetComponent<Image>();
            currentValueFill.color = defaultColor;

            newValue.maxValue = 100;
            newValue.minValue = 0;
            newValueFill = newValue.fillRect.GetComponent<Image>();
            newValueFill.color = defaultColor;

            SetValue(value);
        }

        public void SetValue(float value)
        {
            currentValue.value = value;
            newValue.value = value;
            newValueFill.color = defaultColor;
            currentValueFill.color = defaultColor;
        }

        public void SetNewValue(float value = -1)
        {
            if (value == -1)
            {
                newValue.value = currentValue.value;
            }
            else
            {
                newValue.value = value;
            }

            if (newValue.value > currentValue.value)
            {
                newValue.transform.SetAsFirstSibling();

                newValueFill.color = Color.green;
            }
            else if (newValue.value < currentValue.value)
            {
                newValue.transform.SetAsLastSibling();

                newValueFill.color = Color.red;
            }
        }
    }
}