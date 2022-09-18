using System.Collections.Generic;
using UnityEngine;

public class CharacterPreview : MonoBehaviour
{
    private List<EquipmentHandler> equipmentHandlers;
    EquipmentController playerEquipment;
    InventoryPage inventoryPage;
    private void Start()
    {
        playerEquipment = PlayerManager.EquipController;

        inventoryPage = UI_Manager.instance.GetPageOfType<InventoryPage>();

        equipmentHandlers = new List<EquipmentHandler>();
        var handlers = transform.root.GetComponentsInChildren<EquipmentHandler>(true);
        foreach (var handler in handlers)
        {
            if (handler.Item == null) continue;
            equipmentHandlers.Add(handler);
            handler.gameObject.SetActive(false);
        }

        playerEquipment.onEquipmentChanged += onChangePreview;
    }

    private void onChangePreview(Equipment newItem, Equipment oldItem)
    {
        var handler = equipmentHandlers.Find(x => x.Item == newItem);
        if (handler != null)
            handler.gameObject.SetActive(true);

        handler = equipmentHandlers.Find(x => x.Item == oldItem);
        if (handler != null)
            handler.gameObject.SetActive(false);
    }

    public float speed = 5.0f;

    private void Update()
    {
        if (inventoryPage.gameObject.activeSelf)
        {
            transform.Rotate(0.0f, -Input.GetAxis("Horizontal") * speed, 0.0f);
        }
    }
}
