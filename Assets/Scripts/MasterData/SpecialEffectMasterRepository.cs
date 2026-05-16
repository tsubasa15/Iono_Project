using System.Collections.Generic;

namespace Iono.Game.MasterData
{
    public sealed class SpecialEffectMasterRepository
    {
        private readonly MasterDataCache<SpecialEffectMasterData> cache;

        public SpecialEffectMasterRepository()
        {
            cache = new MasterDataCache<SpecialEffectMasterData>("SpecialEffect", "spEffectId", LoadAll, data => data.SpEffectId);
        }

        public IReadOnlyList<SpecialEffectMasterData> GetAll() => cache.GetAll();
        public bool TryGetById(string id, out SpecialEffectMasterData data) => cache.TryGetById(id, out data);
        public SpecialEffectMasterData GetById(string id) => cache.GetById(id);
        public bool Exists(string id) => cache.Exists(id);
        public void ClearCache() => cache.Clear();
        public bool TryGetBySpEffectId(string spEffectId, out SpecialEffectMasterData data) => TryGetById(spEffectId, out data);
        public SpecialEffectMasterData GetBySpEffectId(string spEffectId) => GetById(spEffectId);

        private static IReadOnlyList<SpecialEffectMasterData> LoadAll()
        {
            var items = new List<SpecialEffectMasterData>();
            Iono.MasterData.specialeffect.ForEachEntity(entity => items.Add(new SpecialEffectMasterData(entity.spEffectId, entity.name, entity.memo, entity.conditionType1, entity.conditionValue1, entity.damageCorrection1, entity.conditionType2, entity.conditionValue2, entity.damageCorrection2, entity.conditionType3, entity.conditionValue3, entity.damageCorrection3)));
            return items;
        }
    }
}
