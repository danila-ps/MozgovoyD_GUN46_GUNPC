using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;
using GamePrototype.Units;
namespace GamePrototype.Utils
{
    // Лёгкий уровень: игрок сильнее, гоблин слабее
    public sealed class EasyUnitFactory : UnitFactory
    {
        public override Unit CreatePlayer(string name)
        {
            var player = new Player(name, 40, 40, 7);
            player.AddItemToInventory(new Weapon(12, 20, "Sword"));
            player.AddItemToInventory(new Armour(20, 20, "Armour"));
            player.AddItemToInventory(new HealthPotion(GameConstants.HealthPotion));
            player.AddItemToInventory(new Grindstone(GameConstants.Grindstone));
            return player;
        }
        public override Unit CreateGoblinEnemy() => new Goblin(GameConstants.Goblin, 12, 12, 2);
    }
}