using GamePrototype.Units;
namespace GamePrototype.Utils
{
    // Абстрактная фабрика юнитов: говорит ЧТО создать, а КАК - решают наследники
    public abstract class UnitFactory
    {
        public abstract Unit CreatePlayer(string name);
        public abstract Unit CreateGoblinEnemy();
    }
}