using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour, IController
{
    ErrorDatabase.ErrorEntity acceptBuying;
    ErrorDatabase.ErrorEntity acceptSelling;
    ErrorDatabase.ErrorEntity notRequiredLevel;
    ErrorDatabase.ErrorEntity notEnoughCoin;
    ErrorDatabase.ErrorEntity notEnoughGem;

    public ErrorDatabase.ErrorEntity NotRequiredLevel { get => notRequiredLevel;  }
    public ErrorDatabase.ErrorEntity NotEnoughCoin { get => notEnoughCoin;  }

    public void Initialization()
    {
        var buyyingEntity = GameManager.ErrorController.FindEntity(700);
        var sellingEntity = GameManager.ErrorController.FindEntity(701);
        var notRequiredLevelEntity = GameManager.ErrorController.FindEntity(702);
        var notEnoughCoinEntity = GameManager.ErrorController.FindEntity(703);
        var notEnoughGemEntity = GameManager.ErrorController.FindEntity(704);
        if (buyyingEntity.Code == 0 || sellingEntity.Code == 0 || notRequiredLevelEntity.Code == 0 || notEnoughCoinEntity.Code == 0 || notEnoughGemEntity.Code == 0)
        {
            Debug.LogError("Shopping entity not found!");
            return;
        }
        acceptBuying = (ErrorDatabase.ErrorEntity)buyyingEntity;
        acceptSelling = (ErrorDatabase.ErrorEntity)sellingEntity;
        notRequiredLevel = (ErrorDatabase.ErrorEntity)notRequiredLevelEntity;
        notEnoughCoin = (ErrorDatabase.ErrorEntity)notEnoughCoinEntity;
        notEnoughGem = (ErrorDatabase.ErrorEntity)notEnoughGemEntity;
    }

    public ErrorDatabase.ErrorEntity Buying(Item item)
    {
        if (AccountController.Profile.Level <= item.RequiredLevel)
        {
            if(AccountController.Profile.CoinAmount <= item.Price)
            {
                PlayerManager.InventoryController.Add(item);                
                return acceptBuying;
            }
            else
            {
                return notEnoughCoin;
            }
        }
        else
        {
            return notRequiredLevel;
        }
    }

    public ErrorDatabase.ErrorEntity Selling(Item item)
    {
        PlayerManager.InventoryController.Remove(item);
        return acceptSelling;
    }
}
