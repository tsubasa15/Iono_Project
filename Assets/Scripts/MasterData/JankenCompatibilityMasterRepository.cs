using System.Collections.Generic;

namespace Iono.Game.MasterData
{
    public sealed class JankenCompatibilityMasterRepository
    {
        private readonly MasterDataCache<JankenCompatibilityMasterData> cache;

        public JankenCompatibilityMasterRepository()
        {
            cache = new MasterDataCache<JankenCompatibilityMasterData>("JankenCompatibility", "jankenId", LoadAll, data => data.JankenId);
        }

        public IReadOnlyList<JankenCompatibilityMasterData> GetAll() => cache.GetAll();
        public bool TryGetById(string id, out JankenCompatibilityMasterData data) => cache.TryGetById(id, out data);
        public JankenCompatibilityMasterData GetById(string id) => cache.GetById(id);
        public bool Exists(string id) => cache.Exists(id);
        public void ClearCache() => cache.Clear();
        public bool TryGetByJankenId(string jankenId, out JankenCompatibilityMasterData data) => TryGetById(jankenId, out data);
        public JankenCompatibilityMasterData GetByJankenId(string jankenId) => GetById(jankenId);

        private static IReadOnlyList<JankenCompatibilityMasterData> LoadAll()
        {
            var items = new List<JankenCompatibilityMasterData>();
            Iono.MasterData.jankenCompatibility.ForEachEntity(entity => items.Add(new JankenCompatibilityMasterData(entity.jankenId, entity.name, entity.jan_Rock, entity.jan_Paper, entity.jan_Scissors, entity.jan_Infinity)));
            return items;
        }
    }
}
