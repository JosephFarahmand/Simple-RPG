using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    //public ItemStoredData StoredData => new ItemStoredData(this);

    public ItemModifier Modifier { get => StaticData.GetItemModifier(Rarity, equipSlot); }

    [Header("Crafting")]
    public bool isCrafted = false;
    public List<Item> requerdItems;

    [Header("Equipment")]
    public EquipmentSlot equipSlot;  // Slot to store equipment in

    [Header("")]
    public ItemPickup itemObject;
    //public EquipmentMeshRegion[] coveredMeshRegions;

    [Header("Modifier")]
    [Tooltip("Increase/decrease in each one")]
    [SerializeField] private ItemModifier modifier;

    public Equipment(ItemModifier modifier)
    {
        this.modifier = modifier;
    }

    // When pressed in inventory
    public override void Use()
    {
        base.Use();

        PlayerManager.EquipController.Equip(this);  // Equip it
        RemoveFromInventory();                      // Remove it from inventory
    }

    [NaughtyAttributes.Button]
    private void Display()
    {
        modifier = Modifier;
    }

    [System.Serializable]
    public struct ItemModifier
    {
        [Min(0), SerializeField] private int damage;
        [Min(0), SerializeField] private int armor;
        [Min(0), SerializeField] private int attackSpeed;

        public ItemModifier(int damage, int armor, int attackSpeed)
        {
            this.damage = damage;
            this.armor = armor;
            this.attackSpeed = attackSpeed;
        }

        public int Damage => damage;
        public int Armor => armor;
        public int AttackSpeed => attackSpeed;
    }
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