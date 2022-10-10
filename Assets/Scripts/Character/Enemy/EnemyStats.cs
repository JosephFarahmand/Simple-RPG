using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    [SerializeField] private EnemyCustomizer customizer;

    protected override void Die()
    {
        base.Die();

        // Supply rewards
        var chest = Instantiate(GameManager.GameData.GetChest());
        chest.transform.position = transform.position;

        StartCoroutine(DestriyDelay());
    }

    IEnumerator DestriyDelay()
    {
        yield return new WaitForSeconds(2);

        Destroy(gameObject);
    }

    public override void Initialization()
    {
        base.Initialization();

        customizer.onEquip += onEquip;
    }

    private void onEquip(Equipment item)
    {
        if (item != null)
        {
            armor.AddModifier(item.Modifier.Armor);
            Damage.AddModifier(item.Modifier.Damage);
            attackSpeed.AddModifier(item.Modifier.AttackSpeed);
        }
    }
}
