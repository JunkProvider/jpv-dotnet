using System;

namespace JPV.RocketScience
{
    public static class Geometry
    {
        public static double GetSphereVolume(double radius)
        {
            return (4d / 3) * Math.PI * Math.Pow(radius, 3);
        }

        public static double GetSphereSurface(double radius)
        {
            return Math.PI * Math.Pow(radius, 2);
        }

        public static double GetCircleCircumference(double radius)
        {
            return 2 * Math.PI * radius;
        }
    }
}
