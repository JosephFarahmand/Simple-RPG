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
                var moveSpeedModifier = reader.GetInt32(6);

                EquipmentItemEntity entity = new EquipmentItemEntity(id, itemId, (EquipmentSlot)slot, damageModifier, armorModifier, attackSpeedModifier, moveSpeedModifier);

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

                myList.Add(entity);
            }

            // Always call Close when done reading.
            reader.Close();

            return myList;
        }

        #endregion

        #endregion

        #region Profile Entity

        public static ProfileEntity? GetProfileEntity(this ProfileDb database, int id)
        {
            IDataReader reader = database.getDataById(id);
            while (reader.Read())
            {
                ProfileEntity entity = GetProfileEntity(reader);

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
                ProfileEntity entity = GetProfileEntity(reader);

                return entity;
            }

            // Always call Close when done reading.
            reader.Close();

            Debug.Log("Item with this ID was not found!!");
            return null;
        }

        public static ProfileEntity? GetProfileEntityByToken(this ProfileDb database, string token)
        {
            IDataReader reader = database.getDataByToken(token);
            while (reader.Read())
            {
                ProfileEntity entity = GetProfileEntity(reader);

                return entity;
            }

            // Always call Close when done reading.
            reader.Close();

            Debug.Log("Item with this ID was not found!!");
            return null;
        }

        public static ProfileEntity GetProfileEntity(IDataReader reader)
        {
            var id = reader.GetInt32(0);
            var username = reader[1].ToString();
            var password = reader[2].ToString();
            var token = reader[3].ToString();
            var email = reader[4].ToString();
            var coin = reader.GetInt32(5);
            var gem = reader.GetInt32(6);
            var level = reader.GetInt32(7);
            var skinId = reader[8].ToString();
            var currentXP = reader.GetFloat(9);

            ProfileEntity entity = new ProfileEntity(id, username, password, token, email, coin, gem, level, skinId, currentXP);

            return entity;
        }

        #endregion

        #region Item Collection Entity

        public static bool HasItem(this ItemCollectionDb database, ItemCollectionEntity itemCollection)
        {
            IDataReader reader = database.getDataByEntity(itemCollection);

            var list = new List<ItemCollectionEntity>();

            while (reader.Read())
            {
                ItemCollectionEntity entity = GetItemCollectionEntity(reader);
                list.Add(entity);
            }

            return list.Count > 0;
        }

        public static List<ItemCollectionEntity> GetItemCollectionEntities(this ItemCollectionDb database, int profileId)
        {
            IDataReader reader = database.getDataByProfileId(profileId);

            var list = new List<ItemCollectionEntity>();

            while (reader.Read())
            {
                ItemCollectionEntity entity = GetItemCollectionEntity(reader);
                list.Add(entity);
            }

            return list;
        }

        private static ItemCollectionEntity GetItemCollectionEntity(IDataReader reader)
        {
            var id = reader.GetInt32(0);
            var profileId = reader.GetInt32(1);
            var itemId = reader[2].ToString();

            ItemCollectionEntity entity = new ItemCollectionEntity(id, profileId, itemId);
            return entity;
        }

        #endregion
    }
}