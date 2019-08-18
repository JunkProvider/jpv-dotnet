using System;

namespace SpaceConsole.ConsoleApp.Model
{
    public struct Vector
    {
        public static double DistanceBetween(Vector left, Vector right)
        {
            return (left - right).Magnitude();
        }

        public static bool operator ==(Vector left, Vector right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Vector left, Vector right)
        {
            return !Equals(left, right);
        }

        public static Vector operator +(Vector left, Vector right)
        {
            return new Vector(left.X + right.X, left.Y + right.Y);
        }

        public static Vector operator -(Vector left, Vector right)
        {
            return new Vector(left.X - right.X, left.Y - right.Y);
        }

        public double X { get; }

        public double Y { get; }

        public Vector(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double Magnitude()
        {
            return Math.Sqrt(SquareMagnitude());
        }

        public double SquareMagnitude()
        {
            return X * X + Y * Y;
        }

        public bool Equals(Vector other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Vector vector && Equals(vector);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X.GetHashCode() * 397) ^ Y.GetHashCode();
            }
        }

        public override string ToString()
        {
            return $"{X.ToStringInvariant()}|{Y.ToStringInvariant()}";
        }
    }
}
