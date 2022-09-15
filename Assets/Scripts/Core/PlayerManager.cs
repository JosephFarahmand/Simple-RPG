using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance; 

    [Header("Components")]
    [SerializeField] private EquipmentController playerEquipController;

    public static EquipmentController EquipController => instance.playerEquipController;

    private void Awake()
    {
        instance = this;
    }
}