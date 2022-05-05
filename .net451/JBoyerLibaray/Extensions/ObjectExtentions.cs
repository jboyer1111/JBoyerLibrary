using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBoyerLibaray.Extensions
{
    public static class ObjectExtentions
    {

        public static bool Is<T>(this object item)
        {
            return item is T;
        }

        public static bool IsNot<T>(this object item)
        {
            return !item.Is<T>();
        }
    }
}
