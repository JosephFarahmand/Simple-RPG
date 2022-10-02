using UnityEngine;

[CreateAssetMenu(fileName = "New Resource", menuName = "Inventory/Resource")]
public class Resource : Item
{
    [Header("Resource")]
    [SerializeField] private ResourceType type;

    public ResourceType Type => type;

    public override void Use()
    {
        base.Use();

        // Use Resource
        if (type == ResourceType.Coin)
        {
            PlayerManager.Profile.AddCoinValue(Count);
        }
        else if (type == ResourceType.Gem)
        {
            PlayerManager.Profile.AddGemValue(Count);
        }

        // Remove it from inventory
        RemoveFromInventory();
    }
}

public enum ResourceType
{
    Coin,
    Gem
}
