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
    //[SerializeField] private ProfileController profile;
    [SerializeField] private CharacterAnimation animationController;

    public static EquipmentController EquipController => instance.equipController;
    public static InventoryController InventoryController => instance.inventoryController;
    public static CharacterCombat Combat => instance.combat;
    public static CharacterStats Stats => instance.stats;
    public static PlayerCustomizer SkinCustomizer => instance.skinCustomizer;
    //public static ProfileController Profile => instance.profile;

    private void Awake()
    {
        instance = this;
    }

    public void Initialization()
    {
        inventoryController.onAddNewItem += AccountController.AddInventoryItem;
        inventoryController.onRemoveItem += AccountController.RemoveInventoryItem;

        equipController.onEquipmentChanged += (newItem, oldItem) =>
        {
            if (newItem != null)
            {
                AccountController.EquipItem(newItem);
            }

            if (oldItem != null)
            {
                AccountController.UnequipItem(oldItem);
            }
        };

        equipController.Initialization();
        skinCustomizer.Initialization();

        //profile.Initialization();

        animationController.Initialization();
        stats.Initialization();

        
    }

    private void Start()
    {
        
    }

    public static PlayerManager GetPlayer()
    {
        return instance;
    }
}