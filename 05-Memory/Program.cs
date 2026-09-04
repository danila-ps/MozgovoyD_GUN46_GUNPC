using System;

namespace HomeWork
{
    internal class Program
    {
        // ===== Структура Interval =====
        private struct Interval
        {
            private Random _random;

            public int Min { get; }
            public int Max { get; }

            public Interval(int minValue, int maxValue)
            {
                if (minValue > maxValue)
                {
                    int temp = minValue;
                    minValue = maxValue;
                    maxValue = temp;
                    Console.WriteLine("Error! minValue was greater than maxValue - values swapped.");
                }

                if (minValue < 0)
                {
                    minValue = 0;
                    Console.WriteLine("Error! minValue was negative - set to 0.");
                }

                if (maxValue < 0)
                {
                    maxValue = 0;
                    Console.WriteLine("Error! maxValue was negative - set to 0.");
                }

                if (minValue == maxValue)
                {
                    maxValue += 10;
                    Console.WriteLine("Error! minValue equals maxValue - maxValue increased by 10.");
                }

                Min = minValue;
                Max = maxValue;
                _random = new Random();
            }

            public int Get()
            {
                return _random.Next(Min, Max + 1);
            }
        }

        // ===== Класс Unit (доработанный) =====
        private class Unit
        {
            public string Name { get; }

            private float _health;
            public float Health
            {
                get { return _health; }
            }

            public Interval DamageInterval { get; private set; }
            public float Armor { get; }

            public Unit(string name)
            {
                Name = name;
                _health = 100f;
                Armor = 0.6f;
                DamageInterval = new Interval(0, 5); 
            }

            public Unit() : this("Unknown Unit")
            {
            }

            public Unit(string name, int maxDamage) : this(name)
            {
                DamageInterval = new Interval(0, maxDamage);
            }

            public float GetRealHealth()
            {
                return Health * (1f + Armor);
            }

            public bool SetDamage(int value)
            {
                _health = Health - value * Armor;
                return Health <= 0f;
            }
        }

        // ===== Класс Weapon =====
        private class Weapon
        {
            public string Name { get; }
            public Interval DamageInterval { get; private set; }
            public float Durability { get; }

            public Weapon(string name)
            {
                Name = name;
                Durability = 1f;
                DamageInterval = new Interval(1, 10); // урон по умолчанию
            }

            public Weapon(string name, int minDamage, int maxDamage) : this(name)
            {
                SetDamageParams(minDamage, maxDamage);
            }

            public void SetDamageParams(int minDamage, int maxDamage)
            {
                DamageInterval = new Interval(minDamage, maxDamage);
            }

            public int GetDamage()
            {
                return DamageInterval.Get();
            }
        }

        // ===== Структура Room =====
        private struct Room
        {
            public Unit Unit;
            public Weapon Weapon;

            public Room(Unit unit, Weapon weapon)
            {
                Unit = unit;
                Weapon = weapon;
            }
        }

        // ===== Класс Dungeon =====
        private class Dungeon
        {
            private Room[] rooms;

            public Dungeon()
            {
                rooms = new Room[]
                {
                    new Room(new Unit("Goblin"), new Weapon("Rusty Sword", 1, 5)),
                    new Room(new Unit("Orc"), new Weapon("Axe", 3, 8)),
                    new Room(new Unit("Dragon"), new Weapon("Fire Breath", 10, 20))
                };
            }

            public void ShowRooms()
            {
                for (int i = 0; i < rooms.Length; i++)
                {
                    var room = rooms[i];
                    Console.WriteLine("Unit of room: " + room.Unit.Name);
                    Console.WriteLine("Weapon of room: " + room.Weapon.Name);
                    Console.WriteLine("---");
                }
            }
        }

        static void Main(string[] args)
        {
            var dungeon = new Dungeon();
            dungeon.ShowRooms();
        }
    }
}
