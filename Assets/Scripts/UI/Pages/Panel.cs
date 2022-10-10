using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Panel:MonoBehaviour
{
    public enum PanelType
    {
        Nothing,
        ChestPanel,
        InventoryPanel
    }

    [SerializeField] private PanelType type;
    [SerializeField] private ChestSlot slotPrefab;
    [SerializeField] private Transform itemsParent;


    [SerializeField] private Button button;
    [SerializeField] private TMP_Text spaceText;

    private List<ChestSlot> slots;

    InventoryController inventory;
    ChestPage chestPage;
    List<Item> items;

    private void Awake()
    {
        SetActionButtonInteractable(false);
    }

    internal void Initilize(InventoryController inventory, ChestPage chestPage)
    {
        this.inventory = inventory;
        this.chestPage = chestPage;

        switch (type)
        {
            case PanelType.Nothing:
                break;
            case PanelType.ChestPanel:
                items = new List<Item>(chestPage.Chest.GetItems());
                CreateSlots(StaticData.maxChestSpace);
                break;
            case PanelType.InventoryPanel:
                items = new List<Item>(AccountController.Profile.InventoryItems);
                CreateSlots(StaticData.inventorySpace);
                break;
        }

        SetActionButtonInteractable(false);
    }

    private void CreateSlots(float slotsCount)
    {

        var counter = 0;

        if (slots == null)
        {
            slots = new List<ChestSlot>();
        }
        else if (slots.Count > 0)
        {
            slotsCount -= itemsParent.childCount;
        }

        for (int i = 0; i < slotsCount; i++)
        {
            var newSlot = Instantiate(slotPrefab, itemsParent);
            newSlot.SetActive(false);

            newSlot.OnSlotSelectedHandler += Slot_OnSlotSelectedHandler;

            if (i < items.Count)
            {
                counter++;
                newSlot.AddItem(items[i]);
            }
            else
            {
                newSlot.ClearSlot();
            }

            slots.Add(newSlot);
        }
        SetSpaceText(counter);
    }

    private void Slot_OnSlotSelectedHandler(bool isSelected, Item item)
    {
        if (isSelected)
        {
            SetActionButtonInteractable(true);

            AddButtonCallback(item);
        }
        else
        {
            SetActionButtonInteractable(false);
        }
    }

    private void AddButtonCallback(Item item)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            if (type == PanelType.InventoryPanel)
            {
                // Remove selected item from inventory
                inventory.Remove(item);

                // Add selected item to chest panel
                chestPage.Chest.AddItem(item);
            }
            else if (type == PanelType.ChestPanel)
            {
                // Add selected item to inventory
                inventory.Add(item);

                // Remove item from chest panel
                chestPage.Chest.RemoveItem(item);
            }

            chestPage.UpdatePanelsSlots();
            SetActionButtonInteractable(false);
        });
    }

    public void UpdateSlots()
    {
        switch (type)
        {
            case PanelType.Nothing:
                break;
            case PanelType.ChestPanel:
                items = new List<Item>(chestPage.Chest.GetItems());
                break;
            case PanelType.InventoryPanel:
                items = new List<Item>(AccountController.Profile.InventoryItems);
                break;
        }

        var counter = 0;
        for (int i = 0; i < slots.Count; i++)
        {
            if (i < items.Count)
            {
                counter++;
                slots[i].AddItem(items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
        SetSpaceText(counter);
    }

    private void SetActionButtonInteractable(bool value)
    {
        button.interactable = value;
    }

    private void SetSpaceText(int currentValue)
    {
        spaceText.text = $"{currentValue} / {slots.Count}";
    }
}