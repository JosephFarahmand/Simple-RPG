using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Chest", menuName = "Inventory/Chest")]
public class Chest : Item
{
    [SerializeField] private List<Item> items;

    public const int maxChestSpace = 24;

    public List<Item> Items
    {
        get
        {
            if (items.Count > maxChestSpace)
            {
                items.RemoveRange(maxChestSpace, items.Count - maxChestSpace);
            }
            return items;
        }
    }

    public override void Use()
    {
        base.Use();

    }
}
