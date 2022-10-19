public static class StaticData
{
    public const int maxChestSpace = 24;
    public const int inventorySpace = 24;

    public const string defaultSkinId = "90";
    public const string defaultEmail = "";
    public const string defaultUsername = "";
    public const string defaultPassword = "";

    public static PlayerProfile SampleProfile => new PlayerProfile("Guest", "Guest Player", 0, 0, 1, defaultSkinId, 0);

    public const float killEnemyXP = 10;
    public const float collectItemXP = 2;

    public const string likeURL = "http://unity3d.com/";
    public const string aboutURL = "http://unity3d.com/";

    public static Equipment.ItemModifier GetItemModifier(Equipment equipment)
    {
        return GetItemModifier(equipment.Rarity, equipment.equipSlot);
    }

    public static Equipment.ItemModifier GetItemModifier(ItemRarity rarity, EquipmentSlot slot)
    {
        int damage = 0;
        int armor = 0;
        int attackSpeed = 0;
        int moveSpeed = 0;

        switch (slot)
        {
            case EquipmentSlot.Weapon:
                damage += 3;
                attackSpeed += 2;
                break;
            case EquipmentSlot.Shield:
                damage += 1;
                break;
            case EquipmentSlot.Head:
                armor += 1;
                break;
            case EquipmentSlot.Shoulders:
                break;
            case EquipmentSlot.Arm:
                break;
            case EquipmentSlot.Hands:
                armor += 1;
                attackSpeed += 1;
                break;
            case EquipmentSlot.Chest:
                attackSpeed += 2;
                armor += 1;
                moveSpeed -= 1;
                break;
            case EquipmentSlot.Belt:
                damage += 1;
                break;
            case EquipmentSlot.Legs:
                armor += 1;
                moveSpeed += 3;
                break;
            case EquipmentSlot.Feet:
                armor += 1;
                moveSpeed += 2;
                break;
        }

        switch (rarity)
        {
            case ItemRarity.Free:
                damage *= 1;
                armor *= 1;
                attackSpeed *= 1;
                moveSpeed *= 1;
                break;
            case ItemRarity.Common:
                damage *= 5;
                armor *= 5;
                attackSpeed *= 5;
                moveSpeed *= 5;
                break;
            case ItemRarity.Rare:
                damage *= 10;
                armor *= 10;
                attackSpeed *= 10;
                moveSpeed *= 10;
                break;
            case ItemRarity.Epic:
                damage *= 15;
                armor *= 15;
                attackSpeed *= 15;
                moveSpeed *= 15;
                break;
            case ItemRarity.Legendary:
                damage *= 20;
                armor *= 20;
                attackSpeed *= 20;
                moveSpeed *= 20;
                break;
        }

        return new Equipment.ItemModifier(damage, armor, attackSpeed / 50, moveSpeed);
    }
}
