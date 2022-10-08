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
            // not found account
            //CreateNewAccount();
            return false;
        }
        else
        {
            return true;
        }
    }

    private static void CreateNewAccount()
    {
        GameManager.SaveOrLoad.PlayerToken = CreateToken();
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

    public static bool Login(string username,string password,bool rememberMe)
    {
        // find in database

        // if find
        //      LoadPlayerProfile()
        //      if rememberMe
        //          save token

        return true;
    }

    public static bool SignUp(string email, string username, string password)
    {
        // validate account

        // if accept
        //      LoadPlayerProfile()
        LoadPlayerProfile();
        return true;
    }

    public static void LoadPlayerProfile()
    {
        // Create new player profile
        Data = new PlayerProfile("", "Diana", 0, 0, 0, "0", new XP());//sample data

        // Load data from DB

        // Apply change to game
        LoadingController.onLoadingComplete += () =>
        {
            onChangeProperty?.Invoke(Data);
        };
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

    public static void ChangeName(string newName)
    {
        Data.UpdateData(newUsername: newName);

        // Apply change to game
        onChangeProperty?.Invoke(Data);
    }

    #endregion
}
