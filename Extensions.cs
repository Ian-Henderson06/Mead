using System;
using System.Collections.Generic;
using System.Text;

namespace Mead
{
    static class Extensions
    {
        public static string SubstringWithEnd(this string s, int start, int end)
        {
            return s.Substring(start, end - start);
        }
    }
}
