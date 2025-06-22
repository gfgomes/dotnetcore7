using System;
using System.Collections.Generic;

namespace SimpleApp.Tests
{
    public class ComparerCreator
    {
        public static Comparer<U?> GetInstance<U>(Func<U?, U?, bool> func)
        {
            return new Comparer<U?>(func);
        }
    }

    public class Comparer<T> : IEqualityComparer<T>
    {
        private Func<T?, T?, bool> comparisonFunction;

        public Comparer(Func<T?, T?, bool> func)
        {
            comparisonFunction = func;
        }

        public bool Equals(T? x, T? y)
        {
            return comparisonFunction(x, y);
        }

        public int GetHashCode(T obj)
        {
            return obj?.GetHashCode() ?? 0;
        }
    }
}