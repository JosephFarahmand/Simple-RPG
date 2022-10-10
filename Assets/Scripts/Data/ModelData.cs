using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelData : MonoBehaviour
{
    [SerializeField] private bool isStaticItem = false;
    [SerializeField,HideIf(nameof(isStaticItem))] private string id;
    public string Id => id;
    public bool IsStaticItem { get => isStaticItem; }

    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }

    public void SetMaterial(Material material)
    {
        if (TryGetComponent(out MeshRenderer meshRenderer))
        {
            meshRenderer.sharedMaterial = material;
        }
        else if (TryGetComponent(out SkinnedMeshRenderer skinnedMeshRenderer))
        {
            skinnedMeshRenderer.sharedMaterial = material;
        }
    }

    public bool Equals(ModelData otherModel)
    {
        return id == otherModel.id;
    }

    public bool Equals<T>(T item) where T : Item
    {
        return id == item.AssetId;
    }
}
