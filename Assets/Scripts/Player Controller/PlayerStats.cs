public class PlayerStats : CharacterStats
{
    private void Start()
    {
        PlayerManager.EquipController.onEquipmentChanged += OnEquipmentChanged;
    }

    private void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            armor.AddModifier(newItem.Modifier.Armor);
            Damage.AddModifier(newItem.Modifier.Damage);
            attackSpeed.AddModifier(newItem.Modifier.AttackSpeed);
        }

        if (oldItem != null)
        {
            armor.RemoveModifier(oldItem.Modifier.Armor);
            Damage.RemoveModifier(oldItem.Modifier.Damage);
            attackSpeed.RemoveModifier(oldItem.Modifier.AttackSpeed);
        }
    }
}
