using DataBank;
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
        DatabaseController.ChangeProfileGemValue(Profile.GemAmount + value);

        // Apply change to game
        onChangeProperty?.Invoke(Profile);
    }

    public static void AddCoinValue(int value)
    {
        Profile.UpdateData(newCoinAmount: Profile.CoinAmount + value);
        DatabaseController.ChangeProfileCoinValue(Profile.CoinAmount + value);

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

    #region UPDATE PLAYER ITEMS COLLECTION

    public static void AddInventoryItem(Item item)
    {
        Profile.AddInventoryItem(item);
    }

    public static void RemoveInventoryItem(Item oldItem)
    {
        Profile.RemoveInventoryItem(oldItem);
    }

    public static void EquipItem(Equipment newItem)
    {
        Profile.EquipItem(newItem);
    }

    public static void UnequipItem(Equipment oldItem)
    {
        Profile.UnequipItem(oldItem);
    }

    #endregion

    #endregion
}
