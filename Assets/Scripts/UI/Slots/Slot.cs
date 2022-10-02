using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Slot : MonoBehaviour
{
    [SerializeField] protected Image itemIcon;
    [SerializeField] protected Image itemBackground;
    [SerializeField] protected Image itemFrame;
    [SerializeField] protected Toggle slotToggle;

    [Header("Customize")]
    [SerializeField] private Sprite selectedFrame;
    [SerializeField] private Sprite defaultFrame;
    [SerializeField] private Color defaultBackgroundColor;

    public event System.Action<bool, Item> OnSlotSelectedHandler;

    protected virtual void Start()
    {
        slotToggle.group = GetComponentInParent<ToggleGroup>();
    }

    public virtual void AddItem(Item item)
    {
        SetActive(true);

        ApplySlotSkin(item);

        SetToggleAction(item);
    }

    private void SetToggleAction(Item item)
    {
        slotToggle.onValueChanged.RemoveAllListeners();
        slotToggle.onValueChanged.AddListener(((value) =>
        {
            OnSlotSelectedHandler?.Invoke(value, item);

            ApplySelectedFrame(value);
        }));
    }

    public virtual void ClearSlot()
    {
        ApplySlotSkin();

        SetActive(false);
    }

    public void SetActive(bool value)
    {
        itemIcon.enabled = value;
    }

    protected void ApplySlotSkin(Item item = null)
    {
        if (item == null)
        {
            itemIcon.sprite = null;

            itemBackground.color = defaultBackgroundColor;
            itemFrame.sprite = defaultFrame;
        }
        else
        {
            itemIcon.sprite = item.Icon;

            var details = GameData.GetCardBackground(item.Type);
            itemBackground.color = details.BackgroundColor;
            itemFrame.sprite = details.FrameSprite;
        }
    }

    protected void ApplySelectedFrame(bool state)
    {
        itemFrame.sprite = state ? selectedFrame : defaultFrame;
    }
}
