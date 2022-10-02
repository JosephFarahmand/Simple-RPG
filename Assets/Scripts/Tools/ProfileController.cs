using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileController : MonoBehaviour
{
    [SerializeField] private Material skinMaterial;

    public System.Action<PlayerProfile> onChangeProperty;

    PlayerProfile data;

    public PlayerProfile Data => data;

    public void Initialization()
    {
        LoadPlayerData();
    }

    private void LoadPlayerData()
    {
        // Create new player profile
        data = new PlayerProfile("", "Diana", 0, 0, 0, skinMaterial);

        // Load data from DB

        // Apply change to game
        onChangeProperty?.Invoke(data);
    }

    public void IncreseLevel()
    {
        data.UpdateData(newLevelValue: data.Level + 1);

        // Apply change to game
        onChangeProperty?.Invoke(data);
    }

    public void AddGemValue(int value)
    {
        data.UpdateData(newGemAmount: data.GemAmount + value);

        // Apply change to game
        onChangeProperty?.Invoke(data);
    }

    public void AddCoinValue(int value)
    {
        data.UpdateData(newCoinAmount: data.CoinAmount + value);

        // Apply change to game
        onChangeProperty?.Invoke(data);
    }

    public void ChangeName(string newName)
    {
        data.UpdateData(newName: newName);

        // Apply change to game
        onChangeProperty?.Invoke(data);
    }
}
