using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : CharacterAnimation
{
    public override void Initialization()
    {
        base.Initialization();
        IsArmed = false;

        PlayerManager.EquipController.onEquipmentChanged += onEquipmentChanged;
    }

    private void onEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if(newItem != null && newItem.equipSlot == EquipmentSlot.Weapon)
        {
            // a weapon equiped
            var animationSet = GameManager.GameData.Animations.GetWeaponAnimationSet(newItem.Id);
            if(animationSet.Length > 0)
            //if (weaponAnimationsDic.ContainsKey(newItem))
            {
                currentAttackAnimSet = animationSet;
                IsArmed = true;
            }
        }
        else if(newItem == null && oldItem != null && oldItem.equipSlot == EquipmentSlot.Weapon)
        {
            // unequiped weapon
            IsArmed = false;
            currentAttackAnimSet = defaultAttackAnimSet;
        }
    }

    //[System.Serializable]
    //public struct WeaponAnimations
    //{
    //    public Equipment equipment;
    //    public AnimationClip[] clips;
    //}
}
