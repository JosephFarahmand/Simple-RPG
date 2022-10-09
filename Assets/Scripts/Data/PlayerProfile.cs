using System.Collections.Generic;

public class PlayerProfile
{
    public string Id { get; private set; }
    public string Username { get; private set; }
    public string Password { get; private set; }
    //public string Nickname { get; private set; }
    public int CoinAmount { get; private set; }
    //public int GemAmount { get; private set; }
    public int Level { get; private set; }

    public XP XP { get; private set; }


    public string SkinId { get; private set; }

    public List<Item> InventoryItems { get; private set; }
    public List<Item> EquipedItems { get; private set; }

    public PlayerProfile(string id, string username, int coinAmount, int level, string skinId, XP xp)
    {
        Id = id;
        //Nickname = nickname;
        Username = username;
        CoinAmount = coinAmount;
        //GemAmount = gemAmount;

        Level = level == 0 ? 1 : level;
        SkinId = skinId;

        InventoryItems = new List<Item>();
        EquipedItems = new List<Item>();

        XP = xp;
    }

    public PlayerProfile(string id, string username,string password, int coinAmount, int level, string skinId, XP xp)
    {
        Id = id;
        Username = username;
        Password = password;
        //Nickname = nickname;
        CoinAmount = coinAmount;
        //GemAmount = gemAmount;

        Level = level == 0 ? 1 : level;
        SkinId = skinId;

        InventoryItems = new List<Item>();
        EquipedItems = new List<Item>();

        XP = xp;
    }

    //public PlayerProfile(string id, string username, int coinAmount, int gemAmount, int level, string skinId)
    //{
    //    Id = id;
    //    Username = username;
    //    CoinAmount = coinAmount;
    //    //GemAmount = gemAmount;

    //    Level = level == 0 ? 1 : level;
    //    SkinId = skinId;

    //    InventoryItems = new List<Item>();
    //    EquipedItems = new List<Item>();
    //}

    public void UpdateData(string newUsername = "",
                           string newPassword = "",
                           //string newNickname = "",
                           string newSkinId = "",
                           int newCoinAmount = -1,
                           int newGemAmount = -1,
                           int newLevelValue = -1)
    {
        Username = newUsername == "" ? Username : newUsername;
        Password = newPassword == "" ? Password : newPassword;
        //Nickname = newNickname == "" ? Nickname : newNickname;

        SkinId = newSkinId == "" ? SkinId : newSkinId;

        CoinAmount = newCoinAmount == -1 ? CoinAmount : newCoinAmount;
        //GemAmount = newGemAmount == -1 ? GemAmount : newGemAmount;

        Level = newLevelValue == -1 ? Level : newLevelValue;
    }

    public void UpdateData(float newValue)
    {
        XP = new XP(newValue, XP.MaximumValue);
    }

    public void AddInventoryItem(Item item)
    {
        InventoryItems.Add(item);
        CoinAmount -= item.Price;
    }

    public void RemoveInventoryItem(Item item)
    {
        if (InventoryItems.Contains(item))
        {
            InventoryItems.Remove(item);

            CoinAmount += item.Price;
        }
    }

    public void EquipItem(Equipment newItem)
    {
        EquipedItems.Add(newItem);
    }

    public void UnequipItem(Equipment oldItem)
    {
        if (EquipedItems.Contains(oldItem))
        {
            EquipedItems.Remove(oldItem);
        }
    }



    //public class PlayerItem
    //{
    //    public List<ItemStoredData> Items { get; private set; }

    //    public void AddItem(Item item)
    //    {
    //        if (!Items.Contains(item.StoredData))
    //        {
    //            Items.Add(item.StoredData);
    //        }
    //    }

    //    public void RemoveItem(Item item)
    //    {
    //        if (Items.Contains(item.StoredData))
    //        {
    //            Items.Add(item.StoredData);
    //        }
    //    }
    //}

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

public struct XP
{
    public XP(float currentValue, float maximumValue)
    {
        CurrentValue = currentValue;
        MaximumValue = maximumValue;
    }

    public float CurrentValue { get; private set; }
    public float MaximumValue { get; private set; }
}

public struct ItemStoredData
{
    public ItemStoredData(string id, string name, bool isDefaultItem, ItemRarity rarity = default, int requiredLevel = 0, int price = 100, int count = 1)
    {
        Id = id;
        Name = name;
        IsDefaultItem = isDefaultItem;
        Rarity = rarity;
        RequiredLevel = requiredLevel;
        Price = price <= 0 ? 100 : price;
        //Count = count <= 0 ? 1 : count;
    }

    public ItemStoredData(Item item) : this(item.Id, item.name, item.IsDefaultItem, item.Rarity, item.Price, item.RequiredLevel)
    {
    }

    public string Id { get; private set; }
    public string Name { get; private set; }
    public bool IsDefaultItem { get; private set; }
    public ItemRarity Rarity { get; private set; }
    public int RequiredLevel { get; private set; }
    public int Price { get; private set; }
    //public int Count { get; set; }

    public JSONObject ToJSON()
    {
        JSONObject json = new JSONObject();

        json.AddField(nameof(Id), Id);
        json.AddField(nameof(Name), Name);
        json.AddField(nameof(IsDefaultItem), IsDefaultItem);

        return json;
    }

    public void FromJSON(JSONObject json)
    {
        Id = json[nameof(Id)].str;
        Name = json[nameof(Name)].str;
        IsDefaultItem = json[nameof(IsDefaultItem)].b;
    }
}

