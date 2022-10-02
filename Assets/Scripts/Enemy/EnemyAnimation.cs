using UnityEngine;

public class EnemyAnimation: CharacterAnimation
{
    [SerializeField] private EnemyCustomizer customizer;

    public override void Initialization()
    {
        base.Initialization();

        customizer.onEquip += onEquip;
        IsArmed = true;
    }

    private void onEquip(Equipment item)
    {
        if (item != null && item.equipSlot == EquipmentSlot.Weapon)
        {
            // a weapon equiped
            var animationSet = GameManager.GameData.Animations.GetWeaponAnimationSet(item.Id);
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