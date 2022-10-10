using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentController : MonoBehaviour
{
    [SerializeField] Transform root;

    [Header("Items")]
    private List<Equipment> defaultItems;

    private List<ModelData> models;

    private Dictionary<EquipmentSlot, Equipment> activeEquipment;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    InventoryController inventory;

    public void Initialization()
    {
        inventory = PlayerManager.InventoryController;

        this.models = new List<ModelData>();
        var models = root.GetComponentsInChildren<ModelData>(true);
        foreach (var model in models)
        {
            if (model.Id.Length == 0) continue;
            if (model.IsStaticItem) continue;
            this.models.Add(model);
            model.SetActive(false);
        }

        activeEquipment = new Dictionary<EquipmentSlot, Equipment>();
        IList list = Enum.GetValues(typeof(EquipmentSlot));
        for (int i = 0; i < list.Count; i++)
        {
            activeEquipment.Add((EquipmentSlot)i, null);
        }

        EquipDefaultItems();
    }

    private void EquipDefaultItems()
    {
        defaultItems = new List<Equipment>();
        foreach (var item in GameManager.GameData.GetEquipmentItems())
        {
            if (item.IsDefaultItem && activeEquipment[item.equipSlot] == null)
            {
                defaultItems.Add(item);
                Equip(item);
            }
        }
    }

    public void EquipDefaultItem(EquipmentSlot slot)
    {
        var defaultItem = defaultItems.Find(x => x.equipSlot == slot);
        if (defaultItem == null)
        {
            Debug.LogWarning($"There is no default item for ({slot})");
            return;
        }
        Equip(defaultItem);
    }

    public void Equip(Equipment newItem)
    {
        // Find out what slot the item fits in
        var model = models.Find(x => x.Equals(newItem));
        if (model == null)
        {
            Debug.LogError($"This items is not available!! {newItem.Name}");
            return;
        }

        var slot = newItem.equipSlot;

        Equipment oldItem = null;
        if (!newItem.IsDefaultItem)
        {
            oldItem = Unequip(slot);
        }

        // Insert the item into the slot
        model.SetActive(true);
        activeEquipment[slot] = newItem;

        // An item has been equipped so we trigger the callback
        onEquipmentChanged?.Invoke(newItem, oldItem);
    }

    /// <summary>
    /// Unequip an item with particular slot type
    /// </summary>
    /// <param name="slot"></param>
    /// <returns></returns>
    public Equipment Unequip(EquipmentSlot slot)
    {
        if (activeEquipment.ContainsKey(slot))
        {
            // Only do this if an item is there
            if (activeEquipment[slot] != null)
            {
                // Add the item to the inventory
                var oldItem = activeEquipment[slot];
                inventory.Add(oldItem);

                // Diactive item
                var model = models.Find(x => x.Equals(oldItem));
                model.SetActive(false);
                activeEquipment[slot] = null;

                // Equipment has been removed, so we trigger the callback
                onEquipmentChanged?.Invoke(null, oldItem);

                return oldItem;
            }
            else
            {
                Debug.LogWarning($"This slot ({slot}) is empty!!");
                return null;
            }
        }
        else
        {
            Debug.LogError($"This slot ({slot}) is not available!!");
            return null;
        }
    }
}
