using UnityEngine;
using UnityEngine.AI;

public class AnimationController : MonoBehaviour
{
    const float locomationAnimationSmoothTime = 0.1f;

    NavMeshAgent agent;
    Animator animator;

    private void Awake()
    {
        agent = transform.root.GetComponent<NavMeshAgent>();
        animator = transform.root.GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        var speedPercent = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("SpeedPercent", speedPercent, locomationAnimationSmoothTime, Time.deltaTime);
    }
}
