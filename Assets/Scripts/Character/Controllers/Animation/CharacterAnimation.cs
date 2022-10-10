using NaughtyAttributes;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimation : MonoBehaviour
{
    private bool isArmed = false;

    protected bool IsArmed
    {
        get => isArmed; set
        {
            isArmed = value;
            AnimationClip[] idleAnimationSet = GameManager.GameData.Animations.GetIdleAnimations(value ? GameAnimations.Mode.Armed : GameAnimations.Mode.UnArmed);
            AnimationClip[] slowWalkAnimationSet = GameManager.GameData.Animations.GetSlowWalkAnimations(value ? GameAnimations.Mode.Armed : GameAnimations.Mode.UnArmed);
            AnimationClip[] walkAnimationSet = GameManager.GameData.Animations.GetWalkAnimations(value ? GameAnimations.Mode.Armed : GameAnimations.Mode.UnArmed);
            AnimationClip[] runAnimationSet = GameManager.GameData.Animations.GetRunAnimations(value ? GameAnimations.Mode.Armed : GameAnimations.Mode.UnArmed);
            ChangeWalkTreeAnimations(idleAnimationSet.RandomItem(),
                                     slowWalkAnimationSet.RandomItem(),
                                     walkAnimationSet.RandomItem(),
                                     runAnimationSet.RandomItem());

            AnimationClip[] deathAnimationSet = GameManager.GameData.Animations.GetDeathAnimations(value ? GameAnimations.Mode.Armed : GameAnimations.Mode.UnArmed);
            ChangeDeathAnimation(deathAnimationSet.RandomItem());

            AnimationClip[] attackIdleAnimationSet = GameManager.GameData.Animations.GetAttackIdleAnimations(value ? GameAnimations.Mode.Armed : GameAnimations.Mode.UnArmed);
            ChangeAttackIdleAnimation(attackIdleAnimationSet.RandomItem());
        }
    }


    [Header("Walk Tree")]
    [SerializeField] AnimationClip replacableIdleAnim;
    [SerializeField] AnimationClip replacableSlowWalkAnim;
    [SerializeField] AnimationClip replacableWalkAnim;
    [SerializeField] AnimationClip replacableRunAnim;

    [Header("Attack Idle")]
    [SerializeField] AnimationClip replacableAttackIdleAnim;

    [Header("Death")]
    [SerializeField] AnimationClip replacableDeathAnim;

    [Header("Attack")]
    [SerializeField] AnimationClip replacableAttackAnim;

    [SerializeField] protected AnimationClip[] defaultAttackAnimSet;
    protected AnimationClip[] currentAttackAnimSet;

    const float locomationAnimationSmoothTime = 0.1f;


    [Header("Parameter")]
    [SerializeField, AnimatorParam(nameof(animator), AnimatorControllerParameterType.Float)] private string speedPercent = "SpeedPercent";
    [SerializeField, AnimatorParam(nameof(animator), AnimatorControllerParameterType.Bool)] private string inCombat = "inCombat";
    [SerializeField, AnimatorParam(nameof(animator), AnimatorControllerParameterType.Trigger)] private string attack = "attack";
    [SerializeField, AnimatorParam(nameof(animator), AnimatorControllerParameterType.Bool)] private string isAlive = "isAlive";

    [Header("Components")]
    [SerializeField] NavMeshAgent agent;
    [SerializeField] protected Animator animator;
    [SerializeField] protected CharacterCombat combat;
    [SerializeField] protected CharacterStats stats;
    protected AnimatorOverrideController overrideController;

    private void Reset()
    {
        animator = transform.root.GetComponentInChildren<Animator>();
    }


    public virtual void Initialization()
    {
        //agent ??= transform.root.GetComponentInChildren<NavMeshAgent>();
        //animator ??= transform.root.GetComponentInChildren<Animator>();
        //combat ??= transform.root.GetComponentInChildren<CharacterCombat>();
        //stats ??= transform.root.GetComponentInChildren<CharacterStats>();

        overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = overrideController;

        currentAttackAnimSet = defaultAttackAnimSet;

        animator.SetBool(isAlive, true);

        combat.OnAttack += OnAttack;
        stats.OnDie += Stats_OnDie;
    }

    private void Stats_OnDie()
    {
        animator.SetBool(isAlive, false);
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

    protected void ChangeWalkTreeAnimations(AnimationClip idle, AnimationClip slowWalk, AnimationClip walk, AnimationClip run)
    {
        overrideController[replacableIdleAnim] = idle ?? replacableIdleAnim;
        overrideController[replacableSlowWalkAnim] = slowWalk ?? replacableSlowWalkAnim;
        overrideController[replacableWalkAnim] = walk ?? replacableWalkAnim;
        overrideController[replacableRunAnim] = run ?? replacableRunAnim;
    }

    protected void ChangeAttackIdleAnimation(AnimationClip clip)
    {
        overrideController[replacableAttackIdleAnim] = clip ?? replacableAttackIdleAnim;
    }

    protected void ChangeDeathAnimation(AnimationClip clip)
    {
        overrideController[replacableDeathAnim] = clip ?? replacableDeathAnim;
    }
}
