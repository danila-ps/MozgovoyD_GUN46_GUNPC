using GamePrototype.Items.EconomicItems;
using GamePrototype.Utils;
namespace GamePrototype.Items.EquipItems
{
    public abstract class EquipItem : Item
    {
        private uint _durability;
        private readonly uint _maxDurability;
        public uint Durability { get => _durability; protected set => _durability = value; }
        public uint MaxDurability => _maxDurability;
        public bool IsBroken => _durability == 0;
        public override bool Stackable => false;
        public abstract EquipSlot Slot { get; }
        protected EquipItem(uint maxDurability, string name) : base(name)
        {
            _maxDurability = maxDurability;
            // новый предмет сразу целый
            _durability = maxDurability;
        }
        // прочность уменьшается, но не уходит ниже 0
        public void ReduceDurability(uint delta) =>
        _durability = delta >= _durability ? 0 : _durability - delta;
        // починка, но не больше максимума (как лечение у юнита)
        public void Repair(uint delta) =>
        _durability = Math.Min(_durability + delta, _maxDurability);
    }
}
