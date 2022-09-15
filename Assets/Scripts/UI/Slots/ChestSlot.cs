using UnityEngine;
using UnityEngine.UI;

public class ChestSlot : MonoBehaviour
{
    (Item, ChestPage.PanelType) item;

    ChestPage chestPage;

    [SerializeField] protected Image itemIcon;
    [SerializeField] private Image itemBackground;
    [SerializeField] private Image itemFrame;
    [SerializeField] private Toggle slotToggle;

    [Header("Customize")]
    [SerializeField] private Sprite selectedFrame;
    [SerializeField] private Sprite defaultFrame;
    [SerializeField] private Color defaultBackgroundColor;

    private void Start()
    {
        chestPage = GetComponentInParent<ChestPage>();

        slotToggle.group = GetComponentInParent<ToggleGroup>();
        slotToggle.onValueChanged.AddListener((value) =>
        {
            if (value)
            {
                if (item.Item1 == null) return;
                chestPage.SetSelectedItem(item.Item1, item.Item2);
                itemFrame.sprite = selectedFrame;
            }
            else
            {
                itemFrame.sprite = defaultFrame;
            }
        });
    }

    public void AddItem(Item newItem, ChestPage.PanelType type)
    {
        item = (newItem, type);

        SetActive(true);

        itemIcon.sprite = newItem.Icon;

        var details = GameData.GetCardBackground(item.Item1.Type);
        itemBackground.color = details.BackgroundColor;
        itemFrame.sprite = details.FrameSprite;
    }

    public void ClearSlot()
    {
        item = (null, ChestPage.PanelType.Nothing);

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
