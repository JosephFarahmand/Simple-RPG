using UnityEngine;
using UnityEngine.UI;

public class ChestSlot : Slot
{
    //(Item, ChestPage.PanelType) item;

    ChestPage chestPage;


    private void Start()
    {
        chestPage = GetComponentInParent<ChestPage>();

        slotToggle.group = GetComponentInParent<ToggleGroup>();
        //slotToggle.onValueChanged.AddListener((value) =>
        //{
        //    if (value)
        //    {
        //        if (item.Item1 == null) return;
        //        chestPage.SetSelectedItem(item.Item1, item.Item2);
        //    }
        //    else
        //    {
        //    }
        //});
    }

    public override void AddItem(Item newItem)
    {
        base.AddItem(newItem);

        //slotToggle.onValueChanged.AddListener((value) =>
        //{
        //    if (value)
        //    {
        //        if (newItem == null) return;
        //        chestPage.SetSelectedItem(newItem);
        //    }
        //});
    }

    //protected override void SetToggleAction(Item item)
    //{
    //    slotToggle.onValueChanged.RemoveAllListeners();
    //    slotToggle.onValueChanged.AddListener(((value) =>
    //    {
    //        if (value)
    //        {
    //            if (item == null) return;
    //            chestPage.SetSelectedItem(item, type);
    //        }
    //        ApplySelectedFrame(value);
    //    }));
    //}
}
