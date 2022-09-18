using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinController : MonoBehaviour
{
    [SerializeField] private Material skinMaterial;

    public void SetMaterial(MeshRenderer meshRenderer)
    {
        meshRenderer.sharedMaterial = skinMaterial;
    }

    public void SetMaterial(SkinnedMeshRenderer skinnedMeshRenderer)
    {
        skinnedMeshRenderer.sharedMaterial = skinMaterial;
    }
}
