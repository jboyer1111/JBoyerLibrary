using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBoyerLibaray.Extensions
{
    public static class HelperExtentions
    {
        public static IEnumerable<T> ToSingleItemList<T>(this T item)
        {
            if (item == null)
            {
                return null;
            }

            return new List<T>() { item };
        }
    }
}
