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

        return true;
    }

    public static void LoadPlayerProfile()
    {
        // create new PlayerProfile
    }
}
