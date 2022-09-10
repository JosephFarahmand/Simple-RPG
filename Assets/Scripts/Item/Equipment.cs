using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public ItemStoredData StoredData => new ItemStoredData(this);

    [Header("Crafting")]
    public bool isCrafted = false;
    public List<Item> requerdItems;

    [Header("Equipment")]
    public EquipmentSlot equipSlot;  // Slot to store equipment in
    public SkinnedMeshRenderer mesh;
    //public EquipmentMeshRegion[] coveredMeshRegions;

    [Header("Modifier")]
    //public int hpModifier;           // Increase/decrease in HP
    //public int attackModifier;       // Increase/decrease in attack
    //public int damageModifier;       // Increase/decrease in damage
    //public int armorModifier;        // Increase/decrease in armor

    [Tooltip("Increase/decrease in each one")]
    public CharacterState itemModifier;

    // When pressed in inventory
    public override void Use()
    {
        base.Use();

        EquipmentManager.instance.Equip(this);      // Equip it
        RemoveFromInventory();                      // Remove it from inventory
    }
}

[System.Serializable]
public struct CharacterState
{
    [Min(0), SerializeField] private float hp;
    [Min(0), SerializeField] private float attack;
    [Min(0), SerializeField] private float damage;
    [Min(0), SerializeField] private float armor;

    public float HP => hp;
    public float Attack => attack;
    public float Damage => damage;
    public float Armor => armor;
}

public enum EquipmentSlot
{
    Weapon,
    Shield,
    Head,
    Shoulders,
    Arm,
    Hands,
    Chest,
    Belt,
    Legs,
    Feet
}

[System.Obsolete]
public enum EquipmentMeshRegion // Corresponds to body blendshapes.
{
    Legs,
    Arms,
    Torso
}