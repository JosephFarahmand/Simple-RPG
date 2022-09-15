using UnityEngine;
using UnityEngine.UI;

public class ChestSlot : MonoBehaviour
{
    Item item;

    public enum ItemType
    {
        ChestItem,
        InventoryItem
    }

    private ItemType type = ItemType.InventoryItem;

    ChestPage chestPage;

    [SerializeField] protected Image itemIcon;
    [SerializeField] private Image itemBackground;
    [SerializeField] private Image itemFrame;
    [SerializeField] private Toggle slotToggle;

    [Header("Customize")]
    [SerializeField] private Sprite selectedSprite;
    [SerializeField] private Sprite notSelectedSprite;

    private void Start()
    {
        chestPage = GetComponentInParent<ChestPage>();

        notSelectedSprite = itemFrame.sprite;

        slotToggle.group = GetComponentInParent<ToggleGroup>();
        slotToggle.onValueChanged.AddListener((value) =>
        {
            if (value)
            {
                if (item == null) return;
                if (type == ItemType.ChestItem)
                {
                    chestPage.SetActiveChestItem(item);
                }
                else
                {
                    chestPage.SetActiveInventoryItem(item);
                }
                itemFrame.sprite = selectedSprite;
            }
            else
            {
                itemFrame.sprite = notSelectedSprite;
            }
        });
    }

    public void SetType(ItemType type)
    {
        this.type = type;
    }

    public void AddItem(Item newItem)
    {
        item = newItem;

        SetActive(true);

        itemIcon.sprite = newItem.Icon;

        var details = GameData.GetCardBackground(item.Type);
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
