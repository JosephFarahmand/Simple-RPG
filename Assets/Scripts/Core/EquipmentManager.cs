using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Keep track of equipment. Has function for adding and removing items. */
public class EquipmentManager : MonoBehaviour
{
    private static EquipmentManager instance;

    [Header("Controllers")]
    [SerializeField] private EquipController playerEquipController;

    [Header("Items")]
    [SerializeField] private Equipment[] defaultItems;

    private void Awake()
    {
        instance = this;

        if (playerEquipController == null)
        {
            playerEquipController = FindObjectOfType<EquipController>();
        }
    }

    private void Start()
    {
        EquipDefaultItems();
    }

    private void EquipDefaultItems()
    {
        foreach (var item in defaultItems)
        {
            Equip(item);
        }
    }

    /// <summary>
    /// Eqiup a new item
    /// </summary>
    /// <param name="newItem"></param>
    public static void Equip(Equipment newItem)
    {
        instance.playerEquipController.Equip(newItem);
    }

    /// <summary>
    /// Unequip an item with particular slot type
    /// </summary>
    /// <param name="slot"></param>
    /// <returns>Old item that has been unequip</returns>
    public static Equipment Unequip(EquipmentSlot slot)
    {
        return instance.playerEquipController.Unequip(slot);
    }

    /// <summary>
    /// Unequip an item with particular index
    /// </summary>
    /// <param name="slotIndex"></param>
    /// <returns>Old item that has been unequip</returns>
    //public static Equipment Unequip(int slotIndex)
    //{
    //    return instance.playerEquipController.Unequip(slotIndex);
    //}
}
