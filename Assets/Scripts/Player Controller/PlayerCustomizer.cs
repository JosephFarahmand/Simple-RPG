using System.Collections.Generic;
using UnityEngine;

public class PlayerCustomizer : MonoBehaviour
{
    List<EquipmentHandler> equipmentHandlers;

    public void Initialization()
    {
        equipmentHandlers = new List<EquipmentHandler>(transform.root.GetComponentsInChildren<EquipmentHandler>());

        PlayerManager.Profile.onChangeProperty += ChangeProperty;
    }

    private void ChangeProperty(PlayerProfile profile)
    {
        ApplySkin(profile.SkinMaterial);
    }

    private void ApplySkin(Material material)
    {
        foreach (var handler in equipmentHandlers)
        {
            handler.SetMaterial(material);
        }
    }
}
