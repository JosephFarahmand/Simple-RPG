using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Material", menuName = "Inventory/Material")]
public class MaterialData : ScriptableObject
{
    [SerializeField] private string id;
    [SerializeField] private Material material;

    public string Id { get => id;  }
    public Material Material { get => material; }
}
