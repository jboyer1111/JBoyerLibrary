using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBoyerLibaray.UnitTests
{
    public interface IUnitTestDisposable : IDisposable
    {
        void UnitTestDispose();
    }
}
