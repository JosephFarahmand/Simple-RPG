using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    Item item;

    InventoryPage inventoryPage;

    //public Image icon;
    //public Button removeButton;
    //public Button slotButton;

    [SerializeField] protected Image itemIcon;
    [SerializeField] private Image itemBackground;
    [SerializeField] private Image itemFrame;
    [SerializeField] private Toggle slotToggle;
    //[SerializeField] private Button slotButton;

    [Header("Customize")]
    [SerializeField] private Sprite selectedSprite;
    [SerializeField] private Sprite notSelectedSprite;

    private void Start()
    {
        //removeButton.onClick.AddListener(OnRemoveButton);

        //slotButton.onClick.AddListener(UseItem);

        inventoryPage = GetComponentInParent<InventoryPage>();

        notSelectedSprite = itemFrame.sprite;

        slotToggle.group = inventoryPage.ItemsToggleGroup;
        slotToggle.onValueChanged.AddListener((value) =>
        {
            if (value)
            {
                if (item == null) return;
                inventoryPage.SetActiveItem(item);
                itemFrame.sprite = selectedSprite;
            }
            else
            {
                itemFrame.sprite = notSelectedSprite;
            }
        });
    }

    public void AddItem(Item newItem)
    {
        item = newItem;

        SetActive(true);

        itemIcon.sprite = newItem.icon;

        var details = GameData.GetCardBackground(item.type);
        itemBackground.color = details.BackgroundColor;
        itemFrame.sprite = details.FrameSprite;
        notSelectedSprite = details.FrameSprite;
        //itemIcon.enabled = true;

        //removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;

        itemIcon.sprite = null;
        //itemIcon.enabled = false;

        SetActive(false);

        //removeButton.interactable = false;
    }

    private void OnRemoveButton()
    {
        Inventory.Instance.Remove(item);
    }

    private void UseItem()
    {
        if (item == null) return;

        item.Use();
    }

    public void SetActive(bool value)
    {
        itemIcon.enabled = value;
    }
}
