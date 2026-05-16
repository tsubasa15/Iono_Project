using System.Collections.Generic;

namespace Iono.Game.MasterData
{
    public sealed class BuffDebuffMasterRepository
    {
        private readonly MasterDataCache<BuffDebuffMasterData> cache;

        public BuffDebuffMasterRepository()
        {
            cache = new MasterDataCache<BuffDebuffMasterData>("BuffDebuff", "buffId", LoadAll, data => data.BuffId);
        }

        public IReadOnlyList<BuffDebuffMasterData> GetAll() => cache.GetAll();
        public bool TryGetById(string id, out BuffDebuffMasterData data) => cache.TryGetById(id, out data);
        public BuffDebuffMasterData GetById(string id) => cache.GetById(id);
        public bool Exists(string id) => cache.Exists(id);
        public void ClearCache() => cache.Clear();
        public bool TryGetByBuffId(string buffId, out BuffDebuffMasterData data) => TryGetById(buffId, out data);
        public BuffDebuffMasterData GetByBuffId(string buffId) => GetById(buffId);

        private static IReadOnlyList<BuffDebuffMasterData> LoadAll()
        {
            var items = new List<BuffDebuffMasterData>();
            Iono.MasterData.buffdebuff.ForEachEntity(entity => items.Add(Convert(entity)));
            return items;
        }

        private static BuffDebuffMasterData Convert(Iono.MasterData.buffdebuff entity)
        {
            var owner = $"BuffDebuff buffId={MasterDataIdUtil.Display(entity.buffId)}";
            return new BuffDebuffMasterData(entity.buffId, entity.name, entity.memo, entity.activationCondition, entity.tag1, entity.tag2, entity.gender, entity.activationRate, entity.turnCount, entity.effectTarget, entity.consumeTiming, entity.powerUpTiming, entity.maxStacks, entity.buffType1, entity.buffValue1, entity.specialEffectTag1, entity.valueModifierType1, MasterDataIdUtil.ToTypeIdList(entity.typeId, owner, nameof(entity.typeId)), entity.unerasableFlag);
        }
    }
}
