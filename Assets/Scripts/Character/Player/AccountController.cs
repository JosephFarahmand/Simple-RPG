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

    #region UPDATE PROFILE PROPERTY

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

    #region PLAYER ITEMS COLLECTION

    #region LOAD

    public static void LoadInventoryItem(List<Item> items)
    {
        InventoryItems = items;
    }

    public static void LoadEquipmentItem(List<Equipment> equipment)
    {
        foreach (var equipmentItem in equipment)
        {
            //Profile.EquipItem(equipmentItem);
            PlayerManager.EquipController.Equip(equipmentItem);
        }
    }

    #endregion

    #region SHOPPING

    public static int BuyItem(Item item)
    {
        if (Profile.Level < item.RequiredLevel) return ErrorCodes.notRequiredLevel;

        switch (item.CurrencyType)
        {
            case CurrencyType.Gold:
                if (Profile.CoinAmount < item.Price)
                {
                    return ErrorCodes.notEnoughCoin;
                }
                AddCoinValue(-item.Price);
                break;
            case CurrencyType.Gem:
                if (Profile.GemAmount < item.Price)
                {
                    return ErrorCodes.notEnoughGem;
                }
                AddGemValue(-item.Price);
                break;
            case CurrencyType.Dollar:
                return ErrorCodes.notDefine;
            default:
                return ErrorCodes.notDefine;
        }

        return ErrorCodes.acceptBuying;
    }

    public static int SellItem(Item oldItem)
    {
        switch (oldItem.CurrencyType)
        {
            case CurrencyType.Gold:
                AddCoinValue(oldItem.Price);
                break;
            case CurrencyType.Gem:
                AddGemValue(oldItem.Price);
                break;
            default:
                return ErrorCodes.notDefine;
        }
        return ErrorCodes.acceptSelling;
    }

    #endregion

    #region INVENTORY

    public static List<Item> InventoryItems { get; private set; } = new List<Item>();

    public static int InventoryFullSpace => InventoryItems.Count;

    public static void AddInventoryItem(Item item)
    {
        InventoryItems.Add(item);
        DatabaseController.AddItemToInventory(item.Id);
    }

    public static void RemoveInventoryItem(Item oldItem)
    {
        if (InventoryItems.Contains(oldItem))
        {
            InventoryItems.Remove(oldItem);
            DatabaseController.RemoveItemFromInventory(oldItem.Id);
        }
    }

    #endregion

    #region EQUIPMENT

    public static List<Item> EquipedItems { get; private set; } = new List<Item>();

    public static void EquipItem(Equipment newItem)
    {
        if (newItem.IsDefaultItem) return;
        //Profile.EquipItem(newItem);
        EquipedItems.Add(newItem);
        DatabaseController.AddItemToEquipment(newItem.Id);
    }

    public static void UnequipItem(Equipment oldItem)
    {
        if (oldItem.IsDefaultItem) return;
        EquipedItems.Remove(oldItem);
        //Profile.UnequipItem(oldItem);
        DatabaseController.RemoveItemFromEquipment(oldItem.Id);
    }

    #endregion

    #endregion

    #endregion
}
