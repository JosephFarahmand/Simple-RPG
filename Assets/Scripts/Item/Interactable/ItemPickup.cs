using UnityEngine;

public class ItemPickup : Interactable
{
    [SerializeField] Item item;

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
}
