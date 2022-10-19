public class PlayerStats : CharacterStats
{
    public override void Initialization()
    {
        base.Initialization();
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

    protected override void Die()
    {
        base.Die();

        UI_Manager.instance.OpenPage(UI_Manager.instance.GetPageOfType<DeadPage>());
    }
}
