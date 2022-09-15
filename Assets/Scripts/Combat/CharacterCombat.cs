using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    private float attackCooldown = 0f;
    [SerializeField] private float attackDelay = 0.6f;

    public event System.Action OnAttack;

    CharacterStats myStats;

    private void Start()
    {
        myStats = GetComponent<CharacterStats>();
    }

    private void Update()
    {
        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }
    }

    public void Attack(CharacterStats targetStats)
    {
        if (attackCooldown <= 0)
        {
            StartCoroutine(DoDamge(targetStats, attackDelay));

            OnAttack?.Invoke();

            attackCooldown = 1 / myStats.AttackSpeed.GetValue();
        }
    }

    private IEnumerator DoDamge(CharacterStats stats,float delay)
    {
        yield return new WaitForSeconds(delay);
        stats.TakeDamage(myStats.Damage.GetValue());

    }
}
