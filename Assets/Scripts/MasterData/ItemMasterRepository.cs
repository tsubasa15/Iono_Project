using System.Collections.Generic;

namespace Iono.Game.MasterData
{
    public sealed class ItemMasterRepository
    {
        private readonly MasterDataCache<ItemMasterData> cache;

        public ItemMasterRepository()
        {
            cache = new MasterDataCache<ItemMasterData>("Item", "itemId", LoadAll, data => data.ItemId);
        }

        public IReadOnlyList<ItemMasterData> GetAll() => cache.GetAll();
        public bool TryGetById(string id, out ItemMasterData data) => cache.TryGetById(id, out data);
        public ItemMasterData GetById(string id) => cache.GetById(id);
        public bool Exists(string id) => cache.Exists(id);
        public void ClearCache() => cache.Clear();
        public bool TryGetByItemId(string itemId, out ItemMasterData data) => TryGetById(itemId, out data);
        public ItemMasterData GetByItemId(string itemId) => GetById(itemId);

        private static IReadOnlyList<ItemMasterData> LoadAll()
        {
            var items = new List<ItemMasterData>();
            Iono.MasterData.item.ForEachEntity(entity => items.Add(Convert(entity)));
            return items;
        }

        private static ItemMasterData Convert(Iono.MasterData.item entity)
        {
            var owner = $"Item itemId={MasterDataIdUtil.Display(entity.itemId)}";
            return new ItemMasterData(entity.itemId, entity.name, entity.nameTextId, entity.descTextId, entity.imageId, entity.rarity, entity.itemCategory, MasterDataIdUtil.ToSkillId(entity.skillId, owner, nameof(entity.skillId)));
        }
    }
}
