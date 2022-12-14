using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShortcuts : MonoBehaviour
{
    private InventoryPage inventoryPage;

    [Header("Keys")]
    [SerializeField] private KeyCode backPressed = KeyCode.Escape;


    private void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            UI_Manager.instance.OpenPage(inventoryPage);
        }

        if (Input.GetKeyDown(backPressed))
        {
            UI_Manager.instance.OnBackPressed();
        }
    }

    public void Initialization()
    {
        inventoryPage = UI_Manager.instance.GetPageOfType<InventoryPage>();
    }
}
