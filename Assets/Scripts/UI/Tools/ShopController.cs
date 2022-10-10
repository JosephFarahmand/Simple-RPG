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


