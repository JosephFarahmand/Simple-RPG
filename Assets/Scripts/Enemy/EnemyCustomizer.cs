using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCustomizer : MonoBehaviour
{
    private List<ModelData> models;
    [SerializeField] private SkinnedMeshRenderer headSkinRenderer;
    [SerializeField] private List<Material> materials;

    public System.Action<Equipment> onEquip;

    private void Awake()
    {
        models = new List<ModelData>(GetComponentsInChildren<ModelData>(true));
        foreach (var model in models)
        {
            if (model.Id.Length == 0)
            {
                Debug.LogWarning("This object has no id", model);
                continue;
            }
            model.SetActive(false);
        }
    }

    private void Start()
    {
        var material = materials.RandomItem();
        var data = GameManager.GameData.GetCharacterEquipment();
        foreach (var equipment in data.Equipment)
        {
            var itemHandler = models.Find(x => x.Equals(equipment));
            itemHandler.SetActive(true);
            itemHandler.SetMaterial(material);

            onEquip?.Invoke(equipment);
        }

        headSkinRenderer.sharedMaterial = material;
    }
}
