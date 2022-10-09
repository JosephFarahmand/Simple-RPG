using UnityEngine;

[CreateAssetMenu(fileName = "New Resource", menuName = "Item/Resource")]
public class Resource : Item
{
    [Header("Resource")]
    [SerializeField] private ResourceType type;
    [SerializeField] private int value = 10;

    public ResourceType Type => type;

    public override void Use()
    {
        base.Use();

        // Use Resource
        if (type == ResourceType.Coin)
        {
            AccountController.AddCoinValue(value);
        }
        else if (type == ResourceType.Gem)
        {
            //AccountController.AddGemValue(value);
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
