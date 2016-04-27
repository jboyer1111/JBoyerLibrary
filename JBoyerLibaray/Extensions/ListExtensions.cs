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
            List<T> list = new List<T>(items);
            int pos = list.Count;
            while (pos > 0)
            {
                int pickPos = _rand.Next(0, pos);
                pos--;

                T temp = list[pos];
                list[pos] = list[pickPos];
                list[pickPos] = temp;
            }

            return list;
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
    }
}
