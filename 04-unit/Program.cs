using System;

namespace HomeWork
{
    internal class Program
    {
        // Класс Unit - юнит для RPG-игры
        private class Unit
        {
            // ===== Свойства =====

            // Имя юнита: задаётся в конструкторе, только для чтения
            public string Name { get; }

            // Здоровье: приватное поле-хранилище + публичное свойство только для чтения
            private float _health;
            public float Health
            {
                get { return _health; }
            }

            // Урон: задаётся в конструкторе, всегда равен 5, только для чтения
            public int Damage { get; }

            // Броня: задаётся в конструкторе, всегда равна 0.6, только для чтения
            public float Armor { get; }

            // ===== Конструкторы =====

            // Конструктор со строковым аргументом - основной
            public Unit(string name)
            {
                Name = name;
                _health = 100f;   // стартовое здоровье юнита
                Damage = 5;
                Armor = 0.6f;
            }

            // Конструктор без аргумента - вызывает конструктор с аргументом через this
            public Unit() : this("Unknown Unit")
            {
            }

            // ===== Методы =====

            // Возвращает фактическое здоровье с учётом брони
            public float GetRealHealth()
            {
                return Health * (1f + Armor);
            }

            // Наносит урон юниту, возвращает true если юнит погиб
            public bool SetDamage(int value)
            {
                _health = Health - value * Armor;
                return Health <= 0f;
            }
        }

        static void Main(string[] args)
        {
            // Пример использования класса Unit
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
