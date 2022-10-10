using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;
using DataBank;

public class GameData : MonoBehaviour, IController
{
    //private static GameData instance;

    [Header("Components")]
    [SerializeField] private GameAnimations animations;
    public GameAnimations Animations => animations ?? GetComponent<GameAnimations>();

    [NaughtyAttributes.HorizontalLine]

    [Header("Item")]
    [SerializeField] private List<ItemPickup> itemsAsset;
    [SerializeField] private List<MaterialData> materials;
    [SerializeField] private List<InteractableChest> chests = new List<InteractableChest>();

    private List<Item> items = new List<Item>();
    private List<Equipment> equipment = new List<Equipment>();
    private List<Resource> resources = new List<Resource>();
    private List<SkinData> skins = new List<SkinData>();

    [Header("UI")]
    [SerializeField] private List<CardBackground> cardBackgrounds;

    [Header("Character")]
    [SerializeField] private List<Enemy> enemies;

    public void Initialization()
    {
        animations ??= GetComponent<GameAnimations>();
    }


    public void AddItem<T>(T newItem) where T : Item
    {
        if (newItem is Equipment newEquipment)
        {
            equipment.Add(newEquipment);
        }
        else if (newItem is SkinData newSkin)
        {
            skins.Add(newSkin);
        }
        else if (newItem is Resource newResource)
        {
            resources.Add(newResource);
        }

        items.Add(newItem);
    }

    public void SetItemModel<T>(T item) where T : Item
    {
        var asset = itemsAsset.Find(x => x.ID == item.AssetId);
        asset.SetItem(item);
    }

    public Item GetItem(string id)
    {
        return items.Find(x => x.Id == id);
    }

    public Material GetMaterial(string id)
    {
        return materials.Find(x => x.Id == id).Material;
    }

    public List<Item> GetItems()
    {
        var list = new List<Item>();
        list.AddRange(resources);
        list.AddRange(skins);
        list.AddRange(equipment);
        return list;
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

    public List<Resource> GetResourceItems()
    {
        return resources;
    }

    public CharacterEquipment GetCharacterEquipment()
    {
        var belt =  equipment.Where(obj => obj.equipSlot == EquipmentSlot.Belt).RandomItem();
        var botton =  equipment.Where(obj => obj.equipSlot == EquipmentSlot.Legs).RandomItem();
        var feet =  equipment.Where(obj => obj.equipSlot == EquipmentSlot.Feet).RandomItem();
        var hand =  equipment.Where(obj => obj.equipSlot == EquipmentSlot.Hands).RandomItem();
        var helmet =  equipment.Where(obj => obj.equipSlot == EquipmentSlot.Head).RandomItem();
        var torso =  equipment.Where(obj => obj.equipSlot == EquipmentSlot.Chest).RandomItem();
        var weapon =  equipment.Where(obj => obj.equipSlot == EquipmentSlot.Weapon).RandomItem();

        CharacterEquipment resualt = new CharacterEquipment(belt, botton, feet, hand, helmet, torso, weapon);

        return resualt;
    }

    public InteractableChest GetChest()
    {
        return  chests.RandomItem();
    }

    public Material GetSkinMaterial(string id)
    {
        var skin = skins.Find(x => x.Id == id);
        if (skin == null)
        {
            skin = skins.Find(x => x.Id == StaticData.defaultSkinId);
        }
        return skin.Material;
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
        public CharacterEquipment(Equipment belt, Equipment botton, Equipment feet, Equipment hand, Equipment helmet, Equipment torso, Equipment weapon)
        {
            Belt = belt;
            Botton = botton;
            Feet = feet;
            Hand = hand;
            Helmet = helmet;
            Torso = torso;
            Weapon = weapon;

            Equipment = new List<Equipment>
            {
                belt,
                botton,
                feet,
                hand,
                helmet,
                torso,
                weapon
            };
        }

        public Equipment Belt { get; private set; }
        public Equipment Botton { get; private set; }
        public Equipment Feet { get; private set; }
        public Equipment Hand { get; private set; }
        public Equipment Helmet { get; private set; }
        public Equipment Torso { get; private set; }
        public Equipment Weapon { get; private set; }

        public List<Equipment> Equipment { get; private set; }
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
