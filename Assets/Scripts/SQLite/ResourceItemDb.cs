using System.Data;
using UnityEngine;

namespace DataBank
{
    public class SkinItemDb : SqliteHelper
    {
        private const string Tag = "Riz: SkinItemDb:\t";

        private const string TABLE_NAME = "SkinItem";
        private const string KEY_ID = "id";
        private const string KEY_ITEM_ID = "itemId";
        private const string KEY_MATERIAL = "materialId";

        public SkinItemDb() : base()
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + " ( " +
                KEY_ID + " INTEGER PRIMARY KEY AUTOINCREMENT, " +
                KEY_ITEM_ID + " INTEGER NOT NULL, " +
                KEY_MATERIAL + " INTEGER NOT NULL " +
                " )";
            dbcmd.ExecuteNonQuery();
        }

        public void addData(SkinItemEntity item)
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "INSERT INTO " + TABLE_NAME
                + " ( "
                + KEY_ITEM_ID + ", "
                + KEY_MATERIAL
                + " ) "

                + "VALUES ( '"
                + item.ItemId + "', '"
                + item.MaterialId + "' "
                + " )";
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

        public override IDataReader getAllData()
        {
            return getAllData(TABLE_NAME);
        }
    }

    public class ResourceItemDb : SqliteHelper
    {
        private const string Tag = "Riz: ResourceItemDb:\t";

        private const string TABLE_NAME = "ResourceItem";
        private const string KEY_ID = "id";
        private const string KEY_ITEM_ID = "itemId";
        private const string KEY_TYPE = "resourceType";
        private const string KEY_VALUE = "value";

        public ResourceItemDb() : base()
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + " ( " +
                KEY_ID + " INTEGER PRIMARY KEY AUTOINCREMENT, " +
                KEY_ITEM_ID + " INTEGER NOT NULL, " +
                KEY_TYPE + " INT DEFAULT 0, " +
                KEY_VALUE + " INT DEFAULT 0 " +
                " )";
            dbcmd.ExecuteNonQuery();
        }

        public void addData(ResourceItemEntity item)
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "INSERT INTO " + TABLE_NAME
                + " ( "
                + KEY_ITEM_ID + ", "
                + KEY_TYPE + ", "
                + KEY_VALUE 
                + " ) "

                + "VALUES ( '"
                + item.ItemId + "', '"
                + (int)item.ResourceType + "', '"
                + item.Value + "' "
                + " )";
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

        public override IDataReader getAllData()
        {
            return getAllData(TABLE_NAME);
        }
    }
}