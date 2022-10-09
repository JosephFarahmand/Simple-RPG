using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Skin", menuName = "Item/Skin")]
public class SkinData : ScriptableObject
{
    [SerializeField] private string id;
    [SerializeField] private Material material;

    public string Id => id;
    public Material Material => material;
}
