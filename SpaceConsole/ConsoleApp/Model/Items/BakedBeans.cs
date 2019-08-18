using SpaceConsole.ConsoleApp.Quantities;

namespace SpaceConsole.ConsoleApp.Model.Items
{
    public sealed class BakedBeans : IItem
    {
        public string Name => "Baked Beans";

        public double Mass => 1.Tons();

        public double Volume => 1.CubicMeter();

        public int MaxStackSize => int.MaxValue;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            return obj.GetType() == GetType();
        }

        public override int GetHashCode()
        {
            return typeof(BakedBeans).GetHashCode();
        }
    }
}