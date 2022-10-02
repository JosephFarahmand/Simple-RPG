using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickAccessMenu : UIElementBase
{
    [SerializeField] private Button inventoryButton;
    [SerializeField] private Button shopButton;
    //[SerializeField] private Button craftingButton;

    public override void SetValues()
    {
        inventoryButton.onClick.RemoveAllListeners();
        inventoryButton.onClick.AddListener(() => UI_Manager.instance.OpenPage(UI_Manager.instance.GetPageOfType<InventoryPage>()));

        shopButton.onClick.RemoveAllListeners();
        shopButton.onClick.AddListener(() => UI_Manager.instance.OpenPage(UI_Manager.instance.GetPageOfType<ShopPage>()));

        //craftingButton.onClick.RemoveAllListeners();
        //craftingButton.onClick.AddListener(() => UI_Manager.instance.OpenPage(UI_Manager.instance.GetPageOfType<CraftingPage>()));
    }
}
