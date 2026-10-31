using GamePrototype.Utils;
namespace GamePrototype.Items.EquipItems
{
    // Шлем: всё как у Armour, но свой слот
    public sealed class Helmet : Armour
    {
        public Helmet(uint defence, uint durability, string name)
        : base(defence, durability, name)
        {
        }
        public override EquipSlot Slot => EquipSlot.Helmet;
    }
}