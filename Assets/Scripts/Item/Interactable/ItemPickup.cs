public class ItemPickup : Interactable
{
    public Item item;

    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    private void PickUp()
    {
        //add item to inventory
        var wasPickedUp = PlayerManager.InventoryController.Add(item);

        if (wasPickedUp)
            Destroy(gameObject);
    }
}
