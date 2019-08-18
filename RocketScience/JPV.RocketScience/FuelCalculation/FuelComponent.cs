namespace JPV.RocketScience.FuelCalculation
{
    public struct FuelComponent
    {
        public double Portion { get; }

        public double AvailableMass { get; }

        public FuelComponent(double portion, double availableMass)
        {
            Portion = portion;
            AvailableMass = availableMass;
        }
    }
}