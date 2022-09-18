using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentHandler : MonoBehaviour
{
    [SerializeField] private Equipment item;
    [SerializeField] private bool setPlayerMaterial;

    public string ID => item.Id;
    public Equipment Item => item;

    public void SetActive(bool value)
    {
        gameObject.SetActive(value);
    }

    private void Start()
    {
        if (setPlayerMaterial)
        {
            if (TryGetComponent(out MeshRenderer meshRenderer))
            {
                PlayerManager.SkinController.SetMaterial(meshRenderer);
            }
            else if (TryGetComponent(out SkinnedMeshRenderer skinnedMeshRenderer))
            {
                PlayerManager.SkinController.SetMaterial(skinnedMeshRenderer);
            }
        }
    }
}
