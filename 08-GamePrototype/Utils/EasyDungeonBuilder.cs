using GamePrototype.Dungeon;
using GamePrototype.Items.EquipItems;
namespace GamePrototype.Utils
{
    // Лёгкое подземелье: один монстр и много снаряжения
    public sealed class EasyDungeonBuilder : DungeonBuilder
    {
        public EasyDungeonBuilder(UnitFactory factory) : base(factory)
        {
        }
        public override DungeonRoom BuildDungeon()
        {
            var enter = new DungeonRoom("Enter");
            var armoury = new DungeonRoom("Armoury", new Helmet(10, 15, "Helmet"));
            var monsterRoom = new DungeonRoom("Monster", Factory.CreateGoblinEnemy());
            var hunterCamp = new DungeonRoom("Hunter camp", new RangeWeapon(6, 15, "Bow"));
            var treasury = new DungeonRoom("Treasury", new Weapon(15, 20, "Great Sword"));
            var finalRoom = new DungeonRoom("Final");
            enter.TrySetDirection(Direction.Forward, armoury);
            armoury.TrySetDirection(Direction.Forward, monsterRoom);
            monsterRoom.TrySetDirection(Direction.Left, hunterCamp);
            monsterRoom.TrySetDirection(Direction.Right, treasury);
            hunterCamp.TrySetDirection(Direction.Forward, treasury);
            treasury.TrySetDirection(Direction.Forward, finalRoom);
            return enter;
        }
    }
}