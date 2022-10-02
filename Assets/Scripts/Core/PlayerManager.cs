using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance; 

    [Header("Components")]
    [SerializeField] private EquipmentController equipController;
    [SerializeField] private InventoryController inventoryController;
    [SerializeField] private CharacterCombat combat;
    [SerializeField] private CharacterStats stats;
    [SerializeField] private PlayerCustomizer skinCustomizer;
    [SerializeField] private PlayerData profileData;

    public static EquipmentController EquipController => instance.equipController;
    public static InventoryController InventoryController => instance.inventoryController;
    public static CharacterCombat Combat => instance.combat;
    public static CharacterStats Stats => instance.stats;
    public static PlayerCustomizer SkinCustomizer => instance.skinCustomizer;
    public static PlayerData ProfileData => instance.profileData;

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