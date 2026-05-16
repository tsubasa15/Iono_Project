using System.Collections.Generic;

namespace Iono.Game.MasterData
{
    public sealed class TypeCompatibilityMasterRepository
    {
        private readonly MasterDataCache<TypeCompatibilityMasterData> cache;

        public TypeCompatibilityMasterRepository()
        {
            cache = new MasterDataCache<TypeCompatibilityMasterData>("TypeCompatibility", "typeId", LoadAll, data => data.TypeId);
        }

        public IReadOnlyList<TypeCompatibilityMasterData> GetAll() => cache.GetAll();
        public bool TryGetById(string id, out TypeCompatibilityMasterData data) => cache.TryGetById(id, out data);
        public TypeCompatibilityMasterData GetById(string id) => cache.GetById(id);
        public bool Exists(string id) => cache.Exists(id);
        public void ClearCache() => cache.Clear();
        public bool TryGetByTypeId(string typeId, out TypeCompatibilityMasterData data) => TryGetById(typeId, out data);
        public TypeCompatibilityMasterData GetByTypeId(string typeId) => GetById(typeId);

        private static IReadOnlyList<TypeCompatibilityMasterData> LoadAll()
        {
            var items = new List<TypeCompatibilityMasterData>();
            Iono.MasterData.typeCompatibility.ForEachEntity(entity => items.Add(new TypeCompatibilityMasterData(entity.typeId, entity.name, entity.type_Normal, entity.type_Fire, entity.type_Water, entity.type_Electric, entity.type_Grass, entity.type_Ice, entity.type_Fighting, entity.type_Poison, entity.type_Ground, entity.type_Flying, entity.type_Psychic, entity.type_Bug, entity.type_Rock, entity.type_Ghost, entity.type_Dragon, entity.type_Dark, entity.type_Steel, entity.type_Fairy)));
            return items;
        }
    }
}
