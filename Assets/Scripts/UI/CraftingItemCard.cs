using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CraftingItemCard : ItemSlot
{
    [SerializeField] private GameObject nofifyObject;

    CraftingPage craftingPage;

    private void Start()
    {
        craftingPage = UI_Manager.instance.GetPageOfType<CraftingPage>();
    }

    public override void SetValue(Item item,bool enableButton = true)
    {
        base.SetValue(item,enableButton);

        if (craftingPage.HasRequeredItem(item))
        {
            nofifyObject.SetActive(true);
        }
        else
        {
            nofifyObject.SetActive(false);
        }
    }

    protected override void OnButtonClicked()
    {
        craftingPage.SetCraftData(slotItem);
    }
}
