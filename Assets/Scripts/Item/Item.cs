using UnityEngine;

//[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public abstract class Item : ScriptableObject
{
    [SerializeField] private string id = "";
    //[SerializeField] private new string name = "New Item";
    [SerializeField] private ItemType type;
    [SerializeField] private Sprite icon = null;
    [SerializeField] private bool isDefaultItem = false;

    public string Id => id;
    public string Name => name;
    public ItemType Type => type;
    public Sprite Icon => icon;
    public bool IsDefaultItem => isDefaultItem;

    public virtual void Use()
    {
        Debug.Log("Using " + Name);
    }

    public void RemoveFromInventory()
    {
        Inventory.Instance.Remove(this);
    }
}

public enum ItemType
{
    None,
    Common,
    Rare,
    Legendary
}