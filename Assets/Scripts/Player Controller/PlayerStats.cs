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
            damage.AddModifier(newItem.Modifier.Damage);
        }

        if (oldItem != null)
        {
            armor.RemoveModifier(oldItem.Modifier.Armor);
            damage.RemoveModifier(oldItem.Modifier.Damage);
        }
    }
}
