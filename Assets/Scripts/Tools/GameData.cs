using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameData : MonoBehaviour
{
    private static GameData instance;

    [SerializeField] private List<Equipment> equipment = new List<Equipment>();
    [SerializeField] private List<InteractableChest> chests = new List<InteractableChest>();

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
    }

    public static Equipment GetEquipmentItem(string id)
    {
        return instance.equipment.Find(x => x.Id == id);
    }

    public static List<Equipment> GetEquipmentItems()
    {
        return instance.equipment;
    }

    public static InteractableChest GetChest()
    {
        return instance.chests[Random.Range(0, instance.chests.Count)];
    }

    [SerializeField] private List<CardBackground> cardBackgrounds;

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
