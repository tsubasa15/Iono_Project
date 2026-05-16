using System.Collections.Generic;

namespace Iono.Game.MasterData
{
    public sealed class TypeMasterRepository
    {
        private readonly MasterDataCache<TypeMasterData> cache;

        public TypeMasterRepository()
        {
            cache = new MasterDataCache<TypeMasterData>("Type", "typeId", LoadAll, data => data.TypeId);
        }

        public IReadOnlyList<TypeMasterData> GetAll() => cache.GetAll();
        public bool TryGetById(string id, out TypeMasterData data) => cache.TryGetById(id, out data);
        public TypeMasterData GetById(string id) => cache.GetById(id);
        public bool Exists(string id) => cache.Exists(id);
        public void ClearCache() => cache.Clear();
        public bool TryGetByTypeId(string typeId, out TypeMasterData data) => TryGetById(typeId, out data);
        public TypeMasterData GetByTypeId(string typeId) => GetById(typeId);

        private static IReadOnlyList<TypeMasterData> LoadAll()
        {
            var items = new List<TypeMasterData>();
            Iono.MasterData.type.ForEachEntity(entity => items.Add(new TypeMasterData(entity.typeId, entity.name, entity.resourceId)));
            return items;
        }
    }
}
