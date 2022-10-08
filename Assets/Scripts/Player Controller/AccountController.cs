using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AccountController
{
    const string AllowedChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

    public static bool CheckAccount()
    {
        if (GameManager.SaveOrLoad.PlayerToken == string.Empty || GameManager.SaveOrLoad.PlayerToken == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private static void SaveToken(string token)
    {
        GameManager.SaveOrLoad.PlayerToken = token;
    }

    private static string CreateToken()
    {
        char[] chars = new char[32];

        for (int i = 0; i < 32; ++i)
        {
            chars[i] = AllowedChars[Random.Range(0, AllowedChars.Length)];
        }

        return new string(chars);
    }

    public static bool Logout()
    {
        GameManager.SaveOrLoad.PlayerToken = null;
        return true;
    }

    public static bool Login(string username,string password,bool rememberMe)
    {
        // find in database

        // if find
        //      LoadPlayerProfile()
        //      if rememberMe
        //          save token --> SaveToken(/*GET FROM DATA*/);

        LoadPlayerProfile();
        
        return true;
    }

    public static bool LoginAsGuest()
    {
        LoadPlayerProfile();
        return true;
    }

    public static bool SignUp(string email, string username, string password)
    {
        // validate account

        // if accept
        //      LoadPlayerProfile()

        LoadPlayerProfile();
        SaveToken(CreateToken());

        return true;
    }

    public static void LoadPlayerProfile(/*PROFILE ENTITY*/)
    {
        // Create new player profile from parameter
        Data = StaticData.SampleProfile;

        // Load data from DB

        // Apply change to game
        LoadingController.onLoadingComplete += () =>
        {
            onChangeProperty?.Invoke(Data);
        };
        LoadingController.LoadAction();
    }

    /////////////////////////////////////////////////////////
    public static System.Action<PlayerProfile> onChangeProperty;
    public static PlayerProfile Data { get; private set; }

    #region UPDATE PLAYER PROFILE

    public static void IncreseLevel()
    {
        Data.UpdateData(newLevelValue: Data.Level + 1);

        // Apply change to game
        onChangeProperty?.Invoke(Data);
    }

    public static void IncreseXP(float value)
    {
        if (Data.XP.CurrentValue + value >= Data.XP.MaximumValue)
        {
            IncreseLevel();
            Data.UpdateData(0);
        }
        else
        {
            Data.UpdateData(Data.XP.CurrentValue + value);
        }

        // Apply change to game
        onChangeProperty?.Invoke(Data);
    }

    //public static void AddGemValue(int value)
    //{
    //    //data.UpdateData(newGemAmount: data.GemAmount + value);

    //    // Apply change to game
    //    onChangeProperty?.Invoke(Data);
    //}

    public static void AddCoinValue(int value)
    {
        Data.UpdateData(newCoinAmount: Data.CoinAmount + value);

        // Apply change to game
        onChangeProperty?.Invoke(Data);
    }

    public static void ChangeUsername(string newUsername)
    {
        Data.UpdateData(newUsername: newUsername);

        // Apply change to game
        onChangeProperty?.Invoke(Data);
    }

    public static void ChangePassword(string newPassword)
    {
        Data.UpdateData(newPassword: newPassword);

        // Apply change to game
        onChangeProperty?.Invoke(Data);
    }

    public static void ChangeNickname(string newNickname)
    {
        Data.UpdateData(newNickname: newNickname);

        // Apply change to game
        onChangeProperty?.Invoke(Data);
    }

    public static void AddInventoryItem(Item item)
    {
        Data.AddInventoryItem(item);
    }

    public static void RemoveInventoryItem(Item oldItem)
    {
        Data.RemoveInventoryItem(oldItem);
    }

    public static void EquipItem(Equipment newItem)
    {
        Data.EquipItem(newItem);
    }

    public static void UnequipItem(Equipment oldItem)
    {
        Data.UnequipItem(oldItem);
    }

    #endregion
}
