using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string Id = "";
    new public string name = "New Item";
    public ItemType type;
    public Sprite icon = null;
    public bool isDefaultItem = false;
    public int amount = 0;

    [Header("Crafting")]
    public bool isCrafted = false;
    public List<Item> requerdItems;


    public virtual void Use()
    {
        Debug.Log("Using " + name);
    }

    public void RemoveFromInventory()
    {
        Inventory.Instance.Remove(this);
    }
}


public enum ItemType
{
    Common,
    Rare,
    Legendary
}