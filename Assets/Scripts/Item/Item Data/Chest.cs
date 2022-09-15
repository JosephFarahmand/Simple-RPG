using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Chest", menuName = "Inventory/Chest")]
public class Chest : Item
{
    [SerializeField] private List<Item> items;

    public List<Item> Items => items;

    public override void Use()
    {
        base.Use();

    }
}
