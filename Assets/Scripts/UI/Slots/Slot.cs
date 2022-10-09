using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Slot : MonoBehaviour
{
    [SerializeField] protected Image itemIcon;
    [SerializeField] protected Image itemBackground;
    [SerializeField] protected Image itemFrame;
    [SerializeField] protected Toggle slotToggle;
    [SerializeField] protected TMP_Text countText;

    [Header("Customize")]
    [SerializeField] private Sprite selectedFrame;
    [SerializeField] private Sprite defaultFrame;
    [SerializeField] private Color defaultBackgroundColor;

    public Item Item { get;private set; }

    public event System.Action<bool, Item> OnSlotSelectedHandler;

    protected virtual void Start()
    {
        slotToggle.group = GetComponentInParent<ToggleGroup>();
        countText.SetText("");
    }

    public virtual void AddItem(Item item)
    {
        SetActive(true);

        Item = item;

        ApplySlotSkin();

        SetToggleAction();
    }

    protected virtual void SetToggleAction()
    {
        slotToggle.onValueChanged.RemoveAllListeners();
        slotToggle.onValueChanged.AddListener((value) =>
        {
            OnSlotSelectedHandler?.Invoke(value, Item);

            ApplySelectedFrame(value);
        });
    }

    public virtual void ClearSlot()
    {
        Item = null;

        ApplySlotSkin();

        SetActive(false);
    }

    public void SetActive(bool value)
    {
        itemIcon.enabled = value;
    }

    protected void ApplySlotSkin()
    {
        if (Item == null)
        {
            itemIcon.sprite = null;

            

            itemBackground.color = defaultBackgroundColor;
            itemFrame.sprite = defaultFrame;
        }
        else
        {
            itemIcon.sprite = Item.Icon;

            //countText.SetText(Item.Count.ToString());

            var details = GameManager.GameData.GetCardBackground(Item.Rarity);
            itemBackground.color = details.BackgroundColor;
            itemFrame.sprite = details.FrameSprite;
        }
    }

    protected void ApplySelectedFrame(bool state)
    {
        itemFrame.sprite = state ? selectedFrame : defaultFrame;
    }

    private void OnEnable()
    {
        SetActive(true);
    }

    private void OnDisable()
    {
        SetActive(false);
    }
}
