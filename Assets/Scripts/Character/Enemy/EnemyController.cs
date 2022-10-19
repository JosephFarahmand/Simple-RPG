using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyStats))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private float lookRadius = 10; // Detetion range for player
    [SerializeField] private float rotationSpeed = 5f;

    NavMeshAgent agent;
    CharacterStats target;
    CharacterCombat combat;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();
    }

    private void Start()
    {
        target = PlayerManager.Stats;
    }

    private void Update()
    {
        if (GameManager.IsRun)
        {
            agent.isStopped = false;
        }
        else
        {
            agent.isStopped = true;
            return;
        }
        if (!target.IsAlive) return;

        // Distance to the target
        var distance = Vector3.Distance(transform.position, target.transform.position);

        // If inside the look radius
        if (distance < lookRadius)
        {
            if (!combat.inCombat)
            {
                // Move toward the target
                agent.SetDestination(target.transform.position);
            }

            // If within attacking distance
            if (distance < agent.stoppingDistance)
            {
                if (target != null && target.IsAlive)
                {
                    combat.Attack(target);
                }

                FaceTarget(); // Mack sure to face toward the target
            }
        }
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
