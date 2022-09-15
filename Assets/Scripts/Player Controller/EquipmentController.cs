using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentController : MonoBehaviour
{
    [Header("Items")]
    private List<Equipment> defaultItems;

    private List<EquipmentHandler> equipmentHandlers;

    private Dictionary<EquipmentSlot, EquipmentHandler> activeEquipment;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    Inventory inventory;

    private void Awake()
    {
        inventory = Inventory.Instance;

        equipmentHandlers = new List<EquipmentHandler>();
        var handlers = GetComponentsInChildren<EquipmentHandler>(true);
        foreach (var handler in handlers)
        {
            if (handler.Item == null) continue;
            equipmentHandlers.Add(handler);
            handler.gameObject.SetActive(false);
        }

        activeEquipment = new Dictionary<EquipmentSlot, EquipmentHandler>();
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
        foreach (var item in GameData.GetEquipmentItems())
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
            Debug.LogError($"There is no default item for ({slot})");
            return;
        }
        Equip(defaultItem);
    }

    public void Equip(Equipment newItem)
    {
        // Find out what slot the item fits in
        var handler = equipmentHandlers.Find(x => x.Item == newItem);// TODO: find by id
        //var handler = equipmentHandlers.Find(x => x.ID == newItem.Id);
        if (handler == null)
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
        handler.SetActive(true);
        activeEquipment[slot] = handler;

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
                var oldItem = activeEquipment[slot].Item;
                inventory.Add(oldItem);

                // Diactive item
                activeEquipment[slot].SetActive(false);
                activeEquipment[slot] = null;

                // Equipment has been removed, so we trigger the callback
                onEquipmentChanged?.Invoke(null, oldItem);

                return oldItem;
            }
            else
            {
                Debug.LogError($"This slot ({slot}) is empty!!");
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
