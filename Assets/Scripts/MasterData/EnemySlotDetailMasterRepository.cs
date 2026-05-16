using System.Collections.Generic;

namespace Iono.Game.MasterData
{
    public sealed class EnemySlotDetailMasterRepository
    {
        private readonly MasterDataCache<EnemySlotDetailMasterData> cache;

        public EnemySlotDetailMasterRepository()
        {
            cache = new MasterDataCache<EnemySlotDetailMasterData>("EnemySlotDetail", "enemyslotId", LoadAll, data => data.EnemySlotId);
        }

        public IReadOnlyList<EnemySlotDetailMasterData> GetAll() => cache.GetAll();
        public bool TryGetById(string id, out EnemySlotDetailMasterData data) => cache.TryGetById(id, out data);
        public EnemySlotDetailMasterData GetById(string id) => cache.GetById(id);
        public bool Exists(string id) => cache.Exists(id);
        public void ClearCache() => cache.Clear();
        public bool TryGetByEnemySlotId(string enemySlotId, out EnemySlotDetailMasterData data) => TryGetById(enemySlotId, out data);
        public EnemySlotDetailMasterData GetByEnemySlotId(string enemySlotId) => GetById(enemySlotId);

        private static IReadOnlyList<EnemySlotDetailMasterData> LoadAll()
        {
            var items = new List<EnemySlotDetailMasterData>();
            Iono.MasterData.enemyslotdetail.ForEachEntity(entity => items.Add(Convert(entity)));
            return items;
        }

        private static EnemySlotDetailMasterData Convert(Iono.MasterData.enemyslotdetail entity)
        {
            var owner = $"EnemySlotDetail enemyslotId={MasterDataIdUtil.Display(entity.enemyslotId)}";
            return new EnemySlotDetailMasterData(entity.enemyslotId, entity.name, entity.memo, entity.recon_frag, MasterDataIdUtil.ToSkillId(entity.skillId, owner, nameof(entity.skillId)));
        }
    }
}
