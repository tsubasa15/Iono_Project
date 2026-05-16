using System.Collections.Generic;

namespace Iono.Game.MasterData
{
    public sealed class EnemyUnitMasterRepository
    {
        private readonly MasterDataCache<EnemyUnitMasterData> cache;

        public EnemyUnitMasterRepository()
        {
            cache = new MasterDataCache<EnemyUnitMasterData>("EnemyUnit", "enemyId", LoadAll, data => data.EnemyId);
        }

        public IReadOnlyList<EnemyUnitMasterData> GetAll() => cache.GetAll();
        public bool TryGetById(string id, out EnemyUnitMasterData data) => cache.TryGetById(id, out data);
        public EnemyUnitMasterData GetById(string id) => cache.GetById(id);
        public bool Exists(string id) => cache.Exists(id);
        public void ClearCache() => cache.Clear();
        public bool TryGetByEnemyId(string enemyId, out EnemyUnitMasterData data) => TryGetById(enemyId, out data);
        public EnemyUnitMasterData GetByEnemyId(string enemyId) => GetById(enemyId);

        private static IReadOnlyList<EnemyUnitMasterData> LoadAll()
        {
            var items = new List<EnemyUnitMasterData>();
            Iono.MasterData.enemyunit.ForEachEntity(entity => items.Add(Convert(entity)));
            return items;
        }

        private static EnemyUnitMasterData Convert(Iono.MasterData.enemyunit entity)
        {
            var owner = $"EnemyUnit enemyId={MasterDataIdUtil.Display(entity.enemyId)}";
            var patternIds = new List<string>(3);
            MasterDataIdUtil.AddIfNotEmpty(patternIds, MasterDataIdUtil.ToPatternId(entity.patternId1, owner, nameof(entity.patternId1)));
            MasterDataIdUtil.AddIfNotEmpty(patternIds, MasterDataIdUtil.ToPatternId(entity.patternId2, owner, nameof(entity.patternId2)));
            MasterDataIdUtil.AddIfNotEmpty(patternIds, MasterDataIdUtil.ToPatternId(entity.patternId3, owner, nameof(entity.patternId3)));

            return new EnemyUnitMasterData(entity.enemyId, entity.name, entity.nameTextId, entity.descTextId, entity.memo, entity.imageId, entity.gender, entity.hp, entity.atk, entity.def, entity.spAtk, entity.spDef, entity.spd, entity.resPoison, entity.resSleep, entity.resParalysis, entity.resConfusion, entity.tag1, entity.tag2, entity.nWincount, MasterDataIdUtil.ToTypeId(entity.typeId, owner, nameof(entity.typeId)), MasterDataIdUtil.ToSkillId(entity.passiveSkillId, owner, nameof(entity.passiveSkillId)), patternIds.AsReadOnly());
        }
    }
}
