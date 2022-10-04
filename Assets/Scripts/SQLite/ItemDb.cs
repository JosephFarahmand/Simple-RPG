using System.Data;
using UnityEngine;

namespace DataBank
{
    public class ItemDb : SqliteHelper
    {
        private const string Tag = "Riz: ItemDb:\t";
        
        private const string TABLE_NAME = "Item";
        private const string KEY_ID = "id";
        private const string KEY_NAME = "name";
        private const string KEY_TYPE = "type";
        private const string KEY_DEFAULT = "isDefaultItem";
        private const string KEY_RARITY = "rarity";
        private const string KEY_REQUIRED_LEVEL = "requiredLevel";
        private const string KEY_PRICE = "price";
        private const string KEY_COUNT = "count";
        private const string KEY_ASSET = "assetId";

        public ItemDb() : base()
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + " ( " +
                KEY_ID + " INTEGER PRIMARY KEY AUTOINCREMENT, " +
                KEY_NAME + " TEXT, " +
                KEY_TYPE + " INT DEFAULT 0, " +
                KEY_DEFAULT + " BOOL DEFAULT false, " +
                KEY_RARITY + " INT DEFAULT 0, " +
                KEY_REQUIRED_LEVEL + " INT DEFAULT 1, " +
                KEY_PRICE + " INT DEFAULT 0, " +
                KEY_COUNT + " INT DEFAULT 1, " +
                KEY_ASSET + " INT NOT NULL " +
                " )";
            dbcmd.ExecuteNonQuery();
        }

        public void addData(ItemEntity item)
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "INSERT INTO " + TABLE_NAME
                + " ( "
                + KEY_NAME + ", "
                + KEY_TYPE + ", "
                + KEY_DEFAULT + ", "
                + KEY_RARITY + ", "
                + KEY_REQUIRED_LEVEL + ", "
                + KEY_PRICE + ", "
                + KEY_COUNT + ", "
                + KEY_ASSET
                + " ) "

                + "VALUES ( '"
                + item.Name+ "', '"
                + item.Type + "', '"
                + item.IsDefaultItem + "', '"
                + item.Rarity + "', '"
                + item.RequiredLevel + "', '"
                + item.Price + "', '"
                + item.Count + "', '"
                + item.AssetId + "' "
                + " )";
            dbcmd.ExecuteNonQuery();
        }

        public void UpdateData(int id, int newCountValue)
        {
            Debug.Log(Tag + "Updating Item's count: " + id);

            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "UPDATE " + TABLE_NAME + " SET " + KEY_COUNT + " = " + newCountValue + " WHERE " + KEY_ID + " = '" + id + "'";
            dbcmd.ExecuteNonQuery();
        }

        public override IDataReader getDataById(int id)
        {
            Debug.Log(Tag + "Getting Item: " + id);

            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "SELECT * FROM " + TABLE_NAME + " WHERE " + KEY_ID + " = '" + id + "'";
            return dbcmd.ExecuteReader();
        }

        //public override void deleteDataById(int id)
        //{
        //    Debug.Log(Tag + "Deleting Item: " + id);

        //    IDbCommand dbcmd = getDbCommand();
        //    dbcmd.CommandText =
        //        "DELETE FROM " + TABLE_NAME + " WHERE " + KEY_ID + " = '" + id + "'";
        //    dbcmd.ExecuteNonQuery();
        //}

        public override IDataReader getAllData()
        {
            return getAllData(TABLE_NAME);
        }
    }
}