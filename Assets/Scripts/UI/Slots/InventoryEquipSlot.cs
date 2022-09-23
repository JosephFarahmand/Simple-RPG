using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryEquipSlot : MonoBehaviour, IPointerClickHandler
{


    [SerializeField] private EquipmentSlot slot;
    [SerializeField] private Image itemIcon;
    [SerializeField] private Sprite defaultSprite;

    public EquipmentSlot Slot { get => slot; set => slot = value; }

    public void SetIcon(Sprite itemSprite)
    {
        itemIcon.sprite = itemSprite == null ? defaultSprite : itemSprite;
    }

    public void AddItem(Equipment newItem)
    {
        if(newItem == null)
        {
            itemIcon.sprite = defaultSprite;
            return;
        }
        itemIcon.sprite = newItem.Icon;
    }

    private void OnValidate()
    {
        name = $"Inventory Equip Slot - {slot}";
    }

    int tap;

    public void OnPointerClick(PointerEventData eventData)
    {
        tap = eventData.clickCount;

        if (tap == 2)
        {
            // uneqip
            PlayerManager.EquipController.Unequip(slot);
            itemIcon.sprite = defaultSprite;
            PlayerManager.EquipController.EquipDefaultItem(slot);
        }

    }
}
