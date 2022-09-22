using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NaughtyAttributes;

public class CharacterAnimation : MonoBehaviour
{
    public AnimationClip replacableAttackAnim;

    public AnimationClip[] defaultAttackAnimSet;
    protected AnimationClip[] currentAttackAnimSet;

    const float locomationAnimationSmoothTime = 0.1f;

    NavMeshAgent agent;
    protected Animator animator;
    protected CharacterCombat combat;
    protected CharacterStats stats;
    protected AnimatorOverrideController overrideController;

    [Header("Parameter")]
    [SerializeField, AnimatorParam(nameof(animator),AnimatorControllerParameterType.Float)] private string speedPercent = "SpeedPercent";
    [SerializeField,AnimatorParam(nameof(animator),AnimatorControllerParameterType.Bool)] private string inCombat= "inCombat";
    [SerializeField, AnimatorParam(nameof(animator),AnimatorControllerParameterType.Trigger)] private string attack = "attack";
    [SerializeField, AnimatorParam(nameof(animator),AnimatorControllerParameterType.Bool)] private string isAlive = "isAlive";

    private void Reset()
    {
        animator = transform.root.GetComponentInChildren<Animator>();
    }

    protected virtual void Start()
    {
        agent ??= transform.root.GetComponentInChildren<NavMeshAgent>();
        animator ??= transform.root.GetComponentInChildren<Animator>();
        combat ??= transform.root.GetComponentInChildren<CharacterCombat>();
        stats ??= transform.root.GetComponentInChildren<CharacterStats>();

        overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = overrideController;

        currentAttackAnimSet = defaultAttackAnimSet;

        animator.SetBool(isAlive, true);

        combat.OnAttack += OnAttack;
        stats.OnChangeHealth += Stats_OnChangeHealth;
    }

    private void Stats_OnChangeHealth(float maxHealth, float currentHealth)
    {
        if (currentHealth < 0)
        {
            animator.SetBool(isAlive, false);
        }
    }

    private void Update()
    {
        var speedPercent = agent.velocity.magnitude / agent.speed;
        animator.SetFloat(this.speedPercent, speedPercent, locomationAnimationSmoothTime, Time.deltaTime);

        animator.SetBool(inCombat, combat.inCombat);
    }

    protected virtual void OnAttack()
    {
        animator.SetTrigger(attack);
        overrideController[replacableAttackAnim] = currentAttackAnimSet.RandomItem();
    }
}
