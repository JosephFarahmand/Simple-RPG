using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChestPage : PageBase
{
    InventoryController inventory;
    //List<Item> chestItems;
    InteractableChest chest;

    [SerializeField] private Panel inventoryPanel;
    [SerializeField] private Panel chestPanel;

    (Item, PanelType) selected;

    public override void SetValues()
    {
        inventoryPanel.UpdateSlots(inventory.items, PanelType.InventoryPanel);

        inventoryPanel.SetInteractable(false);
        chestPanel.SetInteractable(false);
    }

    public override void SetValuesOnSceneLoad()
    {
        inventory = PlayerManager.InventoryController;

        inventoryPanel.CreateItem(inventory.space);
        inventoryPanel.AddButtonCallback(() =>
        {
            // Drop item
            if (selected.Item2 == PanelType.InventoryPanel)
            {
                var selectedItem = selected.Item1;

                // Remove selected item from inventory
                inventory.Remove(selectedItem);

                // Add selected item to chest panel
                chest.SetItem(selectedItem);

                UpdateUI();
            }
        });
    }

    public void SetChest(InteractableChest chest)
    {
        this.chest = chest;
        chestPanel.CreateItem(Chest.maxChestSpace);
        chestPanel.UpdateSlots(chest.GetItems(), PanelType.ChestPanel);
        chestPanel.AddButtonCallback(() =>
        {
            if (selected.Item2 == PanelType.ChestPanel)
            {
                var selectedItem = selected.Item1;

                // Add selected item to inventory
                inventory.Add(selectedItem);

                // Remove item from chest panel
                chest.RemoveItem(selectedItem);

                UpdateUI();
            }
        });
    }

    private void UpdateUI()
    {
        inventoryPanel.UpdateSlots(inventory.items, PanelType.InventoryPanel);
        chestPanel.UpdateSlots(chest.GetItems(), PanelType.ChestPanel);

        inventoryPanel.SetInteractable(false);
        chestPanel.SetInteractable(false);
    }

    public void SetSelectedItem(Item item, PanelType panelType = PanelType.Nothing)
    {
        inventoryPanel.SetInteractable(false);
        chestPanel.SetInteractable(false);

        if (item == null)
        {
            return;
        }

        switch (panelType)
        {
            case PanelType.ChestPanel:
                chestPanel.SetInteractable(true);
                break;
            case PanelType.InventoryPanel:
                inventoryPanel.SetInteractable(true);
                break;
        }

        selected = (item, panelType);
    }

    public enum PanelType
    {
        Nothing,
        ChestPanel,
        InventoryPanel
    }

    [System.Serializable]
    public struct Panel
    {
        [SerializeField] private ChestSlot slotPrefab;
        [SerializeField] private Transform itemsParent;
        [SerializeField] private Button button;
        [SerializeField] private TMP_Text spaceText;

        public List<ChestSlot> slots { get; private set; }

        public void CreateItem(float count)
        {
            if(slots == null)
            {
                slots = new List<ChestSlot>();
            }
            else if (slots.Count > 0)
            {
                count -= itemsParent.childCount;
            }
            for (int i = 0; i < count; i++)
            {
                var newSlot = Instantiate(slotPrefab, itemsParent);
                newSlot.SetActive(false);
                slots.Add(newSlot);
            }
        }

        public void UpdateSlots(List<Item> items, PanelType type)
        {
            var counter = 0;
            for (int i = 0; i < slots.Count; i++)
            {
                if (i < items.Count)
                {
                    counter++;
                    slots[i].AddItem(items[i], type);
                }
                else
                {
                    slots[i].ClearSlot();
                }
            }
            SetSpaceText(counter);
        }

        public void AddButtonCallback(System.Action callback)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => callback?.Invoke());

        }

        public void SetInteractable(bool value)
        {
            button.interactable = value;
        }

        public void SetSpaceText(int currentValue)
        {
            spaceText.text = $"{currentValue} / {slots.Count}";
        }
    }
}