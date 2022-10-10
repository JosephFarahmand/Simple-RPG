using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    //public List<Item> items = new List<Item>();

    public bool Add(Item newItem)
    {
        if (newItem.IsDefaultItem) return false;

        if (AccountController.Profile.GetInventorySpace() >= StaticData.inventorySpace)
        {
            Debug.Log("Not enough room!!", gameObject);
            return false;
        }

        onItemChangedCallback?.Invoke();
        AccountController.AddInventoryItem(newItem);
        return true;
    }

    public void Remove(Item item)
    {
        if (AccountController.Profile.InventoryItems.Contains(item))
        {
            onItemChangedCallback?.Invoke();
            AccountController.RemoveInventoryItem(item);
        }
    }

    public bool HasItem(Item item)
    {
        return AccountController.Profile.InventoryItems.Contains(item);
    }
}