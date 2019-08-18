using SpaceConsole.ConsoleApp.Quantities;

namespace SpaceConsole.ConsoleApp.Model.Items
{
    public sealed class LiquidElementItem : IElementItem
    {
        public string Name => Element.Name;

        public double Mass => Element.LiquidDensity.InKiloGramPerCubicMeter() * Volume.InCubicMeter();

        public double Volume => 1.CubicMeter();

        public int MaxStackSize => int.MaxValue;

        public IElement Element { get; }

        public LiquidElementItem(IElement element)
        {
            Element = element;
        }

        private bool Equals(LiquidElementItem other)
        {
            return Equals(Element, other.Element);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            return obj.GetType() == GetType() && Equals((LiquidElementItem) obj);
        }

        public override int GetHashCode()
        {
            return Element != null ? Element.GetHashCode() : 0;
        }
    }
}