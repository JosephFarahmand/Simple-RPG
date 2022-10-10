using UnityEngine;

public class ItemPickup : Interactable
{
    [SerializeField] private string id;
    Item item;

    public string ID
    {
        get
        {
            if (id == null || id.Length == 0)
            {
                id = GetComponentInChildren<ModelData>().Id;
            }
            return id;
        }
    }

    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    private void PickUp()
    {
        //add item to inventory
        var wasPickedUp = PlayerManager.InventoryController.Add(item);

        AccountController.IncreseXP(StaticData.collectItemXP);

        if (wasPickedUp)
            Destroy(gameObject);
    }

    public void SetItem(Item item)
    {
        this.item = item;
    }

    [ContextMenu("Set Id")]
    private void SetId()
    {
        id = GetComponentInChildren<ModelData>().Id;
    }
}