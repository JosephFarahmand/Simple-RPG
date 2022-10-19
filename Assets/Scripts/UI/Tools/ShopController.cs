using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ShopController
{
    public static int Buying(Item item)
    {
        if (AccountController.Profile.Level < item.RequiredLevel) return ErrorCodes.notRequiredLevel;

        switch (item.CurrencyType)
        {
            case CurrencyType.Gold:
                if (AccountController.Profile.CoinAmount < item.Price)
                {
                    return ErrorCodes.notEnoughCoin;
                }
                AccountController.AddCoinValue(-item.Price);
                break;
            case CurrencyType.Gem:
                if (AccountController.Profile.GemAmount < item.Price)
                {
                    return ErrorCodes.notEnoughGem;
                }
                AccountController.AddGemValue(-item.Price);
                break;
            case CurrencyType.Dollar:
                return ErrorCodes.notDefine;
            default:
                return ErrorCodes.notDefine;
        }

        PlayerManager.InventoryController.Add(item);
        return ErrorCodes.acceptBuying;
    }

    public static int Selling(Item item)
    {
        var code = AccountController.SellItem(item);
        if (code == ErrorCodes.acceptSelling)
        {
            PlayerManager.InventoryController.Remove(item);
        }
        return code;
    }
}


