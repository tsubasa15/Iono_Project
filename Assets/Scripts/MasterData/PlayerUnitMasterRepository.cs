using System.Collections.Generic;
using UnityEngine;

namespace Iono.Game.MasterData
{
    public sealed class PlayerUnitMasterRepository
    {
        private readonly MasterDataCache<PlayerUnitMasterData> cache;

        public PlayerUnitMasterRepository()
        {
            cache = new MasterDataCache<PlayerUnitMasterData>("PlayerUnit", "unitId", LoadAll, data => data.UnitId);
        }

        public IReadOnlyList<PlayerUnitMasterData> GetAll() => cache.GetAll();
        public bool TryGetById(string id, out PlayerUnitMasterData data) => cache.TryGetById(id, out data);
        public PlayerUnitMasterData GetById(string id) => cache.GetById(id);
        public bool Exists(string id) => cache.Exists(id);
        public void ClearCache() => cache.Clear();
        public bool TryGetByUnitId(string unitId, out PlayerUnitMasterData data) => TryGetById(unitId, out data);
        public PlayerUnitMasterData GetByUnitId(string unitId) => GetById(unitId);

        private static IReadOnlyList<PlayerUnitMasterData> LoadAll()
        {
            var items = new List<PlayerUnitMasterData>();
            Iono.MasterData.playerunit.ForEachEntity(entity => items.Add(Convert(entity)));
            return items;
        }

        private static PlayerUnitMasterData Convert(Iono.MasterData.playerunit entity)
        {
            var owner = $"PlayerUnit unitId={MasterDataIdUtil.Display(entity.unitId)}";
            var skillIds = new List<string>(4);
            MasterDataIdUtil.AddIfNotEmpty(skillIds, MasterDataIdUtil.ToSkillId(entity.skillId1, owner, nameof(entity.skillId1)));
            MasterDataIdUtil.AddIfNotEmpty(skillIds, MasterDataIdUtil.ToSkillId(entity.skillId2, owner, nameof(entity.skillId2)));
            MasterDataIdUtil.AddIfNotEmpty(skillIds, MasterDataIdUtil.ToSkillId(entity.skillId3, owner, nameof(entity.skillId3)));
            MasterDataIdUtil.AddIfNotEmpty(skillIds, MasterDataIdUtil.ToSkillId(entity.skillId4, owner, nameof(entity.skillId4)));

            if (entity.skillCount != skillIds.Count)
                Debug.LogWarning($"PlayerUnit master skillCount mismatch. unitId={MasterDataIdUtil.Display(entity.unitId)}, skillCount={entity.skillCount}, actualSkillIds={skillIds.Count}");

            return new PlayerUnitMasterData(entity.unitId, entity.name, entity.nameTextId, entity.descTextId, entity.memo, entity.imageId, entity.rarity, entity.gender, MasterDataIdUtil.ToTypeId(entity.typeId, owner, nameof(entity.typeId)), MasterDataIdUtil.ToSkillId(entity.passiveSkillId, owner, nameof(entity.passiveSkillId)), entity.skillCount, skillIds.AsReadOnly(), entity.maxLevel, entity.hp, entity.atk, entity.def, entity.spAtk, entity.spDef, entity.spd, entity.resPoison, entity.resSleep, entity.resParalysis, entity.resConfusion, entity.tag1, entity.tag2);
        }
    }
}
