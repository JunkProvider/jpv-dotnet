using System;
using System.Collections;
using System.Collections.Generic;

namespace SpaceConsole.ConsoleApp
{
    public static class OptionalEnumerable
    {
        public static OptionalEnumerable<T> Create<T>(T value)
        {
            return new OptionalEnumerable<T>(() => Optional.Create(value));
        }

        public static OptionalEnumerable<T> Create<T>(Func<T> valueFunc)
        {
            return new OptionalEnumerable<T>(() => Optional.Create(valueFunc()));
        }
    }

    public sealed class OptionalEnumerable<T> : IEnumerable<T>
    {
        private Func<Optional<T>> ValueFunc { get; }

        public OptionalEnumerable(Func<Optional<T>> valueFunc)
        {
            ValueFunc = valueFunc;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator()
        {
            var optionalValue = ValueFunc();

            if (optionalValue.TryGet(out var value))
                yield return value;
        }
    }
}