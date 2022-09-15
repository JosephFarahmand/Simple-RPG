public class InteractableChest : Interactable
{
    public Chest chest;

    public override void Interact()
    {
        base.Interact();


        var chestPage = UI_Manager.instance.GetPageOfType<ChestPage>();
        UI_Manager.instance.OpenPage(chestPage);
        chestPage.SetChest(chest);
    }
}
