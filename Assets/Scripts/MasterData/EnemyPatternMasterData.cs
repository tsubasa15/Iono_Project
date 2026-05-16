using System;
using System.Collections.Generic;

namespace Iono.Game.MasterData
{
    public sealed class EnemyPatternMasterData
    {
        public EnemyPatternMasterData(string patternId, string name, IReadOnlyList<string> slotIds)
        {
            PatternId = patternId ?? string.Empty;
            Name = name ?? string.Empty;
            SlotIds = slotIds ?? Array.Empty<string>();
        }

        public string PatternId { get; }
        public string Name { get; }
        public IReadOnlyList<string> SlotIds { get; }
    }
}
