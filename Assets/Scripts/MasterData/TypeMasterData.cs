using System;

namespace Iono.Game.MasterData
{
    public sealed class TypeMasterData
    {
        public TypeMasterData(string typeId, string name, string resourceId)
        {
            TypeId = typeId ?? string.Empty;
            Name = name ?? string.Empty;
            ResourceId = resourceId ?? string.Empty;
        }

        public string TypeId { get; }
        public string Name { get; }
        public string ResourceId { get; }
    }
}
