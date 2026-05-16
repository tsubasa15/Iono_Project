using System;
using System.Collections.Generic;

namespace Iono.Game.MasterData
{
    public sealed class BuffDebuffMasterData
    {
        public BuffDebuffMasterData(string buffId, string name, string memo, string activationCondition, string tag1, string tag2, string gender, int activationRate, int turnCount, string effectTarget, string consumeTiming, string powerUpTiming, int maxStacks, string buffType1, int buffValue1, string specialEffectTag1, string valueModifierType1, IReadOnlyList<string> typeIds, bool unerasableFlag)
        {
            BuffId = buffId ?? string.Empty;
            Name = name ?? string.Empty;
            Memo = memo ?? string.Empty;
            ActivationCondition = activationCondition ?? string.Empty;
            Tag1 = tag1 ?? string.Empty;
            Tag2 = tag2 ?? string.Empty;
            Gender = gender ?? string.Empty;
            ActivationRate = activationRate;
            TurnCount = turnCount;
            EffectTarget = effectTarget ?? string.Empty;
            ConsumeTiming = consumeTiming ?? string.Empty;
            PowerUpTiming = powerUpTiming ?? string.Empty;
            MaxStacks = maxStacks;
            BuffType1 = buffType1 ?? string.Empty;
            BuffValue1 = buffValue1;
            SpecialEffectTag1 = specialEffectTag1 ?? string.Empty;
            ValueModifierType1 = valueModifierType1 ?? string.Empty;
            TypeIds = typeIds ?? Array.Empty<string>();
            UnerasableFlag = unerasableFlag;
        }

        public string BuffId { get; }
        public string Name { get; }
        public string Memo { get; }
        public string ActivationCondition { get; }
        public string Tag1 { get; }
        public string Tag2 { get; }
        public string Gender { get; }
        public int ActivationRate { get; }
        public int TurnCount { get; }
        public string EffectTarget { get; }
        public string ConsumeTiming { get; }
        public string PowerUpTiming { get; }
        public int MaxStacks { get; }
        public string BuffType1 { get; }
        public int BuffValue1 { get; }
        public string SpecialEffectTag1 { get; }
        public string ValueModifierType1 { get; }
        public IReadOnlyList<string> TypeIds { get; }
        public bool UnerasableFlag { get; }
    }
}
