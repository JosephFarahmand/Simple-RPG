using DataBank;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseCreator : MonoBehaviour
{
#if UNITY_EDITOR
    [Header("Item")]
    [SerializeField] private List<ItemPickup> items;
    [SerializeField] private List<Equipment> equipment = new List<Equipment>();
    [SerializeField] private List<Resource> resources = new List<Resource>();
    [SerializeField] private List<SkinData> skins = new List<SkinData>();
    

    [ContextMenu("Add items to database")]
    private void AddItemToDatabase()
    {
        var keys = new Dictionary<string, int>();
        var itemDb = new ItemDb();

        // Get all items from Game data
        var allItems = new List<Item>();
        //allItems.AddRange(resources);
        //allItems.AddRange(equipment);
        allItems.AddRange(skins);

        // add items to database
        foreach (var item in allItems)
        {
            ItemType type;
            string assetId;
            if (item is Equipment)
            {
                type = ItemType.Equipment;
                assetId = FindItem(item.Id);
            }
            else if (item is Resource)
            {
                type = ItemType.Resource;
                assetId = FindItem(item.Id);
            }
            else if(item is SkinData skin)
            {
                type = ItemType.Skin;
                assetId = skin.Material.GetInstanceID().ToString();
            }
            else
            {
                Debug.LogWarning("This type is not defined");
                continue;
            }

            var iconPath = ConvertPath(UnityEditor.AssetDatabase.GetAssetPath(item.Icon));


            var accept = itemDb.addData(new ItemEntity(item.Name, iconPath, type, item.Rarity, item.RequiredLevel, item.Price, 1, assetId,item.CurrencyType));
            if (!accept)
            {
                Debug.LogWarning("The item has not been added to the database");
                return;
            }

            var entity = itemDb.GetItemEntity(assetId);
            if (entity == null)
            {
                continue;
            }
            var itemEntity = (ItemEntity)entity;
            keys.Add(item.Id, itemEntity.Id);
        }

        itemDb.close();

        //var equipmentItemDb = new EquipmentItemDb();
        //foreach (var item in equipment)
        //{
        //    var modifier = StaticData.GetItemModifier(item);
        //    equipmentItemDb.addData(new EquipmentItemEntity(keys[item.Id], item.equipSlot, modifier.Damage, modifier.Armor, modifier.AttackSpeed));
        //}

        //equipmentItemDb.close();

        //var resourceItemDb = new ResourceItemDb();
        //foreach (var item in resources)
        //{
        //    resourceItemDb.addData(new ResourceItemEntity(keys[item.Id], item.Type, item.Value));
        //}

        //resourceItemDb.close();


        var skinItemDb = new SkinItemDb();
        foreach (var item in skins)
        {
            skinItemDb.addData(new SkinItemEntity(keys[item.Id], item.Material.GetInstanceID()));
        }

        skinItemDb.close();
    }

    private string ConvertPath(string path)
    {
        var newPath = path.Replace("Assets/Resources/", "").Replace(System.IO.Path.GetExtension(path), "");
        return newPath;
    }

    private string FindItem(string id)
    {
        //foreach(var item in items)
        //{
        //    if(item.itemID == id)
        //    {
        //        return item.ID;
        //    }
        //}
        return string.Empty;
    }

#endif
}