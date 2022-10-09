using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPreview : MonoBehaviour
{
    private List<EquipmentHandler> equipmentHandlers;
    InventoryPage inventoryPage;
    [SerializeField] private Transform character;

    public void Initialization()
    {
        equipmentHandlers = new List<EquipmentHandler>();
        var handlers = transform.root.GetComponentsInChildren<EquipmentHandler>(true);
        foreach (var handler in handlers)
        {
            if (handler.Item == null) continue;
            if (handler.IsStaticItem) continue;
            equipmentHandlers.Add(handler);
            handler.gameObject.SetActive(false);
        }

        PlayerManager.EquipController.onEquipmentChanged += onChangePreview;
        AccountController.onChangeProperty += (profile) =>
        {
            var material = GameManager.GameData.GetSkinMaterial(profile.SkinId);
            foreach (var handler in handlers)
            {
                handler.SetMaterial(material);
            }
        };

        inventoryPage = UI_Manager.instance.GetPageOfType<InventoryPage>();
    }

    private void onChangePreview(Equipment newItem, Equipment oldItem)
    {
        var handler = equipmentHandlers.Find(x => x.Item == newItem);
        if (handler != null)
            handler.gameObject.SetActive(true);

        handler = equipmentHandlers.Find(x => x.Item == oldItem);
        if (handler != null)
            handler.gameObject.SetActive(false);
    }

    public float speed = 5.0f;

    private void Update()
    {
        if (!GameManager.IsRun) return;
        if (inventoryPage.gameObject.activeSelf)
        {
            character.Rotate(0.0f, -Input.GetAxis("Horizontal") * speed, 0.0f);
        }
    }
}
