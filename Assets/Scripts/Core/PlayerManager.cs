using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance; 

    [Header("Components")]
    [SerializeField] private EquipmentController equipController;
    [SerializeField] private InventoryController inventoryController;

    public static EquipmentController EquipController => instance.equipController;

    public static InventoryController InventoryController => instance.inventoryController;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        equipController.Initialization();
    }
}