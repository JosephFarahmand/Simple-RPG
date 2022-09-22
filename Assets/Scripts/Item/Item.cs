using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public abstract class Item : ScriptableObject
{
    [SerializeField] private string id = "";
    //[SerializeField] private new string name = "New Item";
    [SerializeField] private ItemType type;
    [SerializeField] private Sprite icon = null;
    [SerializeField] private bool isDefaultItem = false;

    [Header("Shop")]
    [SerializeField] private List<Requiement> conditions;
    [SerializeField, Min(0)] private int price = 100;
    [SerializeField, NaughtyAttributes.ReadOnly] private int count = 0;

    public string Id => id;
    public string Name => name;
    public ItemType Type => type;
    public Sprite Icon => icon;
    public bool IsDefaultItem => isDefaultItem;

    public int Price => price;
    public int Count => count;


    public bool HasConditions()
    {
        foreach (var condition in conditions)
        {
            if (condition.Check(this) == false)
            {
                return false;
            }
        }
        return true;
    }

    public virtual void Use()
    {
        Debug.Log("Using " + Name);
    }

    public void RemoveFromInventory()
    {
        PlayerManager.InventoryController.Remove(this);
    }
}

public enum ItemType
{
    None,
    Common,
    Rare,
    Legendary
}
