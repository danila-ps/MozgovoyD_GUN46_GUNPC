using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;
using GamePrototype.Utils;
using System.Text;
namespace GamePrototype.Units
{
    public sealed class Player : Unit
    {
        private const uint MaxDefence = 50;
        private readonly Dictionary<EquipSlot, EquipItem> _equipment = new();
        public Player(string name, uint health, uint maxHealth, uint baseDamage)
        : base(name, health, maxHealth, baseDamage)
        {
        }
        public override uint GetUnitDamage()
        {
            // основное оружие, а если его нет - оружие дальнего боя
            var weapon = GetWeapon(EquipSlot.Weapon) ?? GetWeapon(EquipSlot.RangeWeapon);
            if (weapon == null)
            {
                return BaseDamage;
            }
            // оружие тупится после каждого удара
            weapon.ReduceDurability(1);
            return BaseDamage + weapon.Damage;
        }
        public override void HandleCombatComplete()
        {
            var items = Inventory.Items;
            // идём с конца списка, чтобы удаление не сбивало индексы
            for (int i = items.Count - 1; i >= 0; i--)
            {
                var item = items[i];
                if (TryUseItem(item))
                {
                    Inventory.TryRemove(item);
                }
            }
        }
        public override void AddItemToInventory(Item item)
        {
            if (item is EquipItem equipItem)
            {
                Equip(equipItem);
                return;
            }
            base.AddItemToInventory(item);
        }
        // надеть или заменить экипировку
        public void Equip(EquipItem newItem)
        {
            if (_equipment.TryGetValue(newItem.Slot, out var oldItem))
            {
                Console.WriteLine($"Found {newItem.Name}. " +
                $"Slot {newItem.Slot} is busy: {oldItem.Name}");
                Console.WriteLine("Replace equipment? Type Yes or No");
                if (!AskYesNo())
                {
                    Console.WriteLine($"{newItem.Name} was left behind");
                    return;
                }
                Console.WriteLine($"Equipment replaced: {oldItem.Name} -> {newItem.Name}");
            }
            else
            {
                Console.WriteLine($"{Name} equipped {newItem.Name}");
            }
            _equipment[newItem.Slot] = newItem;
        }
        private static bool AskYesNo()
        {
            while (true)
            {
                var answer = Console.ReadLine()?.Trim().ToLower();
                if (answer == "yes")
                {
                    return true;
                }
                if (answer == "no")
                {
                    return false;
                }
                Console.WriteLine("Wrong input! Type Yes or No");
            }
        }
        // использует предмет; true - если предмет использован и его надо убрать
        private bool TryUseItem(Item item)
        {
            if (item is HealthPotion healthPotion)
            {
                Health = Math.Min(Health + healthPotion.HealthRestore, MaxHealth);
                Console.WriteLine($"{Name} used {item.Name}. Health {Health}/{MaxHealth}");
                return true;
            }
            if (item is Grindstone grindstone
            && _equipment.TryGetValue(EquipSlot.Weapon, out var weapon))
            {
                weapon.Repair(grindstone.DurabilityRestore);
                Console.WriteLine($"{Name} used {item.Name} on {weapon.Name}. " +
                $"Durability {weapon.Durability}/{weapon.MaxDurability}");
                return true;
            }
            return false;
        }
        private Weapon? GetWeapon(EquipSlot slot)
        {
            if (_equipment.TryGetValue(slot, out var item)
            && item is Weapon weapon && !weapon.IsBroken)
            {
                return weapon;
            }
            return null;
        }
        private uint GetDefence(EquipSlot slot)
        {
            if (_equipment.TryGetValue(slot, out var item)
            && item is Armour armour && !armour.IsBroken)
            {
                return armour.Defence;
            }
            return 0;
        }
        protected override uint CalculateAppliedDamage(uint damage)
        {
            var defence = GetDefence(EquipSlot.Armour) + GetDefence(EquipSlot.Helmet);
            if (defence > MaxDefence)
            {
                defence = MaxDefence;
            }
            return damage - (uint)(damage * (defence / 100f));
        }
        // вызывается после каждого полученного урона: броня теряет прочность
        protected override void DamageReceiveHandler()
        {
            ReduceArmourDurability(EquipSlot.Armour);
            ReduceArmourDurability(EquipSlot.Helmet);
        }
        private void ReduceArmourDurability(EquipSlot slot)
        {
            if (_equipment.TryGetValue(slot, out var item) && !item.IsBroken)
            {
                item.ReduceDurability(1);
                Console.WriteLine($"{item.Name} durability " +
                $"{item.Durability}/{item.MaxDurability}");
            }
        }
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine(Name);
            builder.AppendLine($"Health {Health}/{MaxHealth}");
            builder.AppendLine("Equipment:");
            foreach (var pair in _equipment)
            {
                var item = pair.Value;
                builder.AppendLine($"[{pair.Key}] {item.Name} : " +
                $"{item.Durability}/{item.MaxDurability}");
            }
            builder.AppendLine("Loot:");
            var items = Inventory.Items;
            for (int i = 0; i < items.Count; i++)
            {
                builder.AppendLine($"[{items[i].Name}] : {items[i].Amount}");
            }
            return builder.ToString();
        }
    }
}