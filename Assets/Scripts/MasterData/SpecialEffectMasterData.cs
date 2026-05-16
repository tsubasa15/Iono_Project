using System;

namespace Iono.Game.MasterData
{
    public sealed class SpecialEffectMasterData
    {
        public SpecialEffectMasterData(string spEffectId, string name, string memo, string conditionType1, string conditionValue1, int damageCorrection1, string conditionType2, string conditionValue2, int damageCorrection2, string conditionType3, string conditionValue3, int damageCorrection3)
        {
            SpEffectId = spEffectId ?? string.Empty;
            Name = name ?? string.Empty;
            Memo = memo ?? string.Empty;
            ConditionType1 = conditionType1 ?? string.Empty;
            ConditionValue1 = conditionValue1 ?? string.Empty;
            DamageCorrection1 = damageCorrection1;
            ConditionType2 = conditionType2 ?? string.Empty;
            ConditionValue2 = conditionValue2 ?? string.Empty;
            DamageCorrection2 = damageCorrection2;
            ConditionType3 = conditionType3 ?? string.Empty;
            ConditionValue3 = conditionValue3 ?? string.Empty;
            DamageCorrection3 = damageCorrection3;
        }

        public string SpEffectId { get; }
        public string Name { get; }
        public string Memo { get; }
        public string ConditionType1 { get; }
        public string ConditionValue1 { get; }
        public int DamageCorrection1 { get; }
        public string ConditionType2 { get; }
        public string ConditionValue2 { get; }
        public int DamageCorrection2 { get; }
        public string ConditionType3 { get; }
        public string ConditionValue3 { get; }
        public int DamageCorrection3 { get; }
    }
}
