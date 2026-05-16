using System.Collections.Generic;

namespace Iono.Game.MasterData
{
    public sealed class AbnormalConditionMasterRepository
    {
        private readonly MasterDataCache<AbnormalConditionMasterData> cache;

        public AbnormalConditionMasterRepository()
        {
            cache = new MasterDataCache<AbnormalConditionMasterData>("AbnormalCondition", "conditionId", LoadAll, data => data.ConditionId);
        }

        public IReadOnlyList<AbnormalConditionMasterData> GetAll() => cache.GetAll();
        public bool TryGetById(string id, out AbnormalConditionMasterData data) => cache.TryGetById(id, out data);
        public AbnormalConditionMasterData GetById(string id) => cache.GetById(id);
        public bool Exists(string id) => cache.Exists(id);
        public void ClearCache() => cache.Clear();
        public bool TryGetByConditionId(string conditionId, out AbnormalConditionMasterData data) => TryGetById(conditionId, out data);
        public AbnormalConditionMasterData GetByConditionId(string conditionId) => GetById(conditionId);

        private static IReadOnlyList<AbnormalConditionMasterData> LoadAll()
        {
            var items = new List<AbnormalConditionMasterData>();
            Iono.MasterData.abnormalcondition.ForEachEntity(entity => items.Add(Convert(entity)));
            return items;
        }

        private static AbnormalConditionMasterData Convert(Iono.MasterData.abnormalcondition entity)
        {
            var owner = $"AbnormalCondition conditionId={MasterDataIdUtil.Display(entity.conditionId)}";
            return new AbnormalConditionMasterData(entity.conditionId, entity.name, entity.memo, entity.conditionCategory, entity.probability, entity.activationCondition, entity.gender, entity.tag1, entity.tag2, entity.effectTarget, entity.consumeTiming, entity.turnCount, entity.effectType1, entity.effectValue1, entity.effectType2, entity.effectValue2, entity.effectType3, entity.effectValue3, entity.poisonHpDamageRate, entity.paralysisActionRate, entity.confusionActionRate, entity.burnHpDamageRate, entity.burnAtkReduceRate, entity.doomTurnCount, MasterDataIdUtil.ToTypeId(entity.typeId, owner, nameof(entity.typeId)), MasterDataIdUtil.ToTypeId(entity.attachedBuffId1, owner, nameof(entity.attachedBuffId1)), MasterDataIdUtil.ToTypeId(entity.attachedBuffId2, owner, nameof(entity.attachedBuffId2)));
        }
    }
}
