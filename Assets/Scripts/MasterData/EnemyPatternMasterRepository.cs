using System.Collections.Generic;

namespace Iono.Game.MasterData
{
    public sealed class EnemyPatternMasterRepository
    {
        private readonly MasterDataCache<EnemyPatternMasterData> cache;

        public EnemyPatternMasterRepository()
        {
            cache = new MasterDataCache<EnemyPatternMasterData>("EnemyPattern", "patternId", LoadAll, data => data.PatternId);
        }

        public IReadOnlyList<EnemyPatternMasterData> GetAll() => cache.GetAll();
        public bool TryGetById(string id, out EnemyPatternMasterData data) => cache.TryGetById(id, out data);
        public EnemyPatternMasterData GetById(string id) => cache.GetById(id);
        public bool Exists(string id) => cache.Exists(id);
        public void ClearCache() => cache.Clear();
        public bool TryGetByPatternId(string patternId, out EnemyPatternMasterData data) => TryGetById(patternId, out data);
        public EnemyPatternMasterData GetByPatternId(string patternId) => GetById(patternId);

        private static IReadOnlyList<EnemyPatternMasterData> LoadAll()
        {
            var items = new List<EnemyPatternMasterData>();
            Iono.MasterData.enemypattern.ForEachEntity(entity => items.Add(Convert(entity)));
            return items;
        }

        private static EnemyPatternMasterData Convert(Iono.MasterData.enemypattern entity)
        {
            var owner = $"EnemyPattern patternId={MasterDataIdUtil.Display(entity.patternId)}";
            var slotIds = new List<string>(5);
            MasterDataIdUtil.AddIfNotEmpty(slotIds, MasterDataIdUtil.ToEnemySlotId(entity.slotId_1, owner, nameof(entity.slotId_1)));
            MasterDataIdUtil.AddIfNotEmpty(slotIds, MasterDataIdUtil.ToEnemySlotId(entity.slotId_2, owner, nameof(entity.slotId_2)));
            MasterDataIdUtil.AddIfNotEmpty(slotIds, MasterDataIdUtil.ToEnemySlotId(entity.slotId_3, owner, nameof(entity.slotId_3)));
            MasterDataIdUtil.AddIfNotEmpty(slotIds, MasterDataIdUtil.ToEnemySlotId(entity.slotId_4, owner, nameof(entity.slotId_4)));
            MasterDataIdUtil.AddIfNotEmpty(slotIds, MasterDataIdUtil.ToEnemySlotId(entity.slotId_5, owner, nameof(entity.slotId_5)));
            return new EnemyPatternMasterData(entity.patternId, entity.name, slotIds.AsReadOnly());
        }
    }
}
