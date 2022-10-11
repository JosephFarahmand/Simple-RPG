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

        if (AccountController.InventoryFullSpace >= StaticData.inventorySpace)
        {
            Debug.Log("Not enough room!!", gameObject);
            return false;
        }

        AccountController.AddInventoryItem(newItem);
        onItemChangedCallback?.Invoke();
        return true;
    }

    public void Remove(Item item)
    {
        if (AccountController.InventoryItems.Contains(item))
        {
            AccountController.RemoveInventoryItem(item);
            onItemChangedCallback?.Invoke();
        }
    }
}