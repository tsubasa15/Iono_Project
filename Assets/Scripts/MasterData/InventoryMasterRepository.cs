using System.Collections.Generic;

namespace Iono.Game.MasterData
{
    public sealed class InventoryMasterRepository
    {
        private readonly MasterDataCache<InventoryMasterData> cache;

        public InventoryMasterRepository()
        {
            cache = new MasterDataCache<InventoryMasterData>("Inventory", "inventoryId", LoadAll, data => data.InventoryId);
        }

        public IReadOnlyList<InventoryMasterData> GetAll() => cache.GetAll();
        public bool TryGetById(string id, out InventoryMasterData data) => cache.TryGetById(id, out data);
        public InventoryMasterData GetById(string id) => cache.GetById(id);
        public bool Exists(string id) => cache.Exists(id);
        public void ClearCache() => cache.Clear();
        public bool TryGetByInventoryId(string inventoryId, out InventoryMasterData data) => TryGetById(inventoryId, out data);
        public InventoryMasterData GetByInventoryId(string inventoryId) => GetById(inventoryId);

        private static IReadOnlyList<InventoryMasterData> LoadAll()
        {
            var items = new List<InventoryMasterData>();
            Iono.MasterData.inventory.ForEachEntity(entity => items.Add(Convert(entity)));
            return items;
        }

        private static InventoryMasterData Convert(Iono.MasterData.inventory entity)
        {
            var owner = $"Inventory inventoryId={MasterDataIdUtil.Display(entity.inventoryId)}";
            return new InventoryMasterData(entity.inventoryId, entity.name, MasterDataIdUtil.ToBuffId(entity.buffdebuffID, owner, nameof(entity.buffdebuffID)), MasterDataIdUtil.ToSpecialEffectId(entity.spEffectId, owner, nameof(entity.spEffectId)));
        }
    }
}
