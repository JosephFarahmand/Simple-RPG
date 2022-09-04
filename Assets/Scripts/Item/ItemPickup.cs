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
        var wasPickedUp = Inventory.Instance.Add(item);

        if (wasPickedUp)
            Destroy(gameObject);
    }
}
