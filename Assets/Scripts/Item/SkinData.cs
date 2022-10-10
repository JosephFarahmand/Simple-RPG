using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Skin", menuName = "Item/Skin")]
public class SkinData : Item
{
    [Header("Skin")]
    [SerializeField] private Material material;

    public SkinData(string id, string displayName, ItemRarity rarity, Sprite icon, int requiredLevel, int price, CurrencyType currencyType, string assetId, string matarialId) : base(id, displayName, rarity, icon, requiredLevel, price, currencyType,assetId)
    {
        this.material = GameManager.GameData.GetMaterial(matarialId);
    }

    public Material Material => material;
}
