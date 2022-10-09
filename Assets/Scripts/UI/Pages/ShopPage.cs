using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopPage : PageBase
{
    InventoryController inventory;    

    [Header("Slot")]
    [SerializeField] private ShopSlot shopSlotPrefab;
    [SerializeField] private Transform itemsParent;
    List<Slot> slots;

    [Header("Tab")]
    [SerializeField] private TabGroup tabGroup;
    private Dictionary<int, List<Slot>> itemsCollection;

    [Header("Info Panel")]
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text countText;

    [Header("Icon")]
    [SerializeField] protected Image itemIcon;
    [SerializeField] protected Image itemBackground;
    [SerializeField] protected Image itemFrame;

    [Header("Stats")]
    [SerializeField] private EquipmentStatsDisplay statsDisplayPrefab;
    [SerializeField] private Transform statsDisplayParent;


    [Header("Buying")]
    [SerializeField] private WarningMassege warningMessenger;
    [SerializeField] private Button buyButton;
    [SerializeField] private TMP_Text buyText;
    [SerializeField] private Button sellButton;
    [SerializeField] private TMP_Text sellText;

    public override void SetValues()
    {

    }

    public override void SetValuesOnSceneLoad()
    {
        inventory = PlayerManager.InventoryController;

        itemsCollection = new Dictionary<int, List<Slot>>();
        itemsCollection.Add(0, new List<Slot>());

        slots = new List<Slot>();
        var items = GameManager.GameData.GetEquipmentItems();

        var SortedList = items.OrderBy(o => o.Rarity).ToList();

        foreach (var item in SortedList)
        {
            if (item.Rarity == ItemRarity.Free) continue;
            var slot = Instantiate(shopSlotPrefab,itemsParent);
            slot.AddItem(item);

            slot.OnSlotSelectedHandler += Slot_OnSlotSelectedHandler;

            slots.Add(slot);

            itemsCollection[0].Add(slot);
            if (item is Equipment equipment)
            {
                var key = ((int)equipment.equipSlot) + 1;

                if (itemsCollection.ContainsKey(key))
                {
                    itemsCollection[key].Add(slot);
                }
                else
                {
                    itemsCollection.Add(key, new List<Slot>() { slot });
                }
            }
        }

        var keys = itemsCollection.Keys.ToList();
        keys.Sort();
        tabGroup.Initialization(new Queue<int>(keys));
        tabGroup.onTabsAction += onTabChanged;
    }

    private void onTabChanged(int index)
    {
        foreach (var slot in slots)
        {
            if (itemsCollection[index].Contains(slot))
            {
                slot.gameObject.SetActive(true);
            }
            else
            {
                slot.gameObject.SetActive(false);
            }
        }

        Slot_OnSlotSelectedHandler(true, itemsCollection[index].First().Item);
    }

    private void Slot_OnSlotSelectedHandler(bool isSelected, Item item)
    {
        if (isSelected)
        {
            if (item == null) return;

            SetInfoPanelData(item);

            countText.SetText(PlayerManager.InventoryController.GetItemCount(item.Id).ToString()) ;



            buyButton.interactable = false;
            sellButton.gameObject.SetActive(false);

            if(AccountController.Data.Level < item.RequiredLevel)
            {
                warningMessenger.gameObject.SetActive(true);
                warningMessenger.SetWarning(GameManager.ShopController.NotRequiredLevel);
            }
            else
            {
                warningMessenger.gameObject.SetActive(false);
                sellText.SetText(item.Price.ToString());
                buyText.SetText(item.Price.ToString());

                buyButton.interactable = true;
                buyButton.onClick.RemoveAllListeners();
                buyButton.onClick.AddListener(() =>
                {
                    GameManager.ShopController.Buying(item);
                    Slot_OnSlotSelectedHandler(true, item);
                });

                if (inventory.HasItem(item))
                {
                    sellButton.gameObject.SetActive(true);
                    sellButton.onClick.RemoveAllListeners();
                    sellButton.onClick.AddListener(() =>
                    {
                        GameManager.ShopController.Selling(item);
                        Slot_OnSlotSelectedHandler(true, item);
                    });
                }
            }
        }
    }

    private void SetInfoPanelData(Item item)
    {
        nameText.SetText(item.Name);

        #region Icon

        itemIcon.sprite = item.Icon;
        var details = GameManager.GameData.GetCardBackground(item.Rarity);
        itemBackground.color = details.BackgroundColor;
        itemFrame.sprite = details.FrameSprite;

        #endregion

        #region Stats

        statsDisplayParent.DestroyChildren();
        if (item is Equipment equipment)
        {
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
        }

        #endregion
    }
}
