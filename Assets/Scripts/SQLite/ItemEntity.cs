namespace DataBank
{
    public struct ItemEntity
    {
        public ItemEntity(string name, string type, bool isDefaultItem, int rarity, int requiredLevel, int price, int count, string assetId)
        {
            Id = -1;
            Name = name;
            Type = type;
            IsDefaultItem = isDefaultItem;
            Rarity = rarity;
            RequiredLevel = requiredLevel;
            Price = price;
            Count = count;
            AssetId = assetId;
        }

        public ItemEntity(int id, string name, string type, bool isDefaultItem, int rarity, int requiredLevel, int price, int count, string assetId) : this(name, type, isDefaultItem, rarity, requiredLevel, price, count, assetId)
        {
            Id = id;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Type { get; private set; }
        public bool IsDefaultItem { get; private set; }
        public int Rarity { get; private set; }
        public int RequiredLevel { get; private set; }
        public int Price { get; private set; }
        public int Count { get; private set; }
        public string AssetId { get; private set; }
    }
}