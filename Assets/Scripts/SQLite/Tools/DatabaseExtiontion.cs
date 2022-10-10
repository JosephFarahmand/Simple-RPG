using System.Collections.Generic;
using System.Data;
using UnityEngine;

namespace DataBank
{
    public static class DatabaseExtiontion
    {
        #region Item Entity

        public static List<ItemEntity> GetAllData(this ItemDb database)
        {
            IDataReader reader = database.getAllData();
            List<ItemEntity> myList = new List<ItemEntity>();
            while (reader.Read())
            {
                ItemEntity entity = GetItemEntity(reader);

                Debug.Log("id: " + entity.Id);
                myList.Add(entity);
            }

            // Always call Close when done reading.
            reader.Close();

            return myList;
        }

        public static ItemEntity? GetItemEntity(this ItemDb database,int id)
        {
            IDataReader reader = database.getDataById(id);
            while (reader.Read())
            {
                ItemEntity entity = GetItemEntity(reader);

                return entity;
            }

            // Always call Close when done reading.
            reader.Close();

            Debug.Log("Item with this ID was not found!!");
            return null;
        }

        public static ItemEntity? GetItemEntity(this ItemDb database, string id)
        {
            IDataReader reader = database.getDataByAssetId(id);
            while (reader.Read())
            {
                ItemEntity entity = GetItemEntity(reader);

                return entity;
            }

            // Always call Close when done reading.
            reader.Close();

            Debug.Log("Item with this ID was not found!!");
            return null;
        }

        private static ItemEntity GetItemEntity(IDataReader reader)
        {
            var id = reader.GetInt32(0);
            var name = reader[1].ToString();
            var type = reader.GetInt32(2);
            //var isDefaultItem = bool.Parse(reader[3].ToString());
            var rarity = reader.GetInt32(3);
            var requiredLevel = reader.GetInt32(4);
            var price = reader.GetInt32(5);
            var currency = reader.GetInt32(6);
            var count = reader.GetInt32(7);
            var assetId = reader[8].ToString();
            var iconPath = reader[9].ToString();

            ItemEntity entity = new ItemEntity(id, name, type, /*isDefaultItem,*/ rarity, requiredLevel, price, count, assetId, iconPath, (CurrencyType)currency);
            return entity;
        }

        #region EquipmentItem

        public static List<EquipmentItemEntity> GetAllData(this EquipmentItemDb database)
        {
            IDataReader reader = database.getAllData();
            List<EquipmentItemEntity> myList = new List<EquipmentItemEntity>();
            while (reader.Read())
            {
                var id = reader.GetInt32(0);
                var itemId = reader.GetInt32(1);
                var slot = reader.GetInt32(2);
                var damageModifier = reader.GetInt32(3);
                var armorModifier = reader.GetInt32(4);
                var attackSpeedModifier = reader.GetInt32(5);

                EquipmentItemEntity entity = new EquipmentItemEntity(id, itemId, (EquipmentSlot)slot, damageModifier, armorModifier, attackSpeedModifier);

                Debug.Log("id: " + entity.Id);
                myList.Add(entity);
            }

            // Always call Close when done reading.
            reader.Close();

            return myList;
        }

        #endregion

        #region Resource Item

        public static List<ResourceItemEntity> GetAllData(this ResourceItemDb database)
        {
            IDataReader reader = database.getAllData();
            List<ResourceItemEntity> myList = new List<ResourceItemEntity>();
            while (reader.Read())
            {
                var id = reader.GetInt32(0);
                var itemId = reader.GetInt32(1);
                var resourceType = reader.GetInt32(2);
                var value = reader.GetInt32(3);

                ResourceItemEntity entity = new ResourceItemEntity(id, itemId, (ResourceType)resourceType, value);

                Debug.Log("id: " + entity.Id);
                myList.Add(entity);
            }

            // Always call Close when done reading.
            reader.Close();

            return myList;
        }

        #endregion

        #endregion

        #region Profile Entity

        public static ProfileEntity? Convert(this IDataReader reader)
        {
            while (reader.Read())
            {
                var id = reader.GetInt32(0);
                var username = reader[1].ToString();
                var password = reader[2].ToString();
                var nickname = reader[3].ToString();
                var coin = reader.GetInt32(4);
                var gem = reader.GetInt32(5);
                var level = reader.GetInt32(6);
                var skinId = reader[7].ToString();

                ProfileEntity entity = new ProfileEntity(id, username, password, nickname, coin, gem, level, skinId);

                return entity;
            }
            return null;
        }

        public static ProfileEntity? GetProfileEntity(this ProfileDb database, int id)
        {
            IDataReader reader = database.getDataById(id);
            while (reader.Read())
            {
                var username = reader[1].ToString();
                var password = reader[2].ToString();
                var nickname = reader[3].ToString();
                var coin = reader.GetInt32(4);
                var gem = reader.GetInt32(5);
                var level = reader.GetInt32(6);
                var skinId = reader[7].ToString();

                ProfileEntity entity = new ProfileEntity(id, username, password, nickname, coin, gem, level, skinId);

                return entity;
            }

            // Always call Close when done reading.
            reader.Close();

            Debug.Log("Item with this ID was not found!!");
            return null;
        }

        public static ProfileEntity? GetProfileEntity(this ProfileDb database, string username)
        {
            IDataReader reader = database.getDataByUsername(username);
            while (reader.Read())
            {
                var id = reader.GetInt32(0);
                var password = reader[2].ToString();
                var nickname = reader[3].ToString();
                var coin = reader.GetInt32(4);
                var gem = reader.GetInt32(5);
                var level = reader.GetInt32(6);
                var skinId = reader[7].ToString();

                ProfileEntity entity = new ProfileEntity(id, username, password, nickname, coin, gem, level, skinId);

                return entity;
            }

            // Always call Close when done reading.
            reader.Close();

            Debug.Log("Item with this ID was not found!!");
            return null;
        }

        #endregion

        #region Inventory Entity

        public static List<ItemCollectionEntity> GetItemCollectionEntities(this InventoryDb database, int profileId)
        {
            IDataReader reader = database.getDataByProfileId(profileId);

            var list = new List<ItemCollectionEntity>();

            while (reader.Read())
            {
                var id = reader.GetInt32(0);
                var itemId = reader.GetInt32(2);

                ItemCollectionEntity entity = new ItemCollectionEntity(id, profileId, itemId);
                list.Add(entity);
            }

            return list;
        }

        #endregion

        #region Inventory Entity

        public static List<ItemCollectionEntity> GetItemCollectionEntities(this EquipmentDb database, int profileId)
        {
            IDataReader reader = database.getDataByProfileId(profileId);

            var list = new List<ItemCollectionEntity>();

            while (reader.Read())
            {
                var id = reader.GetInt32(0);
                var itemId = reader.GetInt32(2);

                ItemCollectionEntity entity = new ItemCollectionEntity(id, profileId, itemId);
                list.Add(entity);
            }

            return list;
        }

        #endregion
    }
}