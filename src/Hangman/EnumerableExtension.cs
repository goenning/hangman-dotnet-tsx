using System;
using System.Collections.Generic;
using System.Linq;

namespace Hangman
{
    public static class EnumerableExtension
    {
        public static T Random<T>(this IEnumerable<T> enumerable)
        {
            return Random<T>(enumerable, new Random());
        }

        private static T Random<T>(IEnumerable<T> enumerable, Random rand)
        {
            int index = rand.Next(0, enumerable.Count());
            return enumerable.ElementAt(index);
        }

        public static IEnumerable<int> IndexesOf(this string str, char search)
        {
            int minIndex = str.IndexOf(search);
            while (minIndex != -1)
            {
                yield return minIndex;
                minIndex = str.IndexOf(search, minIndex + 1);
            }
        }
    }
}
