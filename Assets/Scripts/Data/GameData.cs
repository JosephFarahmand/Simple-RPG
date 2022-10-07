using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;

public class GameData : MonoBehaviour, IController
{
    //private static GameData instance;

    [Header("Components")]
    [SerializeField] private GameAnimations animations;
    public GameAnimations Animations => animations ?? GetComponent<GameAnimations>();

    [NaughtyAttributes.HorizontalLine]

    [Header("Item")]
    [SerializeField] private List<Equipment> equipment = new List<Equipment>();
    [SerializeField] private List<InteractableChest> chests = new List<InteractableChest>();

    [Header("UI")]
    [SerializeField] private List<CardBackground> cardBackgrounds;

    [Header("Character")]
    [SerializeField] private List<Enemy> enemies;

    public void Initialization()
    {
        animations ??= GetComponent<GameAnimations>();
    }

    #region Item

    public Equipment GetEquipmentItem(string id)
    {
        return equipment.Find(x => x.Id == id);
    }

    public List<Equipment> GetEquipmentItems()
    {
        return equipment;
    }

    public CharacterEquipment GetCharacterEquipment()
    {
        var beltId =  equipment.Where(obj => obj.equipSlot == EquipmentSlot.Belt).RandomItem().Id;
        var bottonId =  equipment.Where(obj => obj.equipSlot == EquipmentSlot.Legs).RandomItem().Id;
        var feetId =  equipment.Where(obj => obj.equipSlot == EquipmentSlot.Feet).RandomItem().Id;
        var handId =  equipment.Where(obj => obj.equipSlot == EquipmentSlot.Hands).RandomItem().Id;
        var helmetId =  equipment.Where(obj => obj.equipSlot == EquipmentSlot.Head).RandomItem().Id;
        var torsoId =  equipment.Where(obj => obj.equipSlot == EquipmentSlot.Chest).RandomItem().Id;
        var weaponId =  equipment.Where(obj => obj.equipSlot == EquipmentSlot.Weapon).RandomItem().Id;

        CharacterEquipment resualt = new CharacterEquipment(beltId, bottonId, feetId, handId, helmetId, torsoId, weaponId);

        return resualt;
    }

    public InteractableChest GetChest()
    {
        return  chests.RandomItem();
    }

    #endregion

    #region User Interface

    public CardBackground GetCardBackground(ItemRarity type)
    {
        var cardDetail =  cardBackgrounds.Find(x => x.ItemType == type);
        return cardDetail;
    }

    #endregion

    #region Character

    /// <summary>
    /// 
    /// </summary>
    /// <returns>A enemy prefab</returns>
    public Enemy GetRandomEnemy()
    {
        return  enemies.RandomItem();
    }

    #endregion

    #region STRUCTURE

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

        public string BeltId { get; private set; }
        public string BottonId { get; private set; }
        public string FeetId { get; private set; }
        public string HandId { get; private set; }
        public string HelmetId { get; private set; }
        public string TorsoId { get; private set; }
        public string WeaponId { get; private set; }

        public List<string> ItemsId => new List<string> { BeltId, BottonId, FeetId, HandId, HelmetId, TorsoId, WeaponId };
    }

    [Serializable]
    public struct CardBackground
    {
        [SerializeField] private ItemRarity itemType;
        [SerializeField] private Sprite frameSprite;
        [SerializeField] private Color backgroundColor;

        public ItemRarity ItemType { get => itemType;  }
        public Sprite FrameSprite { get => frameSprite; }
        public Color BackgroundColor { get => backgroundColor; }
    }

    #endregion
}
