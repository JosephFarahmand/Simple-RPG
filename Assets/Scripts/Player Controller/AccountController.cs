using DataBank;
using System.Collections.Generic;
using UnityEngine;

public static class AccountController
{
    /// <summary>
    /// check device has account or not
    /// </summary>
    /// <returns></returns>
    public static bool CheckAccount()
    {
        var token = GameManager.SaveOrLoad.PlayerToken;
        if (token == null || token == string.Empty)
        {
            return false;
        }
        else
        {
            Login(token);
            return true;
        }
    }

    private static void SaveToken()
    {
        GameManager.SaveOrLoad.PlayerToken = SystemInfo.deviceUniqueIdentifier;
    }

    public static bool Logout()
    {
        GameManager.SaveOrLoad.PlayerToken = null;
        return true;
    }

    public static bool Login(string username, string password, bool rememberMe)
    {
        var confirmation = DatabaseController.Login(username, password);

        if (confirmation)
        {
            LoadPlayerProfile();

            if (rememberMe)
            {
                SaveToken();
            }
        }

        return confirmation;
    }

    private static bool Login(string token)
    {
        var confirmation = DatabaseController.Login(token);

        if (confirmation)
        {
            LoadPlayerProfile();
        }

        return confirmation;
    }

    public static bool LoginAsGuest()
    {
        LoadPlayerProfile();
        return true;
    }

    public static bool SignUp(string email, string username, string password)
    {
        if (!RegexUtilities.IsValidEmail(email))
        {
            Debug.LogError("email is not valid!");
            return false;
        }

        var confirmation = DatabaseController.SignUp(username, password);

        if (confirmation)
        {
            DatabaseController.ChangeProfileEmail(email);

            var token = SystemInfo.deviceUniqueIdentifier;
            DatabaseController.ChangeToken(token);
            SaveToken();

            LoadPlayerProfile();
        }

        return confirmation;
    }

    private static void LoadPlayerProfile()
    {
        // Create profile
        Profile = GetProfile();

        LoadingController.AddAction(() =>
        {
            DatabaseController.LoadProfileItems();
        });

        // Apply change to game
        LoadingController.onLoadingComplete += () =>
        {
            onChangeProperty?.Invoke(Profile);
        };

        // Start load game
        LoadingController.LoadAction();
    }

    #region PLAYER PROFILE

    public static System.Action<PlayerProfile> onChangeProperty;
    public static PlayerProfile Profile { get; private set; }

    public static PlayerProfile GetProfile()
    {
        // Get data
        var entity = DatabaseController.GetProfile();
        if (entity == null)
        {
            // Continue as guest
            return StaticData.SampleProfile;
        }
        else
        {
            // Load data from DB
            var profile = (ProfileEntity)entity;
            var id = profile.Id.ToString();
            var username = profile.Username;
            var password = profile.Password;
            var coin = profile.CoinAmount;
            var gem = profile.GemAmount;
            var level = profile.Level;
            var skinId = profile.SkinId;
            var cueenctXP = profile.CurrentXP;

            // Create new profile
            return new PlayerProfile(id, username, password, coin, gem, level, skinId, cueenctXP);
        }
    }

    #region UPDATE PLAYER PROFILE

    private static void IncreseLevel()
    {
        Profile.UpdateData(newLevelValue: Profile.Level + 1);

        DatabaseController.ChangeProfileLevel(Profile.Level + 1);

        // Apply change to game
        onChangeProperty?.Invoke(Profile);
    }

    public static void IncreseXP(float value)
    {
        float finalValue;
        if (Profile.XP.CurrentValue + value >= Profile.XP.MaximumValue)
        {
            IncreseLevel();
            finalValue = 0;
        }
        else
        {
            finalValue = Profile.XP.CurrentValue + value;
        }

        Profile.UpdateData(finalValue);
        DatabaseController.ChangeProfileXP(finalValue);

        // Apply change to game
        onChangeProperty?.Invoke(Profile);
    }

    public static void AddGemValue(int value)
    {
        Profile.UpdateData(newGemAmount: Profile.GemAmount + value);
        DatabaseController.ChangeProfileGemValue(Profile.GemAmount);

        // Apply change to game
        onChangeProperty?.Invoke(Profile);
    }

    public static void AddCoinValue(int value)
    {
        Profile.UpdateData(newCoinAmount: Profile.CoinAmount + value);
        DatabaseController.ChangeProfileCoinValue(Profile.CoinAmount);

        // Apply change to game
        onChangeProperty?.Invoke(Profile);
    }

    public static bool ChangeUsername(string newUsername)
    {
        // check in DB
        if (DatabaseController.HasUsername(newUsername))
        {
            Debug.LogError("This name is currently in use");
            return false;
        }
        // if accept, then
        Profile.UpdateData(newUsername: newUsername);
        DatabaseController.ChangeProfileUsername(newUsername);

        // Apply change to game
        onChangeProperty?.Invoke(Profile);

        return true;
    }

    public static void ChangePassword(string newPassword)
    {
        Profile.UpdateData(newPassword: newPassword);
        DatabaseController.ChangeProfilePassword(newPassword);

        // Apply change to game
        onChangeProperty?.Invoke(Profile);
    }

    #endregion

    #region ADD PLAYER ITEMS COLLECTION

    public static void LoadInventoryItem(List<Item> items)
    {
        foreach (var item in items)
        {
            Profile.AddInventoryItem(item);
        }
    }

    public static void LoadEquipmentItem(List<Equipment> equipment)
    {
        foreach (var equipmentItem in equipment)
        {
            Profile.EquipItem(equipmentItem);
            PlayerManager.EquipController.Equip(equipmentItem);
        }
    }

    #endregion

    #region UPDATE PLAYER ITEMS COLLECTION

    #region INVENTORY

    public static int BuyItem(Item item)
    {
        var code = Profile.BuyItem(item);
        if (code == ErrorCodes.acceptBuying)
        {
            UpdateCurrencyDatabaseValue(item);

            // Apply change to game
            onChangeProperty?.Invoke(Profile);
        }
        return code;
    }

    public static void SellItem(Item oldItem)
    {
        Profile.SellItem(oldItem);
        UpdateCurrencyDatabaseValue(oldItem);

        // Apply change to game
        onChangeProperty?.Invoke(Profile);
    }

    private static void UpdateCurrencyDatabaseValue(Item oldItem)
    {
        if (oldItem.CurrencyType == CurrencyType.Gold)
        {
            DatabaseController.ChangeProfileCoinValue(Profile.CoinAmount);
        }
        else if (oldItem.CurrencyType == CurrencyType.Gem)
        {
            DatabaseController.ChangeProfileGemValue(Profile.GemAmount);
        }
    }

    public static void AddInventoryItem(Item item)
    {
        Profile.AddInventoryItem(item);
        DatabaseController.AddItemToInventory(item.Id);
    }

    public static void RemoveInventoryItem(Item oldItem)
    {
        Profile.RemoveInventoryItem(oldItem);
        DatabaseController.RemoveItemFromInventory(oldItem.Id);
    }

    #endregion

    public static void EquipItem(Equipment newItem)
    {
        if (newItem.IsDefaultItem) return;
        Profile.EquipItem(newItem);
        DatabaseController.AddItemToEquipment(newItem.Id);
    }

    public static void UnequipItem(Equipment oldItem)
    {
        if (oldItem.IsDefaultItem) return;
        Profile.UnequipItem(oldItem);
        DatabaseController.RemoveItemFromEquipment(oldItem.Id);
    }

    #endregion

    #endregion
}
