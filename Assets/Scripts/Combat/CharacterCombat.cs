using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    private float attackCooldown = 0f;
    private float combatCooldown = 5f;

    public event System.Action OnAttack;

    CharacterStats myStats;
    CharacterStats opennetStats;
    internal bool inCombat;
    private float lastAttackTime;

    private void Start()
    {
        myStats = GetComponent<CharacterStats>();
        myStats.OnChangeHealth += MyStats_OnChangeHealth;
    }

    private void MyStats_OnChangeHealth(float arg1, float currentHealth)
    {
        if(currentHealth < 0)
        {
            inCombat = false;
        }
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
            opennetStats = targetStats;

            OnAttack?.Invoke();

            attackCooldown = 1 / myStats.AttackSpeed.GetValue();
            inCombat = true;
            lastAttackTime = Time.deltaTime;
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
