using System;

namespace HomeWork
{
    internal class Program
    {
        // Класс Unit - юнит для RPG-игры
        private class Unit
        {
            // ===== Свойства =====

            public string Name { get; }

            private float _health;
            public float Health
            {
                get { return _health; }
            }

            public int Damage { get; }

            public float Armor { get; }

            // ===== Конструкторы =====

            public Unit(string name)
            {
                Name = name;
                _health = 100f;   // стартовое здоровье юнита
                Damage = 5;
                Armor = 0.6f;
            }

            public Unit() : this("Unknown Unit")
            {
            }

            // ===== Методы =====

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

        static void Main(string[] args)
        {
            var hero = new Unit("Hero");
            var unknown = new Unit();

            Console.WriteLine($"{hero.Name}: Health={hero.Health}, Damage={hero.Damage}, Armor={hero.Armor}");
            Console.WriteLine($"{unknown.Name}: Health={unknown.Health}");

            Console.WriteLine($"Real health of {hero.Name}: {hero.GetRealHealth()}");

            bool isDead = hero.SetDamage(20);
            Console.WriteLine($"{hero.Name} took 20 damage. Health now: {hero.Health}. Is dead: {isDead}");
        }
    }
}
