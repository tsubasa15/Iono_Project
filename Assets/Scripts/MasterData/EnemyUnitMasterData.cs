using System;
using System.Collections.Generic;

namespace Iono.Game.MasterData
{
    public sealed class EnemyUnitMasterData
    {
        public EnemyUnitMasterData(string enemyId, string name, string nameTextId, string descTextId, string memo, string imageId, string gender, int hp, int atk, int def, int spAtk, int spDef, int spd, int resPoison, int resSleep, int resParalysis, int resConfusion, string tag1, string tag2, int nWincount, string typeId, string passiveSkillId, IReadOnlyList<string> patternIds)
        {
            EnemyId = enemyId ?? string.Empty;
            Name = name ?? string.Empty;
            NameTextId = nameTextId ?? string.Empty;
            DescTextId = descTextId ?? string.Empty;
            Memo = memo ?? string.Empty;
            ImageId = imageId ?? string.Empty;
            Gender = gender ?? string.Empty;
            Hp = hp;
            Atk = atk;
            Def = def;
            SpAtk = spAtk;
            SpDef = spDef;
            Spd = spd;
            ResPoison = resPoison;
            ResSleep = resSleep;
            ResParalysis = resParalysis;
            ResConfusion = resConfusion;
            Tag1 = tag1 ?? string.Empty;
            Tag2 = tag2 ?? string.Empty;
            NWincount = nWincount;
            TypeId = typeId ?? string.Empty;
            PassiveSkillId = passiveSkillId ?? string.Empty;
            PatternIds = patternIds ?? Array.Empty<string>();
        }

        public string EnemyId { get; }
        public string Name { get; }
        public string NameTextId { get; }
        public string DescTextId { get; }
        public string Memo { get; }
        public string ImageId { get; }
        public string Gender { get; }
        public int Hp { get; }
        public int Atk { get; }
        public int Def { get; }
        public int SpAtk { get; }
        public int SpDef { get; }
        public int Spd { get; }
        public int ResPoison { get; }
        public int ResSleep { get; }
        public int ResParalysis { get; }
        public int ResConfusion { get; }
        public string Tag1 { get; }
        public string Tag2 { get; }
        public int NWincount { get; }
        public string TypeId { get; }
        public string PassiveSkillId { get; }
        public IReadOnlyList<string> PatternIds { get; }
    }
}
