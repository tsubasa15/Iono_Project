using System;

namespace Iono.Game.MasterData
{
    public sealed class JankenCompatibilityMasterData
    {
        public JankenCompatibilityMasterData(string jankenId, string name, int janRock, int janPaper, int janScissors, int janInfinity)
        {
            JankenId = jankenId ?? string.Empty;
            Name = name ?? string.Empty;
            JanRock = janRock;
            JanPaper = janPaper;
            JanScissors = janScissors;
            JanInfinity = janInfinity;
        }

        public string JankenId { get; }
        public string Name { get; }
        public int JanRock { get; }
        public int JanPaper { get; }
        public int JanScissors { get; }
        public int JanInfinity { get; }
    }
}
