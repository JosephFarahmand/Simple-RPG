using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameData : MonoBehaviour
{
    private static GameData instance;
    [SerializeField] private GameAnimations animations;

    public static GameAnimations Animations => instance.animations ?? instance.GetComponent<GameAnimations>();

    [SerializeField] private List<Equipment> equipment = new List<Equipment>();
    [SerializeField] private List<InteractableChest> chests = new List<InteractableChest>();

    [SerializeField] private List<CardBackground> cardBackgrounds;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        animations ??= GetComponent<GameAnimations>();
    }

    public static Equipment GetEquipmentItem(string id)
    {
        return instance.equipment.Find(x => x.Id == id);
    }

    public static List<Equipment> GetEquipmentItems()
    {
        return instance.equipment;
    }

    public static CharacterEquipment GetCharacterEquipment()
    {
        var beltId = instance.equipment.Where(obj => obj.equipSlot == EquipmentSlot.Belt).RandomItem().Id;
        var bottonId = instance.equipment.Where(obj => obj.equipSlot == EquipmentSlot.Legs).RandomItem().Id;
        var feetId = instance.equipment.Where(obj => obj.equipSlot == EquipmentSlot.Feet).RandomItem().Id;
        var handId = instance.equipment.Where(obj => obj.equipSlot == EquipmentSlot.Hands).RandomItem().Id;
        var helmetId = instance.equipment.Where(obj => obj.equipSlot == EquipmentSlot.Head).RandomItem().Id;
        var torsoId = instance.equipment.Where(obj => obj.equipSlot == EquipmentSlot.Chest).RandomItem().Id;
        var weaponId = instance.equipment.Where(obj => obj.equipSlot == EquipmentSlot.Weapon).RandomItem().Id;

        CharacterEquipment equipment = new CharacterEquipment(beltId, bottonId, feetId, handId, helmetId, torsoId, weaponId);

        return equipment;
    }

    public struct CharacterEquipment
    {
        public CharacterEquipment(string beltId, string bottonId, string feetId, string handId, string helmetId, string torsoId, string weaponId)
        {
            BeltId = beltId;
            BottonId = bottonId;
            FeetId = feetId;
            HandId = handId;
            HelmetId = helmetId;
            TorsoId = torsoId;
            WeaponId = weaponId;
        }

        public string BeltId {get; private set; }
        public string BottonId {get; private set; }
        public string FeetId {get; private set; }
        public string HandId {get; private set; }
        public string HelmetId {get; private set; }
        public string TorsoId {get; private set; }
        public string WeaponId {get; private set; }

        public List<string> ItemsId => new List<string> { BeltId, BottonId, FeetId, HandId, HelmetId, TorsoId , WeaponId };
    }

    public static InteractableChest GetChest()
    {
        return instance.chests[Random.Range(0, instance.chests.Count)];
    }


    public static CardBackground GetCardBackground(ItemType type)
    {
        var cardDetail = instance.cardBackgrounds.Find(x => x.ItemType == type);
        return cardDetail;
    }

    [System.Serializable]
    public struct CardBackground
    {
        [SerializeField] private ItemType itemType;
        [SerializeField] private Sprite frameSprite;
        [SerializeField] private Color backgroundColor;

        public ItemType ItemType { get => itemType;  }
        public Sprite FrameSprite { get => frameSprite; }
        public Color BackgroundColor { get => backgroundColor; }
    }
}
