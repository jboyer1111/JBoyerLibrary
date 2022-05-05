﻿using System;
using System.Collections.Generic;

namespace JBoyer.Testing.Database
{

    /// <summary>
    /// Class for storing Dapper table data.
    /// </summary>
    internal abstract class TableInfo
    {

        protected Func<IEnumerable<object>> _results;

        public IEnumerable<object> GetResults()
        {
            return _results();
        }

    }

}
