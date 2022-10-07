using NaughtyAttributes;
using UnityEngine;

//[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public abstract class Item : ScriptableObject
{
    [SerializeField, ReadOnly] private string id = "";
    [SerializeField, Label("Name")] private string displayName = "New Item";
    [SerializeField] private ItemRarity rarity;
    [SerializeField] private Sprite icon = null;
    //[SerializeField] private bool isDefaultItem = false;

    [Header("Shop")]
    [SerializeField] private int requiredLevel = 1;
    [SerializeField, Min(0)] private int price = 100;
    //[SerializeField, ReadOnly] private int count = 0;

    public string Id => id;
    public string Name => displayName == "New Item" ? name : displayName;
    public ItemRarity Rarity => rarity;
    public Sprite Icon => icon;
    public bool IsDefaultItem => rarity == ItemRarity.Free;
    public int RequiredLevel => requiredLevel;
    public int Price => price;
    //public int Count => count <= 0 ? 1 : count;

    public ItemStoredData StoredData => new ItemStoredData(this);

    //public void AddCount()
    //{
    //    count++;
    //}

    //public void RemoveCount()
    //{
    //    count--;
    //}

    public virtual void Use()
    {
        Debug.Log("Using " + Name);
    }

    protected void RemoveFromInventory()
    {
        PlayerManager.InventoryController.Remove(this);
    }
}

public enum ItemRarity
{
    /// <summary>
    /// default item
    /// </summary>
    Free,
    Common,
    Rare,
    Epic,
    Legendary
}
