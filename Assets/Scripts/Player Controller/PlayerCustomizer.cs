using System.Collections.Generic;
using UnityEngine;

public class PlayerCustomizer : MonoBehaviour
{
    List<EquipmentHandler> equipmentHandlers;

    public void Initialization()
    {
        equipmentHandlers = new List<EquipmentHandler>(transform.root.GetComponentsInChildren<EquipmentHandler>());

        AccountController.onChangeProperty += ChangeProperty;
    }

    private void ChangeProperty(PlayerProfile profile)
    {
        ApplySkin(profile.SkinId);
    }

    private void ApplySkin(string skinId)
    {
        Material material = GameManager.GameData.GetSkinMaterial(skinId);
        foreach (var handler in equipmentHandlers)
        {
            handler.SetMaterial(material);
        }
    }
}
