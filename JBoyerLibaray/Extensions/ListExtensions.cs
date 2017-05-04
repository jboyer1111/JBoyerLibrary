using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBoyerLibaray.Extensions
{
    public static class ListExtensions
    {
        private static Random _rand = new Random();
        
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> items)
        {
            var list = new List<T>(items);

            list.Shuffle();

            return list;
        }

        public static void Shuffle<T>(this List<T> items)
        {
            int pos = items.Count;
            while (pos > 0)
            {
                int pickPos = _rand.Next(0, pos);
                pos--;

                T temp = items[pos];
                items[pos] = items[pickPos];
                items[pickPos] = temp;
            }
        }

        public static object[] ToListBoxItems<T>(this IEnumerable<T> items)
        {
            var itemArray = items.ToArray();
            object[] objects = new object[itemArray.Length];
            
            itemArray.CopyTo(objects, 0);

            return objects;
        }

        public static T Random<T>(this IEnumerable<T> items)
        {
            List<T> list = new List<T>(items);

            return list[_rand.Next(0, list.Count())];
        }

        public static IEnumerable<T> Median<T>(this IEnumerable<T> items)
        {
            return Median(items, Comparer<T>.Default);
        }

        public static IEnumerable<T> Median<T>(this IEnumerable<T> items, IComparer<T> comparer)
        {
            List<T> list = new List<T>(items);
            list.Sort(comparer);

            int midIndex = list.Count / 2;
            if (list.Count % 2 == 0)
            {
                return new T[] { list[midIndex - 1], list[midIndex] };
            }
            else
            {
                return new T[] { list[midIndex] };
            }
        }

        public static IEnumerable<T> Mode<T>(this IEnumerable<T> items)
        {

            return Mode(items, EqualityComparer<T>.Default);
        }

        public static IEnumerable<T> Mode<T>(this IEnumerable<T> items, IEqualityComparer<T> equalityComparer)
        {
            List<T> list = new List<T>(items);
            var groups = list.GroupBy(t => t, equalityComparer)
                .Select(s =>
                {
                    return new
                    {
                        Group = s.Key,
                        Count = s.Count()
                    };
                });

            int maxCount = groups.Max(s => s.Count);
            if (!groups.Any(g => g.Count != maxCount))
            {
                return new List<T>();
            }

            return groups.Where(g => g.Count == maxCount).Select(g => g.Group).ToList();
        }

        public static int GetHashCodeAggregate<T>(this IEnumerable<T> source)
        {
            return source.GetHashCodeAggregate(17);
        }

        public static int GetHashCodeAggregate<T>(this IEnumerable<T> source, int hash)
        {
            unchecked
            {
                foreach (var item in source)
                {
                    hash = hash * 31 + item.GetHashCode();
                }
            }

            return hash;
        }
    }
}
