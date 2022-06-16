using System;
using System.Collections.Generic;

namespace JBoyerLibaray
{

    public class DeferComparer<T> : IComparer<T>
    {

        private readonly Func<T, T, int> _compareFunc;

        public DeferComparer(Func<T, T, int> compareFunc)
        {
            _compareFunc = compareFunc ?? throw new ArgumentNullException(nameof(compareFunc));
        }

        public int Compare(T x, T y)
        {
            return _compareFunc(x, y);
        }

    }

}
