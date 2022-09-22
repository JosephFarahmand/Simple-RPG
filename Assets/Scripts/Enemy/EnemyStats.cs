using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    protected override void Die()
    {
        base.Die();

        // Supply rewards
        var chest = Instantiate(GameData.GetChest());
        chest.transform.position = transform.position;

        StartCoroutine(DestriyDelay());
    }

    IEnumerator DestriyDelay()
    {
        yield return new WaitForSeconds(2);

        Destroy(gameObject);
    }
}
