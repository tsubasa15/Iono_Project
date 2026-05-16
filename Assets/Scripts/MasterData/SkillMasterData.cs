using System;

namespace Iono.Game.MasterData
{
    public sealed class SkillMasterData
    {
        public SkillMasterData(string skillId, string name, string nameTextId, string descTextId, string memo, string effectName, string hpCostType, int hpCostRate, string targetRange, string skillCategory, string activationTiming, string effectTarget, string effectType, string physicMagicType, int power, int accuracy, int hpDrainRate, int powerAtMaxHp, int powerAtMinHp, string applyBuffBeforeAttack, int barrierCount, string barrierType, int barrierMaxStacks, int barrierTurnCount, string barrierConsumeTiming, int shieldCount, int shieldReduceRate, int shieldReduceValue, string shieldType, int shieldTurnCount, string shieldConsumeTiming, int minHitCount, int maxHitCount, int multiHitProbability, int criticalRank, int fixedDamageValue, string weatherId, string fieldId, string specialEffectConditionId, int actionPriority, string jankenId, string targetBuffId, string userBuffId, string targetConditionId, string userConditionId, bool guaranteedHitFlag, bool pierceBarrierFlag, bool pierceShieldFlag, bool changeWeatherFlag, bool changeFieldFlag, bool fixedDamageFlag, bool selfDestructFlag)
        {
            SkillId = skillId ?? string.Empty;
            Name = name ?? string.Empty;
            NameTextId = nameTextId ?? string.Empty;
            DescTextId = descTextId ?? string.Empty;
            Memo = memo ?? string.Empty;
            EffectName = effectName ?? string.Empty;
            HpCostType = hpCostType ?? string.Empty;
            HpCostRate = hpCostRate;
            TargetRange = targetRange ?? string.Empty;
            SkillCategory = skillCategory ?? string.Empty;
            ActivationTiming = activationTiming ?? string.Empty;
            EffectTarget = effectTarget ?? string.Empty;
            EffectType = effectType ?? string.Empty;
            PhysicMagicType = physicMagicType ?? string.Empty;
            Power = power;
            Accuracy = accuracy;
            HpDrainRate = hpDrainRate;
            PowerAtMaxHp = powerAtMaxHp;
            PowerAtMinHp = powerAtMinHp;
            ApplyBuffBeforeAttack = applyBuffBeforeAttack ?? string.Empty;
            BarrierCount = barrierCount;
            BarrierType = barrierType ?? string.Empty;
            BarrierMaxStacks = barrierMaxStacks;
            BarrierTurnCount = barrierTurnCount;
            BarrierConsumeTiming = barrierConsumeTiming ?? string.Empty;
            ShieldCount = shieldCount;
            ShieldReduceRate = shieldReduceRate;
            ShieldReduceValue = shieldReduceValue;
            ShieldType = shieldType ?? string.Empty;
            ShieldTurnCount = shieldTurnCount;
            ShieldConsumeTiming = shieldConsumeTiming ?? string.Empty;
            MinHitCount = minHitCount;
            MaxHitCount = maxHitCount;
            MultiHitProbability = multiHitProbability;
            CriticalRank = criticalRank;
            FixedDamageValue = fixedDamageValue;
            WeatherId = weatherId ?? string.Empty;
            FieldId = fieldId ?? string.Empty;
            SpecialEffectConditionId = specialEffectConditionId ?? string.Empty;
            ActionPriority = actionPriority;
            JankenId = jankenId ?? string.Empty;
            TargetBuffId = targetBuffId ?? string.Empty;
            UserBuffId = userBuffId ?? string.Empty;
            TargetConditionId = targetConditionId ?? string.Empty;
            UserConditionId = userConditionId ?? string.Empty;
            GuaranteedHitFlag = guaranteedHitFlag;
            PierceBarrierFlag = pierceBarrierFlag;
            PierceShieldFlag = pierceShieldFlag;
            ChangeWeatherFlag = changeWeatherFlag;
            ChangeFieldFlag = changeFieldFlag;
            FixedDamageFlag = fixedDamageFlag;
            SelfDestructFlag = selfDestructFlag;
        }

        public string SkillId { get; }
        public string Name { get; }
        public string NameTextId { get; }
        public string DescTextId { get; }
        public string Memo { get; }
        public string EffectName { get; }
        public string HpCostType { get; }
        public int HpCostRate { get; }
        public string TargetRange { get; }
        public string SkillCategory { get; }
        public string ActivationTiming { get; }
        public string EffectTarget { get; }
        public string EffectType { get; }
        public string PhysicMagicType { get; }
        public int Power { get; }
        public int Accuracy { get; }
        public int HpDrainRate { get; }
        public int PowerAtMaxHp { get; }
        public int PowerAtMinHp { get; }
        public string ApplyBuffBeforeAttack { get; }
        public int BarrierCount { get; }
        public string BarrierType { get; }
        public int BarrierMaxStacks { get; }
        public int BarrierTurnCount { get; }
        public string BarrierConsumeTiming { get; }
        public int ShieldCount { get; }
        public int ShieldReduceRate { get; }
        public int ShieldReduceValue { get; }
        public string ShieldType { get; }
        public int ShieldTurnCount { get; }
        public string ShieldConsumeTiming { get; }
        public int MinHitCount { get; }
        public int MaxHitCount { get; }
        public int MultiHitProbability { get; }
        public int CriticalRank { get; }
        public int FixedDamageValue { get; }
        public string WeatherId { get; }
        public string FieldId { get; }
        public string SpecialEffectConditionId { get; }
        public int ActionPriority { get; }
        public string JankenId { get; }
        public string TargetBuffId { get; }
        public string UserBuffId { get; }
        public string TargetConditionId { get; }
        public string UserConditionId { get; }
        public bool GuaranteedHitFlag { get; }
        public bool PierceBarrierFlag { get; }
        public bool PierceShieldFlag { get; }
        public bool ChangeWeatherFlag { get; }
        public bool ChangeFieldFlag { get; }
        public bool FixedDamageFlag { get; }
        public bool SelfDestructFlag { get; }
    }
}
