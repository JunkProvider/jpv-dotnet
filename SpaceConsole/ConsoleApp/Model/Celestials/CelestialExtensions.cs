using JPV.RocketScience;

namespace SpaceConsole.ConsoleApp.Model.Celestials
{
    public static class CelestialExtensions
    {
        public static double GetGravitationalModifier(this ICelestial celestial)
        {
            return Physics.GetStandardGravitationalParameter(celestial.Mass, Model.Constants.GravitationalConstant);
        }
    }
}