using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    Item item;

    InventoryPage inventoryPage;

    [SerializeField] protected Image itemIcon;
    [SerializeField] private Image itemBackground;
    [SerializeField] private Image itemFrame;
    [SerializeField] private Toggle slotToggle;

    [Header("Customize")]
    [SerializeField] private Sprite selectedFrame;
    [SerializeField] private Sprite defaultFrame;
    [SerializeField] private Color defaultBackgroundColor;

    public event System.Action<bool,Item> OnToggleChange;

    private void Start()
    {
        inventoryPage = GetComponentInParent<InventoryPage>();

        slotToggle.group = GetComponentInParent<ToggleGroup>();
        slotToggle.onValueChanged.AddListener((value) =>
        {
            OnToggleChange?.Invoke(value, item);
            if (value)
            {
                if (item == null) return;
                inventoryPage.SetActiveItem(item);
                itemFrame.sprite = selectedFrame;
            }
            else
            {
                inventoryPage.SetActiveItem(null);
                itemFrame.sprite = defaultFrame;
            }
        });
    }



    public void AddItem(Item newItem)
    {
        item = newItem;

        SetActive(true);

        itemIcon.sprite = newItem.Icon;

        var details = GameData.GetCardBackground(item.Type);
        itemBackground.color = details.BackgroundColor;
        itemFrame.sprite = details.FrameSprite;
        //itemIcon.enabled = true;

        //removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;

        itemIcon.sprite = null;
        itemBackground.color = defaultBackgroundColor;
        itemFrame.sprite = defaultFrame;
        //itemIcon.enabled = false;

        SetActive(false);

        //removeButton.interactable = false;
    }

    //private void OnRemoveButton()
    //{
    //    Inventory.Instance.Remove(item);
    //}

    //private void UseItem()
    //{
    //    if (item == null) return;

    //    item.Use();
    //}

    public void SetActive(bool value)
    {
        itemIcon.enabled = value;
    }
}
