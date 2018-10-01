using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using JBoyerLibaray.Exceptions;
using JBoyerLibaray.UnitTests;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using JBoyerLibaray.Extensions;
using System.Security.Cryptography;
using JBoyerLibaray.UnitTests.Database;
using Dapper;
using Dapper.Contrib.Extensions;
using JBoyerLibaray.Database;
using Moq;
using System.Data;
using System.Diagnostics;

namespace JBoyerLibaray
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class Zzzzzz_TestCode
    {
        [TestMethod]
        public void Zzzzzz_TestMethodOne()
        {
            var exp = Test();

            var stackTrace = new StackTrace(exp);
            var frames = stackTrace.GetFrames();
            var infos = frames.Select(f => new Tuple<string, int>(f.GetMethod().Name, f.GetFileLineNumber()));

            var toString = exp.ToString();

            var output = StackTraceParser.Parse(exp.ToString(), (f, t, m, pl, ps, fn, ln) => new
            {
                Frame = f,
                Type = t,
                Method = m,
                ParameterList = pl,
                Parameters = ps,
                File = fn,
                Line = ln,
            });

        }


        public Exception Test(bool throwExp = false, Exception exception = null)
        {
            if (throwExp)
            {
                throw new Exception(null, exception);
            }

            try
            {
                Test(true, Test2());
                return null;
            }
            catch (Exception e)
            {
                return e;
            }
        }

        public Exception Test2(bool throwExp = false, Exception exception = null)
        {
            if (throwExp)
            {
                throw new Exception(null, exception);
            }

            try
            {
                Test2(true);
                return null;
            }
            catch (Exception e)
            {
                return e;
            }
        }
    }
}
