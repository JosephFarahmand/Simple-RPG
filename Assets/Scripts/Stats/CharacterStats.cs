using System;
using System.Collections;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public float maxHealth = 100;
    /// <summary>
    /// Character's current health
    /// </summary>
    public float Health { get; private set; }

    [Header("Stats")]
    [SerializeField] protected Stats damage;
    [SerializeField] protected Stats armor;

    private void Awake()
    {
        Health = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, float.MaxValue);

        Health -= damage;
        Debug.Log($"{transform.name} takes {damage} damage.");

        if(Health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Debug.Log($"{transform.name} dead.");

    }
}
