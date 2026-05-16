using System.Collections.Generic;

namespace Iono.Game.MasterData
{
    public sealed class JankenMasterRepository
    {
        private readonly MasterDataCache<JankenMasterData> cache;

        public JankenMasterRepository()
        {
            cache = new MasterDataCache<JankenMasterData>("Janken", "jankenId", LoadAll, data => data.JankenId);
        }

        public IReadOnlyList<JankenMasterData> GetAll() => cache.GetAll();
        public bool TryGetById(string id, out JankenMasterData data) => cache.TryGetById(id, out data);
        public JankenMasterData GetById(string id) => cache.GetById(id);
        public bool Exists(string id) => cache.Exists(id);
        public void ClearCache() => cache.Clear();
        public bool TryGetByJankenId(string jankenId, out JankenMasterData data) => TryGetById(jankenId, out data);
        public JankenMasterData GetByJankenId(string jankenId) => GetById(jankenId);

        private static IReadOnlyList<JankenMasterData> LoadAll()
        {
            var items = new List<JankenMasterData>();
            Iono.MasterData.janken.ForEachEntity(entity => items.Add(new JankenMasterData(entity.jankenId, entity.name, entity.resourceId)));
            return items;
        }
    }
}
