namespace DataBank
{
    public struct ItemCollectionEntity
    {
        public ItemCollectionEntity(int profileId, string itemId) : this(-1, profileId, itemId)
        {
        }

        public ItemCollectionEntity(int id, int profileId, string itemId) : this()
        {
            Id = id;
            ProfileId = profileId;
            ItemId = itemId;
        }

        public int Id { get; private set; }
        public int ProfileId { get; private set; }
        public string ItemId { get; private set; }

        public bool Equals(ItemCollectionEntity entity)
        {
            return entity.ItemId == ItemId && entity.ProfileId == ProfileId;
        }
    }
}