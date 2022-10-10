using DataBank;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseController : MonoBehaviour
{
    ProfileDb profileDb;
    ItemDb itemDb;
    EquipmentItemDb equipmentItemDb;
    ResourceItemDb resourceItemDb;
    InventoryDb inventoryDb;
    EquipmentDb equipmentDb;

    List<ItemEntity> itemEntities;
    List<EquipmentItemEntity> equipmentItems;
    List<ResourceItemEntity> resourceItems;
    List<ItemCollectionEntity> inventoryEntities;
    List<ItemCollectionEntity> equipmentEntities;

    int profileToken = -1;

    void Start()
    {
        // Create or open databases
        profileDb = new ProfileDb();
        itemDb = new ItemDb();
        equipmentItemDb = new EquipmentItemDb();
        resourceItemDb = new ResourceItemDb();
        inventoryDb = new InventoryDb();
        equipmentDb = new EquipmentDb();
    }

    #region Profile Database

    /// <summary>
    /// Find profile's id in database
    /// </summary>
    /// <param name="profileId">profile's id stored in PlayerPrefs</param>
    public void GetPlayerProfile(int profileId)
    {
        var profile = profileDb.GetProfileEntity(profileId);
        if (profile == null)
        {
            // Open Register page
        }
        else
        {
            profileToken = profileId;

            // Open Loading Page

            // Load profile data
            CreatePlayerProfile((ProfileEntity)profile);

            // Load items database
            LoadItems();

            // Find Player's item from inventory DB
            LoadPlayerInventory();

            // Find Player's equipment from equipment DB
            LoadPlayerEquipment();

            // Open Home Page
        }
    }

    private void CreatePlayerProfile(ProfileEntity entity)
    {
        profileToken = entity.Id;

        // Create player profile and assign geted profile data
    }

    /// <summary>
    /// New user registration using the entered username and password
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns>Final confirmation of registration</returns>
    public bool RegisterProfile(string username, string password)
    {
        ProfileEntity entity = new ProfileEntity(username, password);
        var registerAccept = profileDb.addData(entity);
        if (registerAccept)
        {
            var profile = (ProfileEntity)profileDb.GetProfileEntity(username);

            profileToken = profile.Id;

            // save profileId in PlayerPrefs
        }
        return registerAccept;
    }

    public void ChangeProfileNickname(string newValue)
    {
        profileDb.UpdateNickname(profileToken, newValue);
    }

    public void ChangeProfileGemValue(int newValue)
    {
        profileDb.UpdateGemAmount(profileToken, newValue);
    }

    public void ChangeProfileCoinValue(int newValue)
    {
        profileDb.UpdateCoinAmount(profileToken, newValue);
    }

    public void ChangeProfileLevel(int newValue)
    {
        profileDb.UpdateLevel(profileToken, newValue);
    }

    public void ChangeProfileSkinId(string newValue)
    {
        profileDb.UpdateSkinId(profileToken, newValue);
    }

    #endregion

    #region Item Database

    [NaughtyAttributes.Button]
    private void LoadItems()
    {
        var itemDb = new ItemDb();
        var equipmentItemDb = new EquipmentItemDb();
        var resourceItemDb = new ResourceItemDb();

        itemEntities = itemDb.GetAllData();
        equipmentItems = equipmentItemDb.GetAllData();
        resourceItems = resourceItemDb.GetAllData();
        SyncItemsWithGame();
    }

    private void SyncItemsWithGame()
    {
        foreach (var entity in itemEntities)
        {
            // find game asset with entity.AssetId from game data

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

            // sync founded item with entity data
        }

        Debug.Log(GameManager.GameData.GetItems().Count);
    }

    #endregion

    #region Inventory Database

    private void LoadPlayerInventory()
    {
        inventoryEntities = inventoryDb.GetItemCollectionEntities(profileToken);

        foreach (var entity in inventoryEntities)
        {
            // find item from game data with entity.ItemId

            // add founded item to player profile inventory
        }
    }

    public bool AddItemToInventory(int itemId)
    {
        var createdAccept = inventoryDb.addData(new ItemCollectionEntity(profileToken, itemId));
        return createdAccept;
    }

    public void RemoveItemFromInventory(int itemId)
    {
        var oldEntities = inventoryEntities.FindAll(x => x.Equals(new ItemCollectionEntity(profileToken, itemId)));
        foreach (var entity in oldEntities)
        {
            inventoryDb.deleteDataByEntity(entity);
        }
    }

    #endregion

    #region Equipment Database

    private void LoadPlayerEquipment()
    {
        equipmentEntities = equipmentDb.GetItemCollectionEntities(profileToken);

        foreach (var entity in inventoryEntities)
        {
            // find item from game data with entity.ItemId

            // add founded item to player profile equipment
        }
    }

    public bool AddItemToEquipment(int itemId)
    {
        var createdAccept = equipmentDb.addData(new ItemCollectionEntity(profileToken, itemId));
        return createdAccept;
    }

    public void RemoveItemFromEquipment(int itemId)
    {
        var oldEntities = equipmentEntities.FindAll(x => x.Equals(new ItemCollectionEntity(profileToken, itemId)));
        foreach (var entity in oldEntities)
        {
            equipmentDb.deleteDataByEntity(entity);
        }
    }

    #endregion

    private void OnApplicationQuit()
    {
        profileDb.close();
        itemDb.close();
        equipmentItemDb.close();
        resourceItemDb.close();
        inventoryDb.close();
        equipmentDb.close();
    }
}
