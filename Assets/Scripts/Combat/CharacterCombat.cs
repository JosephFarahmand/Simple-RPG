using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    private float attackCooldown = 0f;
    private float combatCooldown = 5f;
    [SerializeField] private float attackDelay = 0.6f;

    public event System.Action OnAttack;

    CharacterStats myStats;
    internal bool inCombat;
    private float lastAttackTime;

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

        if (Time.deltaTime - lastAttackTime > combatCooldown)
        {
            inCombat = false;
        }
    }

    public void Attack(CharacterStats targetStats)
    {
        if (attackCooldown <= 0)
        {
            StartCoroutine(DoDamge(targetStats, attackDelay));

            OnAttack?.Invoke();

            attackCooldown = 1 / myStats.AttackSpeed.GetValue();
            inCombat = true;
            lastAttackTime = Time.deltaTime;
        }
    }

    private IEnumerator DoDamge(CharacterStats stats, float delay)
    {
        yield return new WaitForSeconds(delay);
        stats.TakeDamage(myStats.Damage.GetValue());

        if (stats.CurrentHealth <= 0)
        {
            inCombat = false;
        }
    }
}
