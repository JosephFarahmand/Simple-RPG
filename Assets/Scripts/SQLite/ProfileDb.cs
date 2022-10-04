using System;
using System.Data;
using UnityEngine;

namespace DataBank
{
    public class ProfileDb : SqliteHelper
    {
        private const string Tag = "Riz: ProfileDb:\t";

        private const string TABLE_NAME = "Profile";
        private const string KEY_ID = "id";
        private const string KEY_USERNAME = "username";
        private const string KEY_PASSWORD = "password";
        private const string KEY_NICKNAME = "nickname";
        private const string KEY_COIN = "coin";
        private const string KEY_GEM = "gem";
        private const string KEY_LEVEL = "level";
        private const string KEY_SKIN = "skinId";

        public ProfileDb() : base()
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + " ( " +
                KEY_ID + " INTEGER PRIMARY KEY AUTOINCREMENT, " +
                KEY_USERNAME + " TEXT UNIQUE, " +
                KEY_PASSWORD + " TEXT NOT NULL, " +
                KEY_NICKNAME + " TEXT DEFAULT newPlayer, " +
                KEY_COIN + " INTEGER DEFAULT 0, " +
                KEY_GEM + " INTEGER DEFAULT 0, " +
                KEY_LEVEL + " INTEGER DEFAULT 1, " +
                KEY_SKIN + " TEXT DEFAULT 0" +
                " )";
            dbcmd.ExecuteNonQuery();
        }

        public bool addData(ProfileEntity profile)
        {
            try
            {
                IDbCommand dbcmd = getDbCommand();
                dbcmd.CommandText =
                    "INSERT INTO " + TABLE_NAME
                    + " ( "
                    + KEY_USERNAME + ", "
                    + KEY_PASSWORD
                    + " ) "

                    + "VALUES ( '"
                    + profile.Username + "', '"
                    + profile.Password + "', "
                    + " )";
                dbcmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public IDataReader getDataByUsername(string username)
        {
            Debug.Log(Tag + "Getting Profile: " + username);

            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "SELECT * FROM " + TABLE_NAME + " WHERE " + KEY_USERNAME + " = '" + username + "'";
            return dbcmd.ExecuteReader();
        }

        public override IDataReader getDataById(int id)
        {
            Debug.Log(Tag + "Getting Profile: " + id);

            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "SELECT * FROM " + TABLE_NAME + " WHERE " + KEY_ID + " = '" + id + "'";
            return dbcmd.ExecuteReader();
        }

        public override void deleteDataById(int id)
        {
            Debug.Log(Tag + "Deleting Profile: " + id);

            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "DELETE FROM " + TABLE_NAME + " WHERE " + KEY_ID + " = '" + id + "'";
            dbcmd.ExecuteNonQuery();
        }

        public override IDataReader getAllData()
        {
            return getAllData(TABLE_NAME);
        }

        public bool UpdateNickname(int id, string newNickname)
        {
            try
            {
                Debug.Log(Tag + "Updating Profile: " + id);

                IDbCommand dbcmd = getDbCommand();
                dbcmd.CommandText =
                    "UPDATE " + TABLE_NAME + " SET " + KEY_NICKNAME + " = " + newNickname + " WHERE " + KEY_ID + " = '" + id + "'";
                dbcmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool UpdateCoinAmount(int id, int newValue)
        {
            try
            {
                Debug.Log(Tag + "Updating Profile: " + id);

                IDbCommand dbcmd = getDbCommand();
                dbcmd.CommandText =
                    "UPDATE " + TABLE_NAME + " SET " + KEY_COIN + " = " + newValue + " WHERE " + KEY_ID + " = '" + id + "'";
                dbcmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateGemAmount(int id, int newValue)
        {
            try
            {
                Debug.Log(Tag + "Updating Profile: " + id);

                IDbCommand dbcmd = getDbCommand();
                dbcmd.CommandText =
                    "UPDATE " + TABLE_NAME + " SET " + KEY_GEM + " = " + newValue + " WHERE " + KEY_ID + " = '" + id + "'";
                dbcmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void UpdateLevel(int id, int newValue)
        {
            Debug.Log(Tag + "Updating Profile: " + id);

            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "UPDATE " + TABLE_NAME + " SET " + KEY_LEVEL + " = " + newValue + " WHERE " + KEY_ID + " = '" + id + "'";
            dbcmd.ExecuteNonQuery();
        }

        public void UpdateSkinId(int id, string skinId)
        {
            Debug.Log(Tag + "Updating Profile: " + id);

            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "UPDATE " + TABLE_NAME + " SET " + KEY_SKIN + " = " + skinId + " WHERE " + KEY_ID + " = '" + id + "'";
            dbcmd.ExecuteNonQuery();
        }
    }
}