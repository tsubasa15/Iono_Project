using System;

namespace Iono.Game.MasterData
{
    public sealed class InventoryMasterData
    {
        public InventoryMasterData(string inventoryId, string name, string buffDebuffId, string spEffectId)
        {
            InventoryId = inventoryId ?? string.Empty;
            Name = name ?? string.Empty;
            BuffDebuffId = buffDebuffId ?? string.Empty;
            SpEffectId = spEffectId ?? string.Empty;
        }

        public string InventoryId { get; }
        public string Name { get; }
        public string BuffDebuffId { get; }
        public string SpEffectId { get; }
    }
}
