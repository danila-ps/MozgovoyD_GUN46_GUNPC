using GamePrototype.Dungeon;
using GamePrototype.Items.EconomicItems;

using GamePrototype.Dungeon;
namespace GamePrototype.Utils
{
    // Абстрактный строитель подземелий
    public abstract class DungeonBuilder
    {
        // фабрика нужна, чтобы создавать врагов в комнатах
        protected readonly UnitFactory Factory;
        protected DungeonBuilder(UnitFactory factory) => Factory = factory;
        public abstract DungeonRoom BuildDungeon();
    }
}
