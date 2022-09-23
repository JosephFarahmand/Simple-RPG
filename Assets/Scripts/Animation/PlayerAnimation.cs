using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : CharacterAnimation
{
    

    //[SerializeField] private List<WeaponAnimations> weaponAnimations;
    //Dictionary<Equipment, AnimationClip[]> weaponAnimationsDic;
    protected override void Start()
    {
        base.Start();
        IsArmed = false;

        PlayerManager.EquipController.onEquipmentChanged += onEquipmentChanged;

        //weaponAnimationsDic= new Dictionary<Equipment, AnimationClip[]>();
        //foreach(var weaponAnimation in weaponAnimations)
        //{
        //    if (weaponAnimationsDic.ContainsKey(weaponAnimation.equipment))
        //    {
        //        weaponAnimationsDic[weaponAnimation.equipment] = weaponAnimation.clips;
        //    }
        //    else
        //    {
        //        weaponAnimationsDic.Add(weaponAnimation.equipment, weaponAnimation.clips);
        //    }
        //}
    }

    private void onEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if(newItem != null && newItem.equipSlot == EquipmentSlot.Weapon)
        {
            // a weapon equiped
            var animationSet = GameData.Animations.GetWeaponAnimationSet(newItem.Id);
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
