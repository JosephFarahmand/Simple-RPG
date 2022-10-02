using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopPage : PageBase
{
    InventoryController inventory;

    [Header("Button")]
    [SerializeField] private Button buyButton;

    [Header("Slot")]
    [SerializeField] private ShopSlot shopSlotPrefab;
    [SerializeField] private Transform itemsParent;
    List<Slot> slots;

    [Header("Info Panel")]
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text priceText;

    [Header("Icon")]
    [SerializeField] protected Image itemIcon;
    [SerializeField] protected Image itemBackground;
    [SerializeField] protected Image itemFrame;

    [Header("Stats")]
    [SerializeField] private EquipmentStatsDisplay statsDisplayPrefab;
    [SerializeField] private Transform statsDisplayParent;

    public override void SetValues()
    {

    }

    public override void SetValuesOnSceneLoad()
    {
        inventory = PlayerManager.InventoryController;

        slots = new List<Slot>();
        var items = GameData.GetEquipmentItems();
        foreach(var item in items)
        {
            var slot = Instantiate(shopSlotPrefab,itemsParent);
            slot.AddItem(item);

            slot.OnSlotSelectedHandler += Slot_OnSlotSelectedHandler;

            slots.Add(slot);
        }
    }

    private void Slot_OnSlotSelectedHandler(bool isSelected, Item item)
    {
        if (isSelected)
        {
            if (item == null) return;

            if (item is Equipment equipment)
            {
                SetInfoPanelData(equipment);
            }

            priceText.SetText(item.Price.ToString());

            buyButton.interactable = false;

            if (!inventory.HasItem(item) && item.HasConditions())
            {
                buyButton.interactable = true;
                buyButton.onClick.RemoveAllListeners();
                buyButton.onClick.AddListener(() =>
                {

                });
            }
        }
    }

    private void SetInfoPanelData(Equipment equipment)
    {
        nameText.SetText(equipment.Name);

        #region Icon
        itemIcon.sprite = equipment.Icon;
        var details = GameData.GetCardBackground(equipment.Rarity);
        itemBackground.color = details.BackgroundColor;
        itemFrame.sprite = details.FrameSprite;
        #endregion

        #region Stats
        statsDisplayParent.DestroyChildren();
        if (equipment.Modifier.AttackSpeed > 0)
        {
            var stats = Instantiate(statsDisplayPrefab, statsDisplayParent);
            stats.SetValue(equipment.Modifier.AttackSpeed, "Attack Speed");
        }

        if (equipment.Modifier.Damage > 0)
        {
            var stats = Instantiate(statsDisplayPrefab, statsDisplayParent);
            stats.SetValue(equipment.Modifier.Damage, "Damage");
        }

        if (equipment.Modifier.Armor > 0)
        {
            var stats = Instantiate(statsDisplayPrefab, statsDisplayParent);
            stats.SetValue(equipment.Modifier.Armor, "Armor");
        }
        #endregion
    }
}
