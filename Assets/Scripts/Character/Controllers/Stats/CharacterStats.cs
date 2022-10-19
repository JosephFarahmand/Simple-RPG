using System;
using System.Collections;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public float maxHealth = 100;

    [Header("Stats")]
    [SerializeField] protected StatsField damage;
    [SerializeField] protected StatsField armor;
    [SerializeField] protected StatsField attackSpeed;
    [SerializeField] protected StatsField moveSpeed;

    /// <summary>
    /// Character's current health
    /// </summary>
    public float CurrentHealth { get; private set; }
    public bool IsAlive { get; protected set; } = true;
    public StatsField Damage => damage;
    public StatsField Armor => armor;
    public StatsField AttackSpeed => attackSpeed;
    public StatsField MoveSpeed => moveSpeed;

    /// <summary>
    /// (max health, current health)
    /// </summary>
    public event Action<float,float> OnChangeHealth;

    public event Action OnDie;


    public virtual void Initialization()
    {
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, float.MaxValue);

        CurrentHealth -= damage;
        Debug.Log($"{transform.name} takes {damage} damage.");

        OnChangeHealth?.Invoke(maxHealth, CurrentHealth);

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Debug.Log($"{transform.name} dead.");

        OnDie?.Invoke();

        IsAlive = false;
    }
}
