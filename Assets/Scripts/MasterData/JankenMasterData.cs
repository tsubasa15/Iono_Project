using System;

namespace Iono.Game.MasterData
{
    public sealed class JankenMasterData
    {
        public JankenMasterData(string jankenId, string name, string resourceId)
        {
            JankenId = jankenId ?? string.Empty;
            Name = name ?? string.Empty;
            ResourceId = resourceId ?? string.Empty;
        }

        public string JankenId { get; }
        public string Name { get; }
        public string ResourceId { get; }
    }
}
