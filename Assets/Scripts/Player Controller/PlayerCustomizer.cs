using System.Collections.Generic;
using UnityEngine;

public class PlayerCustomizer : MonoBehaviour
{
    [SerializeField] private Material skinMaterial;

    List<MeshRenderer> renderers;
    List<SkinnedMeshRenderer> skinnedMeshRenderers;

    private void Start()
    {
        renderers = new List<MeshRenderer>(transform.root.GetComponentsInChildren<MeshRenderer>());
        skinnedMeshRenderers = new List<SkinnedMeshRenderer>(transform.root.GetComponentsInChildren<SkinnedMeshRenderer>());

        ApplySkin();
    }

    public void ChangeSkin(Material material)
    {
        skinMaterial = material;
        ApplySkin();
    }

    private void ApplySkin()
    {
        foreach (var renderer in renderers)
        {
            renderer.sharedMaterial = skinMaterial;
        }
        foreach (var skinnedMeshRenderer in skinnedMeshRenderers)
        {
            skinnedMeshRenderer.sharedMaterial = skinMaterial;
        }
    }
}
