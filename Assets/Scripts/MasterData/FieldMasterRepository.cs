using System.Collections.Generic;

namespace Iono.Game.MasterData
{
    public sealed class FieldMasterRepository
    {
        private readonly MasterDataCache<FieldMasterData> cache;

        public FieldMasterRepository()
        {
            cache = new MasterDataCache<FieldMasterData>("Field", "fieldId", LoadAll, data => data.FieldId);
        }

        public IReadOnlyList<FieldMasterData> GetAll() => cache.GetAll();
        public bool TryGetById(string id, out FieldMasterData data) => cache.TryGetById(id, out data);
        public FieldMasterData GetById(string id) => cache.GetById(id);
        public bool Exists(string id) => cache.Exists(id);
        public void ClearCache() => cache.Clear();
        public bool TryGetByFieldId(string fieldId, out FieldMasterData data) => TryGetById(fieldId, out data);
        public FieldMasterData GetByFieldId(string fieldId) => GetById(fieldId);

        private static IReadOnlyList<FieldMasterData> LoadAll()
        {
            var items = new List<FieldMasterData>();
            Iono.MasterData.field.ForEachEntity(entity => items.Add(Convert(entity)));
            return items;
        }

        private static FieldMasterData Convert(Iono.MasterData.field entity)
        {
            var owner = $"Field fieldId={MasterDataIdUtil.Display(entity.fieldId)}";
            return new FieldMasterData(entity.fieldId, entity.name, entity.memo, entity.preventOverwriteFlag, entity.duration, entity.healEndTurnRate, entity.plusDamageRate, entity.minusDamageRate, MasterDataIdUtil.ToTypeId(entity.plusDamageTypeId, owner, nameof(entity.plusDamageTypeId)), MasterDataIdUtil.ToTypeId(entity.minusDamageTypeId, owner, nameof(entity.minusDamageTypeId)), MasterDataIdUtil.ToConditionId(entity.negatedConditionId, owner, nameof(entity.negatedConditionId)), MasterDataIdUtil.ToSkillId(entity.negatedSkillId, owner, nameof(entity.negatedSkillId)), entity.preventPriorityFlag);
        }
    }
}
