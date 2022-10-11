using DataBank;
using System.Collections.Generic;
using UnityEngine;

public static class DatabaseController
{
    static ProfileDb profileDb;
    static ItemDb itemDb;
    static EquipmentItemDb equipmentItemDb;
    static ResourceItemDb resourceItemDb;
    static ItemCollectionDb inventoryDb;//Inventory
    static ItemCollectionDb equipmentDb;//Equipment

    static List<ItemEntity> itemEntities;
    static List<EquipmentItemEntity> equipmentItems;
    static List<ResourceItemEntity> resourceItems;
    static List<ItemCollectionEntity> inventoryEntities;
    static List<ItemCollectionEntity> equipmentEntities;

    static string profileToken;
    static int profileId = -1;

    public static void Initialization()
    {
        // Create or open databases
        profileDb = new ProfileDb();
        itemDb = new ItemDb();
        equipmentItemDb = new EquipmentItemDb();
        resourceItemDb = new ResourceItemDb();
        inventoryDb = new ItemCollectionDb("Inventory");
        equipmentDb = new ItemCollectionDb("Equipment");
    }

    #region Profile Database

    public static bool Login(string username, string password)
    {
        var entity = profileDb.GetProfileEntity(username);
        if (entity != null)
        {
            var profile = (ProfileEntity)entity;
            if (profile.Password.Equals(password))
            {
                profileId = profile.Id;
                return true;
            }
            else
            {
                Debug.Log("Wrong password");
                return false;
            }
        }

        Debug.Log("Username not found!");
        return false;
    }

    public static bool Login(string token)
    {
        var entity = profileDb.GetProfileEntityByToken(token);
        if (entity != null)
        {
            var profile = (ProfileEntity)entity;
            profileId = profile.Id;
            return true;
        }

        Debug.Log("Username not found!");
        return false;
    }

    /// <summary>
    /// New user registration using the entered username and password
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns>Final confirmation of registration</returns>
    public static bool SignUp(string username, string password)
    {
        ProfileEntity entity = new ProfileEntity(username, password);
        var registerAccept = profileDb.addData(entity);
        if (registerAccept)
        {
            var profile = (ProfileEntity)profileDb.GetProfileEntity(username);

            profileId = profile.Id;
        }
        return registerAccept;
    }

    public static ProfileEntity? GetProfile()
    {
        if (profileId == -1)
        {
            Debug.Log("Profile not find!");
            return null;
        }
        return profileDb.GetProfileEntity(profileId);
    }

    public static void LoadProfileItems()
    {
        // Find Player's item from inventory DB
        LoadPlayerInventory();

        // Find Player's equipment from equipment DB
        LoadPlayerEquipment();
    }

    #region Update Methods

    //public static void ChangeProfileNickname(string newValue)
    //{
    //    profileDb.UpdateNickname(profileId, newValue);
    //}

    public static void ChangeProfileUsername(string newValue)
    {
        profileDb.UpdateUsername(profileId, newValue);
    }

    public static void ChangeProfilePassword(string newValue)
    {
        profileDb.UpdatePassword(profileId, newValue);
    }

    public static void ChangeProfileEmail(string newValue)
    {
        profileDb.UpdateEmail(profileId, newValue);
    }

    public static void ChangeToken(string newValue)
    {
        profileDb.UpdateToken(profileId, newValue);
    }

    public static void ChangeProfileGemValue(int newValue)
    {
        profileDb.UpdateGemAmount(profileId, newValue);
    }

    public static void ChangeProfileCoinValue(int newValue)
    {
        profileDb.UpdateCoinAmount(profileId, newValue);
    }

    public static void ChangeProfileLevel(int newValue)
    {
        profileDb.UpdateLevel(profileId, newValue);
    }

    public static void ChangeProfileXP(float newValue)
    {
        profileDb.UpdateXP(profileId, newValue);
    }

    public static void ChangeProfileSkinId(string newValue)
    {
        profileDb.UpdateSkinId(profileId, newValue);
    }

    #endregion

    public static bool HasUsername(string username)
    {
        return profileDb.HasUsername(username);
    }

    #endregion

    #region Item Database

    //[NaughtyAttributes.Button]
    public static void LoadItems()
    {
        itemEntities = itemDb.GetAllData();
        equipmentItems = equipmentItemDb.GetAllData();
        resourceItems = resourceItemDb.GetAllData();

        SyncItemsWithGame();
    }

    private static void SyncItemsWithGame()
    {
        foreach (var entity in itemEntities)
        {
            var id = entity.Id.ToString();
            var name = entity.Name;
            var rarity = entity.Rarity;
            var icon = Resources.Load<Sprite>(entity.IconPath);
            var requiredLevel = entity.RequiredLevel;
            var price = entity.Price;
            var currency = entity.CurrencyType;
            var assetId = entity.AssetId;

            if (entity.Type == ItemType.Equipment)
            {
                var equipmentEntity = equipmentItems.Find(x => x.ItemId == entity.Id);
                var equipSlot = equipmentEntity.EquipSlot;
                var modifier = new Equipment.ItemModifier(equipmentEntity.DamageModifier, equipmentEntity.ArmorModifier, equipmentEntity.AttackSpeedModifier);

                Equipment equipment = new Equipment(id, name, rarity, icon, requiredLevel, price, currency, assetId, equipSlot, modifier);

                GameManager.GameData.AddItem(equipment);
            }
            else if (entity.Type == ItemType.Resource)
            {
                var resourceEntity = resourceItems.Find(x => x.ItemId == entity.Id);
                var type = resourceEntity.ResourceType;
                var value = resourceEntity.Value;

                Resource resource = new Resource(id, name, rarity, icon, requiredLevel, price, currency, assetId, type, value);

                GameManager.GameData.AddItem(resource);
            }
            else if (entity.Type == ItemType.Skin)
            {
                SkinData skin = new SkinData(id, name, rarity, icon, requiredLevel, price, currency, assetId, assetId);

                GameManager.GameData.AddItem(skin);
            }
            else
            {
                Debug.LogWarning($"Incorrect type {entity.Type}");
                continue;
            }
        }

        Debug.Log(GameManager.GameData.GetItems().Count);
    }

    #endregion

    #region Inventory Database

    private static void LoadPlayerInventory()
    {
        inventoryEntities = inventoryDb.GetItemCollectionEntities(profileId);

        List<Item> inventoryItems = new List<Item>();
        foreach (var entity in inventoryEntities)
        {
            // find item from game data with entity.ItemId
            var item = GameManager.GameData.GetItem(entity.ItemId);
            if (item == null)
            {
                Debug.LogWarning($"Item not found!! (Equipment search: {entity.ItemId})");
                continue;
            }
            inventoryItems.Add(item);
        }

        // add founded item to player profile inventory
        AccountController.LoadInventoryItem(inventoryItems);
    }

    public static bool AddItemToInventory(string itemId)
    {
        var createdAccept = inventoryDb.addData(new ItemCollectionEntity(profileId, itemId));
        if (createdAccept)
        {
            inventoryEntities = inventoryDb.GetItemCollectionEntities(profileId);
        }
        return createdAccept;
    }

    public static void RemoveItemFromInventory(string itemId)
    {
        var oldEntity = inventoryEntities.Find(x => x.Equals(new ItemCollectionEntity(profileId, itemId)));
        var deleteAccept = inventoryDb.deleteDataById(oldEntity.Id);
        if (deleteAccept)
        {
            inventoryEntities.Remove(oldEntity);
        }
    }

    #endregion

    #region Equipment Database

    private static void LoadPlayerEquipment()
    {
        equipmentEntities = equipmentDb.GetItemCollectionEntities(profileId);

        var equipmentItems = new List<Equipment>();
        foreach (var entity in equipmentEntities)
        {
            // find item from game data with entity.ItemId
            var item = GameManager.GameData.GetEquipmentItem(entity.ItemId);
            if (item == null)
            {
                Debug.LogWarning($"Item not found!! (Equipment search: {entity.ItemId})");
                continue;
            }
            equipmentItems.Add(item);

        }

        // add founded item to player profile equipment
        AccountController.LoadEquipmentItem(equipmentItems);
    }

    public static bool AddItemToEquipment(string itemId)
    {
        var collectionEntity = new ItemCollectionEntity(profileId, itemId);
        if (equipmentDb.HasItem(collectionEntity))
        {
            return false;
        }
        var createdAccept = equipmentDb.addData(collectionEntity);
        if (createdAccept)
        {
            equipmentEntities = equipmentDb.GetItemCollectionEntities(profileId);
        }
        return createdAccept;
    }

    public static void RemoveItemFromEquipment(string itemId)
    {
        equipmentDb.deleteDataByEntity(new ItemCollectionEntity(profileId, itemId));
    }

    #endregion

    public static bool HasItem(string itemId)
    {
        var collectionEntity = new ItemCollectionEntity(profileId, itemId);
        return equipmentDb.HasItem(collectionEntity) || inventoryDb.HasItem(collectionEntity);
    }
}
