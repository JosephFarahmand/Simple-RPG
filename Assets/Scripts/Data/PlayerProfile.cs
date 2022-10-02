using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerProfile
{
    public string Id { get; private set; }
    public string Name { get; private set; }
    public int CoinAmount { get; private set; }
    public int GemAmount { get; private set; }
    public int Level { get; private set; }


    public Material SkinMaterial { get; private set; }

    //public PlayerStoredItems Inventory { get; private set; }
    //public PlayerStoredItems Equipment { get; private set; }

    public PlayerProfile()
    {
        //Inventory = new PlayerStoredItems();
        //Equipment = new PlayerStoredItems();
    }

    public PlayerProfile(string id, string name, int coinAmount, int gemAmount, int level, Material skinMaterial)
    {
        Id = id;
        Name = name;
        CoinAmount = coinAmount;
        GemAmount = gemAmount;

        Level = level == 0 ? 1 : level;
        SkinMaterial = skinMaterial;
    }

    public void UpdateData(string newName = "",int newCoinAmount = -1, int newGemAmount = -1, int newLevelValue = -1)
    {
        Name = newName == "" ? Name : newName;
        CoinAmount = newCoinAmount == -1 ? CoinAmount : newCoinAmount;
        GemAmount = newGemAmount == -1 ? GemAmount : newGemAmount;
        Level = newLevelValue == -1 ? Level : newLevelValue;
    }

    //public JSONObject ToJSON()
    //{
    //    JSONObject json = new JSONObject();

    //    json.AddField(nameof(Id), Id);
    //    json.AddField(nameof(Name), Name);
    //    json.AddField(nameof(Inventory), Inventory.ToJSON());
    //    json.AddField(nameof(Equipment), Equipment.ToJSON());

    //    return json;
    //}

    //public void FromJSON(JSONObject json)
    //{
    //    Id = json[nameof(Id)].str;
    //    Name = json[nameof(Name)].str;
    //    Inventory = new PlayerStoredItems(json[nameof(Inventory)]);
    //    Equipment = new PlayerStoredItems(json[nameof(Equipment)]);
    //}

    ////public class PlayerInventory
    ////{
    ////    public List<ItemStoredData> Items { get; private set; }

    ////    public PlayerInventory()
    ////    {
    ////        Items = new List<ItemStoredData>();
    ////        Items.Add(new ItemStoredData("", "", false, false, new List<string>() { "" }));
    ////        Items.Add(new ItemStoredData("", "", false, false, new List<string>() { "" }));
    ////        Items.Add(new ItemStoredData("", "", false, false, new List<string>() { "" }));
    ////    }

    ////    public JSONObject ToJSON()
    ////    {
    ////        JSONObject json = new JSONObject(JSONObject.Type.ARRAY);

    ////        foreach (var item in Items)
    ////        {
    ////            json.Add(item.ToJSON());
    ////        }

    ////        return json;
    ////    }

    ////    public void FromJSON(JSONObject json)
    ////    {
    ////        for (int i = 0; i < json.Count; i++)
    ////        {
    ////            JSONObject item = json[i];
    ////            ItemStoredData itemStoredData = new ItemStoredData(item);
    ////            Items.Add(itemStoredData);
    ////        }
    ////    }

    ////    public void UpdateItem(ItemStoredData data)
    ////    {
    ////        RemoveItem(data);
    ////        AddItem(data);
    ////    }

    ////    public void AddItem(ItemStoredData data)
    ////    {
    ////        RemoveItem(data);
    ////        Items.Add(data);
    ////    }

    ////    public bool RemoveItem(ItemStoredData data)
    ////    {
    ////        if (Items.Contains(data.Id))
    ////        {
    ////            Items.Remove(data.Id);
    ////            return true;
    ////        }
    ////        else
    ////        {
    ////            Debug.LogWarning($"This worker is not in the list!! ({data.Id})");
    ////            return false;
    ////        }
    ////    }
    ////}

    //public class PlayerStoredItems
    //{
    //    public List<ItemStoredData> Items { get; private set; }

    //    public PlayerStoredItems()
    //    {
    //        Items = new List<ItemStoredData>();
    //    }

    //    public PlayerStoredItems(JSONObject json)
    //    {
    //        FromJSON(json);
    //    }

    //    public PlayerStoredItems(List<ItemStoredData> items)
    //    {
    //        Items = items;
    //    }

    //    #region JSON

    //    public JSONObject ToJSON()
    //    {
    //        JSONObject json = new JSONObject(JSONObject.Type.ARRAY);

    //        foreach (var item in Items)
    //        {
    //            json.Add(item.ToJSON());
    //        }

    //        return json;
    //    }

    //    public void FromJSON(JSONObject json)
    //    {
    //        for (int i = 0; i < json.Count; i++)
    //        {
    //            JSONObject item = json[i];
    //            ItemStoredData itemStoredData = new ItemStoredData(item);
    //            Items.Add(itemStoredData);
    //        }
    //    }

    //    #endregion

    //    public void AddItem(ItemStoredData data)
    //    {
    //        RemoveItem(data);
    //        Items.Add(data);
    //    }

    //    public void RemoveItem(ItemStoredData data)
    //    {
    //        if (Items.Contains(data.Id))
    //        {
    //            Items.Remove(data.Id);
    //        }
    //        else
    //        {
    //            Debug.LogWarning($"This worker is not in the list!! ({data.Id})");
    //        }
    //    }

    //    public void UpdateItem(ItemStoredData data)
    //    {
    //        RemoveItem(data);
    //        AddItem(data);
    //    }

    //    public ItemStoredData GetItem(string id)
    //    {
    //        return Items.Find(x => x.Id == id);
    //    }

    //    public List<ItemStoredData> GetAllItems()
    //    {
    //        return Items;
    //    }
    //}
}


//public class ItemStoredData
//{
//    public ItemStoredData(string id, string name, bool isDefaultItem, bool isCrafted, List<string> requiredItem)
//    {
//        Id = id;
//        Name = name;
//        IsDefaultItem = isDefaultItem;
//        IsCrafted = isCrafted;
//        RequiredItem = requiredItem;
//    }

//    public ItemStoredData(Equipment item) : this(item.Id, item.Name, item.IsDefaultItem, item.isCrafted, item.requerdItems.Select(x => x.Id).ToList())
//    {

//    }

//    public ItemStoredData(JSONObject json)
//    {
//        FromJSON(json);
//    }

//    public string Id { get; private set; }
//    public string Name { get; private set; }
//    public bool IsDefaultItem { get; private set; }
//    public bool IsCrafted { get; private set; }
//    public List<string> RequiredItem { get; private set; }

//    public JSONObject ToJSON()
//    {
//        JSONObject json = new JSONObject();

//        json.AddField(nameof(Id), Id);
//        json.AddField(nameof(Name), Name);
//        json.AddField(nameof(IsDefaultItem), IsDefaultItem);
//        json.AddField(nameof(IsCrafted), IsCrafted);

//        JSONObject RequiredItemJson = new JSONObject(JSONObject.Type.ARRAY);
//        foreach (string item in RequiredItem)
//        {
//            RequiredItemJson.Add(item);
//        }
//        json.AddField(nameof(RequiredItem), RequiredItemJson);

//        return json;
//    }

//    public void FromJSON(JSONObject json)
//    {
//        Id = json[nameof(Id)].str;
//        Name = json[nameof(Name)].str;
//        IsDefaultItem = json[nameof(IsDefaultItem)].b;
//        IsCrafted = json[nameof(IsCrafted)].b;

//        RequiredItem = new List<string>();
//        for (int i = 0; i < json[nameof(RequiredItem)].Count; i++)
//        {
//            var itemID = json[nameof(RequiredItem)][i].str;
//            RequiredItem.Add(itemID);
//        }
//    }
//}

//public static class StoredDataExtention
//{
//    public static bool Contains(this List<ItemStoredData> datas, string dataId)
//    {
//        return datas.Find(obj => obj.Id == dataId) != null;
//    }

//    public static void Remove(this List<ItemStoredData> datas, string dataId)
//    {
//        var index = datas.FindIndex(x => x.Id == dataId);
//        datas.RemoveAt(index);
//    }
//}

