using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;
using GamePrototype.Units;
namespace GamePrototype.Utils
{
    // Сложный уровень: игрок слабее, гоблин сильнее
    public sealed class HardUnitFactory : UnitFactory
    {
        public override Unit CreatePlayer(string name)
        {
            var player = new Player(name, 30, 30, 5);
            player.AddItemToInventory(new Weapon(8, 10, "Old Sword"));
            player.AddItemToInventory(new Armour(5, 10, "Leather Armour"));
            player.AddItemToInventory(new HealthPotion(GameConstants.HealthPotion));
            return player;
        }
        public override Unit CreateGoblinEnemy() => new Goblin("Goblin Warrior", 24, 24, 4);
    }
}