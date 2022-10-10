using UnityEngine;

[CreateAssetMenu(fileName = "New Resource", menuName = "Item/Resource")]
public class Resource : Item
{
    [Header("Resource")]
    [SerializeField] private ResourceType type;
    [SerializeField] private int value = 10;

    public Resource(string id, string displayName, ItemRarity rarity, Sprite icon, int requiredLevel, int price, CurrencyType currencyType, string assetId, ResourceType type, int value) : base(id, displayName, rarity, icon, requiredLevel, price, currencyType,assetId)
    {
        this.type = type;
        this.value = value;

        GameManager.GameData.SetItemModel(this);
    }

    public ResourceType Type => type;

    public int Value => value;

    public override void Use()
    {
        base.Use();

        // Use Resource
        if (type == ResourceType.Gold)
        {
            AccountController.AddCoinValue(value);
        }
        else if (type == ResourceType.Gem)
        {
            AccountController.AddGemValue(value);
        }

        // Remove it from inventory
        RemoveFromInventory();
    }
}

public enum ResourceType
{
    Gold,
    Gem
}
