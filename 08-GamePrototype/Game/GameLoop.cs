using GamePrototype.Combat;
using GamePrototype.Dungeon;
using GamePrototype.Units;
using GamePrototype.Utils;
namespace GamePrototype.Game
{
    public sealed class GameLoop
    {
        private Unit _player;
        private DungeonRoom _dungeon;
        private readonly CombatManager _combatManager = new CombatManager();

        public void StartGame()
        {
            Initialize();
            Console.WriteLine("Entering the dungeon");
            StartGameLoop();
        }
        #region Game Loop
        private void Initialize()
        {
            Console.WriteLine("Welcome, player!");
            var difficulty = ChooseDifficulty();
            // выбираем фабрику и строителя по уровню сложности
            UnitFactory unitFactory;
            DungeonBuilder dungeonBuilder;
            if (difficulty == Difficulty.Easy)
            {
                unitFactory = new EasyUnitFactory();
                dungeonBuilder = new EasyDungeonBuilder(unitFactory);
            }
            else
            {
                unitFactory = new HardUnitFactory();
                dungeonBuilder = new HardDungeonBuilder(unitFactory);
            }
            _dungeon = dungeonBuilder.BuildDungeon();
            Console.WriteLine("Enter your name");
            _player = unitFactory.CreatePlayer(Console.ReadLine());
            Console.WriteLine($"Hello {_player.Name}");
        }
        private Difficulty ChooseDifficulty()
        {
            Console.WriteLine($"Choose difficulty: {Difficulty.Easy} = {(int)Difficulty.Easy}, " +
            $"{Difficulty.Hard} = {(int)Difficulty.Hard}");
            while (true)
            {
                var input = Console.ReadLine();
                if (Enum.TryParse<Difficulty>(input, true, out var difficulty)
                && Enum.IsDefined(difficulty))
                {
                    Console.WriteLine($"Difficulty: {difficulty}");
                    return difficulty;
                }
                Console.WriteLine("Wrong difficulty! Type Easy or Hard (0 or 1)");
            }
        }
        private void StartGameLoop()
        {
            var currentRoom = _dungeon;

            while (currentRoom.IsFinal == false)
            {
                StartRoomEncounter(currentRoom, out var success);
                if (!success)
                {
                    Console.WriteLine("Game over!");
                    return;
                }
                DisplayRouteOptions(currentRoom);
                while (true)
                {
                    // проверяем, что такое направление есть в этой комнате
                    if (Enum.TryParse<Direction>(Console.ReadLine(), true, out var direction)
                    && currentRoom.Rooms.ContainsKey(direction))
                    {
                        currentRoom = currentRoom.Rooms[direction];
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Wrong direction!");
                    }
                }
            }
            Console.WriteLine($"Congratulations, {_player.Name}");
            Console.WriteLine("Result: ");
            Console.WriteLine(_player.ToString());
        }
        private void StartRoomEncounter(DungeonRoom currentRoom, out bool success)
        {
            success = true;
            Console.WriteLine($"Entered room {currentRoom.Name}");
            if (currentRoom.Loot != null)
            {
                _player.AddItemToInventory(currentRoom.Loot);
            }
            if (currentRoom.Enemy != null)
            {
                if (_combatManager.StartCombat(_player, currentRoom.Enemy) == _player)
                {
                    _player.HandleCombatComplete();
                    LootEnemy(currentRoom.Enemy);
                }
                else
                {
                    success = false;
                }
            }
            void LootEnemy(Unit enemy)
            {
                _player.AddItemsFromUnitToInventory(enemy);
            }
        }
        private void DisplayRouteOptions(DungeonRoom currentRoom)
        {
            Console.WriteLine("Where to go?");
            foreach (var room in currentRoom.Rooms)
            {
                Console.Write($"{room.Key} - {(int)room.Key}\t");
            }
            Console.WriteLine();
        }

        #endregion
    }
}
