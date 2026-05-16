using System.Collections.Generic;

namespace Iono.Game.MasterData
{
    public sealed class SkillMasterRepository
    {
        private readonly MasterDataCache<SkillMasterData> cache;

        public SkillMasterRepository()
        {
            cache = new MasterDataCache<SkillMasterData>("Skill", "skillId", LoadAll, data => data.SkillId);
        }

        public IReadOnlyList<SkillMasterData> GetAll() => cache.GetAll();
        public bool TryGetById(string id, out SkillMasterData data) => cache.TryGetById(id, out data);
        public SkillMasterData GetById(string id) => cache.GetById(id);
        public bool Exists(string id) => cache.Exists(id);
        public void ClearCache() => cache.Clear();
        public bool TryGetBySkillId(string skillId, out SkillMasterData data) => TryGetById(skillId, out data);
        public SkillMasterData GetBySkillId(string skillId) => GetById(skillId);

        private static IReadOnlyList<SkillMasterData> LoadAll()
        {
            var items = new List<SkillMasterData>();
            Iono.MasterData.skill.ForEachEntity(entity => items.Add(Convert(entity)));
            return items;
        }

        private static SkillMasterData Convert(Iono.MasterData.skill entity)
        {
            var owner = $"Skill skillId={MasterDataIdUtil.Display(entity.skillId)}";
            return new SkillMasterData(entity.skillId, entity.name, entity.nameTextId, entity.descTextId, entity.memo, entity.effectName, entity.hpCostType, entity.hpCostRate, entity.targetRange, entity.skillCategory, entity.activationTiming, entity.effectTarget, entity.effectType, entity.physicMagicType, entity.power, entity.accuracy, entity.hpDrainRate, entity.powerAtMaxHp, entity.powerAtMinHp, entity.applyBuffBeforeAttack, entity.barrierCount, entity.barrierType, entity.barrierMaxStacks, entity.barrierTurnCount, entity.barrierConsumeTiming, entity.shieldCount, entity.shieldReduceRate, entity.shieldReduceValue, entity.shieldType, entity.shieldTurnCount, entity.shieldConsumeTiming, entity.minHitCount, entity.maxHitCount, entity.multiHitProbability, entity.criticalRank, entity.fixedDamageValue, entity.weatherId, entity.fieldId, entity.specialEffectConditionId, entity.actionPriority, MasterDataIdUtil.ToJankenId(entity.jankenId, owner, nameof(entity.jankenId)), MasterDataIdUtil.ToBuffId(entity.targetBuffId, owner, nameof(entity.targetBuffId)), MasterDataIdUtil.ToBuffId(entity.userBuffId, owner, nameof(entity.userBuffId)), MasterDataIdUtil.ToConditionId(entity.targetConditionId, owner, nameof(entity.targetConditionId)), MasterDataIdUtil.ToConditionId(entity.userConditionId, owner, nameof(entity.userConditionId)), entity.guaranteedHitFlag, entity.pierceBarrierFlag, entity.pierceShieldFlag, entity.changeWeatherFlag, entity.changeFieldFlag, entity.fixedDamageFlag, entity.selfDestructFlag);
        }
    }
}
