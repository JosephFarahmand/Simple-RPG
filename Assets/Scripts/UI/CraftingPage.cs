using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingPage : PageBase
{
    Inventory inventory;

    [Header("Items")]
    [SerializeField] private CraftingItemCard cardPrefab;
    [SerializeField] private Transform cardParent;
    private List<CraftingItemCard> itemCards;
    List<Item> items;

    [Header("Craft Panel")]
    [SerializeField] private CraftingItemCard previewSlot;
    [SerializeField] private CraftingItemCard[] craftingSlot = new CraftingItemCard[3];
    [SerializeField] private Transform slotParent;

    public override void SetValues()
    {
        items = GameData.GetItems();
        foreach (var item in items)
        {
            var newCard = Instantiate(cardPrefab, cardParent);
            newCard.SetValue(item);
            itemCards.Add(newCard);
        }
    }

    public override void SetValuesOnSceneLoad()
    {
        inventory = Inventory.Instance;
    }

    public bool HasRequeredItem(Item item)
    {
        //if (item.isCrafted)
        //    return false;

        //foreach (var requerdItem in item.requerdItems)
        //{
        //    var itemInInventory = items.Find(x=>x.Id == requerdItem.Id);
        //    if (itemInInventory.isDefaultItem) continue;
        //    if (itemInInventory.isCrafted) continue;
        //    if (itemInInventory.count > 0) continue;
        //    return false;
        //}
        return true;
    }

    public void SetCraftData(Item item)
    {
        if (item == null) return;

        // show preview
        previewSlot.SetValue(item, false);

        //if (item.isCrafted)
        //{
        //    slotParent.gameObject.SetActive(false);
        //}
        //else
        //{
        //    // show required item
        //    slotParent.gameObject.SetActive(true);
        //    SetSlotData(item);

        //    // show craft button
        //    Craft(item);
        //}
    }

    private void SetSlotData(Item item)
    {
        int count = 3;

        //if (item.requerdItems.Count < count)
        //{
        //    count = item.requerdItems.Count;
        //}

        //for (int i = 0; i < count; i++)
        //{
        //    craftingSlot[i].SetValue(item.requerdItems[i], false);
        //}
    }

    private void Craft(Item item)
    {
        // add to inventory
        inventory.Add(item);

        // remove requerd items from inventory
        //for (int i = 0; i < item.requerdItems.Count; i++)
        //{
        //    inventory.Remove(item);
        //}
    }

}

[RequireComponent(typeof(Image))]
public class CraftingSlot : MonoBehaviour
{

}
