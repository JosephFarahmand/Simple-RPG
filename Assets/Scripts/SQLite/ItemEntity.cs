namespace DataBank
{
    public struct SkinItemEntity
    {
        public SkinItemEntity(int itemId, int materialId)
        {
            Id = -1;
            ItemId = itemId;
            MaterialId = materialId;
        }

        public SkinItemEntity(int id, int itemId, int materialId)
        {
            Id = id;
            ItemId = itemId;
            MaterialId = materialId;
        }

        public int Id { get; private set; }
        public int ItemId { get; private set; }
        public int MaterialId { get; private set; }
    }

    public struct ResourceItemEntity
    {
        public ResourceItemEntity(int itemId, ResourceType resourceType, int value)
        {
            Id = -1;
            ItemId = itemId;
            ResourceType = resourceType;
            Value = value;
        }

        public ResourceItemEntity(int id, int itemId, ResourceType resourceType, int value)
        {
            Id = id;
            ItemId = itemId;
            ResourceType = resourceType;
            Value = value;
        }

        public int Id { get; private set; }
        public int ItemId { get; private set; }
        public ResourceType ResourceType { get; private set; }
        public int Value { get; private set; }
    }

    public struct EquipmentItemEntity
    {
        public EquipmentItemEntity(int itemId, EquipmentSlot equipSlot, int damageModifier, int armorModifier, int attackSpeedModifier)
        {
            Id = -1;
            ItemId = itemId;
            EquipSlot = equipSlot;
            DamageModifier = damageModifier;
            ArmorModifier = armorModifier;
            AttackSpeedModifier = attackSpeedModifier;
        }

        public EquipmentItemEntity(int id, int itemId, EquipmentSlot equipSlot, int damageModifier, int armorModifier, int attackSpeedModifier)
        {
            Id = id;
            ItemId = itemId;
            EquipSlot = (EquipmentSlot)equipSlot;
            DamageModifier = damageModifier;
            ArmorModifier = armorModifier;
            AttackSpeedModifier = attackSpeedModifier;
        }

        public int Id { get; private set; }
        public int ItemId { get; private set; }
        public EquipmentSlot EquipSlot { get; private set; }
        public int DamageModifier { get; private set; }
        public int ArmorModifier { get; private set; }
        public int AttackSpeedModifier { get; private set; }
    }

    public struct ItemEntity
    {
        public ItemEntity(string name, string iconPath, ItemType type, ItemRarity rarity, int requiredLevel, int price, int count, string assetId, CurrencyType currencyType) : this()
        {
            Name = name;
            IconPath = iconPath;
            Type = type;
            Rarity = rarity;
            RequiredLevel = requiredLevel;
            Price = price;
            Count = count;
            AssetId = assetId;
            CurrencyType = currencyType;
        }

        public ItemEntity(int id, string name, int type/*, bool isDefaultItem*/, int rarity, int requiredLevel, int price, int count, string assetId, string iconPath, CurrencyType currencyType)
        {
            Id = id;
            Name = name;
            Type = (ItemType)type;
            //IsDefaultItem = isDefaultItem;
            Rarity = (ItemRarity)rarity;
            RequiredLevel = requiredLevel;
            Price = price;
            Count = count;
            AssetId = assetId;
            IconPath = iconPath;
            CurrencyType = currencyType;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string IconPath { get; private set; }
        public ItemType Type { get; private set; }
        //public bool IsDefaultItem { get; private set; }
        public ItemRarity Rarity { get; private set; }
        public int RequiredLevel { get; private set; }
        public int Price { get; private set; }
        public CurrencyType CurrencyType { get; private set; }
        public int Count { get; private set; }
        public string AssetId { get; private set; }
    }

    public enum ItemType
    {
        Resource,
        Equipment,
        Skin
    }
}