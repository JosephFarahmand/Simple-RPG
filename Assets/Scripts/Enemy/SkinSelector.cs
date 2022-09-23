using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSelector : MonoBehaviour
{
    private List<EquipmentHandler> equipmentHandlers;
    [SerializeField] private SkinnedMeshRenderer headSkinRenderer;
    [SerializeField] private List<Material> materials;

    private void Awake()
    {
        equipmentHandlers = new List<EquipmentHandler>();
        var handlers = GetComponentsInChildren<EquipmentHandler>(true);
        foreach (var handler in handlers)
        {
            if (handler.Item == null) continue;
            equipmentHandlers.Add(handler);
            handler.SetActive(false);
        }
    }

    private void OnEnable()
    {
        var material = materials.RandomItem();
        var data = GameData.GetCharacterEquipment();
        foreach(var itemID in data.ItemsId)
        {
            var item = equipmentHandlers.Find(item => item.ID == itemID);
            item.SetActive(true);
            item.SetMaterial(material);
        }

        headSkinRenderer.sharedMaterial = material;
    }
}
