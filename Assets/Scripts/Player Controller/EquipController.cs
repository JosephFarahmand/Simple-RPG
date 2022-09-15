using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipController : MonoBehaviour
{
    private List<EquipField> fields;

    private Dictionary<EquipmentSlot, EquipField> equipFields;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    Inventory inventory;

    private void Awake()
    {
        inventory = Inventory.Instance;

        this.fields = new List<EquipField>();
        var fields = GetComponentsInChildren<EquipField>(true);
        foreach (var field in fields)
        {
            if (field.item == null) continue;
            this.fields.Add(field);
            field.gameObject.SetActive(false);
        }

        equipFields = new Dictionary<EquipmentSlot, EquipField>();
        IList list = System.Enum.GetValues(typeof(EquipmentSlot));
        for (int i = 0; i < list.Count; i++)
        {
            equipFields.Add((EquipmentSlot)i, null);
        }
    }

    public void Equip(Equipment newItem)
    {
        // Find out what slot the item fits in
        var newItemField = fields.Find(x => x.item == newItem);
        if (newItemField == null)
        {
            Debug.LogError("This items is not available!!");
            return;
        }

        var slot = newItemField.item.equipSlot;

        var oldItem = Unequip(slot);

        // Insert the item into the slot
        newItemField.gameObject.SetActive(true);
        equipFields[slot] = newItemField;

        // An item has been equipped so we trigger the callback
        onEquipmentChanged?.Invoke(newItemField.item, oldItem);
    }

    /// <summary>
    /// Unequip an item with particular slot type
    /// </summary>
    /// <param name="slot"></param>
    /// <returns></returns>
    public Equipment Unequip(EquipmentSlot slot)
    {
        if (equipFields.ContainsKey(slot))
        {
            // Only do this if an item is there
            if (equipFields[slot] != null)
            {
                // Add the item to the inventory
                var oldItem = equipFields[slot].item;
                inventory.Add(oldItem);

                // Diactive item
                equipFields[slot].gameObject.SetActive(false);

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

    public Equipment Unequip(int slotIndex)
    {
        return Unequip((EquipmentSlot)slotIndex);
    }

    public void UnequipAll()
    {
        foreach (var slot in equipFields.Keys)
        {
            Unequip(slot);
        }
    }
}
