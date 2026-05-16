using System;

namespace Iono.Game.MasterData
{
    public sealed class TypeCompatibilityMasterData
    {
        public TypeCompatibilityMasterData(string typeId, string name, int typeNormal, int typeFire, int typeWater, int typeElectric, int typeGrass, int typeIce, int typeFighting, int typePoison, int typeGround, int typeFlying, int typePsychic, int typeBug, int typeRock, int typeGhost, int typeDragon, int typeDark, int typeSteel, int typeFairy)
        {
            TypeId = typeId ?? string.Empty;
            Name = name ?? string.Empty;
            TypeNormal = typeNormal;
            TypeFire = typeFire;
            TypeWater = typeWater;
            TypeElectric = typeElectric;
            TypeGrass = typeGrass;
            TypeIce = typeIce;
            TypeFighting = typeFighting;
            TypePoison = typePoison;
            TypeGround = typeGround;
            TypeFlying = typeFlying;
            TypePsychic = typePsychic;
            TypeBug = typeBug;
            TypeRock = typeRock;
            TypeGhost = typeGhost;
            TypeDragon = typeDragon;
            TypeDark = typeDark;
            TypeSteel = typeSteel;
            TypeFairy = typeFairy;
        }

        public string TypeId { get; }
        public string Name { get; }
        public int TypeNormal { get; }
        public int TypeFire { get; }
        public int TypeWater { get; }
        public int TypeElectric { get; }
        public int TypeGrass { get; }
        public int TypeIce { get; }
        public int TypeFighting { get; }
        public int TypePoison { get; }
        public int TypeGround { get; }
        public int TypeFlying { get; }
        public int TypePsychic { get; }
        public int TypeBug { get; }
        public int TypeRock { get; }
        public int TypeGhost { get; }
        public int TypeDragon { get; }
        public int TypeDark { get; }
        public int TypeSteel { get; }
        public int TypeFairy { get; }
    }
}
