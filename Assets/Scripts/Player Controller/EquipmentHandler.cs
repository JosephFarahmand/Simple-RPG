using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentHandler : MonoBehaviour
{
    [SerializeField] private Equipment item;

    public string ID => item.Id;
    public Equipment Item => item;

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
}
