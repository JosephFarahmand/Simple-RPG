using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Keep track of equipment. Has function for adding and removing items. */
public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager instance;

    public Equipment[] defaultItems;
    public SkinnedMeshRenderer targetMesh;
    Equipment[] currentEquipment;  // Items we currently have equipped
    SkinnedMeshRenderer[] currentMeshes;

    // Callback for when an item eqipped/unequipped
    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    Inventory inventory;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        inventory = Inventory.Instance;

        // Intialize currentEquipment based on number of equipment slot
        var numSlots = System.Enum.GetValues(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
        currentMeshes = new SkinnedMeshRenderer[numSlots];

        EquipDefaultItems();
    }

    // Eqiup a new item
    public void Equip(Equipment newItem)
    {
        // Find out what slot the item fits in
        int slotIndex = (int)newItem.equipSlot;

        Equipment oldItem = Unequip(slotIndex);

        // An item has been equipped so we trigger the callback
        onEquipmentChanged?.Invoke(newItem, oldItem);

        //SetEquipmentBlendShapes(newItem, 100);

        // Insert the item into the slot
        currentEquipment[slotIndex] = newItem;

        var newMesh = Instantiate(newItem.mesh);
        newMesh.transform.parent = targetMesh.transform;

        newMesh.bones = targetMesh.bones;
        newMesh.rootBone = targetMesh.rootBone;
        currentMeshes[slotIndex] = newMesh;
    }

    // Unequip an item with particular index
    public Equipment Unequip(int slotIndex)
    {
        // Only do this if an item is there
        if(currentEquipment[slotIndex] != null)
        {
            if(currentMeshes[slotIndex] != null)
            {
                Destroy(currentMeshes[slotIndex].gameObject);
            }

            // Add the item to the inventory
            var oldItem = currentEquipment[slotIndex];
            //SetEquipmentBlendShapes(oldItem, 0);
            inventory.Add(oldItem);

            // Remove the item from equipment array
            currentEquipment[slotIndex] = null;

            // Equipment has been removed, so we trigger the callback
            onEquipmentChanged?.Invoke(null, oldItem);

            return oldItem;
        }
        return null;
    }

    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }

        EquipDefaultItems();
    }

    //private void SetEquipmentBlendShapes(Equipment item,int weight)
    //{
    //    foreach(var blendShapes in item.coveredMeshRegions)
    //    {
    //        targetMesh.SetBlendShapeWeight((int)blendShapes, weight);
    //    }
    //}

    private void EquipDefaultItems()
    {
        foreach (var item in defaultItems)
        {
            Equip(item);
        }
    }
}
