using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public InventoryPage inventoryDialog;

    private void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryDialog.gameObject.SetActive(!inventoryDialog.gameObject.activeSelf);
        }
    }
}
