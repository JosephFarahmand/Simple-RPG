using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class ItemSlot : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private Image itemBackground;
    [SerializeField] private Image itemFrame;
    [SerializeField] private Button slotButton;

    public Item slotItem { get; protected set; }

    public virtual void SetValue(Item item, bool enableButton = true)
    {
        slotItem = item;

        itemIcon.sprite = item.icon;

        var details = GameData.GetCardBackground(item.type);
        itemBackground.color = details.BackgroundColor;
        itemFrame.sprite = details.FrameSprite;

        slotButton.enabled = enableButton;
        if (enableButton)
        {
            slotButton.onClick.RemoveAllListeners();
            slotButton.onClick.AddListener(() =>
            {
                OnButtonClicked();
            });
        }
    }

    protected abstract void OnButtonClicked();
}