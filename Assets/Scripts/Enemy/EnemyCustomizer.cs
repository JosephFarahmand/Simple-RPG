using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCustomizer : MonoBehaviour
{
    private List<EquipmentHandler> equipmentHandlers;
    [SerializeField] private SkinnedMeshRenderer headSkinRenderer;
    [SerializeField] private List<Material> materials;

    public System.Action<Equipment> onEquip;

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

    private void Start()
    {
        var material = materials.RandomItem();
        var data = GameData.GetCharacterEquipment();
        foreach (var itemID in data.ItemsId)
        {
            var itemHandler = equipmentHandlers.Find(item => item.ID == itemID);
            itemHandler.SetActive(true);
            itemHandler.SetMaterial(material);

            onEquip?.Invoke(itemHandler.Item);
        }

        headSkinRenderer.sharedMaterial = material;
    }
}
