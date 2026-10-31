using GamePrototype.Utils;
namespace GamePrototype.Items.EquipItems
{
    // Оружие дальнего боя: всё как у Weapon, но свой слот
    public sealed class RangeWeapon : Weapon
    {
        public RangeWeapon(uint damage, uint durability, string name)
        : base(damage, durability, name)
        {
        }
        public override EquipSlot Slot => EquipSlot.RangeWeapon;
    }
}