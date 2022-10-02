using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    private float attackCooldown = 0f;
    private float combatCooldown = 1f;

    public event System.Action OnAttack;

    CharacterStats myStats;
    CharacterStats opennetStats;
    internal bool inCombat;
    private float lastAttackTime;

    private void Start()
    {
        myStats = GetComponent<CharacterStats>();
        myStats.OnDie += Stats_OnDie;
    }

    private void Stats_OnDie()
    {
        inCombat = false;
    }

    private void Update()
    {
        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }

        if (lastAttackTime > 0)
        {
            lastAttackTime -= Time.deltaTime;

            if (lastAttackTime <= 0)
            {
                inCombat = false;
            }
        }
    }

    public void Attack(CharacterStats targetStats)
    {
        if (attackCooldown <= 0)
        {
            opennetStats = targetStats;

            OnAttack?.Invoke();

            attackCooldown = 1 / myStats.AttackSpeed.GetValue();
            inCombat = true;
            lastAttackTime = combatCooldown;
        }
    }

    public void AttackHit_AnimationEvent()
    {
        opennetStats.TakeDamage(myStats.Damage.GetValue());

        if (opennetStats.CurrentHealth <= 0)
        {
            inCombat = false;
        }
    }
}
