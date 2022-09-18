using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance; 

    [Header("Components")]
    [SerializeField] private EquipmentController equipController;
    [SerializeField] private InventoryController inventoryController;
    [SerializeField] private CharacterCombat combat;
    [SerializeField] private CharacterStats stats;
    [SerializeField] private SkinController skinController;

    public static EquipmentController EquipController => instance.equipController;
    public static InventoryController InventoryController => instance.inventoryController;
    public static CharacterCombat Combat => instance.combat;
    public static CharacterStats Stats => instance.stats;
    public static SkinController SkinController => instance.skinController;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        equipController.Initialization();
    }

    public static PlayerManager GetPlayer()
    {
        return instance;
    }
}