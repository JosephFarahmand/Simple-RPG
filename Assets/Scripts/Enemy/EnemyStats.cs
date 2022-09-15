using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    protected override void Die()
    {
        base.Die();

        // Die Animation

        // Supply rewards

        Destroy(gameObject);
    }
}
