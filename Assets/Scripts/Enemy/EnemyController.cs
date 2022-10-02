using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyStats))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private float lookRadius = 10; // Detetion range for player
    [SerializeField] private float rotationSpeed = 5f;

    NavMeshAgent agent;
    Transform target;
    CharacterCombat combat;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();
    }

    private void Start()
    {
        target = PlayerManager.GetPlayer().transform;
    }

    private void Update()
    {
        // Distance to the target
        var distance = Vector3.Distance(transform.position, target.position);

        // If inside the look radius
        if (distance < lookRadius)
        {
            // Move toward the target
            agent.SetDestination(target.position);

            // If within attacking distance
            if (distance < agent.stoppingDistance)
            {
                var stats = target.GetComponent<CharacterStats>();
                if (stats != null)
                {
                    combat.Attack(stats);
                }

                FaceTarget(); // Mack sure to face toward the target
            }
        }
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
