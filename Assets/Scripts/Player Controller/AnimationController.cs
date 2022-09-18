using UnityEngine;
using UnityEngine.AI;
using NaughtyAttributes;

public class AnimationController : MonoBehaviour
{
    const float locomationAnimationSmoothTime = 0.1f;

    [SerializeField,AnimatorParam(nameof(animator),AnimatorControllerParameterType.Float)] private string walk;
    [SerializeField,AnimatorParam(nameof(animator),AnimatorControllerParameterType.Float)] private string attack;
    [SerializeField,AnimatorParam(nameof(animator),AnimatorControllerParameterType.Trigger)] private string die;
    readonly int[] attackAnimation = new int[4] { 5, 2, 3, 4 };

    NavMeshAgent agent;
    Animator animator;

    CharacterCombat combat;
    CharacterStats stats;

    private void Awake()
    {
        agent = transform.root.GetComponent<NavMeshAgent>();
        if(animator == null)
        animator = transform.root.GetComponentInChildren<Animator>();

        ResetAttackAnimation();

        combat = GetComponentInParent<CharacterCombat>();
        combat.OnAttack += AttackAnimation;

        stats = GetComponentInParent<CharacterStats>();
        stats.OnDie += DieAnimation;
    }

    private void DieAnimation()
    {
        animator.SetTrigger(die);
    }

    private void Reset()
    {
        animator = transform.root.GetComponentInChildren<Animator>();

    }

    private void Update()
    {
        var speedPercent = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("SpeedPercent", speedPercent, locomationAnimationSmoothTime, Time.deltaTime);
    }

    [Button]
    public void AttackAnimation()
    {
        animator.SetFloat(attack, attackAnimation[Random.Range(0, attackAnimation.Length)]);
    }

    public void ResetAttackAnimation()
    {
        animator.SetFloat(attack, 0);
    }
}
