public static class StaticData
{
    public const int maxChestSpace = 24;
    public const int inventorySpace = 24;

    public static Equipment.ItemModifier GetItemModifier(ItemType itemType,EquipmentSlot slot)
    {
        int damage = 0;
        int armor = 0;
        int attackSpeed = 0;

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
                break;
            case EquipmentSlot.Belt:
                damage += 1;
                break;
            case EquipmentSlot.Legs:
                armor += 1;
                break;
            case EquipmentSlot.Feet:
                armor += 1;
                break;
        }

        switch (itemType)
        {
            case ItemType.None:
                break;
            case ItemType.Common:
                damage *= 5;
                armor *= 5;
                attackSpeed *= 5;
                break;
            case ItemType.Rare:
                damage *= 10;
                armor *= 10;
                attackSpeed *= 10;
                break;
            case ItemType.Legendary:
                damage *= 20;
                armor *= 20;
                attackSpeed *= 20;
                break;
        }

        return new Equipment.ItemModifier(damage, armor, attackSpeed / 50);
    }
}
