using System;
using System.Collections.Generic;
using System.Linq;

namespace TestApp
{
    /// <summary>
    /// FirstOrDefault with Default Value
    /// </summary>
    /// <remarks>
    /// In .NET 6 FirstOrDefault(), LastOrDefault() SingleOrDefault() now let's specify the default value.
    /// /// </remarks>
    public static class IEnumerableExtensions
    {
        // var numbers = Enumerable.Range(0, 10);
        // var firstNumber = numbers.FirstOrDefault(x => x > 11, -1) == -1;
        public static T FirstOrDefault<T>(this IEnumerable<T> source, Func<T, bool> predicate, T defaultValue)
        {
            T value = source.FirstOrDefault(predicate);

            return GetDefaultValue(defaultValue, value);
        }

        // var numbers = Enumerable.Range(0, 10);
        // var lastNumber = numbers.LastOrDefault(x => x > 11, -1) == -1;
        public static T LastOrDefault<T>(this IEnumerable<T> source, Func<T, bool> predicate, T defaultValue)
        {
            throw new NotImplementedException();
        }

        // var numbers = Enumerable.Range(0, 10);
        // var number = numbers.SingleOrDefault(x => x > 11, -1) == -1;
        public static T SingleOrDefault<T>(this IEnumerable<T> source, Func<T, bool> predicate, T defaultValue)
        {
            throw new NotImplementedException();
        }

        private static T GetDefaultValue<T>(T defaultValue, T value) => EqualityComparer<T>.Default.Equals(value, default) ? defaultValue : value;


    }
}
