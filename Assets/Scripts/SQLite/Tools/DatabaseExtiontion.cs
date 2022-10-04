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
                var id = reader.GetInt32(0);
                var name = reader[1].ToString();
                var type = reader[2].ToString();
                var isDefaultItem = bool.Parse(reader[3].ToString());
                var rarity = reader.GetInt32(4);
                var requiredLevel = reader.GetInt32(5);
                var price = reader.GetInt32(6);
                var count = reader.GetInt32(7);
                var assetId = reader[8].ToString();

                ItemEntity entity = new ItemEntity(id, name, type, isDefaultItem, rarity, requiredLevel, price, count, assetId);

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
                var name = reader[1].ToString();
                var type = reader[2].ToString();
                var isDefaultItem = bool.Parse(reader[3].ToString());
                var rarity = reader.GetInt32(4);
                var requiredLevel = reader.GetInt32(5);
                var price = reader.GetInt32(6);
                var count = reader.GetInt32(7);
                var assetId = reader[8].ToString();

                ItemEntity entity = new ItemEntity(id, name, type, isDefaultItem, rarity, requiredLevel, price, count, assetId);

                return entity;
            }

            // Always call Close when done reading.
            reader.Close();

            Debug.Log("Item with this ID was not found!!");
            return null;
        }

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