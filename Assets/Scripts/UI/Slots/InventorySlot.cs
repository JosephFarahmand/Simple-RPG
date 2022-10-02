using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] protected Image itemIcon;
    [SerializeField] private Image itemBackground;
    [SerializeField] private Image itemFrame;
    [SerializeField] private Toggle slotToggle;

    [Header("Customize")]
    [SerializeField] private Sprite selectedFrame;
    [SerializeField] private Sprite defaultFrame;
    [SerializeField] private Color defaultBackgroundColor;

    public event System.Action<bool, Item> OnToggleChange;

    private void Start()
    {
        slotToggle.group = GetComponentInParent<ToggleGroup>();
    }

    public void AddItem(Item item)
    {
        SetActive(true);

        itemIcon.sprite = item.Icon;

        var details = GameData.GetCardBackground(item.Type);
        itemBackground.color = details.BackgroundColor;
        itemFrame.sprite = details.FrameSprite;

        SetToggleAction(item);
    }

    private void SetToggleAction(Item item)
    {
        slotToggle.onValueChanged.RemoveAllListeners();
        slotToggle.onValueChanged.AddListener((value) =>
        {
            OnToggleChange?.Invoke(value, item);
            if (value)
            {
                if (item == null) return;
                itemFrame.sprite = selectedFrame;
            }
            else
            {
                itemFrame.sprite = defaultFrame;
            }
        });
    }

    public void ClearSlot()
    {
        itemIcon.sprite = null;
        itemBackground.color = defaultBackgroundColor;
        itemFrame.sprite = defaultFrame;

        SetActive(false);
    }

    public void SetActive(bool value)
    {
        itemIcon.enabled = value;
    }
}
