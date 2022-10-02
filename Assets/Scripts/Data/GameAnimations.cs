using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAnimations : MonoBehaviour
{
    [SerializeField] private WalkTreeAnimations WalkTree;
    //[SerializeField] private Animations Idle;
    [SerializeField] private Animations Death;
    [SerializeField] private Animations AttackIdle;

    [SerializeField] private List<WeaponAnimations> weaponAnimations;

    public AnimationClip[] GetIdleAnimations(Mode mode) => WalkTree.Idle(mode);
    public AnimationClip[] GetSlowWalkAnimations(Mode mode) => WalkTree.SlowWalk(mode);
    public AnimationClip[] GetWalkAnimations(Mode mode) => WalkTree.Walk(mode);
    public AnimationClip[] GetRunAnimations(Mode mode) => WalkTree.Run(mode);

    public AnimationClip[] GetDeathAnimations(Mode mode) => Death.GetClips(mode);
    public AnimationClip[] GetAttackIdleAnimations(Mode mode) => AttackIdle.GetClips(mode);

    public AnimationClip[] GetWeaponAnimationSet(string weaponId)
    {
        var weapon = weaponAnimations.Find(obj => obj.Id == weaponId);
        return weapon.Clips;
    }

    [System.Serializable]
    public struct WalkTreeAnimations
    {
        [SerializeField] private Animations idle;
        [SerializeField] private Animations slowWalk;
        [SerializeField] private Animations walk;
        [SerializeField] private Animations run;

        public AnimationClip[] Idle(Mode mode) => idle.GetClips(mode);
        public AnimationClip[] SlowWalk(Mode mode) => slowWalk.GetClips(mode);
        public AnimationClip[] Walk(Mode mode) => walk.GetClips(mode);
        public AnimationClip[] Run(Mode mode) => run.GetClips(mode);
    }


    [System.Serializable]
    public struct Animations
    {
        [SerializeField] private AnimationClip[] armed;
        [SerializeField] private AnimationClip[] unarmed;

        public AnimationClip[] GetClips(Mode mode)
        {
            return mode switch
            {
                Mode.Armed => armed,
                Mode.UnArmed => unarmed,
                _ => unarmed,
            };
        }
    }

    public enum Mode
    {
        Armed,
        UnArmed
    }

    [System.Serializable]
    public struct WeaponAnimations
    {
        [SerializeField] private Equipment equipment;
        [SerializeField] private AnimationClip[] clips;

        public string Id => equipment.Id;

        public AnimationClip[] Clips => clips;
    }
}
