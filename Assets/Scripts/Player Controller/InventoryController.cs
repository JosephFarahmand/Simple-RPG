using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space = 20;

    public List<Item> items = new List<Item>();

    public bool Add(Item newItem)
    {
        if (newItem.IsDefaultItem) return false;

        if (items.Count >= space)
        {
            Debug.Log("Not enough room!!", gameObject);
            return false;
        }
        items.Add(newItem);

        onItemChangedCallback?.Invoke();
        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);
        onItemChangedCallback?.Invoke();
    }
}