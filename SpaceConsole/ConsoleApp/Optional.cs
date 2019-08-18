using System.Collections.Generic;

namespace SpaceConsole.ConsoleApp
{
    public static class Optional
    {
        public static Optional<T> Empty<T>()
        {
            return new Optional<T>();
        }

        public static Optional<T> Create<T>(T value)
        {
            return new Optional<T>(value);
        }

        public static Optional<T> FromNullable<T>(T value)
            where T : class
        {
            return value == null ? Empty<T>() : Create(value);
        }
    }

    public struct Optional<T>
    {
        public bool HasValue { get; }

        private T Value { get; }

        public Optional(T value) : this()
        {
            Value = value;
            HasValue = true;
        }

        public bool TryGet(out T value)
        {
            value = Value;
            return HasValue;
        }

        public bool Equals(Optional<T> other)
        {
            return HasValue == other.HasValue && EqualityComparer<T>.Default.Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            return obj is Optional<T> optional && Equals(optional);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (HasValue.GetHashCode() * 397) ^ EqualityComparer<T>.Default.GetHashCode(Value);
            }
        }

        public override string ToString()
        {
            return HasValue ? Value.ToString() : "Empty";
        }
    }
}