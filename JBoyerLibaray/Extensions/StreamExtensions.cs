using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JBoyerLibaray.Extensions
{
    public static class StreamExtensions
    {
        public static long Seek(this Stream stream, int position)
        {
            return stream.Seek(position, SeekOrigin.Begin);
        }
    }
}
