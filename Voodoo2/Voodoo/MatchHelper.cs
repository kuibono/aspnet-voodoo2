using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Voodoo
{
    public static class MatchHelper
    {
        public static List<string> GetMatchList(this Match m, string key)
        {
            List<string> result = new List<string>();

            while (m.Success)
            {
                result.Add(m.Groups[key].Value);
                m = m.NextMatch();
            }
            return result;
        }

    }
}
