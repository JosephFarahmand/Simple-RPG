public class EnemyAnimation: CharacterAnimation
{
    protected override void Awake()
    {
        base.Awake();
        transform.root.GetComponentInChildren<EnemyCustomizer>().onEquip += onEquip;
    }

    protected override void Start()
    {
        base.Start();
        IsArmed = true;

    }

    private void onEquip(Equipment item)
    {
        if (item != null && item.equipSlot == EquipmentSlot.Weapon)
        {
            // a weapon equiped
            var animationSet = GameData.Animations.GetWeaponAnimationSet(item.Id);
            if (animationSet.Length > 0)
            {
                currentAttackAnimSet = animationSet;
                
            }
            else
            {
                currentAttackAnimSet = defaultAttackAnimSet;
            }
        }
    }
}