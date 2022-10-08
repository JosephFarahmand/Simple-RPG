using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactable
{
    CharacterStats myStats;
    EnemyAnimation myAnimation;

    public CharacterStats Stats { get => myStats;  }

    private void Start()
    {
        myStats = GetComponent<CharacterStats>();
        myStats.Initialization();
        myAnimation = GetComponent<EnemyAnimation>();
        myAnimation.Initialization();
    }

    public override void Interact()
    {
        base.Interact();

        // Attack the enemy

        PlayerManager.Combat.Attack(myStats);
    }
}
