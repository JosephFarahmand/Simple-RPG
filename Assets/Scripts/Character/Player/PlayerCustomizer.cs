using System.Collections.Generic;
using UnityEngine;

public class PlayerCustomizer : MonoBehaviour
{
    List<ModelData> models;

    public void Initialization()
    {
        models = new List<ModelData>(transform.root.GetComponentsInChildren<ModelData>(true));

        AccountController.onChangeProperty += ChangeProperty;
    }

    private void ChangeProperty(PlayerProfile profile)
    {
        ApplySkin(profile.SkinId);
    }

    private void ApplySkin(string skinId)
    {
        Material material = GameManager.GameData.GetSkinMaterial(skinId);
        foreach (var handler in models)
        {
            handler.SetMaterial(material);
        }
    }
}
