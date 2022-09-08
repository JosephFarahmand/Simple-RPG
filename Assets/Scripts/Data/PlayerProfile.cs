using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProfile
{

    public string Id { get;private set; }
    public string Name { get; private set; }
    public PlayerInventory Inventory { get; private set; }

    public PlayerProfile()
    {
        Inventory = new PlayerInventory();
    }

    public JSONObject ToJSON()
    {
        JSONObject json = new JSONObject();

        json.AddField(nameof(Id), Id);
        json.AddField(nameof(Name), Name);
        json.AddField(nameof(Inventory), Inventory.ToJSON());

        return json;
    }

    public void FromJSON(JSONObject json)
    {
        Id = json[nameof(Id)].str;
        Name = json[nameof(Name)].str;
        Inventory = new PlayerInventory();
        Inventory.FromJSON(json);
    }

    public class PlayerInventory
    {
        public List<ItemStoredData> Items { get; private set; }


        public PlayerInventory()
        {
            Items = new List<ItemStoredData>();
            Items.Add(new ItemStoredData("", "", false, false, new List<string>() { "" }));
            Items.Add(new ItemStoredData("", "", false, false, new List<string>() { "" }));
            Items.Add(new ItemStoredData("", "", false, false, new List<string>() { "" }));
        }

        public JSONObject ToJSON()
        {
            JSONObject json = new JSONObject(JSONObject.Type.ARRAY);

            foreach(var item in Items)
            {
                json.Add(item.ToJSON());
            }

            return json;
        }

        public void FromJSON(JSONObject json)
        {
            for (int i = 0; i < json.Count; i++)
            {
                JSONObject item = json[i];
                ItemStoredData itemStoredData = new ItemStoredData();
                itemStoredData.FromJSON(item);
                Items.Add(itemStoredData);
            }
        }
    }
}


public struct ItemStoredData
{
    public ItemStoredData(string id, string name, bool isDefaultItem, bool isCrafted, List<string> requiredItem)
    {
        Id = id;
        Name = name;
        IsDefaultItem = isDefaultItem;
        IsCrafted = isCrafted;
        RequiredItem = requiredItem;
    }

    public string Id { get;private set;}
    public string Name { get; private set; }
    public bool IsDefaultItem { get; private set; }
    public bool IsCrafted { get; private set; }
    public List<string> RequiredItem { get; private set; }

    public JSONObject ToJSON()
    {
        JSONObject json = new JSONObject();

        json.AddField(nameof(Id), Id);
        json.AddField(nameof(Name), Name);
        json.AddField(nameof(IsDefaultItem), IsDefaultItem);
        json.AddField(nameof(IsCrafted), IsCrafted);

        JSONObject RequiredItemJson = new JSONObject(JSONObject.Type.ARRAY);
        foreach (string item in RequiredItem)
        {
            RequiredItemJson.Add(item);
        }
        json.AddField(nameof(RequiredItem), RequiredItemJson);

        return json;
    }

    public void FromJSON(JSONObject json)
    {
        Id = json[nameof(Id)].str;
        Name = json[nameof(Name)].str;
        IsDefaultItem = json[nameof (IsDefaultItem)].b;
        IsCrafted = json[nameof (IsCrafted)].b;

        RequiredItem = new List<string>();
        for (int i = 0; i < json[nameof(RequiredItem)].Count; i++)
        {
            var itemID = json[nameof(RequiredItem)][i].str;
            RequiredItem.Add(itemID);
        }
    }
}

