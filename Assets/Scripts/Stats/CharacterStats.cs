using System;
using System.Collections;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public float maxHealth = 100;
    /// <summary>
    /// Character's current health
    /// </summary>
    public float CurrentHealth { get; private set; }
    public Stats Damage => damage;
    public Stats Armor => armor;
    public Stats AttackSpeed => attackSpeed;

    /// <summary>
    /// (max health, current health)
    /// </summary>
    public event Action<float,float> OnChangeHealth;

    [Header("Stats")]
    [SerializeField] protected Stats damage;
    [SerializeField] protected Stats armor;
    [SerializeField] protected Stats attackSpeed;

    public event System.Action OnDie;


    protected virtual void Awake()
    {
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, float.MaxValue);

        CurrentHealth -= damage;
        Debug.Log($"{transform.name} takes {damage} damage.");

        OnChangeHealth?.Invoke(maxHealth,CurrentHealth);

        if(CurrentHealth <= 0)
        {
            Die();
        }

    }

    protected virtual void Die()
    {
        Debug.Log($"{transform.name} dead.");

        OnDie?.Invoke();
    }
}
