using NaughtyAttributes;
using UnityEngine;

//[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public abstract class Item : ScriptableObject
{
    [SerializeField, ReadOnly] private string id = "";
    [SerializeField, Label("Name")] private string displayName = "New Item";
    [SerializeField] private ItemRarity rarity;
    [SerializeField] private Sprite icon = null;
    public string AssetId { get; private set; }

    [Header("Shop")]
    [SerializeField] private int requiredLevel = 1;
    //[SerializeField, ReadOnly] private int count = 0;

    [Header("Price")]
    [SerializeField, Min(0)] private int price = 100;
    [SerializeField] private CurrencyType currencyType;

    protected Item(string id, string displayName, ItemRarity rarity, Sprite icon, int requiredLevel, int price, CurrencyType currencyType, string assetId)
    {
        this.id = id;
        this.displayName = displayName;
        this.rarity = rarity;
        this.icon = icon;
        this.requiredLevel = requiredLevel;
        this.price = price;
        this.currencyType = currencyType;
        this.AssetId = assetId;
    }

    //public string Id => id == "" ? GetInstanceID().ToString() : id;
    public string Id => id;
    public string Name => displayName == "New Item" ? name : displayName;
    public ItemRarity Rarity => rarity;
    public Sprite Icon => icon;
    public bool IsDefaultItem => rarity == ItemRarity.Free;
    public int RequiredLevel => requiredLevel;
    public int Price => price;
    public CurrencyType CurrencyType => currencyType;

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

public enum CurrencyType
{
    Gold,
    Gem,
    Dollar
}