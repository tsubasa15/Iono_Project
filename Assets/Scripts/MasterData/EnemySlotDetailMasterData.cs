using System;

namespace Iono.Game.MasterData
{
    public sealed class EnemySlotDetailMasterData
    {
        public EnemySlotDetailMasterData(string enemySlotId, string name, string memo, string reconFlagId, string skillId)
        {
            EnemySlotId = enemySlotId ?? string.Empty;
            Name = name ?? string.Empty;
            Memo = memo ?? string.Empty;
            ReconFlagId = reconFlagId ?? string.Empty;
            SkillId = skillId ?? string.Empty;
        }

        public string EnemySlotId { get; }
        public string Name { get; }
        public string Memo { get; }
        public string ReconFlagId { get; }
        public string SkillId { get; }
    }
}
