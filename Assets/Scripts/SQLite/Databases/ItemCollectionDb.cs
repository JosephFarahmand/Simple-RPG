using System.Data;
using UnityEngine;

namespace DataBank
{
    public class ItemCollectionDb : SqliteHelper
    {
        private const string Tag = "Riz: InventoryDb:\t";

        private readonly string TABLE_NAME;
        private const string KEY_ID = "id";
        private const string KEY_PROFILE = "profileId";
        private const string KEY_ITEM = "itemId";

        public ItemCollectionDb(string TABLE_NAME) : base()
        {
            this.TABLE_NAME = TABLE_NAME;
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + " ( " +
                KEY_ID + " INTEGER PRIMARY KEY AUTOINCREMENT, " +
                KEY_PROFILE + " INT NOT NULL, " +
                KEY_ITEM + " INT NOT NULL" +
                " )";
            dbcmd.ExecuteNonQuery();
        }

        public bool addData(ItemCollectionEntity entity)
        {
            try
            {
                IDbCommand dbcmd = getDbCommand();
                dbcmd.CommandText =
                    "INSERT INTO " + TABLE_NAME
                    + " ( "
                    + KEY_PROFILE + ", "
                    + KEY_ITEM
                    + " ) "

                    + "VALUES ( '"
                    + entity.ProfileId + "', '"
                    + entity.ItemId + "' "
                    + " )";
                dbcmd.ExecuteNonQuery();

                return true;
            }
            catch (System.Exception)
            {
                return false;
                throw;
            }
        }

        public override void deleteDataById(int id)
        {
            Debug.Log(Tag + "Deleting Item: " + id);

            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "DELETE FROM " + TABLE_NAME + " WHERE " + KEY_ID + " = '" + id + "'";
            dbcmd.ExecuteNonQuery();
        }

        public void deleteDataByEntity(ItemCollectionEntity entity)
        {
            deleteDataById(entity.Id);
        }

        public override IDataReader getAllData()
        {
            return getAllData(TABLE_NAME);
        }

        public IDataReader getDataByProfileId(int profileId)
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "SELECT * FROM " + TABLE_NAME + " WHERE " + KEY_PROFILE + " = '" + profileId + "' ";
            return dbcmd.ExecuteReader();
        }

        public IDataReader getDataByEntity(ItemCollectionEntity entity)
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "SELECT * FROM " + TABLE_NAME + " WHERE " + KEY_PROFILE + " = '" + entity.ProfileId + "' AND " + KEY_ITEM + " = '" + entity.ItemId + "' ";
            return dbcmd.ExecuteReader();
        }
    }
}