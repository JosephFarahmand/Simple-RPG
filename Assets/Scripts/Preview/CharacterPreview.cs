using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPreview : MonoBehaviour
{
    private List<ModelData> models;
    InventoryPage inventoryPage;
    [SerializeField] private Transform character;

    public void Initialization()
    {
        this.models = new List<ModelData>();
        var models = transform.root.GetComponentsInChildren<ModelData>(true);
        foreach (var model in models)
        {
            if (model.Id.Length == 0) continue;
            if (model.IsStaticItem) continue;
            this.models.Add(model);
            model.gameObject.SetActive(false);
        }

        PlayerManager.EquipController.onEquipmentChanged += onChangePreview;
        AccountController.onChangeProperty += (profile) =>
        {
            var material = GameManager.GameData.GetSkinMaterial(profile.SkinId);
            foreach (var handler in models)
            {
                handler.SetMaterial(material);
            }
        };

        inventoryPage = UI_Manager.instance.GetPageOfType<InventoryPage>();
    }

    private void onChangePreview(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            var model = models.Find(x => x.Equals(newItem));
            if (model != null)
                model.gameObject.SetActive(true);
        }

        if (oldItem != null)
        {
            var model = models.Find(x => x.Equals(oldItem));
            if (model != null)
                model.gameObject.SetActive(false);
        }
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
