using System;

namespace Iono.Game.MasterData
{
    public sealed class AbnormalConditionMasterData
    {
        public AbnormalConditionMasterData(string conditionId, string name, string memo, string conditionCategory, int probability, string activationCondition, string gender, string tag1, string tag2, string effectTarget, string consumeTiming, string turnCount, string effectType1, int effectValue1, string effectType2, int effectValue2, string effectType3, int effectValue3, int poisonHpDamageRate, int paralysisActionRate, int confusionActionRate, int burnHpDamageRate, int burnAtkReduceRate, int doomTurnCount, string typeId, string attachedBuffId1, string attachedBuffId2)
        {
            ConditionId = conditionId ?? string.Empty;
            Name = name ?? string.Empty;
            Memo = memo ?? string.Empty;
            ConditionCategory = conditionCategory ?? string.Empty;
            Probability = probability;
            ActivationCondition = activationCondition ?? string.Empty;
            Gender = gender ?? string.Empty;
            Tag1 = tag1 ?? string.Empty;
            Tag2 = tag2 ?? string.Empty;
            EffectTarget = effectTarget ?? string.Empty;
            ConsumeTiming = consumeTiming ?? string.Empty;
            TurnCount = turnCount ?? string.Empty;
            EffectType1 = effectType1 ?? string.Empty;
            EffectValue1 = effectValue1;
            EffectType2 = effectType2 ?? string.Empty;
            EffectValue2 = effectValue2;
            EffectType3 = effectType3 ?? string.Empty;
            EffectValue3 = effectValue3;
            PoisonHpDamageRate = poisonHpDamageRate;
            ParalysisActionRate = paralysisActionRate;
            ConfusionActionRate = confusionActionRate;
            BurnHpDamageRate = burnHpDamageRate;
            BurnAtkReduceRate = burnAtkReduceRate;
            DoomTurnCount = doomTurnCount;
            TypeId = typeId ?? string.Empty;
            AttachedBuffId1 = attachedBuffId1 ?? string.Empty;
            AttachedBuffId2 = attachedBuffId2 ?? string.Empty;
        }

        public string ConditionId { get; }
        public string Name { get; }
        public string Memo { get; }
        public string ConditionCategory { get; }
        public int Probability { get; }
        public string ActivationCondition { get; }
        public string Gender { get; }
        public string Tag1 { get; }
        public string Tag2 { get; }
        public string EffectTarget { get; }
        public string ConsumeTiming { get; }
        public string TurnCount { get; }
        public string EffectType1 { get; }
        public int EffectValue1 { get; }
        public string EffectType2 { get; }
        public int EffectValue2 { get; }
        public string EffectType3 { get; }
        public int EffectValue3 { get; }
        public int PoisonHpDamageRate { get; }
        public int ParalysisActionRate { get; }
        public int ConfusionActionRate { get; }
        public int BurnHpDamageRate { get; }
        public int BurnAtkReduceRate { get; }
        public int DoomTurnCount { get; }
        public string TypeId { get; }
        public string AttachedBuffId1 { get; }
        public string AttachedBuffId2 { get; }
    }
}
