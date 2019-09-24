using System;
using System.Globalization;
using System.Threading;

namespace JBoyerLibaray
{

    public class CultureInfoHelper : IDisposable
    {

        #region Private Variables

        private CultureInfo _lastCulture;

        #endregion

        #region Constructor

        private CultureInfoHelper()
        {
            _lastCulture = Thread.CurrentThread.CurrentCulture;
        }

        #endregion

        #region Static Methods

        public static void ExecuteInCulture(string name, Action action)
        {
            ExecuteInCulture(new CultureInfo(name), action);
        }

        public static void ExecuteInCulture(CultureInfo cultureInfo, Action action)
        {
            var prev = Thread.CurrentThread.CurrentCulture;
            try
            {
                Thread.CurrentThread.CurrentCulture = cultureInfo;

                action();
            }
            finally
            {
                Thread.CurrentThread.CurrentCulture = prev;
            }
        }

        public static CultureInfoHelper SetCulture(string name)
        {
            return SetCulture(new CultureInfo(name));
        }

        public static CultureInfoHelper SetCulture(CultureInfo cultureInfo)
        {
            var result = new CultureInfoHelper();

            Thread.CurrentThread.CurrentCulture = cultureInfo;

            return result;
        }

        #endregion

        public void Dispose()
        {
            Thread.CurrentThread.CurrentCulture = _lastCulture;
        }

    }

}
