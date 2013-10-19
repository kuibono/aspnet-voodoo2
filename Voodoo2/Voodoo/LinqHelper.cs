using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Voodoo
{
    public static class LinqHelper
    {
        public static List<string> Merge(this List<string> str, List<string> newStr)
        {
            foreach (string s in newStr)
            {
                str.Add(s);
            }
            return str;
        }


    }
}
