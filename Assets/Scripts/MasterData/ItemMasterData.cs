using System;

namespace Iono.Game.MasterData
{
    public sealed class ItemMasterData
    {
        public ItemMasterData(string itemId, string name, string nameTextId, string descTextId, string imageId, string rarity, string itemCategory, string skillId)
        {
            ItemId = itemId ?? string.Empty;
            Name = name ?? string.Empty;
            NameTextId = nameTextId ?? string.Empty;
            DescTextId = descTextId ?? string.Empty;
            ImageId = imageId ?? string.Empty;
            Rarity = rarity ?? string.Empty;
            ItemCategory = itemCategory ?? string.Empty;
            SkillId = skillId ?? string.Empty;
        }

        public string ItemId { get; }
        public string Name { get; }
        public string NameTextId { get; }
        public string DescTextId { get; }
        public string ImageId { get; }
        public string Rarity { get; }
        public string ItemCategory { get; }
        public string SkillId { get; }
    }
}
