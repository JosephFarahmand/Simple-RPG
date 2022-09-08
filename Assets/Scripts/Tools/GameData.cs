using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameData : MonoBehaviour
{
    private static GameData instance;

    [SerializeField] private List<Item> items = new List<Item>();

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

    public static void AddItem(Item item)
    {
        instance.items.Add(item);
    }

    public static Item GetItem(string id)
    {
        return instance.items.Find(x => x.Id == id);
    }

    public static List<Item> GetItems()
    {
        return instance.items;
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
