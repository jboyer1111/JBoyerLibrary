using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBoyerLibaray.Database
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    public class ReaderData
    {
        #region Private Variables

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private List<DataResult> _results = new List<DataResult>();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private object DebuggerDisplay
        {
            get
            {
                if (_results.Count < 1)
                {
                    return null;
                }

                if (_results.Count < 2)
                {
                    return "Test";
                }

                return "Test2";
            }
        }


        #endregion




        #region Public Methods

        public void Add(DataResult result)
        {
            _results.Add(result);
        }

        #endregion
        
        

    }
}
