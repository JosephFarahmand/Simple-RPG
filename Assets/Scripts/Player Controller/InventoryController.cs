using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public List<Item> items = new List<Item>();

    public System.Action<Item> onAddNewItem;
    public System.Action<Item> onRemoveItem;

    public bool Add(Item newItem)
    {
        if (newItem.IsDefaultItem) return false;

        if (items.Count >= StaticData.inventorySpace)
        {
            Debug.Log("Not enough room!!", gameObject);
            return false;
        }

        items.Add(newItem);

        onItemChangedCallback?.Invoke();
        onAddNewItem?.Invoke(newItem);
        return true;
    }

    public void Remove(Item item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            onItemChangedCallback?.Invoke();
            onRemoveItem?.Invoke(item);
        }
    }

    public bool HasItem(Item item)
    {
        return items.Contains(item);
    }

    public int GetItemCount(string itemId)
    {
        int count = 0;
        foreach (Item item in items)
        {
            if (item.Id == itemId)
            {
                count++;
            }
        }
        return count;
    }
}