using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    //private static PlayerData instance;

    public static System.Action<PlayerProfile> onChangeProperty;

    PlayerProfile profile;

    public PlayerProfile Profile => profile;

    private void Awake()
    {
        LoadPlayerData();
    }

    private void LoadPlayerData()
    {
        // Create new player profile
        profile = new PlayerProfile("","Diana",0,0,0);

        // Load data from DB

        // Apply change to game
        onChangeProperty?.Invoke(profile);
    }

    public void IncreseLevel()
    {
        profile.UpdateData(newLevelValue: profile.Level + 1);

        // Apply change to game
        onChangeProperty?.Invoke(profile);
    }

    public void AddGemValue(int value)
    {
        profile.UpdateData(newGemAmount: profile.GemAmount + value);

        // Apply change to game
        onChangeProperty?.Invoke(profile);
    }

    public void AddCoinValue(int value)
    {
        profile.UpdateData(newCoinAmount: profile.CoinAmount + value);

        // Apply change to game
        onChangeProperty?.Invoke(profile);
    }

    public void ChangeName(string newName)
    {
        profile.UpdateData(newName: newName);

        // Apply change to game
        onChangeProperty?.Invoke(profile);
    }
}
