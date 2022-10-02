using System;
using System.Collections.Generic;
using UnityEngine;

public partial class ChestPage : PageBase
{
    InventoryController inventory;
    public InteractableChest Chest { get; set; }
    [SerializeField] private List<Panel> panels;

    public override void SetValues()
    {
        foreach(var panel in panels)
        {
            panel.Initilize(inventory, this);
        }
    }

    public override void SetValuesOnSceneLoad()
    {
        inventory = PlayerManager.InventoryController;

        panels = new List<Panel>(GetComponentsInChildren<Panel>());
    }

    public void SetChest(InteractableChest chest)
    {
        this.Chest = chest;
    }

    public void UpdatePanelsSlots()
    {
        foreach (var panel in panels)
        {
            panel.UpdateSlots();
        }
    }

}