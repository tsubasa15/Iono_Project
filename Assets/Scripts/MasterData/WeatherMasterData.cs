using System;
using System.Collections.Generic;

namespace Iono.Game.MasterData
{
    public sealed class WeatherMasterData
    {
        public WeatherMasterData(string weatherId, string name, string memo, int plusDamageRate, int minusDamageRate, int buffDefRate, int buffSpDefRate, string preventOverwriteFlag, int duration, IReadOnlyList<string> slipDamageImmuneTypeIds, string negatedConditionId, string plusDamageTypeId, string minusDamageTypeId, string buffDefTypeId, string buffSpDefTypeId, int slipDamageRate)
        {
            WeatherId = weatherId ?? string.Empty;
            Name = name ?? string.Empty;
            Memo = memo ?? string.Empty;
            PlusDamageRate = plusDamageRate;
            MinusDamageRate = minusDamageRate;
            BuffDefRate = buffDefRate;
            BuffSpDefRate = buffSpDefRate;
            PreventOverwriteFlag = preventOverwriteFlag ?? string.Empty;
            Duration = duration;
            SlipDamageImmuneTypeIds = slipDamageImmuneTypeIds ?? Array.Empty<string>();
            NegatedConditionId = negatedConditionId ?? string.Empty;
            PlusDamageTypeId = plusDamageTypeId ?? string.Empty;
            MinusDamageTypeId = minusDamageTypeId ?? string.Empty;
            BuffDefTypeId = buffDefTypeId ?? string.Empty;
            BuffSpDefTypeId = buffSpDefTypeId ?? string.Empty;
            SlipDamageRate = slipDamageRate;
        }

        public string WeatherId { get; }
        public string Name { get; }
        public string Memo { get; }
        public int PlusDamageRate { get; }
        public int MinusDamageRate { get; }
        public int BuffDefRate { get; }
        public int BuffSpDefRate { get; }
        public string PreventOverwriteFlag { get; }
        public int Duration { get; }
        public IReadOnlyList<string> SlipDamageImmuneTypeIds { get; }
        public string NegatedConditionId { get; }
        public string PlusDamageTypeId { get; }
        public string MinusDamageTypeId { get; }
        public string BuffDefTypeId { get; }
        public string BuffSpDefTypeId { get; }
        public int SlipDamageRate { get; }
    }
}
