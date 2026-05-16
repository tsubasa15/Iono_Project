using System;

namespace Iono.Game.MasterData
{
    public sealed class FieldMasterData
    {
        public FieldMasterData(string fieldId, string name, string memo, string preventOverwriteFlag, int duration, int healEndTurnRate, int plusDamageRate, int minusDamageRate, string plusDamageTypeId, string minusDamageTypeId, string negatedConditionId, string negatedSkillId, bool preventPriorityFlag)
        {
            FieldId = fieldId ?? string.Empty;
            Name = name ?? string.Empty;
            Memo = memo ?? string.Empty;
            PreventOverwriteFlag = preventOverwriteFlag ?? string.Empty;
            Duration = duration;
            HealEndTurnRate = healEndTurnRate;
            PlusDamageRate = plusDamageRate;
            MinusDamageRate = minusDamageRate;
            PlusDamageTypeId = plusDamageTypeId ?? string.Empty;
            MinusDamageTypeId = minusDamageTypeId ?? string.Empty;
            NegatedConditionId = negatedConditionId ?? string.Empty;
            NegatedSkillId = negatedSkillId ?? string.Empty;
            PreventPriorityFlag = preventPriorityFlag;
        }

        public string FieldId { get; }
        public string Name { get; }
        public string Memo { get; }
        public string PreventOverwriteFlag { get; }
        public int Duration { get; }
        public int HealEndTurnRate { get; }
        public int PlusDamageRate { get; }
        public int MinusDamageRate { get; }
        public string PlusDamageTypeId { get; }
        public string MinusDamageTypeId { get; }
        public string NegatedConditionId { get; }
        public string NegatedSkillId { get; }
        public bool PreventPriorityFlag { get; }
    }
}
