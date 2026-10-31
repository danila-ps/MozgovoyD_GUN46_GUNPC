using GamePrototype.Dungeon;
using GamePrototype.Items.EquipItems;
namespace GamePrototype.Utils
{
    // Сложное подземелье: два монстра и мало снаряжения
    public sealed class HardDungeonBuilder : DungeonBuilder
    {
        public HardDungeonBuilder(UnitFactory factory) : base(factory)
        {
        }
        public override DungeonRoom BuildDungeon()
        {
            var enter = new DungeonRoom("Enter");
            var firstMonster = new DungeonRoom("Goblin hall", Factory.CreateGoblinEnemy());
            var oldArmoury = new DungeonRoom("Old armoury", new Helmet(5, 10, "Rusty Helmet"));
            var storage = new DungeonRoom("Storage", new Armour(15, 15, "Chain Armour"));
            var secondMonster = new DungeonRoom("Goblin lair", Factory.CreateGoblinEnemy());
            var finalRoom = new DungeonRoom("Final");
            enter.TrySetDirection(Direction.Forward, firstMonster);
            firstMonster.TrySetDirection(Direction.Left, oldArmoury);
            firstMonster.TrySetDirection(Direction.Right, storage);
            oldArmoury.TrySetDirection(Direction.Forward, secondMonster);
            storage.TrySetDirection(Direction.Forward, secondMonster);
            secondMonster.TrySetDirection(Direction.Forward, finalRoom);
            return enter;
        }
    }
}
