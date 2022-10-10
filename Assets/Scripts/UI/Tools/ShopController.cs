using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ShopController
{
    public static int Buying(Item item)
    {
        var code = AccountController.BuyItem(item);
        if (code == ErrorCodes.acceptBuying)
        {
            PlayerManager.InventoryController.Add(item);
        }
        return code;
    }

    public static int Selling(Item item)
    {
        AccountController.SellItem(item);
        PlayerManager.InventoryController.Remove(item);
        return ErrorCodes.acceptSelling;
    }
}

public static class ErrorCodes
{
    public static int notDefine { get; } = -1;

    //////////////////////////////////////////////////////////// SHOP
    public static int acceptBuying { get; } = 700;
    public static int acceptSelling { get; }= 701;
    public static int notRequiredLevel{ get; } = 702;
    public static int notEnoughCoin{ get; } = 703;
    public static int notEnoughGem { get; }= 704;
}


