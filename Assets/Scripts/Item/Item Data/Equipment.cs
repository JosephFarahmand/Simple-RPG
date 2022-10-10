using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Item/Equipment")]
public class Equipment : Item
{
    [Header("Equipment")]
    public EquipmentSlot equipSlot;  // Slot to store equipment in

    [Header("Modifier")]
    [Tooltip("Increase/decrease in each one")]
    [SerializeField, ReadOnly] private ItemModifier modifier;

    public Equipment(string id, string displayName, ItemRarity rarity, Sprite icon, int requiredLevel, int price, CurrencyType currencyType, string assetId, EquipmentSlot equipSlot, ItemModifier modifier) : base(id, displayName, rarity, icon, requiredLevel, price, currencyType,assetId)
    {
        this.equipSlot = equipSlot;
        this.modifier = modifier;

        GameManager.GameData.SetItemModel(this);
    }

    public ItemModifier Modifier => modifier;

    // When pressed in inventory
    public override void Use()
    {
        base.Use();

        PlayerManager.EquipController.Equip(this);  // Equip it
        RemoveFromInventory();                      // Remove it from inventory
    }

    [Button]
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