using System.Data;
using UnityEngine;

namespace DataBank
{
    public class EquipmentItemDb : SqliteHelper
    {
        private const string Tag = "Riz: EquipmentItemDb:\t";

        private const string TABLE_NAME = "EquipmentItem";
        private const string KEY_ID = "id";
        private const string KEY_ITEM_ID = "itemId";
        private const string KEY_SLOT = "equipSlot";
        private const string KEY_mDAMAGE = "damageModifier";
        private const string KEY_mARMOR = "armorModifier";
        private const string KEY_mATTACK_SPEED = "attackSpeedModifier";

        public EquipmentItemDb() : base()
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + " ( " +
                KEY_ID + " INTEGER PRIMARY KEY AUTOINCREMENT, " +
                KEY_ITEM_ID + " INTEGER NOT NULL, " +
                KEY_SLOT + " INTEGER DEFAULT 0, " +
                KEY_mDAMAGE + " INTEGER DEFAULT 0, " +
                KEY_mARMOR + " INTEGER DEFAULT 0, " +
                KEY_mATTACK_SPEED + " INTEGER DEFAULT 0 " +
                " )";
            dbcmd.ExecuteNonQuery();
        }

        public void addData(EquipmentItemEntity item)
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "INSERT INTO " + TABLE_NAME
                + " ( "
                + KEY_ITEM_ID + ", "
                + KEY_SLOT + ", "
                + KEY_mDAMAGE + ", "
                + KEY_mARMOR + ", "
                + KEY_mATTACK_SPEED
                + " ) "

                + "VALUES ( '"
                + item.ItemId + "', '"
                + (int)item.EquipSlot + "', '"
                + item.DamageModifier + "', '"
                + item.ArmorModifier + "', '"
                + item.AttackSpeedModifier + "' "
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