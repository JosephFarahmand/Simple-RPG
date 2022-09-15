using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyStats))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private float lookRadius = 10;

    NavMeshAgent agent;
    Transform target;
    [SerializeField]private float rotationSpeed = 5f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        target = PlayerManager.GetPlayer().transform;
    }

    private void Update()
    {
        var distance = Vector3.Distance(transform.position, target.position);

        if (distance < lookRadius)
        {
            agent.SetDestination(target.position);

            if (distance < agent.stoppingDistance)
            {
                agent.isStopped = true;

                // Attack the target

                // Face the target
                FaceTarget();
            }
        }
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z)); ;
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
